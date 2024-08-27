using UnityEngine;

public class CharacterState : StateBase
{
    protected PlayerInputManager InputManager;
    protected CharacterManager Manager;
    protected CharacterParameters Parameters;

    private Vector3 direction;
    private Vector3 moveDirection;
    private Vector3 mouseWorldPosition;
    private Vector3 worldAimTarget;
    private Vector3 aimDirection;
    protected Vector3 ScreenCenterPoint;

    private float targetAngle;
    private float rotation;
    private float turnSmoothVelocitY;

    private Ray ray;

    public CharacterState(string _name, CharacterManager _stateMachine, PlayerInputManager _inputManager, CharacterParameters _parameters) : base(_name, _stateMachine)
    {
        InputManager = _inputManager;
        Manager = _stateMachine;
        Parameters = _parameters;
        ScreenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    public override void StateEnter()
    {
        base.StateEnter();
        Manager.FollowCameraNoise.m_AmplitudeGain = 0;
        Manager.FollowCameraNoise.m_FrequencyGain = 0;
    }

    protected void Move(float _speed)
    {
        if (InputManager.MoveInput == Vector2.zero)
            return;

        direction = new Vector3(InputManager.MoveInput.x, 0f, InputManager.MoveInput.y).normalized;

        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Manager.MainCamera.transform.eulerAngles.y;
        rotation = Mathf.SmoothDampAngle(Manager.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocitY, Parameters.TurnSmoothTime);

        if (!InputManager.Aim)
            Manager.transform.rotation = Quaternion.Euler(0f, rotation, 0f);

        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        Manager.CharacterController.Move(moveDirection.normalized * Time.deltaTime * _speed);
    }

    protected float GetSpeed()
    {
        if (InputManager.MoveInput == Vector2.zero)
            return 0;

        if (InputManager.Run)
            return Parameters.RunSpeed;

        return Parameters.WalkSpeed;
    }

    protected void ShootingTargetPosition()
    {
        if (!InputManager.Aim)
            return;

        ray = Manager.MainCamera.ScreenPointToRay(ScreenCenterPoint);
        mouseWorldPosition = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit, 999f, Parameters.AimLayers))
            mouseWorldPosition = hit.point;
    }

    protected void LookAtAim()
    {
        if (!InputManager.Aim)
            return;

        if (mouseWorldPosition == Vector3.zero)
        {
            Manager.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            return;
        }

        worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = Manager.transform.position.y;
        aimDirection = (worldAimTarget - Manager.transform.position).normalized;
        Manager.transform.forward = Vector3.Lerp(Manager.transform.forward, aimDirection, Time.deltaTime * Parameters.AimingRotateSpeed);
    }
}
