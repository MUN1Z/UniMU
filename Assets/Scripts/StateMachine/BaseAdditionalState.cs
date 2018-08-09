public abstract class BaseAdditionalState : IAdditionalState
{
    [Inject]
    public IStateMachine StateMachine { get; private set; }

    [Inject]
    public HardwareBackPressSignal HardwareBackPressSignal { get; private set; }

    protected bool OnTop
    {
        get
        {
            return StateMachine.LastState == this;
        }
    }

    public virtual void Load()
    {
        HardwareBackPressSignal.AddListener(OnHardwareBackPress);
    }

    public virtual void Unload()
    {
        HardwareBackPressSignal.RemoveListener(OnHardwareBackPress);
    }

    protected virtual void OnHardwareBackPress()
    {
        if (!OnTop)
            return;

        StateMachine.Unload(false);
    }
}