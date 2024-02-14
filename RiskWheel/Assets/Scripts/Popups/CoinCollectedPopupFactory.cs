using Design.Patterns.Factory;
using Design.Patterns.ServiceLocator;
using UnityEngine;

public class CoinCollectedPopupFactory : ObjectFactory<CoinCollectedPopupView>
{
    [Resolve] private PopupHolderService _popupHolderService;
    
    public ICoinCollectedPopupView Spawn()
    {
        CoinCollectedPopupView view = Create(_popupHolderService.transform);
        RectTransform rectTransform = view.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.localScale = Vector3.one;
        return view;
    }
    
    public CoinCollectedPopupFactory(CoinCollectedPopupView prefabObject) : base(prefabObject)
    {
    }
}
