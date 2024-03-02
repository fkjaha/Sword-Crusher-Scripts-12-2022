using System.Collections;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelProgressionUI : MonoBehaviour
{
    [SerializeField] private float initializeAfterTime;
    [SerializeField] private Image breakingProgress;
    [SerializeField] private Image nextUpgradeProgress;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private MMFeedbacks levelCompletedFeedbacks;

    private void Start()
    {
        StartCoroutine(DelayedInitialization(initializeAfterTime));
    }

    private void Initialize()
    {
        Level.Instance.CoinsCollected += UpgradeNextUpgradeProgressVisualization;
        Level.Instance.GetLevelShape.OnDamaged += UpgradeBreakingProgressVisualization;
        GameManager.Instance.OnLevelCompleted += levelCompletedFeedbacks.PlayFeedbacks;
        
        UpgradeNextUpgradeProgressVisualization();
        UpgradeBreakingProgressVisualization();
        UpdateLevelIndexText();
    }

    private void UpgradeNextUpgradeProgressVisualization()
    {
        nextUpgradeProgress.fillAmount = Level.Instance.GetNextUpgradeProgress()/100f;
    }

    private void UpgradeBreakingProgressVisualization()
    {
        breakingProgress.fillAmount = Level.Instance.GetLevelProgress() / 100f;
    }

    private void UpdateLevelIndexText()
    {
        levelText.text = "LEVEL " + Level.Instance.GetLevelIndex;
        Debug.Log("a");
    }

    private IEnumerator DelayedInitialization(float delay)
    {
        yield return new WaitForSeconds(delay);
        Initialize();
    }
}
