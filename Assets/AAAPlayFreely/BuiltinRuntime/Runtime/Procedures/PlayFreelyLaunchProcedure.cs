using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 游戏流程入口【非热更】
    /// </summary>
    public class PlayFreelyLaunchProcedure:PlayFreelyProcedureBase
    {
        public override bool UseNativeDialog => false;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //加载游戏本地配置
            PlayFreelyGameBuiltinEntry.AppBuiltinRuntimeConfigs.LoadGameLocalConfig( );
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner , float elapseSeconds , float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner , elapseSeconds , realElapseSeconds);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner , bool isShutdown)
        {
            base.OnLeave(procedureOwner , isShutdown);
        }

    }
}
