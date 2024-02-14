using Design.Patterns.Inventory;

public class SpinInventory : Inventory
{
    protected override string KeyPrefix => "SpinInventory";
}

public class PlayedSpinNumberItem : Item
{
    protected override string key => "PlayedSpinNumber";
}

public class RewardMultiplierNumberItem : Item
{
    protected override string key => "RewardMultiplierNumber";
    protected override int minAmount => 1;
}

public class CoinNumberItem : Item
{
    protected override string key => "CoinItem";
}

public class MoneyNumberItem : Item
{
    protected override string key => "MoneyItem";
}

public class CaseNumberItem : Item
{
    protected override string key => "CaseItem";
}

public class SpecialNumberItem : Item
{
    protected override string key => "SpecialItem";
}