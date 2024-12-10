using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#else
using UnityEngine.Experimental.UIElements;
#endif

namespace PlayFreelyGameFramework.EditorTools
{
    /// <summary>
    /// Unity编辑器工具栏
    /// </summary>
    [InitializeOnLoad]
    public static class UnityEditorToolbar
    {
        /// <summary>
        /// 左边工具栏GUI绘制
        /// </summary>
        public static readonly List<Action> LeftToolbarGUI = new List<Action>( );
        /// <summary>
        /// 右边工具栏GUI绘制
        /// </summary>
        public static readonly List<Action> RightToolbarGUI = new List<Action>( );


        static UnityEditorToolbar( )
        {
            Type toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
            ToolbarCallback.OnToolbarGUI = OnGUI;
            ToolbarCallback.OnToolbarGUILeft = OnGUILeft;
            ToolbarCallback.OnToolbarGUIRight = OnGUIRight;
        }

        private static void OnGUI( )
        {

        }

        private static void OnGUILeft( )
        {
            GUILayout.BeginHorizontal( );
            foreach(var handler in LeftToolbarGUI)
            {
                handler( );
            }
            GUILayout.EndHorizontal( );
        }

        private static void OnGUIRight( )
        {
            GUILayout.BeginHorizontal( );
            foreach(var handler in RightToolbarGUI)
            {
                handler( );
            }
            GUILayout.EndHorizontal( );
        }

    }

    /// <summary>
    /// 工具栏回调
    /// </summary>
    public static class ToolbarCallback
    {
        static ToolbarCallback( )
        {
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        #region 通过命名获取到类型

        private static readonly Type m_toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static readonly Type m_guiViewType = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");
#if UNITY_2020_1_OR_NEWER
        private static readonly Type m_iWindowBackendType = typeof(Editor).Assembly.GetType("UnityEditor.IWindowBackend");
        private static readonly PropertyInfo m_windowBackend = m_guiViewType.GetProperty("windowBackend" , BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly PropertyInfo m_viewVisualTree = m_iWindowBackendType.GetProperty("visualTree" , BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#else
		private static readonly PropertyInfo m_viewVisualTree = m_guiViewType.GetProperty("visualTree",BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#endif
        private static readonly FieldInfo m_imguiContainerOnGui = typeof(IMGUIContainer).GetField("m_OnGUIHandler" , BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        #endregion

        static ScriptableObject m_currentToolbar;

        /// <summary>
        /// 工具栏OGUI的回调方法
        /// </summary>
        public static Action OnToolbarGUI;
        /// <summary>
        /// 工具栏左边OGUI的回调方法
        /// </summary>
        public static Action OnToolbarGUILeft;
        /// <summary>
        /// 工具栏右边OGUI的回调方法
        /// </summary>
        public static Action OnToolbarGUIRight;

        private static void OnGUI( )
        {
            OnToolbarGUI?.Invoke( );
        }

        private static void OnUpdate( )
        {
            //依赖于工具栏是ScriptableObject的事实，并在布局改变时被删除
            if(m_currentToolbar == null)
            {
                //查找工具栏
                var toolbars = Resources.FindObjectsOfTypeAll(m_toolbarType);
                m_currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
                if(m_currentToolbar != null)
                {
#if UNITY_2021_1_OR_NEWER
                    var root = m_currentToolbar.GetType( ).GetField("m_Root" , BindingFlags.NonPublic | BindingFlags.Instance);
                    var rawRoot = root.GetValue(m_currentToolbar);
                    var mRoot = rawRoot as VisualElement;
                    RegisterCallback("ToolbarZoneLeftAlign" , OnToolbarGUILeft);
                    RegisterCallback("ToolbarZoneRightAlign" , OnToolbarGUIRight);

                    void RegisterCallback(string root , Action cb)
                    {
                        var toolbarZone = mRoot.Q(root);

                        var parent = new VisualElement( )
                        {
                            style = { flexGrow = 1 , flexDirection = FlexDirection.Row , }
                        };
                        var container = new IMGUIContainer( );
                        container.style.flexGrow = 1;
                        container.onGUIHandler += ( ) =>
                        {
                            cb?.Invoke( );
                        };
                        parent.Add(container);
                        toolbarZone.Add(parent);
                    }
#else
#if UNITY_2020_1_OR_NEWER
					var windowBackend = m_windowBackend.GetValue(m_currentToolbar);

					// 获取到它的视觉树
					var visualTree = (VisualElement) m_viewVisualTree.GetValue(windowBackend, null);
#else
					// 获取到它的视觉树
					var visualTree = (VisualElement) m_viewVisualTree.GetValue(m_currentToolbar, null);
#endif

					// 获取第一个子节点碰巧是工具栏IMGUIContainer
					var container = (IMGUIContainer) visualTree[0];

					// (重新)附加处理程序
					var handler = (Action) m_imguiContainerOnGui.GetValue(container);
					handler -= OnGUI;
					handler += OnGUI;
					m_imguiContainerOnGui.SetValue(container, handler);
					
#endif
                }
            }
        }
    }
}
