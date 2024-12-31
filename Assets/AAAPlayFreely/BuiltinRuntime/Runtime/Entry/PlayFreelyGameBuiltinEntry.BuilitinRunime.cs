using UnityGameFramework.Runtime;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 内置模块
    /// </summary>
    public partial class PlayFreelyGameBuiltinEntry
    {
        /// <summary>
        /// app内置设置【非热更】
        /// </summary>
        public static AppBuiltinRuntimeSettings BuiltinRuntimeSettings
        {
            get;
            private set;
        }

        /// <summary>
        /// 热更组件
        /// </summary>
        public static HybridclrComponent Hybridclr
        {
            get; private set;
        }


        /// <summary>
        /// 初始化内置模块
        /// </summary>
        private void InitBuiltinRuntimeComponents( )
        {
            BuiltinRuntimeSettings = AppBuiltinRuntimeSettings.LoadAppBuiltinRuntimesSettings( );
            Hybridclr = GameEntry.GetComponent<HybridclrComponent>( );

            DontDestroyOnLoad(this);
        }
    }
}
