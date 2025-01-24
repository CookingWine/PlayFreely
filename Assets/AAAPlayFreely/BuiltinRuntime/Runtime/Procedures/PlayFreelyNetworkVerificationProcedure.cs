using System;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 网络验证流程
    /// </summary>
    public class PlayFreelyNetworkVerificationProcedure:PlayFreelyProcedureBase
    {

        /// <summary>
        /// 是否使用本地对话框
        /// </summary>
        private bool m_UseNativeDialog;

        public override bool UseNativeDialog
        {
            get
            {
                return m_UseNativeDialog;
            }
        }


        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_UseNativeDialog = true;
            IsEnterNextProcedure = Application.internetReachability != NetworkReachability.NotReachable;

            Resources.UnloadUnusedAssets( );
            GC.Collect( );
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner , float elapseSeconds , float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner , elapseSeconds , realElapseSeconds);


        }

    }
}
