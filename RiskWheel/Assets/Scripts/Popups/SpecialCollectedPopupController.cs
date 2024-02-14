using Design.Patterns.ServiceLocator;

public class SpecialCollectedPopupController : BasePopupController<ISpecialCollectedPopupView>, IInitializable
{
    public void Initialize()
    {
        this.FollowEvent<OnSpecialItemCollectedEvent>(OnOpenEvent);
    }

    private void OnOpenEvent(OnSpecialItemCollectedEvent payload)
    {
        _view = this.Resolve<SpecialCollectedPopupFactory>().Spawn();
        (_view as SpecialCollectedPopupView).SetAmount(payload.Amount);
        OpenPopup();
    }
}
