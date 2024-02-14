using System;
using Design.Patterns.ServiceLocator;
using UnityEngine;
using Event = Design.Patterns.ServiceLocator.Event;
using Random = UnityEngine.Random;

public class SpinController : IInitializable
{
    private const int SILVER_SPIN_INTERVAL = 5;
    private const int GOLD_SPIN_INTERVAL = 30;
    
    private const int MIN_SPIN_ROTATION = 3;
    private const int MAX_SPIN_ROTATION = 6;
    
    private const float MIN_SPIN_DURATION = 2.1f;
    private const float MAX_SPIN_DURATION = 5.5f;
    
    private const int MIN_REWARD_AMOUNT = 1;
    private const int MAX_REWARD_AMOUNT = 5;
    
    
    [Resolve] private SpinInventory _spinInventory;
    [Resolve] private ScriptableObjectService _scriptableObjectService;
    [Resolve] private TextureService _textureService;
    
    private ISpinView _spinView;
    private int _selectedItemIndex;
    private SpinType _spinType;
    private IItemFiller _spinItemFiller;
    
    private ICollectable[] _items;
    private int[] _amounts;
    
    public SpinController(ISpinView spinView)
    {
        _spinView = spinView;
    }
    public void Initialize()
    {
        this.FollowEvent<SpinWheelResetEvent>(CreateSpinWheel);
        CreateSpinWheel();
    }

    private void CreateSpinWheel()
    {
        SetSpinType();
        SetSpinItemFiller();
        SpinModel spinModel = SetSpinModel();
        SetView(spinModel);
    }

    private void SetSpinItemFiller()
    {
        switch (_spinType)
        {
            case SpinType.Bronze:
                _spinItemFiller = new BronzeSpinItemFiller();
                break;
            case SpinType.Silver:
                _spinItemFiller = new SilverSpinItemFiller();
                break;
            case SpinType.Gold:
                _spinItemFiller = new GoldSpinItemFiller();
                break;
        }
    }
    private void SetSpinType()
    {
        int playedSpinNumber = _spinInventory.Get<PlayedSpinNumberItem>();
        if (playedSpinNumber == 0)
        {
            _spinType = SpinType.Bronze;
            return;
        }

        if (playedSpinNumber % GOLD_SPIN_INTERVAL == 0)
            _spinType = SpinType.Gold;
        else if (playedSpinNumber % SILVER_SPIN_INTERVAL == 0)
            _spinType = SpinType.Silver;
        else
            _spinType = SpinType.Bronze;
    }

    private void SetView(SpinModel spinModel)
    {
        spinModel.OnSpinButtonClicked = OnSpinButtonCLickedClicked;
        _spinView.SetView(spinModel);
    }
    
    private void OnSpinButtonCLickedClicked()
    {
        this.SendEvent(new OnSpinningEvent(true));
        _spinInventory.Add<PlayedSpinNumberItem>(1, "Spin completed");
        float degree = Random.Range(360 * MIN_SPIN_ROTATION, 360 * MAX_SPIN_ROTATION + 1);
        _selectedItemIndex = this.Resolve<SpinControllerHelper>().GetSpinSelectedItemIndex(degree);
        Animate(degree);
    }

    private void Animate(float degree)
    {
        _spinView.AnimateSpin(new SpinAnimationModel()
        {
            Degree = degree,
            Duration = Random.Range(MIN_SPIN_DURATION, MAX_SPIN_DURATION),
            OnComplete = OnSpinAnimationComplete
        });
    }

    private void OnSpinAnimationComplete()
    {
        this.SendEvent(new OnSpinningEvent(false));
        _items[_selectedItemIndex].Collect(_amounts[_selectedItemIndex]);
        this.SendEvent(new SpinWheelResetEvent());
    }

    private SpinModel SetSpinModel()
    {
        SpinModel spinModel = new SpinModel();
        SetSpinWheelModel(spinModel);
        SetSpinItemsModel(spinModel);
        spinModel.RewardMultiplier = _spinInventory.Get<RewardMultiplierNumberItem>();
#if UNITY_EDITOR
        spinModel.SpinController = this;
#endif
        return spinModel;
    }

    private void SetSpinItemsModel(SpinModel spinModel)
    {
        spinModel.Amounts = new int[8];
        spinModel.TextureService = _textureService;
        _spinItemFiller.Fill(spinModel, _spinInventory, MIN_REWARD_AMOUNT, MAX_REWARD_AMOUNT);
        _items = this.Resolve<SpinControllerHelper>().GetSpinItems(spinModel.Items);
        _amounts = spinModel.Amounts;
    }
    
    private void SetSpinWheelModel(SpinModel spinModel)
    {
        switch (_spinType)
        {
            case SpinType.Bronze:
                SetBronzeSpinModel(spinModel);
                break;
            case SpinType.Silver:
                SetSilverSpinModel(spinModel);
                break;
            case SpinType.Gold:
                SetGoldSpinModel(spinModel);
                break;
        }
    }

    private void SetBronzeSpinModel(SpinModel spinModel)
    {
        spinModel.Background = _scriptableObjectService.BronzeSpin.background;
        spinModel.Indicator = _scriptableObjectService.BronzeSpin.indicator;
        spinModel.Name = "Bronze Spin";
    }

    private void SetSilverSpinModel(SpinModel spinModel)
    {
        spinModel.Background = _scriptableObjectService.SilverSpin.background;
        spinModel.Indicator = _scriptableObjectService.SilverSpin.indicator;
        spinModel.Name = "Silver Spin";
    }

    private void SetGoldSpinModel(SpinModel spinModel)
    {
        spinModel.Background = _scriptableObjectService.GoldSpin.background;
        spinModel.Indicator = _scriptableObjectService.GoldSpin.indicator;
        spinModel.Name = "Gold Spin";
    }
    
    public void ChangeSpinItemFromEditor(SpinItemType spinItemType, int spinItemAmount, int spinItemIndex)
    {
        _items[spinItemIndex] = this.Resolve<SpinControllerHelper>().GetSpinItem(spinItemType);
        _amounts[spinItemIndex] = spinItemAmount;
    }
}

public class SpinModel
{
    public string Name;
    public int RewardMultiplier;
    public Sprite Indicator;
    public Sprite Background;
    public Action OnSpinButtonClicked;
    
    public SpinItemType[] Items;
    public int[] Amounts;
    
    public TextureService TextureService;
    public SpinController SpinController;
}

public class SpinAnimationModel
{
    public float Degree;
    public float Duration;
    public Action OnComplete;
    public Transform RotatingAreaTransform;
}

public class SpinWheelResetEvent : Event
{
}

public class OnSpinningEvent : Event
{
    public bool IsSpinning;

    public OnSpinningEvent(bool isSpinning)
    {
        IsSpinning = isSpinning;
    }
}

public enum SpinType
{
    Bronze,
    Silver,
    Gold
}