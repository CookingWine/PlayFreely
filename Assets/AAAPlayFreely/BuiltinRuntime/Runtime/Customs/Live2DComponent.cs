using UnityEngine;
using UnityGameFramework.Runtime;
namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// Live2D组件
    /// </summary>
    public class Live2DComponent:GameFrameworkComponent
    {
        /// <summary>
        /// Live2D物体的父节点
        /// </summary>
        public Transform Live2DParentNode { get; private set; }

        /// <summary>
        /// Live2D物体的专属渲染相机
        /// </summary>
        [SerializeField]
        private Camera m_Live2DCamera;

        /// <summary>
        /// Live2D物体的专属相机
        /// </summary>
        public Camera Live2DCamera
        {
            get
            {
                if(m_Live2DCamera == null)
                {
                    m_Live2DCamera = GameObject.Find("Live2DCamera").GetComponent<Camera>( );
                }
                return m_Live2DCamera;
            }
        }

        protected override void Awake( )
        {
            base.Awake( );
            Live2DParentNode = transform;
            InitLive2DConfigs( );
        }

        /// <summary>
        /// 初始化Live2D的配置
        /// </summary>
        private void InitLive2DConfigs( )
        {
            if(Live2DCamera != null)
            {
                int layer = LayerMask.NameToLayer("Live2D");
                Live2DCamera.gameObject.layer = layer;
                Live2DCamera.clearFlags = CameraClearFlags.Depth;
                Live2DCamera.orthographic = true;
                Live2DCamera.orthographicSize = 1;
                Live2DCamera.depth = 1;
            }
        }
    }
}
