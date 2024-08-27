public class StateBase
{
    protected string Name;
    protected StateMachine StateMachine;

    public StateBase(string _name, StateMachine _stateMachine)
    {
        Name = _name;
        StateMachine = _stateMachine;
    }

    public virtual void StateEnter() { }
    public virtual void StateExit() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void LateUpdate() { }
}
