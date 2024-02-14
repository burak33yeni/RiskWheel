using System;
using UnityEngine;

public class TextureService : MonoBehaviour
{
    [SerializeField] private SpinItemConfig[] _spinItemConfigs;

    public Sprite GetSpinItemSprite(SpinItemType type)
    {
        foreach (var config in _spinItemConfigs)
        {
            if (config.Type == type)
            {
                return config.Sprite;
            }
        }

        return null;
    }
}

[Serializable]
public class SpinItemConfig
{
    public SpinItemType Type;
    public Sprite Sprite;
}