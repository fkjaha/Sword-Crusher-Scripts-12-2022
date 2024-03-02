using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private UpgradesSlotUI slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private UpgradesManager upgradesManager;
    [SerializeField] private UnityEvent onUpgradeSelected;

    private List<UpgradesSlotUI> _slots;
    private List<AUpgradable> _lastChoices;

    public void Initialize(int slots)
    {
        SpawnSlots(slots);
    }

    private void SpawnSlots(int number)
    {
        _slots = new();
        for (int i = 0; i < number; i++)
        {
            _slots.Add(Instantiate(slotPrefab, slotsParent));
            _slots[i].Initialize(this, i);
        }
    }

    public void DisablePanel()
    {
        upgradesPanel.SetActive(false);
    }

    public void ShowUpgradeOptions(List<AUpgradable> options)
    {
        _lastChoices = options;
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].UpdateSlotView(_lastChoices[i].GetInfo);
        }
        upgradesPanel.SetActive(true);
    }

    public void SelectUpgrade(int index)
    {
        // Debug.Log(index, this);
        upgradesManager.UpgradeUpgradable(_lastChoices[index]);
        // upgradesPanel.SetActive(false);
        onUpgradeSelected.Invoke();
    }
}
