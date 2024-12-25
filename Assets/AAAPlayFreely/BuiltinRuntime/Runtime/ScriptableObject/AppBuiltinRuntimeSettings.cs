using GameFramework.Resource;
using UnityEngine;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// app运行的基础设置
    /// </summary>
    [CreateAssetMenu(fileName = "AppBuiltinRuntimeSettings",menuName = "ScriptableObject/AppSettings【App内置配置参数】")]
    public class AppBuiltinRuntimeSettings:ScriptableObject
    {
        /// <summary>
        /// 加载资源的模式【默认package模式加载】
        /// </summary>
        public ResourceMode LoadResourceMode = ResourceMode.Package;

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
            if(PlayFreelyGameBuiltinEntry.BuiltinRuntimeSettings != null)
            {
                return PlayFreelyGameBuiltinEntry.BuiltinRuntimeSettings;
            }
            return Resources.Load<AppBuiltinRuntimeSettings>("AppBuiltinRuntimeSettings");
        }

    }
}
