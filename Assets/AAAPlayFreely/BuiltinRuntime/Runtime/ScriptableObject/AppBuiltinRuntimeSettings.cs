using UnityEngine;
using GameFramework.Resource;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// app运行的基础设置
    /// </summary>
    [CreateAssetMenu(fileName = "AppBuiltinRuntimeSettings" , menuName = "ScriptableObject/AppSettings【App内置配置参数】")]
    public class AppBuiltinRuntimeSettings:ScriptableObject
    {

#if UNITY_EDITOR
        /// <summary>
        /// 实例
        /// </summary>
        private static AppBuiltinRuntimeSettings m_Instance;

        /// <summary>
        /// app运行配置【该值只会在editor模式下调用】
        /// </summary>
        public static AppBuiltinRuntimeSettings Instance
        {
            get
            {
                if(m_Instance == null)
                {
                    m_Instance = Resources.Load<AppBuiltinRuntimeSettings>("AppBuiltinRuntimeSettings");
                }
                return m_Instance;
            }
        }
#endif
        /// <summary>
        /// 屏幕适配模式
        /// </summary>
        public enum ScreenFitMode
        {
            /// <summary>
            /// 宽度适配
            /// </summary>
            Width,
            /// <summary>
            /// 高度适配
            /// </summary>
            Height,
        }

        /// <summary>
        /// 加载资源的模式【默认package模式加载】
        /// </summary>
        public ResourceMode LoadResourceMode = ResourceMode.Updatable;

        /// <summary>
        /// 屏幕初始设置分辨率
        /// </summary>
        public Vector2Int DesignResolution;

        /// <summary>
        /// 加载内置设置模块
        /// </summary>
        /// <returns></returns>
        public static AppBuiltinRuntimeSettings LoadAppBuiltinRuntimesSettings( )
        {
            if(PlayFreelyGameBuiltinEntry.AppBuiltinRuntimeConfigs != null)
            {
                return PlayFreelyGameBuiltinEntry.AppBuiltinRuntimeConfigs;
            }
            return Resources.Load<AppBuiltinRuntimeSettings>("AppBuiltinRuntimeSettings");
        }

        /// <summary>
        /// 加载本地用户配置
        /// </summary>
        public void LoadGameLocalConfig( )
        {
            //PlayFreelyGameBuiltinEntry.Config.ReadData("");
        }

    }
}
