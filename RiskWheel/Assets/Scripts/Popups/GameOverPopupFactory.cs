using Design.Patterns.Factory;
using Design.Patterns.ServiceLocator;
using UnityEngine;

public class GameOverPopupFactory : ObjectFactory<GameOverPopupView>
{
    [Resolve] private PopupHolderService _popupHolderService;
    
    public IGameOverPopupView Spawn()
    {
        GameOverPopupView view = Create(_popupHolderService.transform); 
        RectTransform rectTransform = view.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.localScale = Vector3.one;
        return view;
    }

    public GameOverPopupFactory(GameOverPopupView prefabObject) : base(prefabObject)
    {
    }
}
