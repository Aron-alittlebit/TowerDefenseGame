using TMPro;
using UnityEngine;

public class PlayerGemPickUp : MonoBehaviour
{
    [SerializeField]float Radius = 5f;
    public static int GemCounter = 15;
    public LayerMask GemLayer;
    public TextMeshProUGUI GemCounterText;
    private void Start()
    {
        GemCounterText.text = $"{GemCounter}";
    }

    private void OnEnable()
    {
        TowerEvents.OnGemSpent += SpentGems;
        TowerEvents.OnTowerSold += SoldTower;
    }
    private void OnDisable()
    {
        TowerEvents.OnGemSpent -= SpentGems;
        TowerEvents.OnTowerSold -= SoldTower;
    }
    void Update()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position,Radius, GemLayer);
        foreach(var collider in colliders)
        {
            GemCounter++;
            GemCounterText.text = $"{GemCounter}";
            Destroy(collider.gameObject);
        }
    }

    void SpentGems(int gems)
    {
        GemCounter -= gems;
        GemCounterText.text = $"{GemCounter}";
    }

    void SoldTower(int gems)
    {
        GemCounter += gems;
        GemCounterText.text = $"{GemCounter}";
    }


}
