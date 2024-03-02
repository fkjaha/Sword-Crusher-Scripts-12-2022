using UnityEngine;

public class PixelExchanger : AUpgradable
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private CoinsAnimator coinsAnimator;

    [Space(20f)] 
    [SerializeField] private Vector2Int startIncomeBounds;
    [Tooltip("Will be converted to int!")]
    [SerializeField] private float incomeBoundsIncreasePerLevel;

    private Vector2Int _currentIncomeBounds;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pixel pixel))
        {
            Vector3 pixelPosition = pixel.transform.position;
            Destroy(pixel.gameObject);
            coinsAnimator.AnimateSingleCoin(ref pixelPosition);
            Level.Instance.CollectCoins(Random.Range(_currentIncomeBounds.x, _currentIncomeBounds.y));
        }
    }

    private protected override void ApplyLevel()
    {
        _currentIncomeBounds = startIncomeBounds + new Vector2Int((int)(incomeBoundsIncreasePerLevel * (level/2 + level%2)),
            (int)(incomeBoundsIncreasePerLevel * level/2));
        Debug.Log(_currentIncomeBounds);
    }
}
