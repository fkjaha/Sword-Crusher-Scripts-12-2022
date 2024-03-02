using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoadingVisualizer : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image progressImage;

    private void Awake()
    {
        // level.LevelInitializationUpdated += UpgradeProgress;
        level.LevelInitialized += DisableLoadingVisualization;
        loadingPanel.SetActive(true);
    }

    private void UpgradeProgress(float progress)
    {
        Debug.Log(progress);
        progressImage.fillAmount = progress;
    }

    private void DisableLoadingVisualization()
    {
        loadingPanel.SetActive(false);
        Debug.Log("b");
    }
    
    // private IEnumerator LoadingVisualization()
    // {
    //     yield return new WaitUntil()
    //        
    // }
}
