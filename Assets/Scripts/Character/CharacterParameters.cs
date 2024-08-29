using UnityEngine;

[CreateAssetMenu(fileName = "Character Parameters", menuName = "Scriptable Objects/Character/Parameters")]
public class CharacterParameters : ScriptableObject
{
    // Move
    [field: Header("Move")]
    [field: Tooltip("Walk speed of the character")]
    [field: SerializeField] public float WalkSpeed { get; private set; }

    [field: Tooltip("Run speed of the character")]
    [field: SerializeField] public float RunSpeed { get; private set; }

    [field: Tooltip("How fast the character turns to face movement direction")]
    [field: SerializeField] public float TurnSmoothTime { get; private set; }

    [field: Tooltip("The height the player can jump")]
    [field: SerializeField] public float JumpHeight { get; private set; }

    // Animation
    [field: Header("Animation")]
    [field: Tooltip("Animation blend speed value")]
    [field: SerializeField] public float BlendSpeed { get; private set; }

    [field: Tooltip("Animation blend threshold value to stop blending")]
    [field: SerializeField] public float BlendThreshold { get; private set; }

    [field: Tooltip("Jump Animation ground check threshold value. Depends on jump animation")]
    [field: SerializeField] public float JumpGroundCheckThreshold { get; private set; }

    [field: Tooltip("Idle, Walk, Run Blend Tree Speed Parameter Name")]
    [field: SerializeField] public string SpeedParameterName { get; private set; }

    [field: Tooltip("Animator Jump Parameter Name")]
    [field: SerializeField] public string JumpParameterName { get; private set; }

    [field: Tooltip("Animator Fall Parameter Name")]
    [field: SerializeField] public string FallParameterName { get; private set; }

    // Aim
    [field: Header("Aim")]
    [field: Tooltip("Look Sensitivity")]
    [field: SerializeField] public float Sensitivity { get; private set; }
    [field: Tooltip("Aim Sensitivity")]
    [field: SerializeField] public float AimSensitivity { get; private set; }

    [field: Tooltip("Rotate Speed When Aiming")]
    [field: SerializeField] public float AimingRotateSpeed { get; private set; }

    [field: Tooltip("Aim Layer")]
    [field: SerializeField] public LayerMask AimLayers { get; private set; }

    // GroundCheck
    [field: Header("Ground Check")]
    [field: Tooltip("The radius of the grounded check")]
    [field: SerializeField] public float GroundedRadius { get; private set; }

    [field: Tooltip("Offset for rough ground")]
    [field: SerializeField] public float GroundedOffset { get; private set; }

    [field: Tooltip("Offset for rough ground")]
    [field: SerializeField] public float Gravity { get; private set; }

    [field: Tooltip("Ground Layer")]
    [field: SerializeField] public LayerMask GroundLayers { get; private set; }

    //Interaction
    [field: Header("Interaction Distance")]
    [field: Tooltip("Distance for player to interact objects")]
    [field: SerializeField] public float InteractionDistance { get; private set; }
    [field: Tooltip("Interactable Object Layers")]
    [field: SerializeField] public LayerMask InteractionLayer { get; private set; }
}
