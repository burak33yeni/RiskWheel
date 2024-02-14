using Design.Patterns.ServiceLocator;

public class MoneyItem : ICollectable
{
    [Resolve] private SpinInventory _spinInventory;
    
    public void Collect(int amount)
    {
        _spinInventory.Add<MoneyNumberItem>(amount, "Money added");
        _spinInventory.Add<RewardMultiplierNumberItem>(1, "Reward multiplier added");
        this.SendEvent(new OnMoneyItemCollectedEvent(amount));

    }
}