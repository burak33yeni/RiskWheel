using System.Collections;
using System.Collections.Generic;
using Design.Patterns.Factory;
using Design.Patterns.ServiceLocator;
using UnityEngine;

public class SpecialCollectedPopupFactory : ObjectFactory<SpecialCollectedPopupView>
{
    [Resolve] private PopupHolderService _popupHolderService;
    
    public ISpecialCollectedPopupView Spawn()
    {
        SpecialCollectedPopupView view = Create(_popupHolderService.transform); 
        RectTransform rectTransform = view.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.localScale = Vector3.one;
        return view;
    }
    
    public SpecialCollectedPopupFactory(SpecialCollectedPopupView prefabObject) : base(prefabObject)
    {
    }
}