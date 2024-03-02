using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    [SerializeField] private UpgradesUI upgradesUI;
    [SerializeField] private List<AUpgradable> upgradables;
    [SerializeField] private int upgradesPerChoice;

    private bool _lastOfferAccepted = true;
    private List<AUpgradable> _lastOfferedUpgrades = new();
    private AUpgradable _lastUsedUpgradable;

    private void Awake()
    {
        Instance = this;
        upgradesUI.Initialize(upgradesPerChoice);
    }

    public void OfferUpgrade()
    {
        if(!_lastOfferAccepted) ApplyRandomUpgradeFromLast();
        
        _lastOfferedUpgrades = GetRandomUpgradables(upgradesPerChoice);
        upgradesUI.ShowUpgradeOptions(_lastOfferedUpgrades);
        _lastOfferAccepted = false;
    }

    public List<AUpgradable> GetRandomUpgradables(int number)
    {
        List<AUpgradable> possibleUpgradables = new();
        possibleUpgradables.AddRange(upgradables);
        List<AUpgradable> resultUpgradables = new();
        possibleUpgradables.Remove(_lastUsedUpgradable);
        if (number > upgradables.Count) number = upgradables.Count;
        for (int i = 0; i < number; i++)
        {
            AUpgradable chosen = possibleUpgradables[Random.Range(0, possibleUpgradables.Count)];
            resultUpgradables.Add(chosen);
            possibleUpgradables.Remove(chosen);
        }
        
        return resultUpgradables;
    }

    public void UpgradeUpgradable(AUpgradable upgradable)
    {
        _lastUsedUpgradable = upgradable;
        upgradable.UpgradeLevelByOne();
        if(_lastOfferedUpgrades.Contains(upgradable)) 
            _lastOfferAccepted = true;
    }

    private void ApplyRandomUpgradeFromLast()
    {
        UpgradeUpgradable(_lastOfferedUpgrades[0]);
    }
}
