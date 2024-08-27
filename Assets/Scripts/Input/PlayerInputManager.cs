using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [field: SerializeField] public Vector2 MoveInput { get; private set; } = Vector2.zero;
    [field: SerializeField] public Vector2 LookInput { get; private set; } = Vector2.zero;
    [field: SerializeField] public bool Jump { get; private set; } = false;
    [field: SerializeField] public bool Interact { get; private set; } = false;
    [field: SerializeField] public bool Aim { get; private set; }
    [field: SerializeField] public bool Run { get; private set; }

    private PlayerInputActions _input = null;

    private void OnEnable()
    {
        _input = new PlayerInputActions();

        _input.HumanoidLand.Enable();

        _input.HumanoidLand.Move.performed += SetMove;
        _input.HumanoidLand.Move.canceled += SetMove;

        _input.HumanoidLand.Look.performed += SetLook;
        _input.HumanoidLand.Look.canceled += SetLook;

        _input.HumanoidLand.Jump.started += SetJump;
        _input.HumanoidLand.Jump.canceled += SetJump;

        _input.HumanoidLand.Interact.started += SetInteract;
        _input.HumanoidLand.Interact.canceled += SetInteract;

        _input.HumanoidLand.Aim.started += SetAim;
        _input.HumanoidLand.Aim.canceled += SetAim;

        _input.HumanoidLand.Run.started += SetRun;
        _input.HumanoidLand.Run.canceled += SetRun;
    }

    private void OnDisable()
    {
        _input.HumanoidLand.Move.performed -= SetMove;
        _input.HumanoidLand.Move.canceled -= SetMove;

        _input.HumanoidLand.Look.performed -= SetLook;
        _input.HumanoidLand.Look.canceled -= SetLook;

        _input.HumanoidLand.Jump.started -= SetJump;
        _input.HumanoidLand.Jump.canceled -= SetJump;

        _input.HumanoidLand.Interact.started -= SetInteract;
        _input.HumanoidLand.Interact.canceled -= SetInteract;

        _input.HumanoidLand.Aim.started -= SetAim;
        _input.HumanoidLand.Aim.canceled -= SetAim;

        _input.HumanoidLand.Run.started -= SetRun;
        _input.HumanoidLand.Run.canceled -= SetRun;

        _input.HumanoidLand.Disable();
    }

    private void SetMove(InputAction.CallbackContext _ctx) => MoveInput = _ctx.ReadValue<Vector2>();
    private void SetLook(InputAction.CallbackContext _ctx) => LookInput = _ctx.ReadValue<Vector2>();
    private void SetJump(InputAction.CallbackContext _ctx) => Jump = !Jump;
    private void SetInteract(InputAction.CallbackContext _ctx) => Interact = !Interact;
    private void SetAim(InputAction.CallbackContext _ctx) => Aim = !Aim;
    private void SetRun(InputAction.CallbackContext _ctx) => Run = !Run;
}
