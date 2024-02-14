using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupAnimation
{
    public void OpenPopupAnimation(PopupAnimationModel model)
    {
        model.FadePanel.color = new Color(0, 0, 0, 0);
        model.FadePanel.DOFade(0.95f, 0.5f);
        
        model.CanvasGroup.alpha = 0;
        model.CanvasGroup.DOFade(1f, 0.5f).OnComplete(() => model.OnComplete?.Invoke());
    }
    public void ClosePopupAnimation(PopupAnimationModel model)
    {
        model.FadePanel.color = new Color(0, 0, 0, 0.95f);
        model.FadePanel.DOFade(0f, 0.5f);
        
        model.CanvasGroup.alpha = 1;
        model.CanvasGroup.DOFade(0f, 0.5f).OnComplete(() => model.OnComplete?.Invoke());
    }
}

public class PopupAnimationModel
{
    public Image FadePanel;
    public CanvasGroup CanvasGroup;
    public Action OnComplete;
}
