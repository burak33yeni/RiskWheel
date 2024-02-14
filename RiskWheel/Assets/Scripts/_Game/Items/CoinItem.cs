using Design.Patterns.ServiceLocator;

public class CoinItem : ICollectable
{
    [Resolve] private SpinInventory _spinInventory;
    
    public void Collect(int amount)
    {
        _spinInventory.Add<CoinNumberItem>(amount, "Coin added");
        _spinInventory.Add<RewardMultiplierNumberItem>(1, "Reward multiplier added");
        this.SendEvent(new OnCoinItemCollectedEvent(amount));

    }
}
