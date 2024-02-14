using DG.Tweening;
using UnityEngine;

public static class Animators
{
    public static void AnimateSpinWheel(SpinAnimationModel model)
    {
        var rotation = new Vector3(0, 0, model.Degree);
        model.RotatingAreaTransform.DOLocalRotate(rotation, model.Duration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.OutQuart)
            .OnComplete(() => model.OnComplete?.Invoke());
    }
}