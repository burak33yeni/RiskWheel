using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinView : MonoBehaviour, ISpinView
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _rewardAmountText;
    [SerializeField] private Image _indicatorImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Button _spinButton;
    [SerializeField] private Transform _rotatingAreaTransform;
    [SerializeField] private SpinItemView[] _spinItemViews;
    
    # region Runtime Editor content of slice change
    [Header("Content of slice change")]
    [SerializeField] private SpinItemType editorSpinItemType;
    [SerializeField] private int editorSpinItemAmount;
    [SerializeField] private bool editorSpinItemAmountChange;
    [SerializeField] private int editorSpinItemIndex;
    private SpinController _spinController;
    private TextureService _textureService;
    
#if UNITY_EDITOR
    void Update()
    {
        if (editorSpinItemAmountChange)
        {
            editorSpinItemAmountChange = false;
            _spinController.ChangeSpinItemFromEditor(editorSpinItemType, editorSpinItemAmount, editorSpinItemIndex);
            _spinItemViews[editorSpinItemIndex].SetView(new CollectableModel()
            {
                Amount = editorSpinItemAmount.ToString(),
                Sprite = _textureService.GetSpinItemSprite(editorSpinItemType)
            });
        }
    }
#endif

    #endregion
    private bool _isInitialized;
    public void SetView(SpinModel model)
    {
        _rotatingAreaTransform.localRotation = Quaternion.Euler(Vector3.zero);
        #if UNITY_EDITOR
        _spinController = model.SpinController;
        _textureService = model.TextureService;
        #endif
        SetWheel(model);
        SetItems(model);
    }
    
    private void SetItems(SpinModel model)
    {
        string amount = "";
        for (int i = 0; i < _spinItemViews.Length; i++)
        {
            if (model.Items[i] != SpinItemType.Bomb)
                amount = model.Amounts[i].ToString();
            
            _spinItemViews[i].SetView(new CollectableModel()
            {
                Amount = amount,
                Sprite = model.TextureService.GetSpinItemSprite(model.Items[i])
            });
        }
    }

    private void SetWheel(SpinModel model)
    {
        _nameText.SetText(model.Name);
        _rewardAmountText.SetText("Up to x" + model.RewardMultiplier + " rewards");
        _indicatorImage.sprite = model.Indicator;
        _backgroundImage.sprite = model.Background;
        
        if (!_isInitialized)
        {
            _isInitialized = true;
            _spinButton.onClick.AddListener(() => model.OnSpinButtonClicked?.Invoke());
        }
    }

    public void AnimateSpin(SpinAnimationModel model)
    {
        SetButtonInteractable(model);
        model.RotatingAreaTransform = _rotatingAreaTransform;
        Animators.AnimateSpinWheel(model);
    }

    private void SetButtonInteractable(SpinAnimationModel model)
    {
        _spinButton.interactable = false;
        model.Duration = 3f;
        StartCoroutine(SpinCompletedCoroutine(duration: model.Duration));
    }

    private IEnumerator SpinCompletedCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _spinButton.interactable = true;
    }
    
}

public interface ISpinView
{
    void SetView(SpinModel model);
    void AnimateSpin(SpinAnimationModel model);
}