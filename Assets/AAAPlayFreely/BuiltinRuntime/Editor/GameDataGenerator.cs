using PlayFreely.BuiltinRuntime;
using OfficeOpenXml;
using UnityEditor;
using System.Collections.Generic;
namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 数据表生成
    /// </summary>
    public class GameDataGenerator
    {

        private static IList<KeyValuePair<int , string>> m_DataTableVarType = null;

        [InitializeOnEnterPlayMode]
        private static void InitEPPlusLicense( )
        {
            //主要用于指定 ExcelPackage 库的使用许可方式。在某些库中，根据不同的使用场景和许可协议，你可能需要设置不同的许可上下文来确保你的使用符合相关的许可证规定。
            //它可以帮助库的开发者和使用者确保在合法和符合许可协议的前提下使用该库，避免潜在的法律问题和使用限制。
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// 生成所有数据表
        /// </summary>
        public static void GenerateDataTable( )
        {

        }

        public static string GameDataExcelRelative2FullPath(PlayFreelyGameDataType tp , string relativeExcelPath)
        {
            var excelDir = GetGameDataExcelDir(tp);
            return UtilityBuiltin.AssetsPath.GetCombinePath(excelDir , relativeExcelPath + ".xlsx");
        }

        /// <summary>
        /// 获取各种游戏数据表Excel的所在路径
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static string GetGameDataExcelDir(PlayFreelyGameDataType tp)
        {
            string excelDir = "";
            switch(tp)
            {
                case PlayFreelyGameDataType.DataTable:
                    excelDir = PlayFreelyConstEditor.DataTableExcelPath;
                    break;
                case PlayFreelyGameDataType.Config:
                    excelDir = PlayFreelyConstEditor.ConfigeExcelPath;
                    break;
                case PlayFreelyGameDataType.Language:
                    excelDir = PlayFreelyConstEditor.LanguageExcelPath;
                    break;
            }
            return excelDir;
        }
    }
}
