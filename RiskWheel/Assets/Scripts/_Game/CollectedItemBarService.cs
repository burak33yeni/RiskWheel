using Design.Patterns.ServiceLocator;
using TMPro;
using UnityEngine;

public class CollectedItemBarService : MonoBehaviour, IInitializable
{
    [SerializeField] private TextMeshProUGUI[] collectedItemTexts;
    [Resolve] private SpinInventory _spinInventory;

    public void Initialize()
    {
        this.FollowEvent<SpinWheelResetEvent>(OnSpeenWheelResetEvent);
        OnSpeenWheelResetEvent();
    }
    
    private void OnSpeenWheelResetEvent()
    {
        collectedItemTexts[0].text = _spinInventory.Get<CoinNumberItem>().ToString();
        collectedItemTexts[1].text = _spinInventory.Get<MoneyNumberItem>().ToString();
        collectedItemTexts[2].text = _spinInventory.Get<CaseNumberItem>().ToString();
        collectedItemTexts[3].text = _spinInventory.Get<SpecialNumberItem>().ToString();

    }
}
