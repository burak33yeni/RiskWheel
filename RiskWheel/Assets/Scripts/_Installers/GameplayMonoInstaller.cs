using Design.Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayMonoInstaller : MonoInstaller
{
    [FormerlySerializedAs("gameController")] [SerializeField] private ElementsController elementsController;
    [SerializeField] private SpinView _spinView;
    [SerializeField] private ScriptableObjectService _scriptableObjectService;
    [SerializeField] private TextureService _textureService;
    [SerializeField] private PopupHolderService _popupHolderService;
    
    [SerializeField] private GameOverPopupView _gameOverPopupViewPrefab;
    [SerializeField] private CoinCollectedPopupView _coinCollectedPopupViewPrefab;
    [SerializeField] private MoneyCollectedPopupView _moneyCollectedPopupViewPrefab;
    [SerializeField] private CaseCollectedPopupView _caseCollectedPopupViewPrefab;
    [SerializeField] private SpecialCollectedPopupView _specialCollectedPopupViewPrefab;
    [SerializeField] private CollectedItemBarService _collectedItemBarService;

    public override void Install(Context context)
    {
        context.Register<ElementsController>().NonLazy().FromInstance(elementsController);
        context.Register<SpinController>().FromNew().NonLazy().WithArguments(_spinView);
        context.Register<ScriptableObjectService>().NonLazy().FromInstance(_scriptableObjectService);
        context.Register<TextureService>().NonLazy().FromInstance(_textureService);
        context.Register<PopupHolderService>().NonLazy().FromInstance(_popupHolderService);
        context.Register<CollectedItemBarService>().NonLazy().FromInstance(_collectedItemBarService);
        
        RegisterPopups(context);

        context.Register<SpinInventory>().FromNew();
        
        RegisterEvents(context);
        RegisterItems(context);

        context.Register<SpinControllerHelper>().FromNew().NonLazy();
    }

    private void RegisterPopups(Context context)
    {
        context.Register<GameOverPopupController>().FromNew().NonLazy();
        context.Register<GameOverPopupFactory>().FromNew().NonLazy().WithArguments(_gameOverPopupViewPrefab);
        
        context.Register<CoinCollectedPopupController>().FromNew().NonLazy();
        context.Register<CoinCollectedPopupFactory>().FromNew().NonLazy().WithArguments(_coinCollectedPopupViewPrefab);
        
        context.Register<MoneyCollectedPopupController>().FromNew().NonLazy();
        context.Register<MoneyCollectedPopupFactory>().FromNew().NonLazy().WithArguments(_moneyCollectedPopupViewPrefab);
        
        context.Register<CaseCollectedPopupController>().FromNew().NonLazy();
        context.Register<CaseCollectedPopupFactory>().FromNew().NonLazy().WithArguments(_caseCollectedPopupViewPrefab);
        
        context.Register<SpecialCollectedPopupController>().FromNew().NonLazy();
        context.Register<SpecialCollectedPopupFactory>().FromNew().NonLazy().WithArguments(_specialCollectedPopupViewPrefab);
    }

    private static void RegisterItems(Context context)
    {
        context.Register<BombItem>().NonLazy().FromNew();
        context.Register<CoinItem>().NonLazy().FromNew();
        context.Register<MoneyItem>().NonLazy().FromNew();
        context.Register<CaseItem>().NonLazy().FromNew();
        context.Register<SpecialItem>().NonLazy().FromNew();
    }

    private static void RegisterEvents(Context context)
    {
        context.RegisterEvent<OnBombItemCollectedEvent>();
        context.RegisterEvent<OnCoinItemCollectedEvent>();
        context.RegisterEvent<OnMoneyItemCollectedEvent>();
        context.RegisterEvent<OnCaseItemCollectedEvent>();
        context.RegisterEvent<OnSpecialItemCollectedEvent>();
        context.RegisterEvent<SpinWheelResetEvent>();
        context.RegisterEvent<OnSpinningEvent>();
    }
}
