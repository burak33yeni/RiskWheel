using UnityEngine;using Event = Design.Patterns.ServiceLocator.Event;

public class SpinControllerHelper
{
    public int GetSpinSelectedItemIndex(float degree)
    {
        float realDegree = degree % 360;
        if (realDegree > 180)
            realDegree -= 360;
        switch (realDegree)
        {
            case >= -180f and < -157.5f:
                return 4;
            case >= -157.5f and < -112.5f:
                return 5;
            case >= -112.5f and < -67.5f:
                return 6;
            case >= -67.5f and < -22.5f:
                return 7;
            case >= -22.5f and < 22.5f:
                return 0;
            case >= 22.5f and < 67.5f:
                return 1;
            case >= 67.5f and < 112.5f:
                return 2;
            case >= 112.5f and < 157.5f:
                return 3;
            case >= 157.5f and < 180f:
                return 4;
            default:
                return -1;
        }
        
    }
    
    public ICollectable[] GetSpinItems(SpinItemType[] spinItemType)
    {
        ICollectable[] items = new ICollectable[spinItemType.Length];
        for (int i = 0; i < spinItemType.Length; i++)
        {
            switch (spinItemType[i])
            {
                case SpinItemType.Coin:
                    items[i] = this.Resolve<CoinItem>();
                    break;
                case SpinItemType.Money:
                    items[i] = this.Resolve<MoneyItem>();
                    break;
                case SpinItemType.Case:
                    items[i] = this.Resolve<CaseItem>();
                    break;
                case SpinItemType.Bomb:
                    items[i] = this.Resolve<BombItem>();
                    break;
                case SpinItemType.Special:
                    items[i] = this.Resolve<SpecialItem>();
                    break;
                default:
                    return null;
            }
        }

        return items;
    }

    public ICollectable GetSpinItem(SpinItemType spinItemType)
    {
        switch (spinItemType)
        {
            case SpinItemType.Coin:
                return this.Resolve<CoinItem>();
            case SpinItemType.Money:
                return this.Resolve<MoneyItem>();
            case SpinItemType.Case:
                return this.Resolve<CaseItem>();
            case SpinItemType.Bomb:
                return this.Resolve<BombItem>();
            case SpinItemType.Special:
                return this.Resolve<SpecialItem>();
            default:
                return null;
        }
    }
}

public class OnBombItemCollectedEvent : Event
{
}

public class OnCoinItemCollectedEvent : Event
{
    public int Amount;
    
    public OnCoinItemCollectedEvent(int amount)
    {
        Amount = amount;
    }
}

public class OnMoneyItemCollectedEvent : Event
{
    public int Amount;
    
    public OnMoneyItemCollectedEvent(int amount)
    {
        Amount = amount;
    }
}

public class OnCaseItemCollectedEvent : Event
{
    public int Amount;
    
    public OnCaseItemCollectedEvent(int amount)
    {
        Amount = amount;
    }
}

public class OnSpecialItemCollectedEvent : Event
{
    public int Amount;
    
    public OnSpecialItemCollectedEvent(int amount)
    {
        Amount = amount;
    }
}

public enum SpinItemType
{
    Coin,
    Money,
    Case,
    Bomb,
    Special
}