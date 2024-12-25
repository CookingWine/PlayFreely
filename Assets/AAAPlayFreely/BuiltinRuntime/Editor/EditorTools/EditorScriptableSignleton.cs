using System;
using System.IO;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

namespace PlayFreely.EditorTools
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FilePathAttribute:Attribute
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        internal string Filepath { get; private set; }

        /// <summary>
        /// 单例存放路径
        /// </summary>
        /// <param name="path">相对 Project 路径</param>
        public FilePathAttribute(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Invalid relative path (it is empty)");
            }
            if(path[0] == '/')
            {
                path = path.Substring(1);
            }
            Filepath = path;
        }
    }

    /// <summary>
    /// 编辑器可视化单例
    /// </summary>
    public class EditorScriptableSignleton<T>:ScriptableObject where T : ScriptableObject
    {
        /// <summary>
        /// 实例
        /// </summary>
        private static T m_Instance;

        /// <summary>
        /// 实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if(!m_Instance)
                {

                }
                return m_Instance;
            }
        }

        /// <summary>
        /// 加载或者创建实例
        /// </summary>
        /// <returns></returns>
        public static T LoadOrCreate( )
        {
            string filePath = GetFilePath( );
            if(!string.IsNullOrEmpty(filePath))
            {
                var arr = InternalEditorUtility.LoadSerializedFileAndForget(filePath);
                m_Instance = arr.Length > 0 ? arr[0] as T : m_Instance ?? CreateInstance<T>( );
            }
            else
            {
                Debug.LogError($"{nameof(EditorScriptableSignleton<T>)}: 请设置持久化存档路径！ ");
            }
            return m_Instance;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="allowTextSerialization">允许文本序列化</param>
        public static void Save(bool allowTextSerialization = true)
        {
            if(!m_Instance)
            {
                return;
            }

            string filePath = GetFilePath( );
            if(!string.IsNullOrEmpty(filePath))
            {
                string directoryName = Path.GetDirectoryName(filePath);
                if(!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                UnityEngine.Object[] obj = new T[1] { m_Instance };
                InternalEditorUtility.SaveToSerializedFileAndForget(obj , filePath , allowTextSerialization);
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        protected static string GetFilePath( )
        {
            return typeof(T).GetCustomAttributes(inherit: true).Cast<FilePathAttribute>( ).FirstOrDefault(v => v != null)?.Filepath;
        }
    }
}
