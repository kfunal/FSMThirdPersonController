using Cinemachine;
using UnityEngine;

public class CharacterManager : StateMachine
{

    #region States

    public IdleState IdleState { get; private set; } = null;
    public MoveState MoveState { get; private set; } = null;
    public JumpState JumpState { get; private set; } = null;

    #endregion

    #region Inspector

    #region Camera

    [field: Header("Camera")]
    [field: Tooltip("Main Camera Transform")]
    [field: SerializeField] public Camera MainCamera { get; private set; }

    [field: Tooltip("Camera Target Transform")]
    [field: SerializeField] public Transform CameraTarget { get; private set; }

    [field: Tooltip("Aim Camera")]
    [field: SerializeField] public CinemachineVirtualCamera AimCamera { get; private set; }

    [field: Tooltip("Follow Camera")]
    [field: SerializeField] public CinemachineVirtualCamera FollowCamera { get; private set; }

    [field: Tooltip("How far in degrees can you move the camera up")]
    [field: SerializeField] public float TopClamp { get; private set; }

    [field: Tooltip("How far in degrees can you move the camera down")]
    [field: SerializeField] public float BottomClamp { get; private set; }

    [field: Tooltip("Shake Amplitude while running")]
    [field: SerializeField] public float RunShakeAmplitude { get; private set; }

    [field: Tooltip("Shake Frequency while running")]
    [field: SerializeField] public float RunShakeFrequency { get; private set; }

    #endregion

    #region Script References

    [Header("Script References")]
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private CharacterUI ui;

    #endregion

    #region Scriptable Objects

    [Header("Scriptable Objects")]
    [SerializeField] private CharacterParameters parameters;

    #endregion

    #region Audio

    [field: Header("Audio")]
    [field: SerializeField] public AudioSource MoveAudioSource { get; private set; }
    [field: SerializeField] public AudioClip FootStepSound { get; private set; }
    [field: SerializeField] public AudioClip JumpSound { get; private set; }
    [field: SerializeField] public AudioClip LandSound { get; private set; }

    #endregion

    #region Weapon

    [field: Header("Weapon")]
    [field: SerializeField] public Transform WeaponSlot { get; private set; }

    #endregion

    #endregion

    public CharacterController CharacterController { get; private set; } = null;
    public Animator Animator { get; private set; } = null;
    public CinemachineBasicMultiChannelPerlin FollowCameraNoise { get; private set; } = null;

    private const float cameraThreshold = 0.01f;
    private float CameraTargetHorizontalAngle = 0;
    private float CameraTargetVerticalAngle = 0;
    private float blendSpeed;
    private float blendVelocity;
    private float verticalVelocity;

    private Vector3 gravity;
    private Vector3 groundCheckSpherePosition;

    public Vector3 CurrentGravity => gravity;

    private void Awake()
    {
        Components();
        StateInit();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Components()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        FollowCameraNoise = FollowCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void StateInit()
    {
        IdleState = new IdleState(this, inputManager, parameters, ui);
        MoveState = new MoveState(this, inputManager, parameters, ui);
        JumpState = new JumpState(this, inputManager, parameters, ui);
    }

    public override StateBase InitState() => IdleState;

    internal void CameraRotation(float _sensitivity)
    {
        if (inputManager.LookInput.sqrMagnitude >= cameraThreshold)
        {
            CameraTargetHorizontalAngle += inputManager.LookInput.x * _sensitivity;
            CameraTargetVerticalAngle += inputManager.LookInput.y * _sensitivity;
        }

        CameraTargetHorizontalAngle = ClampAngle(CameraTargetHorizontalAngle, float.MinValue, float.MaxValue);
        CameraTargetVerticalAngle = ClampAngle(CameraTargetVerticalAngle, BottomClamp, TopClamp);

        CameraTarget.transform.rotation = Quaternion.Euler(CameraTargetVerticalAngle, CameraTargetHorizontalAngle, 0f);
    }

    internal void AnimationBlend(float _targetSpeed, string _animationParameter)
    {
        if (Mathf.Abs(blendSpeed - _targetSpeed) < parameters.BlendThreshold)
        {
            blendSpeed = _targetSpeed;
            return;
        }

        blendSpeed = Mathf.SmoothDamp(blendSpeed, _targetSpeed, ref blendVelocity, parameters.BlendSpeed);
        Animator.SetFloat(_animationParameter, blendSpeed);
    }

    internal void ClearAnimationBlendSpeed() => blendSpeed = 0;

    internal void Gravity()
    {
        verticalVelocity += parameters.Gravity * Time.deltaTime;

        if (IsGrounded() && verticalVelocity <= 0f)
            verticalVelocity = -2f;

        gravity = Vector3.up * verticalVelocity;

        CharacterController.Move(gravity * Time.deltaTime);
    }

    internal void SetVerticalVelocity(float _newVelocity) => verticalVelocity = _newVelocity;

    internal bool IsGrounded()
    {
        groundCheckSpherePosition = new Vector3(transform.position.x, transform.position.y - parameters.GroundedOffset, transform.position.z);
        return Physics.CheckSphere(groundCheckSpherePosition, parameters.GroundedRadius, parameters.GroundLayers, QueryTriggerInteraction.Ignore); // Ignores trigger colliders
    }

    public void OnFootStep(float _targetMoveSpeed)
    {
        if (!IsGrounded()) return;

        if (GetMovementState(_targetMoveSpeed) == GetPlayerSpeed())
            MoveAudioSource.PlayOneShot(FootStepSound);
    }

    private float GetMovementState(float _speed)
    {
        if (_speed < parameters.WalkSpeed)
            return 0;

        if (_speed < parameters.RunSpeed)
            return parameters.WalkSpeed;

        return parameters.RunSpeed;
    }

    private float GetPlayerSpeed()
    {
        if (CurrentState == IdleState)
            return 0;

        if (CurrentState == JumpState)
            return 0;

        if (inputManager.Run)
            return parameters.RunSpeed;

        return parameters.WalkSpeed;
    }

    public void OnJump() => MoveAudioSource.PlayOneShot(JumpSound);

    public void OnLand() => MoveAudioSource.PlayOneShot(LandSound);

    private float ClampAngle(float _angle, float _min, float _max)
    {
        if (_angle < -360f) _angle += 360f;
        if (_angle > 360f) _angle -= 360f;
        return Mathf.Clamp(_angle, _min, _max);
    }
}
