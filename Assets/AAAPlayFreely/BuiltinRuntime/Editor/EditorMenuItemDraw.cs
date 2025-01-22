using UnityEditor;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 绘制下拉列表
    /// </summary>
    internal class EditorMenuItemDraw
    {
        [MenuItem("Game Framework/Play Freely/Refresh All Excels【刷新所有数据表】" , false , 10001)]
        public static void GenerateAllDataTable( )
        {
            GameDataGenerator.GenerateDataTable( );
        }
    }
}
