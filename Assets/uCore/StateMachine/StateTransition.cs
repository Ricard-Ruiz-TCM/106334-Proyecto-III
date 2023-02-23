

public class StateTransition {

    public delegate bool TConditionDel();
    private TConditionDel _condition;

    public delegate void TTriggerDel();
    private TTriggerDel _trigger;

    private BasicState _destiny;
    public BasicState Next() { return _destiny; }

    public StateTransition(TConditionDel condition, TTriggerDel trigger = null) {
        _condition = condition;
        _trigger = trigger;
    }

    public StateTransition Destiny(BasicState destinty) {
        _destiny = destinty;
        return this;
    }

    public bool CheckTransition() { return _condition(); }
    public void OnTrigger() { _trigger?.Invoke(); }


}
