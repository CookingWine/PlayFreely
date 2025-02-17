using UnityGameFramework.Runtime;

namespace PlayFreely.HotfixRuntime
{
    /// <summary>
    /// 热更入口
    /// </summary>
    public class HotfixEntry
    {
        /// <summary>
        /// 有限状态机
        /// </summary>
        public static FsmManager FSM { get; private set; }

        /// <summary>
        /// 事件管理器
        /// </summary>
        public static EventManager Event { get; private set; }

        /// <summary>
        /// 计时器
        /// </summary>
        public static TimerManager Timer { get; private set; }

        /// <summary>
        /// 不可调用,供给HybridclrComponent使用【相当于Mono.Start】
        /// </summary>
        public static void Start( )
        {
            Log.Info("<color=lime>Hot update startup.</color>");
        }

        /// <summary>
        /// 不可调用,供给HybridclrComponent使用【相当于Mono.Update】
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public static void Update(float elapseSeconds , float realElapseSeconds)
        {

        }

        /// <summary>
        /// 不可调用,供给HybridclrComponent使用
        /// </summary>
        public static void Shutdown( )
        {

        }
    }
}
