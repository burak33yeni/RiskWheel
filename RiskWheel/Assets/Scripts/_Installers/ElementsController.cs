using Design.Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

public class ElementsController : MonoBehaviour, IInitializable
{
    [Resolve] private SpinInventory _spinInventory;
    [SerializeField] private Button _exitButton;
    public void Initialize()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        this.FollowEvent<OnSpinningEvent>(OnSpinWheelSpinning);
    }
    
    private void OnExitButtonClick()
    {
        Application.Quit();
    }
    
    private void OnSpinWheelSpinning(OnSpinningEvent payload)
    {
        _exitButton.interactable = !payload.IsSpinning;
    }
}
