using Design.Patterns.ServiceLocator;

public class MoneyCollectedPopupController : BasePopupController<IMoneyCollectedPopupView>, IInitializable
{
    public void Initialize()
    {
        this.FollowEvent<OnMoneyItemCollectedEvent>(OnOpenEvent);
    }

    private void OnOpenEvent(OnMoneyItemCollectedEvent payload)
    {
        _view = this.Resolve<MoneyCollectedPopupFactory>().Spawn();
        (_view as MoneyCollectedPopupView).SetMoneyAmount(payload.Amount);
        OpenPopup();
    }
}
