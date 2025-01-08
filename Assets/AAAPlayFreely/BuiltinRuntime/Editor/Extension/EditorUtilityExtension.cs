using UnityEditor;
using UnityEditor.Build;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 编辑器工具扩展
    /// </summary>
    public static class EditorUtilityExtension
    {

        /// <summary>
        /// 启用宏定义
        /// </summary>
        /// <param name="definition"></param>
        public static void EnableMacroDefinition(string definition)
        {
#if UNITY_2021_1_OR_NEWER
            var buildTarget = GetCurrentNamedBuildTarget( );
            PlayerSettings.GetScriptingDefineSymbols(buildTarget , out string[] defin);
#else
            
#endif
            if(!ArrayUtility.Contains(defin , definition))
            {
                ArrayUtility.Add(ref defin , definition);

#if UNITY_2021_1_OR_NEWER
                PlayerSettings.SetScriptingDefineSymbols(buildTarget , defin);
#else
                
#endif
                
            }
        }

        /// <summary>
        /// 禁用宏定义
        /// </summary>
        /// <param name="definition"></param>
        public static void DisableMacroDefinition(string definition)
        {

        }


        /// <summary>
        /// 获取当前构建平台群体
        /// </summary>
        /// <returns></returns>
        public static BuildTargetGroup GetCurrentBuildTarget( )
        {
#if UNITY_ANDROID
            return BuildTargetGroup.Android;
#elif UNITY_IOS
        return BuildTargetGroup.iOS;
#elif UNITY_STANDALONE
        return BuildTargetGroup.Standalone;
#elif UNITY_WEBGL
        return BuildTargetGroup.WebGL;
#else
        return BuildTargetGroup.Unknown;
#endif
        }

        /// <summary>
        /// 获取当前构建平台的名称
        /// </summary>
        /// <returns></returns>
        public static NamedBuildTarget GetCurrentNamedBuildTarget( )
        {
#if UNITY_ANDROID
            return NamedBuildTarget.Android;
#elif UNITY_IOS
        return NamedBuildTarget.iOS;
#elif UNITY_STANDALONE
        return NamedBuildTarget.Standalone;
#elif UNITY_WEBGL
        return NamedBuildTarget.WebGL;
#else
        return NamedBuildTarget.Unknown;
#endif
        }
    }
}
