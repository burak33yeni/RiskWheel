using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinItemView : MonoBehaviour
{
    [SerializeField] protected Image _image;
    [SerializeField] protected TextMeshProUGUI _amountText;

    public void SetView(CollectableModel model)
    {
        _image.sprite = model.Sprite;
        _amountText.SetText("x" + model.Amount);
    }
}
