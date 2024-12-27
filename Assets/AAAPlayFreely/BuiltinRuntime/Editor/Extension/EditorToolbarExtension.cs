using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// Editor工具栏扩展
    /// </summary>
    public class EditorToolbarExtension
    {
        /// <summary>
        /// 绘制切换场景按钮
        /// </summary>
        private static GUIContent g_SwitchSceneBtContent;

        /// <summary>
        /// Toolbar栏工具箱下拉列表
        /// </summary>
        private static List<Type> m_EditorToolList;
        /// <summary>
        /// 场景下拉列表
        /// </summary>
        private static List<string> m_SceneAssetList;


        /// <summary>
        /// 初始化
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize( )
        {
            m_EditorToolList = new List<Type>( );
            m_SceneAssetList = new List<string>( );

            var curOpenSceneName = EditorSceneManager.GetActiveScene( ).name;
            g_SwitchSceneBtContent = EditorGUIUtility.TrTextContentWithIcon("Switch Scene" , "切换场景" , "UnityLogo");

            ScanEditorToolClass( );

            UnityEditorToolbar.RightToolbarGUI.Add(OnRightToolbarGUI);
            UnityEditorToolbar.LeftToolbarGUI.Add(OnLeftToolbarGUI);
        }

        /// <summary>
        /// 获取所有EditorTool扩展工具类,用于显示到Toolbar的Tools菜单栏
        /// </summary>
        static void ScanEditorToolClass( )
        {
            m_EditorToolList.Clear( );
            var editorDll = Utility.Assembly.GetAssemblies( ).First(dll => dll.GetName( ).Name.CompareTo(PlayFreelyConstEditor.PlayFreelyEditorAssembly) == 0);
            var allEditorTool = editorDll.GetTypes( ).Where(tp => ( tp.IsClass && !tp.IsAbstract && tp.IsSubclassOf(typeof(EditorToolBase)) && tp.GetCustomAttribute(typeof(EditorToolMenuAttribute)) != null ));

            m_EditorToolList.AddRange(allEditorTool);
            m_EditorToolList.Sort((x , y) =>
            {
                int xOrder = x.GetCustomAttribute<EditorToolMenuAttribute>( ).MenuOrder;
                int yOrder = y.GetCustomAttribute<EditorToolMenuAttribute>( ).MenuOrder;
                return xOrder.CompareTo(yOrder);
            });
        }

        /// <summary>
        /// 绘制右边的工具栏
        /// </summary>
        private static void OnRightToolbarGUI( )
        {

        }

        /// <summary>
        /// 绘制左边的工具栏
        /// </summary>
        private static void OnLeftToolbarGUI( )
        {
            GUILayout.FlexibleSpace( );
            if(EditorGUILayout.DropdownButton(g_SwitchSceneBtContent , FocusType.Passive , EditorStyles.toolbarPopup , GUILayout.MaxWidth(150)))
            {
                DrawSwithSceneDropdownMenus( );
            }
            EditorGUILayout.Space(10);
        }

        /// <summary>
        /// 绘制切换场景的下拉菜单
        /// </summary>
        static void DrawSwithSceneDropdownMenus( )
        {
            GenericMenu popMenu = new GenericMenu( );
            popMenu.allowDuplicateNames = true;
            m_SceneAssetList.Clear( );
            DrawSceneMenusInfo(popMenu , PlayFreelyConstEditor.EditorScenePath);
            DrawSceneMenusInfo(popMenu , PlayFreelyConstEditor.BuiltinRuntimeScenePath);
            DrawSceneMenusInfo(popMenu , PlayFreelyConstEditor.HotfixRuntimeScenePath);
            popMenu.ShowAsContext( );
        }

        private static void DrawSceneMenusInfo(GenericMenu popMenu , string path)
        {
            var sceneGuids = AssetDatabase.FindAssets("t:Scene" , new string[] { path });
            for(int i = 0; i < sceneGuids.Length; i++)
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                m_SceneAssetList.Add(scenePath);
                string fileDir = System.IO.Path.GetDirectoryName(scenePath);
                bool isInRootDir = Utility.Path.GetRegularPath(path).TrimEnd('/') == Utility.Path.GetRegularPath(fileDir).TrimEnd('/');
                var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                string displayName = sceneName;
                if(!isInRootDir)
                {
                    var sceneDir = System.IO.Path.GetRelativePath(path , fileDir);
                    displayName = $"{sceneDir}/{sceneName}";
                }

                popMenu.AddItem(new GUIContent(displayName) , false , menuIdx => { SwitchScene((int)menuIdx); } , m_SceneAssetList.Count - 1 + i);
            }
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="menuIdx">索引</param>
        private static void SwitchScene(int menuIdx)
        {
            if(menuIdx >= 0 && menuIdx < m_SceneAssetList.Count)
            {
                var scenePath = m_SceneAssetList[menuIdx];
                var curScene = EditorSceneManager.GetActiveScene( );
                if(curScene != null && curScene.isDirty)
                {
                    int opIndex = EditorUtility.DisplayDialogComplex("警告" , $"当前场景{curScene.name}未保存,是否保存?" , "保存" , "取消" , "不保存");
                    switch(opIndex)
                    {
                        case 0:
                            if(!EditorSceneManager.SaveOpenScenes( ))
                            {
                                return;
                            }
                            break;
                        case 1:
                            return;
                    }
                }
                EditorSceneManager.OpenScene(scenePath , OpenSceneMode.Single);
            }
        }
    }
}
