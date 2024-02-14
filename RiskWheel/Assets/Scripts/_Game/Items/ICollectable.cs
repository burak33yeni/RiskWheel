using UnityEngine;

public interface ICollectable
{
    void Collect(int amount);
}

public class CollectableModel
{
    public Sprite Sprite;
    public string Amount;
}