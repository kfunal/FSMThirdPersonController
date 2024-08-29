using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [Header("Common Parameters")]
    [SerializeField] private bool useEvents;
    [SerializeField] private string promptMessage;
    [Space(15), SerializeField] private UnityEvent onInteract;
    [Space(15), SerializeField] private UnityEvent onInteractEnd;

    public string PromptMessage => promptMessage;

    public void Interact()
    {
        if (useEvents)
            onInteract?.Invoke();
        else
            OnInteract();
    }

    public void EndInteract()
    {
        if (useEvents)
            onInteractEnd?.Invoke();
        else
            OnInteractEnd();
    }

    protected abstract void OnInteract();
    protected abstract void OnInteractEnd();
}
