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

namespace PlayFreely.EditorTools
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
        private static int m_toolCount;
        private static GUIStyle m_commandStyle = null;

        static UnityEditorToolbar( )
        {
            Type toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
            ToolbarCallback.OnToolbarGUI = OnGUI;
            ToolbarCallback.OnToolbarGUILeft = OnGUILeft;
            ToolbarCallback.OnToolbarGUIRight = OnGUIRight;
        }
#if UNITY_2019_3_OR_NEWER
        public const float space = 8;
#else
		public const float space = 10;
#endif
        public const float largeSpace = 20;
        public const float buttonWidth = 32;
        public const float dropdownWidth = 80;
#if UNITY_2019_1_OR_NEWER
        public const float playPauseStopWidth = 140;
#else
		public const float playPauseStopWidth = 100;
#endif
        private static void OnGUI( )
        {
            // Create two containers, left and right
            // Screen is whole toolbar

            m_commandStyle ??= new GUIStyle("CommandLeft");

            var screenWidth = EditorGUIUtility.currentViewWidth;

            // Following calculations match code reflected from Toolbar.OldOnGUI()
            float playButtonsPosition = Mathf.RoundToInt(( screenWidth - playPauseStopWidth ) / 2);

            Rect leftRect = new Rect(0 , 0 , screenWidth , Screen.height);
            leftRect.xMin += space; // Spacing left
            leftRect.xMin += buttonWidth * m_toolCount; // Tool buttons
#if UNITY_2019_3_OR_NEWER
            leftRect.xMin += space; // Spacing between tools and pivot
#else
			leftRect.xMin += largeSpace; // Spacing between tools and pivot
#endif
            leftRect.xMin += 64 * 2; // Pivot buttons
            leftRect.xMax = playButtonsPosition;

            Rect rightRect = new Rect(0 , 0 , screenWidth , Screen.height);
            rightRect.xMin = playButtonsPosition;
            rightRect.xMin += m_commandStyle.fixedWidth * 3; // Play buttons
            rightRect.xMax = screenWidth;
            rightRect.xMax -= space; // Spacing right
            rightRect.xMax -= dropdownWidth; // Layout
            rightRect.xMax -= space; // Spacing between layout and layers
            rightRect.xMax -= dropdownWidth; // Layers
#if UNITY_2019_3_OR_NEWER
            rightRect.xMax -= space; // Spacing between layers and account
#else
			rightRect.xMax -= largeSpace; // Spacing between layers and account
#endif
            rightRect.xMax -= dropdownWidth; // Account
            rightRect.xMax -= space; // Spacing between account and cloud
            rightRect.xMax -= buttonWidth; // Cloud
            rightRect.xMax -= space; // Spacing between cloud and collab
            rightRect.xMax -= 78; // Colab

            // Add spacing around existing controls
            leftRect.xMin += space;
            leftRect.xMax -= space;
            rightRect.xMin += space;
            rightRect.xMax -= space;

            // Add top and bottom margins
#if UNITY_2019_3_OR_NEWER
            leftRect.y = 4;
            leftRect.height = 22;
            rightRect.y = 4;
            rightRect.height = 22;
#else
			leftRect.y = 5;
			leftRect.height = 24;
			rightRect.y = 5;
			rightRect.height = 24;
#endif

            if(leftRect.width > 0)
            {
                GUILayout.BeginArea(leftRect);
                GUILayout.BeginHorizontal( );
                foreach(var handler in LeftToolbarGUI)
                {
                    handler( );
                }

                GUILayout.EndHorizontal( );
                GUILayout.EndArea( );
            }

            if(rightRect.width > 0)
            {
                GUILayout.BeginArea(rightRect);
                GUILayout.BeginHorizontal( );
                foreach(var handler in RightToolbarGUI)
                {
                    handler( );
                }

                GUILayout.EndHorizontal( );
                GUILayout.EndArea( );
            }
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
