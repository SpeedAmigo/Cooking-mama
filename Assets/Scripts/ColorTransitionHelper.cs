using System.Collections;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class ColorTransitionHelper
{
    public void ColorTransition(DOGetter<Color> getter, DOSetter<Color> setter, Color toColor, float duration)
    {
        DOTween.To(getter, setter, toColor, duration).SetEase(Ease.OutCubic);
    }
}
