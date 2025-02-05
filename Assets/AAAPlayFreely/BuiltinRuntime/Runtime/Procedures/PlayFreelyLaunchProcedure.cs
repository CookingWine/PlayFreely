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

            IsEnterNextProcedure = false;

            //加载游戏本地配置
            PlayFreelyGameBuiltinEntry.AppBuiltinRuntimeConfigs.LoadGameLocalConfig( );

            //加载初始界面
            PlayFreelyGameBuiltinEntry.BuiltinRuntimeData.InitDefalutResourceUI((load) =>
            {
                IsEnterNextProcedure = load;
                if(!load)
                {
                    UnityGameFramework.Runtime.Log.Fatal("Failed to load default interface.");
                    PlayFreelyGameBuiltinEntry.Shutdown(UnityGameFramework.Runtime.ShutdownType.Restart);
                }
            });
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner , float elapseSeconds , float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner , elapseSeconds , realElapseSeconds);
            if(IsEnterNextProcedure)
            {
                ChangeState<PlayFreelyNetworkVerificationProcedure>(procedureOwner);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner , bool isShutdown)
        {
            base.OnLeave(procedureOwner , isShutdown);
        }

    }
}
