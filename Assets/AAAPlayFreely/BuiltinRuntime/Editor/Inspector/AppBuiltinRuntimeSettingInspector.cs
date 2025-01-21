using PlayFreely.BuiltinRuntime;
using UnityEditor;
using UnityEngine;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// AppBuiltinRuntimeSetting界面重绘
    /// </summary>
    [CustomEditor(typeof(AppBuiltinRuntimeSettings))]
    public class AppBuiltinRuntimeSettingInspector:Editor
    {
        private class ItemData
        {
            /// <summary>
            /// 是否选中
            /// </summary>
            public bool IsOn { get; set; }

            /// <summary>
            /// 表格名称
            /// </summary>
            public string ExcelName { get; }

            public ItemData(bool isOn , string excelName)
            {
                IsOn = isOn;
                ExcelName = excelName;
            }
        }

        /// <summary>
        /// 数据滑动列表
        /// </summary>
        private class GameDataScrollView
        {
            public PlayFreelyGameDataType BuiltinGameDataType { get; private set; }

            public Vector2 ScrollPosition;

            private readonly AppBuiltinRuntimeSettings m_AppBuiltinRuntimeData;

            public GameDataScrollView(AppBuiltinRuntimeSettings appData , PlayFreelyGameDataType dataType)
            {
                m_AppBuiltinRuntimeData = appData;
                BuiltinGameDataType = dataType;


            }

            /// <summary>
            /// 重新加载
            /// </summary>
            public void Reload( )
            {
                if(m_AppBuiltinRuntimeData != null)
                {
                    return;
                }

            }
        }


        /// <summary>
        /// app设置资源
        /// </summary>
        private AppBuiltinRuntimeSettings m_AppBuiltinRuntimeSettings;


        private void OnEnable( )
        {
            m_AppBuiltinRuntimeSettings = (AppBuiltinRuntimeSettings)target;
        }

        private void OnDisable( )
        {


        }

        public override void OnInspectorGUI( )
        {

        }

        private void SaveConfigs(AppBuiltinRuntimeSettings configs)
        {



        }

    }
}
