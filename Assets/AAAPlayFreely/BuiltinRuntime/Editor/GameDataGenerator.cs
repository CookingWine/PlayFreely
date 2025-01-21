using PlayFreely.BuiltinRuntime;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 数据表生成
    /// </summary>
    public class GameDataGenerator
    {

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
