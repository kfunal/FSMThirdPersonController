public class IdleState : CharacterState
{
    public IdleState(CharacterManager _stateMachine, PlayerInputManager _inputManager, CharacterParameters _parameters, CharacterUI _ui) : base(nameof(IdleState), _stateMachine, _inputManager, _parameters, _ui) { }

    public override void StateEnter()
    {
        base.StateEnter();
        Manager.Animator.SetFloat(Parameters.SpeedParameterName, 0f);
        Manager.ClearAnimationBlendSpeed();
    }
    public override void UpdateLogic()
    {
        if (Manager.IsGrounded() && InputManager.Jump)
        {
            Manager.ChangeState(Manager.JumpState);
            return;
        }

        if (InputManager.MoveInput.sqrMagnitude > 0)
        {
            Manager.ChangeState(Manager.MoveState);
            return;
        }

        Manager.Gravity();
        LookAtAim();

        Manager.AimCamera.gameObject.SetActive(InputManager.Aim && Manager.IsGrounded());
        Manager.Animator.SetBool(Parameters.FallParameterName, !Manager.IsGrounded());
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        ShootingTargetPosition();
        Interaction();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        Manager.CameraRotation(InputManager.Aim ? Parameters.AimSensitivity : Parameters.Sensitivity);
    }
}
