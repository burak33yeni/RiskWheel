using System;

public abstract class BaseSpinItemFiller
{
    protected SpinItemType[] GetItems(SpinModel spinModel, SpinInventory spinInventory, int minAmount, int maxAmount)
    {
        SpinItemType[] items = new SpinItemType[8];
        int rewardMultiplier = spinInventory.Get<RewardMultiplierNumberItem>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = (SpinItemType) RandomService.GetInt(0, Enum.GetValues(typeof(SpinItemType)).Length - 2);
            spinModel.Amounts[i] = RandomService.GetInt(minAmount, maxAmount + 1) * rewardMultiplier;
        }
        return items;
    }
}
public class BronzeSpinItemFiller : BaseSpinItemFiller, IItemFiller
{
    public void Fill(SpinModel spinModel, SpinInventory spinInventory, int minAmount, int maxAmount)
    {
        SpinItemType[] items = GetItems(spinModel, spinInventory, minAmount, maxAmount);
        items[RandomService.GetInt(0, items.Length)] = SpinItemType.Bomb;
        spinModel.Items = items;
    }
}

public class SilverSpinItemFiller : BaseSpinItemFiller, IItemFiller
{
    public void Fill(SpinModel spinModel, SpinInventory spinInventory, int minAmount, int maxAmount)
    {
        SpinItemType[] items = GetItems(spinModel, spinInventory, minAmount, maxAmount);
        spinModel.Items = items;
    }
}

public class GoldSpinItemFiller : BaseSpinItemFiller, IItemFiller
{
    public void Fill(SpinModel spinModel, SpinInventory spinInventory, int minAmount, int maxAmount)
    {
        SpinItemType[] items = new SpinItemType[8];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = SpinItemType.Special;
            spinModel.Amounts[i] = RandomService.GetInt(minAmount, maxAmount + 1);
        }
        spinModel.Items = items;
    }
}

public interface IItemFiller
{
    void Fill(SpinModel spinModel, SpinInventory spinInventory, int minAmount, int maxAmount);
}