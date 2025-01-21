using GameFramework;
using System.IO;
using UnityEngine;
using UnityGameFramework.Editor;
using UnityGameFramework.Editor.ResourceTools;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// GF配置
    /// </summary>
    public static class GameFrameworkConfigs
    {
#if PLAYFREELY_OFFICIALLY_LAUNCHED

        [BuildSettingsConfigPath]
        public static string BuildSettingsConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/Launched/BuildSettings.xml"));

        [ResourceBuilderConfigPath]
        public static string ResourceBuilderConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/Launched/ResourceBuilder.xml"));
#elif PLAYFREELY_CLOSE_BETA
        [BuildSettingsConfigPath]
        public static string BuildSettingsConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/Beta/BuildSettings.xml"));

        [ResourceBuilderConfigPath]
        public static string ResourceBuilderConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/Beta/ResourceBuilder.xml"));
#elif PLAYFREELY_DEVELOPMENT_ENVIRONMENT
        [BuildSettingsConfigPath]
        public static string BuildSettingsConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/Environment/BuildSettings.xml"));

        [ResourceBuilderConfigPath]
        public static string ResourceBuilderConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/Environment/ResourceBuilder.xml"));
#endif
        [ResourceCollectionConfigPath]
        public static string ResourceCollectionConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/ResourceCollection.xml"));

        [ResourceEditorConfigPath]
        public static string ResourceEditorConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath , "ConfigAssets/GameFramework/Configs/ResourceEditor.xml"));
    }
}
