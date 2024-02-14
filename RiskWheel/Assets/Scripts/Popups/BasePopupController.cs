using System;

public abstract class BasePopupController<TView> where TView : class
{
    private BasePopupView view => _view as BasePopupView;
    protected TView _view;
    protected Action onCompleteOpeningCallback;
    
    protected void OpenPopup()
    {
        view.OpenView(onCompleteOpeningCallback);
    }
}
