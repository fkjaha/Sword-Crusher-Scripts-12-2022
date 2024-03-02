using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Texture2D GetValidLevelTexture => levels[_currentLevelIndex % levels.Count];
    public int GetCurrentLevelIndex => _currentLevelIndex;

    public event UnityAction OnLevelCompleted;
    
    [SerializeField] private List<Texture2D> levels;

    private int _currentLevelIndex;
    private const string SaveKey = "LevelProgress";

    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate * 3;
        if (Instance == null)
        {
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        DontDestroyOnLoad(this);
        Debug.Log("Target framerate: " + Application.targetFrameRate);
        _currentLevelIndex = LoadLastLevelIndexFromSave();
        Instance = this;
    }

    public void AlertLevelCompletion()
    {
        if(!Level.Instance.IsCompleted) return;

        SaveProgress(_currentLevelIndex + 1);
        OnLevelCompleted?.Invoke();
    }

    public void TryLoadNextLevel()
    {
        if(!Level.Instance.IsCompleted) return;
        
        _currentLevelIndex++;
        LoadLevel(_currentLevelIndex);
    }

    public void ReloadLevel()
    {
        LoadLevel(_currentLevelIndex);
    }

    private void LoadLevel(int index)
    {
        ResetPublicEvents();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetPublicEvents()
    {
        OnLevelCompleted = null;
    }

    private void SaveProgress(int progress)
    {
        PlayerPrefs.SetInt(SaveKey, progress);
    }

    private int LoadLastLevelIndexFromSave()
    {
        return PlayerPrefs.GetInt(SaveKey, 0);
    }
}
