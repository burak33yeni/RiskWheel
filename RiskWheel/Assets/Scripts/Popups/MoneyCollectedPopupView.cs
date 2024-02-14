using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyCollectedPopupView : BasePopupView, IMoneyCollectedPopupView
{
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    
    protected override void Initialize()
    {
        StartCoroutine(CloseCoroutine());
    }
    
    private IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CloseView();
    }
    
    public void SetMoneyAmount(int amount)
    {
        moneyAmountText.SetText(amount + " more money gained!");
    }   
}

public interface IMoneyCollectedPopupView
{
    
}