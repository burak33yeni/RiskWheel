using Design.Patterns.ServiceLocator;

public class CaseCollectedPopupController  : BasePopupController<ICaseCollectedPopupView>, IInitializable
{
    public void Initialize()
    {
        this.FollowEvent<OnCaseItemCollectedEvent>(OnOpenEvent);
    }

    private void OnOpenEvent(OnCaseItemCollectedEvent payload)
    {
        _view = this.Resolve<CaseCollectedPopupFactory>().Spawn();
        (_view as CaseCollectedPopupView).SetAmount(payload.Amount);
        OpenPopup();
    }
}
