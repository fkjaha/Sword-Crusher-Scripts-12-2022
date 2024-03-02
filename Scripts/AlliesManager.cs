using System.Collections.Generic;
using UnityEngine;

public class AlliesManager : AUpgradable
{
    [SerializeField] private List<AllayController> allayPrefabs;
    [SerializeField] private Transform allaySpawnParent;
    [SerializeField] private int startCount;
    [SerializeField] private int countPerUpgrade;

    private int _currentCount;
    private List<AllayController> _allies = new();

    private protected override void ApplyLevel()
    {
        _currentCount = startCount + level * countPerUpgrade;
        MatchAlliesCount();
    }

    private void MatchAlliesCount()
    {
        if (_currentCount > _allies.Count)
        {
            int deltaCount = _currentCount - _allies.Count;
            for (int i = 0; i < deltaCount; i++)
            {
                SpawnOneAllay();
            }
        }
    }

    private void SpawnOneAllay()
    {
        _allies.Add(Instantiate(allayPrefabs[_allies.Count % allayPrefabs.Count], allaySpawnParent));
    }
}
