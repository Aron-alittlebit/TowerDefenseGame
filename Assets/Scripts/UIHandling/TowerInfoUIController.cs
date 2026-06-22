using UnityEngine;
using UnityEngine.UIElements;

public class TowerInfoUIController : MonoBehaviour
{
    [SerializeField] UIDocument uidocument;
    VisualElement root;
    TowerUIModell model;

    public bool IsOpened { get; private set; }

    private void Awake()
    {
        IsOpened = false;
        root = uidocument.rootVisualElement.Q("TowerInfo");
        root.style.display = DisplayStyle.None;
    }

    public void Show(TowerUIModell towerInfo)
    {
        model = towerInfo;
        root.Q<Label>("TowerName").text = model.TowerName;
        root.Q<Label>("Tier").text = $"Tier: {model.Tier}";
        root.Q<Label>("Damage").text = $"Damage: {model.Damage}";
        root.Q<Label>("Range").text = $"Range: {model.Range}";
        root.Q<Label>("FireRate").text = $"Firerate: {model.FireRate}";
        root.Q<Label>("KillCount").text = $"Kills: {model.KillCount}";
        root.style.display = DisplayStyle.Flex;
        IsOpened = true;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
        IsOpened = false;
    }
}
