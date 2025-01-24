using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 自定义组件
    /// </summary>
    public class BuiltinRuntimeComponent:GameFrameworkComponent
    {
        /// <summary>
        /// 非热更主界面
        /// </summary>
        public BuiltinRuntimeInterface RuntimeInterface
        {
            get;
            private set;
        }

        /// <summary>
        /// 加载默认界面UI
        /// </summary>
        /// <param name="loadInterfaceCompelet">界面加载完成</param>
        public async void InitDefalutResourceUI(Action<bool> loadInterfaceCompelet)
        {
            GameFramework.UI.IUIGroup group = PlayFreelyGameBuiltinEntry.UI.GetUIGroup("BaseUI");
            if(group == null)
            {
                Log.Warning("Not find IUIGroup by name :BaseUI");
                return;
            }
            PlayFreelyUGuiGroupHelper gui = (PlayFreelyUGuiGroupHelper)group.Helper;
            await LoadBuiltinRuntimeInterface(gui.transform , loadInterfaceCompelet);
        }

        /// <summary>
        /// 加载主界面
        /// </summary>
        /// <returns></returns>
        private async Task LoadBuiltinRuntimeInterface(Transform root , Action<bool> loadInterfaceCompelet)
        {
            ResourceRequest request = Resources.LoadAsync<GameObject>("UIPrefab/BuiltinRuntimeInterface");
            await request;
            if(request.asset != null)
            {
                GameObject interfaceObject = request.asset as GameObject;
                GameObject go = Instantiate(interfaceObject);
                go.transform.SetParent(root);
                go.transform.SetLocalPositionAndRotation(Vector3.one , Quaternion.identity);
                go.transform.localScale = Vector3.one;
                RuntimeInterface = go.GetComponent<BuiltinRuntimeInterface>( );
                RuntimeInterface.InitInterfaceData( );
                loadInterfaceCompelet?.Invoke(true);
            }
            else
            {
                loadInterfaceCompelet?.Invoke(false);
            }
        }
    }
}
