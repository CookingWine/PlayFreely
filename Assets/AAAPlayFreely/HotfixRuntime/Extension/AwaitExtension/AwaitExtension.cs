using GameFramework;
using GameFramework.Event;
using GameFramework.Resource;
using PlayFreely.BuiltinRuntime;
using System;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace PlayFreely.HotfixRuntime
{
    /// <summary>
    /// 可等待扩展
    /// </summary>
    public static class AwaitExtension
    {
        /// <summary>
        /// 加载UI
        /// </summary>
        private static TaskCompletionSource<UIForm> s_UIFormTcs;
        /// <summary>
        /// 加载实体
        /// </summary>
        private static TaskCompletionSource<Entity> s_EntityTcs;
        /// <summary>
        /// 加载场景
        /// </summary>
        private static TaskCompletionSource<bool> s_SceneTcs;
        /// <summary>
        /// 下载任务
        /// </summary>
        private static TaskCompletionSource<bool> s_DownloadTcs;
        /// <summary>
        /// 加载资源任务完成
        /// </summary>
        private static TaskCompletionSource<object> s_LoadAssetTcs;

        /// <summary>
        /// UI界面序列号
        /// </summary>
        private static int? s_UIFormSerialId;
        /// <summary>
        /// 实体序列号
        /// </summary>
        private static int s_EntitySerialId;
        /// <summary>
        /// 加载场景资源名称
        /// </summary>
        private static string s_LoadSceneAssetName;
        /// <summary>
        /// 下载序列号
        /// </summary>
        private static int s_DownloadSerialId;
        /// <summary>
        /// 加载资源完成回调
        /// </summary>
        private static readonly LoadAssetCallbacks s_LoadAssetCallbacks;

        /// <summary>
        /// 可等待扩展
        /// </summary>
        static AwaitExtension( )
        {
            PlayFreelyGameBuiltinEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId , OnOpenUIFormSuccessCallback);
            PlayFreelyGameBuiltinEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId , OnOpenUIFormFailureCallback);

            s_UIFormSerialId = null;
            s_EntitySerialId = int.MinValue;
            s_LoadSceneAssetName = null;
            s_DownloadSerialId = int.MinValue;
            s_LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadAssetSuccessCallback , OnLoadAssetFailureCallback);
        }

        /// <summary>
        /// 打开UI成功回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnOpenUIFormSuccessCallback(object sender , GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs us = (OpenUIFormSuccessEventArgs)e;
            if(s_UIFormSerialId.HasValue && us.UIForm.SerialId == s_UIFormSerialId.Value)
            {
                s_UIFormTcs.SetResult(us.UIForm);
                s_UIFormTcs = null;
            }
        }
        /// <summary>
        /// 打开UI失败回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnOpenUIFormFailureCallback(object sender , GameEventArgs e)
        {
            OpenUIFormFailureEventArgs ne = (OpenUIFormFailureEventArgs)e;
            if(s_UIFormSerialId.HasValue && ne.SerialId == s_UIFormSerialId.Value)
            {
                s_UIFormTcs.SetException(new GameFrameworkException(ne.ErrorMessage));
                s_UIFormTcs = null;
            }
        }

        /// <summary>
        /// 加载资源成功回调函数。
        /// </summary>
        /// <param name="assetName">要加载的资源名称。</param>
        /// <param name="asset">已加载的资源。</param>
        /// <param name="duration">加载持续时间。</param>
        /// <param name="userData">用户自定义数据。</param>
        private static void OnLoadAssetSuccessCallback(string assetName , object asset , float duration , object userData)
        {
            s_LoadAssetTcs.SetResult(asset);
        }
        /// <summary>
        /// 加载资源失败回调函数。
        /// </summary>
        /// <param name="assetName">要加载的资源名称。</param>
        /// <param name="status">加载资源状态。</param>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="userData">用户自定义数据。</param>
        private static void OnLoadAssetFailureCallback(string assetName , LoadResourceStatus status , string errorMessage , object userData)
        {
            s_LoadAssetTcs.SetException(new GameFrameworkException(errorMessage));
        }


        /// <summary>
        /// 加载资源（可等待）
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="self">资源组件</param>
        /// <param name="assetName">资源名称</param>
        /// <returns>资源</returns>
        public static async Task<T> AwaitLoadAsset<T>(this ResourceComponent self , string assetName)
        {
            object asset = await self.AwaitInternalLoadAsset<T>(assetName);
            return (T)asset;
        }

        /// <summary>
        /// 加载内部资源(可等待)
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="self">资源组件</param>
        /// <param name="assetName">资源名称</param>
        /// <returns>资源</returns>
        private static Task<object> AwaitInternalLoadAsset<T>(this ResourceComponent self , string assetName)
        {
            s_LoadAssetTcs = new TaskCompletionSource<object>( );

            self.LoadAsset(assetName , typeof(T) , s_LoadAssetCallbacks);

            return s_LoadAssetTcs.Task;
        }
    }
}
