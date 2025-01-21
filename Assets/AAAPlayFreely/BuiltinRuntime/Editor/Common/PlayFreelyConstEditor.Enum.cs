using System;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 加载的资源类型
    /// </summary>
    public enum PlayFreelyLoadAssetType
    {
        /// <summary>
        /// 非热更资源
        /// </summary>
        BuiltinRuntimeAssets,

        /// <summary>
        /// 热更资源
        /// </summary>
        HotfixRuntimeAssets
    }

    /// <summary>
    /// 游戏数据表
    /// </summary>
    [Flags]
    public enum PlayFreelyGameDataType
    {
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable = 1,

        /// <summary>
        /// 游戏配置配置
        /// </summary>
        Config = 2,

        /// <summary>
        /// 语言配置
        /// </summary>
        Language = 4
    }
}
