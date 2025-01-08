using System.IO;
using UnityEngine;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 默认编辑器配置项
    /// </summary>
    public class PlayFreelyConstEditor
    {
        /// <summary>
        /// 新建脚本时自动修改脚本编码方式为utf-8以支持中文
        /// </summary>
        public const bool AutoScriptUTF8 = true;

        #region 宏定义

        /// <summary>
        /// 禁用禁用热更模式
        /// </summary>
        public const string DISABLE_HYBRIDCLR = "DISABLE_HYBRIDCLR";

        /// <summary>
        /// 正式服环境
        /// </summary>
        public const string PLAYFREELY_OFFICIALLY_LAUNCHED = "PLAYFREELY_OFFICIALLY_LAUNCHED";

        /// <summary>
        /// 测试服环境
        /// </summary>
        public const string PLAYFREELY_CLOSE_BETA = "PLAYFREELY_CLOSE_BETA";

        /// <summary>
        /// 开发环境
        /// </summary>
        public const string PLAYFREELY_DEVELOPMENT_ENVIRONMENT = "PLAYFREELY_DEVELOPMENT_ENVIRONMENT";

        #endregion

        #region 程序集

        /// <summary>
        /// Editor程序集名称
        /// </summary>
        public static string PlayFreelyEditorAssembly => "PlayFreely.EditorTools";

        /// <summary>
        /// 内置运行程序集名称【非热更】
        /// </summary>
        public static string PlayFreelyBuiltinRuntimeAssembly => "PlayFreely.BuiltinRuntime";

        /// <summary>
        /// 热更运行程序集名称【热更】
        /// </summary>
        public static string PlayFreelyHotfixRuntimeAssembly => "PlayFreely.HotfixRuntime";

        #endregion

        #region 文件路径

        //----------------------------------------------↓↓↓↓↓Assets内↓↓↓↓----------------------------------------------------------------------------------------//
        /// <summary>
        /// 编辑器场景文件路径
        /// </summary>
        public static string EditorScenePath => "Assets/EditorScene";

        /// <summary>
        /// 内置运行场景文件【非热更】
        /// </summary>
        public static string BuiltinRuntimeScenePath => "Assets/BuiltinRuntimeScene";

        /// <summary>
        /// 热更运行场景文件【热更】
        /// </summary>
        public static string HotfixRuntimeScenePath => "Assets/AAAPlayFreely/HotfixRuntimeAsset/HotfixScene";

        #region 数据表



        #endregion

        //----------------------------------------------↑↑↑↑↑Assets内↑↑↑↑↑----------------------------------------------------------------------------------------//


        //----------------------------------------------↓↓↓↓↓Project内↓↓↓↓----------------------------------------------------------------------------------------//

        /// <summary>
        /// 【热更】游戏场景配置表
        /// </summary>
        public static string HotfixPlayFreelyGameScenesPath => GetPlayFreelyProjectDataTablePath(PlayFreelyLoadAssetType.HotfixRuntimeAssets , "PlayFreelyGameScenes.xlsx");

        /// <summary>
        /// 【热更】游戏界面配置表
        /// </summary>
        public static string HotfixPlayFreelyGameUIFormPath => GetPlayFreelyProjectDataTablePath(PlayFreelyLoadAssetType.HotfixRuntimeAssets , "PlayFreelyGameUIForm.xlsx");

        /// <summary>
        /// 【热更】游戏背景音乐配置表
        /// </summary>
        public static string HotfixPlayFreelyGameMusicPath => GetPlayFreelyProjectDataTablePath(PlayFreelyLoadAssetType.HotfixRuntimeAssets , "PlayFreelyGameMusic.xlsx");

        /// <summary>
        /// 【热更】游戏UI音效配置表
        /// </summary>
        public static string HotfixPlayFreelyGameUISoundPath => GetPlayFreelyProjectDataTablePath(PlayFreelyLoadAssetType.HotfixRuntimeAssets , "PlayFreelyGameUISound.xlsx");


        //----------------------------------------------↑↑↑↑↑Project内↑↑↑↑↑----------------------------------------------------------------------------------------//

        #endregion


        /// <summary>
        /// 获取project下的数据表的资源路径
        /// </summary>
        /// <param name="loadType">资源类型</param>
        /// <param name="names">资源名称【需要带后缀】</param>
        /// <returns></returns>
        private static string GetPlayFreelyProjectDataTablePath(PlayFreelyLoadAssetType loadType , string names)
        {
            string assetType = loadType == PlayFreelyLoadAssetType.BuiltinRuntimeAssets ? "BuiltinRuntime" : "HotfixRuntime";

            return Path.Combine(Directory.GetParent(Application.dataPath).FullName , $"PlayFreelyDataTable/{assetType}/{names}");
        }
    }
}
