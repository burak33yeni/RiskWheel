using Design.Patterns.Factory;
using Design.Patterns.ServiceLocator;
using UnityEngine;

public class MoneyCollectedPopupFactory : ObjectFactory<MoneyCollectedPopupView>
{
    [Resolve] private PopupHolderService _popupHolderService;
    
    public IMoneyCollectedPopupView Spawn()
    {
        MoneyCollectedPopupView view = Create(_popupHolderService.transform); 
        RectTransform rectTransform = view.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.localScale = Vector3.one;
        return view;
    }
    
    public MoneyCollectedPopupFactory(MoneyCollectedPopupView prefabObject) : base(prefabObject)
    {
    }
}
