using UnityEngine;
using UnityEngine.Events;

public abstract class AUpgradable: MonoBehaviour
{
    public int GetLevel => level;
    public UpgradableInfo GetInfo => info;
    
    [SerializeField] private protected int level;
    [SerializeField] private UpgradableInfo info;
    [SerializeField] private UnityEvent onLevelUpgraded;

    private protected void Start()
    {
        ApplyLevel();
    }

    public void UpgradeLevelByOne()
    {
        level++;
        onLevelUpgraded.Invoke();
        ApplyLevel();
    }
    
    private protected abstract void ApplyLevel();
}
