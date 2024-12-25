using PlayFreely.BuiltinRuntime;
using UnityEditor;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// AppBuiltinRuntimeSetting界面重绘
    /// </summary>
    [CustomEditor(typeof(AppBuiltinRuntimeSettings))]
    public class AppBuiltinRuntimeSettingInspector:Editor
    {
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
