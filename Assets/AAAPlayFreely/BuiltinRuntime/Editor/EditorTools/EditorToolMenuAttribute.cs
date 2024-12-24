using System;

namespace PlayFreelyGameFramework.EditorTools
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EditorToolMenuAttribute:Attribute
    {
        /// <summary>
        /// 菜单工具路径
        /// </summary>
        public string ToolMenuPath { get; private set; }
        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int MenuOrder { get; private set; }
        /// <summary>
        ///是否公用 
        /// </summary>
        public bool IsUtility { get; private set; }
        /// <summary>
        /// 标记子工具类所属的工具Editor
        /// </summary>
        public Type OwnerType { get; private set; }

        /// <summary>
        /// 编辑器工具菜单特效标记
        /// </summary>
        /// <param name="menu">菜单路径</param>
        /// <param name="owner">标记子工具类所属的工具Editor</param>
        /// <param name="menuOrder">菜单顺序</param>
        /// <param name="isUtility">是否公用</param>
        public EditorToolMenuAttribute(string menu , Type owner , int menuOrder = 0 , bool isUtility = false)
        {
            ToolMenuPath = menu;
            OwnerType = owner;
            MenuOrder = menuOrder;
            IsUtility = isUtility;
        }
    }
}
