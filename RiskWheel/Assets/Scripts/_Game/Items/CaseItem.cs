using Design.Patterns.ServiceLocator;

public class CaseItem : ICollectable
{
    [Resolve] private SpinInventory _spinInventory;
    public void Collect(int amount)
    {
        _spinInventory.Add<CaseNumberItem>(amount, "Case added");
        _spinInventory.Add<RewardMultiplierNumberItem>(1, "Reward multiplier added");
        this.SendEvent(new OnCaseItemCollectedEvent(amount));

    }
}
