using System.Collections;
using TMPro;
using UnityEngine;

public class CaseCollectedPopupView : BasePopupView, ICaseCollectedPopupView
{
    [SerializeField] private TextMeshProUGUI amountText;
    
    protected override void Initialize()
    {
        StartCoroutine(CloseCoroutine());
    }
    
    private IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CloseView();
    }
    
    public void SetAmount(int amount)
    {
        amountText.SetText(amount + " more knife gained!");
    }
}

public interface ICaseCollectedPopupView
{
    
}