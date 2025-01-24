using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// UGUI界面辅助器
    /// </summary>
    public class PlayFreelyUGuiGroupHelper:UIGroupHelperBase
    {
        /// <summary>
        /// 缓存画布
        /// </summary>
        private Canvas m_CachedCanvas = null;

        /// <summary>
        /// 画布缩放
        /// </summary>
        private CanvasScaler m_CachedCanvasScaler = null;

        /// <summary>
        /// 2d界面相机
        /// </summary>
        private Camera m_2DUICamera = null;

        /// <summary>
        /// 深度
        /// </summary>
        private int m_Depth = 0;

        /// <summary>
        /// 深度系数
        /// </summary>
        public const int DepthFactor = 10000;

        public override void SetDepth(int depth)
        {
            m_Depth = depth;
            m_CachedCanvas.overrideSorting = true;
            m_CachedCanvas.sortingOrder = DepthFactor * depth;
        }

        private void Awake( )
        {
            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>( );
            m_CachedCanvasScaler = gameObject.GetOrAddComponent<CanvasScaler>( );
            gameObject.GetOrAddComponent<GraphicRaycaster>( );
            m_2DUICamera = GameObject.Find("2DCamera").GetComponent<Camera>( );
        }

        private void Start( )
        {
            InitCanvas( );
        }

        /// <summary>
        /// 初始化画布
        /// </summary>
        private void InitCanvas( )
        {
            RectTransform rect = GetComponent<RectTransform>( );
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;

            if(m_Depth == 0)
            {
                //主界面
                m_CachedCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                m_CachedCanvas.worldCamera = m_2DUICamera;
                m_CachedCanvas.planeDistance = m_CachedCanvas.worldCamera.farClipPlane / 2;
                m_CachedCanvas.sortingLayerName = name;
            }
            else
            {
                m_CachedCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                m_CachedCanvas.overrideSorting = true;
                m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;
            }
            //设置设计得默认分辨率
            m_CachedCanvasScaler.referenceResolution = PlayFreelyGameBuiltinEntry.AppBuiltinRuntimeConfigs.DesignResolution;
            m_CachedCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            float designRation = m_CachedCanvasScaler.referenceResolution.x / m_CachedCanvasScaler.referenceResolution.y;
            AppBuiltinRuntimeSettings.ScreenFitMode canvasFitMode = Screen.width / Screen.height > designRation ? AppBuiltinRuntimeSettings.ScreenFitMode.Height : AppBuiltinRuntimeSettings.ScreenFitMode.Width;
            m_CachedCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            m_CachedCanvasScaler.matchWidthOrHeight = (int)canvasFitMode;
        }
    }
}
