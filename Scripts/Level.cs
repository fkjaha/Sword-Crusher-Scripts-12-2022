using System;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public static Level Instance;
    public DestructiveShape GetLevelShape => levelShape;
    public int GetLevelIndex => _leveIndex;
    public bool IsCompleted => GetLevelProgress() == 100;
    public event UnityAction CoinsCollected;
    public event UnityAction<float> LevelInitializationUpdated;
    public event UnityAction LevelInitialized;
    
    [SerializeField] private int startCoinsTargetForUpgrade;
    [SerializeField] private int coinsTargetIncreasePerUpgrade;
    [SerializeField] private DestructiveShape levelShape;
    [SerializeField] private MaterialsController materialsController;
    [SerializeField] private UpgradesManager upgradesManager;
    [SerializeField] private DestructiveShapeGenerator shapeGenerator;

    private int _currentCoinsTargetForUpgrade;
    private int _collectedCoins;
    private int _leveIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize(GameManager.Instance.GetValidLevelTexture, GameManager.Instance.GetCurrentLevelIndex);
    }

    public void Initialize(Texture2D levelSprite, int levelIndex)
    {
        _leveIndex = levelIndex;
        shapeGenerator.GenerateDestructiveShape(levelShape, levelSprite, LevelInitializationUpdated);
        PositionRandomizer.Instance.RandomizeShapePixels(levelShape);
        materialsController.ApplyPreset(_leveIndex);
        _currentCoinsTargetForUpgrade = startCoinsTargetForUpgrade;
        levelShape.OnDamaged += CheckLevelCompletion;
        LevelInitialized?.Invoke();
    }

    public void RequestLevelPassing()
    {
        GameManager.Instance.TryLoadNextLevel();
    }
    
    public void RequestLevelReload()
    {
        GameManager.Instance.ReloadLevel();
    }

    public void CollectCoins(int number)
    {
        _collectedCoins += number;
        CheckForTargetReached();
        CoinsCollected?.Invoke();
    }

    public int GetLevelProgress()
    {
        return levelShape.GetDestructionProgress;
    }

    public int GetNextUpgradeProgress()
    {
        return (int) ((float) _collectedCoins / _currentCoinsTargetForUpgrade * 100);
    }

    private void CheckForTargetReached()
    {
        if (_collectedCoins >= _currentCoinsTargetForUpgrade)
        {
            _collectedCoins -= _currentCoinsTargetForUpgrade;
            upgradesManager.OfferUpgrade();
            _currentCoinsTargetForUpgrade += coinsTargetIncreasePerUpgrade;
        }
    }

    private void CheckLevelCompletion()
    {
        if (IsCompleted)
        {
            GameManager.Instance.AlertLevelCompletion();
        }
    }
}
