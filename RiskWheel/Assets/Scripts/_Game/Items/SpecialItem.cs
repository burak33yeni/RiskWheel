using Design.Patterns.ServiceLocator;

public class SpecialItem : ICollectable
{
    [Resolve] private SpinInventory _spinInventory;

    public void Collect(int amount)
    {
        _spinInventory.Add<SpecialNumberItem>(amount, "Special added");
        _spinInventory.Add<RewardMultiplierNumberItem>(1, "Reward multiplier added");
        this.SendEvent(new OnSpecialItemCollectedEvent(amount));

    }
}