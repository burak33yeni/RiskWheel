using System;
using UnityEngine;

namespace Design.Patterns.Inventory
{
    public abstract class Inventory
    {
        protected abstract string KeyPrefix { get; }
        
        public int Get<TInventoryItem>() where TInventoryItem : Item
        {
            Type type = typeof(TInventoryItem);
            TInventoryItem item = (TInventoryItem)Activator.CreateInstance(type);
            return PlayerPrefs.GetInt(KeyPrefix + "." + type.Name, item.minAmount);
        }

        public void Add<TInventoryItem>(int diff, string reason) where TInventoryItem : Item
        {
            Type type = typeof(TInventoryItem);
            TInventoryItem item = (TInventoryItem)Activator.CreateInstance(type);

            if (PlayerPrefs.HasKey(KeyPrefix + "." + type.Name) == false)
            {
                int newAmount = item.minAmount + diff;
                if (newAmount < item.minAmount)
                    newAmount = item.minAmount;
                PlayerPrefs.SetInt(KeyPrefix + "." + type.Name, newAmount);

            }
            else
                PlayerPrefs.SetInt(KeyPrefix + "." + type.Name, diff + PlayerPrefs.GetInt(KeyPrefix + "." + type.Name, item.minAmount));
        }
    }
}