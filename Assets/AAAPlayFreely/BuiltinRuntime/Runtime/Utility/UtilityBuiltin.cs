using GameFramework;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;
using ZXing.QrCode;
using ZXing;
using System.Collections.Generic;
namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 内置运行工具
    /// </summary>
    public static class UtilityBuiltin
    {
        /// <summary>
        /// 资源加载路径
        /// </summary>
        public static class AssetsPath
        {

            public static string GetCombinePath(params string[] args)
            {
                return Utility.Path.GetRegularPath(Path.Combine(args));
            }

            /// <summary>
            /// 加载序列化物体路径
            /// </summary>
            /// <param name="v"></param>
            /// <returns></returns>
            public static string GetHotfixScriptableAsset(string v)
            {
                return Utility.Text.Format("Assets/AAAPlayFreely/HotfixRuntimeAsset/ScriptableObject/{0}.asset" , v);
            }
        }

        /// <summary>
        /// Bezier扩展
        /// </summary>
        public static class BezierExtend
        {
            /// <summary>
            /// 线性
            /// </summary>
            /// <param name="p0">开始点</param>
            /// <param name="p1">终点</param>
            /// <param name="t">0-1</param>
            /// <returns></returns>
            public static Vector3 BezierPoint(Vector3 p0 , Vector3 p1 , float t)
            {
                return ( ( 1 - t ) * p0 ) + t * p1;
            }

            /// <summary>
            /// 二阶贝塞尔曲线
            /// </summary>
            /// <param name="p1"></param>
            /// <param name="p1"></param>
            /// <param name="p2"></param>
            /// <param name="t"></param>
            /// <returns></returns>
            public static Vector3 BezierPoint(Vector3 p0 , Vector3 p1 , Vector3 p2 , float t)
            {
                Vector3 p0p1 = ( 1 - t ) * p0 + t * p1;
                Vector3 p1p2 = ( 1 - t ) * p1 + t * p2;
                Vector3 result = ( 1 - t ) * p0p1 + t * p1p2;
                return result;
            }

            /// <summary>
            /// 三阶曲线
            /// </summary>
            /// <param name="p0"></param>
            /// <param name="p1"></param>
            /// <param name="p2"></param>
            /// <param name="p3"></param>
            /// <param name="t"></param>
            /// <returns></returns>
            public static Vector3 BezierPoint(Vector3 p0 , Vector3 p1 , Vector3 p2 , Vector3 p3 , float t)
            {
                Vector3 result;
                Vector3 p0p1 = ( 1 - t ) * p0 + t * p1;
                Vector3 p1p2 = ( 1 - t ) * p1 + t * p2;
                Vector3 p2p3 = ( 1 - t ) * p2 + t * p3;
                Vector3 p0p1p2 = ( 1 - t ) * p0p1 + t * p1p2;
                Vector3 p1p2p3 = ( 1 - t ) * p1p2 + t * p2p3;
                result = ( 1 - t ) * p0p1p2 + t * p1p2p3;
                return result;
            }

            /// <summary>
            /// 多阶曲线  （可以递归 有多组线性组合）
            /// </summary>
            /// <param name="t"></param>
            /// <param name="p"></param>
            /// <returns></returns>
            public static Vector3 BezierPoint(float t , List<Vector3> p)
            {
                if(p.Count < 2)
                    return p[0];
                List<Vector3> newP = new List<Vector3>( );
                for(int i = 0; i < p.Count - 1; i++)
                {
                    Vector3 p0p1 = ( 1 - t ) * p[i] + t * p[i + 1];
                    newP.Add(p0p1);
                }
                return BezierPoint(t , newP);
            }

            /// <summary>
            /// 获取存储贝塞尔曲线点的数组(二阶)
            /// </summary>
            /// <param name="startPoint">起始点</param>
            /// <param name="controlPoint">控制点</param>
            /// <param name="endPoint">目标点</param>
            /// <param name="segmentNum">采样点的数量</param>
            /// <returns>存储贝塞尔曲线点的数组</returns>
            public static Vector3[] GetBeizerPointList(Vector3 startPoint , Vector3 controlPoint , Vector3 endPoint , int segmentNum)
            {
                Vector3[] path = new Vector3[segmentNum];
                for(int i = 1; i <= segmentNum; i++)
                {
                    float t = i / (float)segmentNum;
                    Vector3 pixel = BezierPoint(startPoint , controlPoint , endPoint , t);
                    path[i - 1] = pixel;
                }
                return path;
            }

            /// <summary>
            /// 获取存储贝塞尔曲线点的数组(多阶)
            /// </summary>
            /// <param name="segmentNum">采样点的数量</param>
            /// <param name="p">控制点集合</param>
            /// <returns></returns>
            public static Vector3[] GetBeizerPointList(int segmentNum , List<Vector3> p)
            {
                Vector3[] path = new Vector3[segmentNum];
                for(int i = 1; i <= segmentNum; i++)
                {
                    float t = i / (float)segmentNum;
                    Vector3 pixel = BezierPoint(t , p);
                    path[i - 1] = pixel;
                }
                return path;
            }
        }

        /// <summary>
        /// 二维码生成工具
        /// </summary>
        public class QRCodeUtility
        {
            /// <summary>
            /// 创建二维码
            /// </summary>
            /// <param name="content">内容</param>
            /// <param name="width">宽度</param>
            /// <param name="height">高度</param>
            /// <returns></returns>
            public static Sprite GeneratorQRCode(string content , int width = 256 , int height = 256)
            {
                Texture2D texture2D = GetQRCode(content , width , height);

                return Sprite.Create(texture2D , new Rect(0 , 0 , width , height) , new Vector2(0.5f , 0.5f));
            }

            /// <summary>
            /// 生成二维码的参数
            /// </summary>
            /// <param name="formatStr">字符串</param>
            /// <param name="width">宽</param>
            /// <param name="height">高</param>
            /// <returns></returns>
            private static Texture2D GetQRCode(string str , int width , int height)
            {
                // 创建一个新的Texture2D对象，根据传入的宽和高进行初始化
                Texture2D texture = new Texture2D(width , height);
                // 调用GeneQRCode方法生成二维码的颜色数组
                Color32[] colors = GenenQRCode(str , width , height);
                // 将生成的颜色数组设置到纹理中
                texture.SetPixels32(colors);
                // 应用纹理的修改（更新）
                texture.Apply( );
                // 返回生成的二维码纹理
                return texture;
            }
            /// <summary>
            /// 生成二维码的参数
            /// </summary>
            /// <param name="formatStr">字符串</param>
            /// <param name="width">宽</param>
            /// <param name="height">高</param>
            /// <returns></returns>
            private static Color32[] GenenQRCode(string formatStr , int width , int height)
            {
                // 创建QrCodeEncodingOptions对象，用于设置二维码编码参数
                QrCodeEncodingOptions options = new QrCodeEncodingOptions( ); // 绘制二维码之前进行设置

                options.CharacterSet = "UTF-8"; // 设置字符编码，确保字符串信息保持正确

                options.Width = width; // 设置二维码宽度
                options.Height = height; // 设置二维码高度
                options.Margin = 1; // 设置二维码留白（值越大，留白越大，二维码越小）

                return new BarcodeWriter { Format = BarcodeFormat.QR_CODE , Options = options }.Write(formatStr); // 实例化字符串绘制二维码工具
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        public class Encryption
        {
            /// <summary>
            /// DES加密
            /// </summary>
            /// <param name="stringToEncrypt">加密内容</param>
            /// <param name="sKey">密钥</param>
            /// <returns>加密后的数据</returns>
            public static string DESEncrypt(string stringToEncrypt , string sKey = BuiltinRuntimeGlobalData.DES_KEY)
            {
                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider( );
                    byte[] inputByteArray = Utility.Converter.GetBytes(stringToEncrypt);
                    des.Key = Encoding.UTF8.GetBytes(sKey);
                    des.IV = Encoding.UTF8.GetBytes(sKey);
                    MemoryStream ms = new MemoryStream( );
                    CryptoStream cs = new CryptoStream(ms , des.CreateEncryptor( ) , CryptoStreamMode.Write);
                    cs.Write(inputByteArray , 0 , inputByteArray.Length);
                    cs.FlushFinalBlock( );
                    StringBuilder ret = new StringBuilder( );
                    foreach(byte b in ms.ToArray( ))
                    {
                        ret.AppendFormat("{0:X2}" , b);
                    }
                    return ret.ToString( );
                }
                catch(Exception e)
                {
                    Log.Error($"加密失败:{e.Message}");
                    return string.Empty;
                }
            }

            /// <summary>
            /// DES解密
            /// </summary>
            /// <param name="stringToDecrypt">加密内容</param>
            /// <param name="sKey">密钥</param>
            /// <returns>解密后的内容</returns>
            public static string DESDecrypt(string stringToDecrypt , string sKey = BuiltinRuntimeGlobalData.DES_KEY)
            {
                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider( );
                    byte[] inputByteArray = new byte[stringToDecrypt.Length / 2];
                    for(int x = 0; x < stringToDecrypt.Length / 2; x++)
                    {
                        string strx = stringToDecrypt.Substring(x * 2 , 2);
                        inputByteArray[x] = Convert.ToByte(strx , 16);
                    }
                    des.Key = Utility.Converter.GetBytes(sKey);
                    des.IV = Utility.Converter.GetBytes(sKey);
                    MemoryStream ms = new MemoryStream( );

                    CryptoStream cs = new CryptoStream(ms , des.CreateDecryptor( ) , CryptoStreamMode.Write);
                    cs.Write(inputByteArray , 0 , inputByteArray.Length);
                    cs.FlushFinalBlock( );
                    return Utility.Converter.GetString(ms.ToArray( ));
                }
                catch(Exception e)
                {
                    Log.Error("解密失败:{0}" , e.Message);
                    return string.Empty;
                }
            }

            ///<summary>
            ///使用md5加密字符
            ///</summary>
            public static string MD5EncryptString(string str)
            {
                MD5 md5 = MD5.Create( );
                byte[] byteOld = Encoding.UTF8.GetBytes(str);
                byte[] byteName = md5.ComputeHash(byteOld);
                StringBuilder sb = new StringBuilder( );
                foreach(byte b in byteName)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString( );
            }
        }
    }
    /// <summary>
    /// Vector扩展
    /// </summary>
    public static class VectorExtend
    {
        /// <summary>
        /// 字节组转string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(this byte[] bytes)
        {
            return Utility.Converter.GetString(bytes);
        }
        /// <summary>
        /// 2D叉乘
        /// </summary>
        /// <param name="v1">点1</param>
        /// <param name="v2">点2</param>
        /// <returns></returns>
        public static float CrossProduct2D(this Vector2 v1 , Vector2 v2)
        {
            //叉乘运算公式 x1*y2 - x2*y1
            return v1.x * v2.y - v2.x * v1.y;
        }

        /// <summary>
        /// 点是否在直线上
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="lineStart">线的开始点</param>
        /// <param name="lineEnd">线的结束点</param>
        /// <returns></returns>
        public static bool IsPointOnLine(this Vector2 point , Vector2 lineStart , Vector2 lineEnd)
        {
            float value = CrossProduct2D(point - lineStart , lineEnd - lineStart);
            /* 使用 Mathf.Approximately(value,0) 方式，在斜线上好像无法趋近为0*/
            return Math.Abs(value) < 0.003;
        }

        /// <summary>
        /// 点是否在线段上
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        public static bool IsPointOnSegment(this Vector2 point , Vector2 lineStart , Vector2 lineEnd)
        {
            //1.先通过向量的叉乘确定点是否在直线上
            //2.在拍段点是否在指定线段的矩形范围内
            if(IsPointOnLine(point , lineStart , lineEnd))
            {
                //点的x值大于最小，小于最大x值 以及y值大于最小，小于最大
                if(point.x >= Mathf.Min(lineStart.x , lineEnd.x) && point.x <= Mathf.Max(lineStart.x , lineEnd.x) &&
                    point.y >= Mathf.Min(lineStart.y , lineEnd.y) && point.y <= Mathf.Max(lineStart.y , lineEnd.y))
                    return true;
            }
            return false;
        }

    }
}
