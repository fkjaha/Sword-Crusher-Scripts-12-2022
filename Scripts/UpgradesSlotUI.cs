using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesSlotUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI description;

    public void Initialize(UpgradesUI upgradesUI, int slotIndex)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            upgradesUI.SelectUpgrade(slotIndex);
            // button.interactable = false;
        });
    }
    
    public void UpdateSlotView(UpgradableInfo info)
    {
        image.sprite = info.GetSprite;
        label.text = info.GetLabel;
        description.text = info.GetDescription;
        button.interactable = true;
    }
}
