using System.IO;
using UnityEngine;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 默认编辑器配置项
    /// </summary>
    internal class PlayFreelyConstEditor
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


        //----------------------------------------------↑↑↑↑↑Assets内↑↑↑↑↑----------------------------------------------------------------------------------------//


        //----------------------------------------------↓↓↓↓↓Project内↓↓↓↓----------------------------------------------------------------------------------------//

        /// <summary>
        /// 数据表excel目录
        /// </summary>
        public static string DataTableExcelPath => GetPlayFreelyProjectDataTablePath("DataTables");
        /// <summary>
        /// 配置表excel目录
        /// </summary>
        public static string ConfigeExcelPath => GetPlayFreelyProjectDataTablePath("Configs");
        /// <summary>
        /// 语言本地化excel目录
        /// </summary>
        public static string LanguageExcelPath => GetPlayFreelyProjectDataTablePath("Languages");


        //----------------------------------------------↑↑↑↑↑Project内↑↑↑↑↑----------------------------------------------------------------------------------------//

        #endregion


        private static string GetPlayFreelyProjectDataTablePath(string fileName)
        {
            return Path.Combine(Directory.GetParent(Application.dataPath).FullName , $"PlayFreelyDataTable/{fileName}");
        }
    }
}
