using UnityEditor;
using UnityEngine;

namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 编辑器工具基类
    /// </summary>
    public abstract class EditorToolBase:EditorWindow
    {
        /// <summary>
        /// 工具名称
        /// </summary>
        public abstract string ToolName { get; }
        /// <summary>
        /// 窗口大小
        /// </summary>
        public abstract Vector2Int WinSize { get; }
        private void Awake( )
        {
            this.titleContent = new GUIContent(ToolName);
            this.position.Set(this.position.x , this.position.y , this.WinSize.x , this.WinSize.y);
        }
    }
}
