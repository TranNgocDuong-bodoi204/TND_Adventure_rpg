using UnityEngine;

public class StateMachine
{
    public EntityState currentState;
    public EntityState previousState;
    public void UpdateActiveState() => currentState.UpdateState();
    public void FixedUpdateActiveState() => currentState.FixUpdateState();
    public void SetInitialState(EntityState initialState)
    {
        currentState = initialState;
        currentState.EnterState();
    }
    public void ChangeState(EntityState newstate)
    {
        if (newstate == currentState)
            return;
            
        previousState = currentState;
        if (currentState != null)
            currentState.ExitState();

        currentState = newstate;

        if (currentState != null)
            currentState.EnterState();
    }
    public void ChangeState(EntityState state, bool forceChange)
    {
        if(!forceChange) return;
        state.ExitState();
        state.EnterState();
    }
}
