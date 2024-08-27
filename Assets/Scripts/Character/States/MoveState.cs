public class MoveState : CharacterState
{
    public MoveState(CharacterManager _stateMachine, PlayerInputManager _inputManager, CharacterParameters _parameters) : base(nameof(MoveState), _stateMachine, _inputManager, _parameters) { }

    public override void StateEnter()
    {
        base.StateEnter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Manager.IsGrounded() && InputManager.Jump)
        {
            Manager.ChangeState(Manager.JumpState);
            return;
        }

        if (Manager.IsGrounded() && InputManager.MoveInput.sqrMagnitude == 0)
        {
            Manager.ChangeState(Manager.IdleState);
            return;
        }

        Manager.Animator.SetBool(Parameters.FallParameterName, !Manager.IsGrounded());

        Move(GetSpeed());
        Manager.Gravity();

        LookAtAim();

        if (Manager.IsGrounded())
            Manager.AnimationBlend(GetSpeed(), Parameters.SpeedParameterName);

        Manager.AimCamera.gameObject.SetActive(InputManager.Aim && Manager.IsGrounded());

        Manager.FollowCameraNoise.m_AmplitudeGain = InputManager.Run ? Manager.RunShakeAmplitude : 0;
        Manager.FollowCameraNoise.m_FrequencyGain = InputManager.Run ? Manager.RunShakeFrequency : 0;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        ShootingTargetPosition();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        Manager.CameraRotation(InputManager.Aim ? Parameters.AimSensitivity : Parameters.Sensitivity);
    }
}
