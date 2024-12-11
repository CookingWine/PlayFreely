using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PlayFreelyGameFramework.EditorTools
{
    /// <summary>
    /// Editor工具栏扩展
    /// </summary>
    public class EditorToolbarExtension
    {

        private static GUIContent switchSceneBtContent;

        /// <summary>
        /// Toolbar栏工具箱下拉列表
        /// </summary>
        private static List<Type> editorToolList;
        /// <summary>
        /// 场景下拉列表
        /// </summary>
        private static List<string> sceneAssetList;


        /// <summary>
        /// 初始化
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize( )
        {
            editorToolList = new List<Type>( );
            sceneAssetList = new List<string>( );

            var curOpenSceneName = EditorSceneManager.GetActiveScene( ).name;
            switchSceneBtContent = EditorGUIUtility.TrTextContentWithIcon( "Switch Scene" , "切换场景" , "UnityLogo");

            ScanEditorToolClass( );

            UnityEditorToolbar.RightToolbarGUI.Add(OnRightToolbarGUI);
            UnityEditorToolbar.LeftToolbarGUI.Add(OnLeftToolbarGUI);
        }

        /// <summary>
        /// 获取所有EditorTool扩展工具类,用于显示到Toolbar的Tools菜单栏
        /// </summary>
        static void ScanEditorToolClass( )
        {
            editorToolList.Clear( );
            var editorDll = Utility.Assembly.GetAssemblies( ).First(dll => dll.GetName( ).Name.CompareTo("PlayFreelyGameFramework.EditorTools") == 0);
            var allEditorTool = editorDll.GetTypes( ).Where(tp => ( tp.IsClass && !tp.IsAbstract && tp.IsSubclassOf(typeof(EditorToolBase)) && tp.GetCustomAttribute(typeof(EditorToolMenuAttribute)) != null ));

            editorToolList.AddRange(allEditorTool);
            editorToolList.Sort((x , y) =>
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
            if(EditorGUILayout.DropdownButton(switchSceneBtContent , FocusType.Passive , EditorStyles.toolbarPopup , GUILayout.MaxWidth(150)))
            {
                DrawSwithSceneDropdownMenus( );
            }
            EditorGUILayout.Space(10);
        }

        static void DrawSwithSceneDropdownMenus( )
        {
            GenericMenu popMenu = new GenericMenu( );
            popMenu.allowDuplicateNames = true;
            var sceneGuids = AssetDatabase.FindAssets("t:Scene" , new string[] { ConstEditor.ScenePath });
            sceneAssetList.Clear( );
            for(int i = 0; i < sceneGuids.Length; i++)
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                sceneAssetList.Add(scenePath);
                string fileDir = System.IO.Path.GetDirectoryName(scenePath);
                bool isInRootDir = Utility.Path.GetRegularPath(ConstEditor.ScenePath).TrimEnd('/') == Utility.Path.GetRegularPath(fileDir).TrimEnd('/');
                var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                string displayName = sceneName;
                if(!isInRootDir)
                {
                    var sceneDir = System.IO.Path.GetRelativePath(ConstEditor.ScenePath , fileDir);
                    displayName = $"{sceneDir}/{sceneName}";
                }

                popMenu.AddItem(new GUIContent(displayName) , false , menuIdx => { SwitchScene((int)menuIdx); } , i);
            }
            popMenu.ShowAsContext( );
        }
        private static void SwitchScene(int menuIdx)
        {
            if(menuIdx >= 0 && menuIdx < sceneAssetList.Count)
            {
                var scenePath = sceneAssetList[menuIdx];
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
