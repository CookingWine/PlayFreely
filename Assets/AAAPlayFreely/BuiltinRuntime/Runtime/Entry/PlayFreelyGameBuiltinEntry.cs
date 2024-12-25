using UnityEngine;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 游戏入口【非热更层】
    /// </summary>
    public partial class PlayFreelyGameBuiltinEntry:MonoBehaviour
    {
        private void Start( )
        {
            //不在Awake中执行的原因：
            //因为所有的模块全部继承于GameFrameworkComponent
            //在Awake的时候会自动往GameFrameworkComponents链表里面去注册进去后续会从链表内取值
            //如果在Awake中执行那么链表内可能没有该值，就会导致空引用
            //【优先初始化GameFramework的模块】
            InitGameFrameworkComponents( );
            //加载非热更的模块
            InitBuiltinRuntimeComponents( );
        }

    }
}
