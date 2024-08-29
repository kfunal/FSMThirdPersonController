using UnityEngine;

public class Door : Interactable
{
    [Header("Door Parameters")]
    [SerializeField] private string animationParameterName;
    [SerializeField] private Animator animator;

    protected override void OnInteract()
    {
        if (animator.GetBool(animationParameterName)) return;

        animator.SetBool(animationParameterName, true);
    }

    protected override void OnInteractEnd()
    {
        if (!animator.GetBool(animationParameterName)) return;
        animator.SetBool(animationParameterName, false);
    }
}
