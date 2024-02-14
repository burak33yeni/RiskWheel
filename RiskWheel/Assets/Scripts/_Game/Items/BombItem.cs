using Design.Patterns.ServiceLocator;
using UnityEngine;

public class BombItem : ICollectable
{
    [Resolve] private SpinInventory _spinInventory;
    
    public  void Collect(int amount)
    {
        PlayerPrefs.DeleteAll();
        this.SendEvent(new OnBombItemCollectedEvent());
    }
}