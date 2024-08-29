using UnityEngine;

public class JumpState : CharacterState
{
    public JumpState(CharacterManager _stateMachine, PlayerInputManager _inputManager, CharacterParameters _parameters, CharacterUI _ui) : base(nameof(JumpState), _stateMachine, _inputManager, _parameters, _ui) { }

    public override void StateEnter()
    {
        base.StateEnter();
        Manager.AimCamera.gameObject.SetActive(false);
        Manager.Animator.SetBool(Parameters.JumpParameterName, true);
        Manager.Animator.SetFloat(Parameters.SpeedParameterName, 0f);
        Manager.ClearAnimationBlendSpeed();
        Manager.SetVerticalVelocity(Mathf.Sqrt(Parameters.JumpHeight * -2f * Parameters.Gravity));
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Move(GetSpeed());
        Manager.Gravity();

        if (Manager.IsGrounded() && Manager.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= Parameters.JumpGroundCheckThreshold)
        {
            Manager.OnLand();
            Manager.ChangeState(InputManager.MoveInput.sqrMagnitude == 0 ? Manager.IdleState : Manager.MoveState);
            return;
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        Manager.CameraRotation(Parameters.Sensitivity);
    }

    public override void StateExit()
    {
        base.StateExit();

        Manager.Animator.SetBool(Parameters.JumpParameterName, false);
        Manager.Animator.SetBool(Parameters.FallParameterName, false);
    }
}
