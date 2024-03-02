using DG.Tweening;
using UnityEngine;

public class SwordSizeManager : AUpgradable
{
    [SerializeField] private Transform swordScaleTransform;
    [SerializeField] private Vector3 startScale;
    [SerializeField] private Vector3 scalePerUpgrade;

    [SerializeField] private float sizeIncreasingEffectForce;
    [SerializeField] private float sizeIncreasingTime;
    [SerializeField, Range(0, 1)] private float sizeIncreasingEffectTimelinePosition;
    [SerializeField] private Ease sizeChangingEase;

    private protected override void ApplyLevel()
    {
        // swordScaleTransform.localScale = startScale + scalePerUpgrade * level;ss

        swordScaleTransform.DOComplete(true);
        Vector3 targetScale = startScale + scalePerUpgrade * level;
        swordScaleTransform.DOScale(targetScale * sizeIncreasingEffectForce,
                sizeIncreasingEffectTimelinePosition * sizeIncreasingTime)
            .SetEase(sizeChangingEase).onComplete += () => swordScaleTransform.DOScale(targetScale,
            (1 -sizeIncreasingEffectTimelinePosition) * sizeIncreasingTime).SetEase(sizeChangingEase);
        
    }
}
