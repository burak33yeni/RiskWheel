using Design.Patterns.ServiceLocator;

public class CoinCollectedPopupController : BasePopupController<ICoinCollectedPopupView>, IInitializable
{
    public void Initialize()
    {
        this.FollowEvent<OnCoinItemCollectedEvent>(OnOpenEvent);
    }

    private void OnOpenEvent(OnCoinItemCollectedEvent payload)
    {
        _view = this.Resolve<CoinCollectedPopupFactory>().Spawn();
        (_view as CoinCollectedPopupView).SetCoinAmount(payload.Amount);
        OpenPopup();
    }
}
