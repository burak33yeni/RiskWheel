using UnityEngine;
using UnityEngine.UI;

public class GameOverPopupView : BasePopupView, IGameOverPopupView
{
    [SerializeField] private Button giveUpButton;

    protected override void Initialize()
    {
        giveUpButton.onClick.AddListener(OnGiveUpButtonClicked);
    }

    private void OnGiveUpButtonClicked()
    {
        CloseView();
    }
}

public interface IGameOverPopupView
{
    
}