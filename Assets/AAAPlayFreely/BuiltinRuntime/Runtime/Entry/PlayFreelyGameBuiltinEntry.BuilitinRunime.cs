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
        public static AppBuiltinRuntimeSettings AppBuiltinRuntimeConfigs
        {
            get;
            private set;
        }

        /// <summary>
        /// 自定义
        /// </summary>
        public static BuiltinRuntimeComponent BuiltinRuntimeData
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
        /// Live2D
        /// </summary>
        public static Live2DComponent Live2D
        {
            get;
            private set;
        }

        /// <summary>
        /// AVPro视频播放器
        /// </summary>
        public static AVProComponent AVProData
        {
            get;
            private set;
        }


        /// <summary>
        /// 初始化内置模块
        /// </summary>
        private void InitBuiltinRuntimeComponents( )
        {
            AppBuiltinRuntimeConfigs = AppBuiltinRuntimeSettings.LoadAppBuiltinRuntimesSettings( );
            BuiltinRuntimeData = GameEntry.GetComponent<BuiltinRuntimeComponent>( );
            Hybridclr = GameEntry.GetComponent<HybridclrComponent>( );
            Live2D = GameEntry.GetComponent<Live2DComponent>( );
            AVProData = GameEntry.GetComponent<AVProComponent>( );
            DontDestroyOnLoad(this);
        }
    }
}
