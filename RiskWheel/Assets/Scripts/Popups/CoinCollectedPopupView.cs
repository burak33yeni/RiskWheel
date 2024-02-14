using System.Collections;
using TMPro;
using UnityEngine;

public class CoinCollectedPopupView : BasePopupView, ICoinCollectedPopupView
{
    [SerializeField] private TextMeshProUGUI coinAmountText;
    
    protected override void Initialize()
    {
        StartCoroutine(CloseCoroutine());
    }
    
    private IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        CloseView();
    }
    
    public void SetCoinAmount(int amount)
    {
        coinAmountText.SetText(amount + " more coins gained!");
    }
}

public interface ICoinCollectedPopupView
{
    
}