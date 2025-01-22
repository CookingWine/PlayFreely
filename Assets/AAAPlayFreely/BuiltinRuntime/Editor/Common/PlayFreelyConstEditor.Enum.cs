using System;

namespace PlayFreely.EditorTools
{
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
