using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [field: SerializeField] public StateBase CurrentState { get; private set; } = null;

    private void Start()
    {
        CurrentState = InitState();

        if (CurrentState != null)
            CurrentState.StateEnter();
    }

    public virtual void Update()
    {
        if (CurrentState != null)
            CurrentState.UpdateLogic();
    }

    public virtual void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.UpdatePhysics();
    }

    public virtual void LateUpdate()
    {
        if (CurrentState != null)
            CurrentState.LateUpdate();
    }

    public void ChangeState(StateBase _nextState)
    {
        CurrentState.StateExit();
        CurrentState = _nextState;
        CurrentState.StateEnter();
    }

    public abstract StateBase InitState();

}
