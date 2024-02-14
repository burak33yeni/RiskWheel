using UnityEngine;

public class ScriptableObjectService : MonoBehaviour
{
    [SerializeField] private SpinSO _bronzeSpin, _silverSpin, _goldSpin;
    
    public SpinSO BronzeSpin
    {
        get => _bronzeSpin;
        private set { }
    }
    public SpinSO SilverSpin
    {
        get => _silverSpin;
        private set { }
    }
    public SpinSO GoldSpin
    {
        get => _goldSpin;
        private set { }
    }
}
