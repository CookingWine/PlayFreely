namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 全局数据
    /// </summary>
    public class BuiltinRuntimeGlobalData
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string AppConfigsFilePath
        {
            get
            {
                return PlayFreelyGameBuiltinEntry.Resource.ReadWritePath + "/Configs";
            }
        }
    }
}
