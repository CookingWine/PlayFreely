using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityGameFramework.Runtime;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 自定义组件
    /// </summary>
    public class BuiltinRuntimeComponent:GameFrameworkComponent
    {
        /// <summary>
        /// 下一次ping的时间
        /// </summary>
        private static float m_LastPingTime = 0;
        /// <summary>
        /// 开始ping的时间
        /// </summary>
        private static float m_StartPingTime = 0;
        /// <summary>
        /// 延迟时间
        /// </summary>
        private static float m_PingDuration = 0;

#if UNITY_EDITOR
        public float PingDuration;
#endif
        private void Update( )
        {
            DetectNet( );
#if  UNITY_EDITOR
            PingDuration = m_PingDuration;
#endif
        }

        /// <summary>
        /// 检测网络
        /// </summary>
        private void DetectNet( )
        {
            //10秒超时继续ping
            if(m_StartPingTime > 0 && Time.time - m_StartPingTime > 10)
            {
                m_StartPingTime = 0;
            }
            if(m_StartPingTime > 0)
            {
                return;
            }
            bool canExec = false;
            if(m_LastPingTime == 0)
            {
                canExec = true;
            }
            else if(Time.time - m_LastPingTime > 3)
            {
                canExec = true;
            }
            if(!canExec)
            {
                return;
            }
            m_StartPingTime = Time.time + 0.0001f;
            StartCoroutine(PingNet("https://www.baidu.com"));
        }


        /// <summary>
        /// ping 网络
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pingTag"></param>
        /// <returns></returns>
        private IEnumerator PingNet(string url)
        {
            UnityWebRequest request = new UnityWebRequest(url);
            yield return request.SendWebRequest( );
            if(request.result != UnityWebRequest.Result.ProtocolError)
            {
                m_PingDuration = Time.time - m_StartPingTime;
                //Ping成功了
                m_StartPingTime = 0;
                m_LastPingTime = Time.time + 0.0001f;
            }
        }
    }
}
