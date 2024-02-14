using Design.Patterns.ServiceLocator;

public class GameOverPopupController : BasePopupController<IGameOverPopupView>, IInitializable
{

    public void Initialize()
    {
        this.FollowEvent<OnBombItemCollectedEvent>(OnOpenEvent);
    }

    private void OnOpenEvent()
    {
        _view = this.Resolve<GameOverPopupFactory>().Spawn();
        OpenPopup();
    }
}
