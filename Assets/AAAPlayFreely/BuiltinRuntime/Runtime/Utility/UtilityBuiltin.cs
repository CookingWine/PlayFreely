using GameFramework;

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
    }
}
