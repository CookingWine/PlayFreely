namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 默认编辑器配置项
    /// </summary>
    public class ConstEditor
    {
        #region 程序集

        /// <summary>
        /// Editor程序集名称
        /// </summary>
        public const string PlayFreelyEditorAssembly = "PlayFreely.EditorTools";

        /// <summary>
        /// 内置运行程序集名称【非热更】
        /// </summary>
        public const string PlayFreelyBuiltinRuntimeAssembly = "PlayFreely.BuiltinRuntime";

        /// <summary>
        /// 热更运行程序集名称【热更】
        /// </summary>
        public const string PlayFreelyHotfixRuntimeAssembly = "PlayFreely.HotfixRuntime";

        #endregion

        #region 文件路径

        /// <summary>
        /// 编辑器场景文件路径
        /// </summary>
        public const string EditorScenePath = "Assets/EditorScene";

        /// <summary>
        /// 内置运行场景文件【非热更】
        /// </summary>
        public const string BuiltinRuntimeScenePath = "Assets/AAAPlayFreely/BuiltinRuntimeAsset/BuiltinRuntimeScene";

        /// <summary>
        /// 热更运行场景文件【热更】
        /// </summary>
        public const string HotfixRuntimeScenePath = "Assets/AAAPlayFreely/HotfixRuntimeAsset/HotfixScene";


        #endregion
    }
}
