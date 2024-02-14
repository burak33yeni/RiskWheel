using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePopupView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _fadePanel;

    protected abstract void Initialize();
    
    public void OpenView(Action onCompleteOpeningCallback = null)
    {
        Initialize();
        OnStartedOpening(onCompleteOpeningCallback);
    }

    protected void CloseView()
    {
        OnStartedClosing();
    }
    
    private void OnStartedOpening(Action onCompleteOpeningCallback = null)
    {
        new PopupAnimation().OpenPopupAnimation(new PopupAnimationModel
        {
            FadePanel = _fadePanel,
            CanvasGroup = _canvasGroup,
            OnComplete = onCompleteOpeningCallback
        });
    }
    
    private void OnStartedClosing()
    {
        Action onCompleteOpeningCallback = () => Destroy(gameObject);
        new PopupAnimation().ClosePopupAnimation(new PopupAnimationModel
        {
            FadePanel = _fadePanel,
            CanvasGroup = _canvasGroup,
            OnComplete = onCompleteOpeningCallback
        });
    }
    
}
