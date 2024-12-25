using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 流程基类
    /// </summary>
    public abstract class PlayFreelyProcedureBase:ProcedureBase
    {
        /// <summary>
        /// 当前流程
        /// </summary>
        private ProcedureOwner m_CurrentProcedureOwner;

        /// <summary>
        /// 当前流程
        /// </summary>
        public ProcedureOwner CurrentProcedureOwner
        {
            get
            {
                return m_CurrentProcedureOwner;
            }
        }

        /// <summary>
        /// 是否使用本地对话框
        /// <para>在一些特殊的流程（如游戏逻辑对话框资源更新完成前的流程）中，可以考虑调用原生对话框进行消息提示行为</para>
        /// </summary>
        public abstract bool UseNativeDialog { get; }

        /// <summary>
        /// 是否进入下一个流程
        /// </summary>
        protected bool IsEnterNextProcedure;

        /// <summary>
        /// 当前流程持续时间
        /// </summary>
        protected float m_DruationTimer = 0f;

        /// <summary>
        /// 流程最大持续时间
        /// </summary>
        protected readonly static float s_UpdateMaxDurationTime = 0.5f;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_CurrentProcedureOwner = procedureOwner;
        }

    }
}
