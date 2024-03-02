using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CoinsAnimator : MonoBehaviour
{
    [SerializeField] private CoinsPool coinsPool;

    [Header("Animation Options")] 
    [SerializeField] private float sizeChangingTime;
    [SerializeField] private float lifeTime;
    [SerializeField] private Ease ease;

    private void Awake()
    {
        DOTween.Init();
    }

    public void AnimateSingleCoin(ref Vector3 startPosition)
    {
        GameObject coin = coinsPool.GetCoin();

        StartCoroutine(AnimateCoin(coin, startPosition));
    }

    private IEnumerator AnimateCoin(GameObject coin, Vector3 startPosition)
    {
        Transform coinTransform = coin.transform;
        coinTransform.position = startPosition;

        Vector3 startScale = coinTransform.localScale;
        
        coinTransform.DOComplete();
        coinTransform.DOKill();
        coinTransform.localScale = Vector3.one;
        coin.SetActive(true);

        yield return coinTransform.DOScale(Vector3.zero, sizeChangingTime).From().SetEase(ease).WaitForCompletion();
        yield return new WaitForSeconds(lifeTime);
        yield return coinTransform.DOScale(Vector3.zero, sizeChangingTime).SetEase(ease).WaitForCompletion();
        
        coin.SetActive(false);
        coinTransform.DOScale(startScale, 0f);
    }
}
