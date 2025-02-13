using PlayFreely.BuiltinRuntime;
using UnityEngine;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace PlayFreely.HotfixRuntime
{
    [CreateAssetMenu(fileName = "AppHotfixRuntimeSettings" , menuName = "ScriptableObject/AppHotfixSettings【App热更配置参数】")]
    public class AppHotfixRuntimeSettings:ScriptableObject
    {
        /// <summary>
        /// 实例
        /// </summary>
        private static AppHotfixRuntimeSettings m_Instance;

        [Header("数据表")]
        [SerializeField] string[] mDataTables;
        public string[] DataTables => mDataTables;


        [Header("配置表")]
        [SerializeField] string[] mConfigs;
        public string[] Configs => mConfigs;

        [Header("多语言表")]
        [SerializeField] string[] mLanguages;
        public string[] Languages => mLanguages;

        [Header("已启用流程列表")]
        [SerializeField] string[] mProcedures;

        public string[] Procedures => mProcedures;

#if UNITY_EDITOR
        /// <summary>
        /// 获取app热更配置的实例【编辑器下使用】
        /// </summary>
        /// <returns></returns>
        public static AppHotfixRuntimeSettings GetAppInstance( )
        {
            if(m_Instance == null)
            {
                string config = UtilityBuiltin.AssetsPath.GetHotfixScriptableAsset("AppHotfixRuntimeSettings");
                m_Instance = AssetDatabase.LoadAssetAtPath<AppHotfixRuntimeSettings>(config);
            }
            return m_Instance;
        }
#endif

        /// <summary>
        /// 获取app热更配置实例
        /// </summary>
        /// <returns></returns>
        public static async Task<AppHotfixRuntimeSettings> GetAppInstanceSync( )
        {
            string config = UtilityBuiltin.AssetsPath.GetHotfixScriptableAsset("AppHotfixRuntimeSettings");
            if(m_Instance == null)
            {
                m_Instance = await PlayFreelyGameBuiltinEntry.Resource.AwaitLoadAsset<AppHotfixRuntimeSettings>(config);
            }
            return m_Instance;
        }

    }
}
