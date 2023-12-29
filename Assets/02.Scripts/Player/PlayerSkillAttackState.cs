using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;

    public PlayerSkillAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.SkillAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.SkillAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Player.Data.SkillData.ForceTransitionTime)
                TryApplyForce();

            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.SkillData.Dealing_Start_TransitionTime)
            {
                stateMachine.Player.Weapon.SetAttack(stateMachine.Player.Data.SkillData.SkillDamage, stateMachine.Player.Data.SkillData.Force);
                stateMachine.Player.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }

            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.SkillData.Dealing_End_TransitionTime)
            {
                stateMachine.Player.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * stateMachine.Player.Data.SkillData.Force);

    }
}
