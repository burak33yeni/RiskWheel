using System.Collections;
using TMPro;
using UnityEngine;

public class SpecialCollectedPopupView : BasePopupView, ISpecialCollectedPopupView
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
        amountText.SetText(amount + " more weapon gained!");
    }
}

public interface ISpecialCollectedPopupView
{
    
}