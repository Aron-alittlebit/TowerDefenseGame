using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class BuildingTowers : MonoBehaviour
{
    bool IsBuilding;
    float RotateAmount = 50f;
    int Cost;
    Tower tower;
    TowerData defaultTowerData;
    
    [SerializeField] Material BuildingMat;
    [SerializeField] Material PlacedMat;
    [SerializeField] TowerData towerData1;
    [SerializeField] TowerData towerData2;
    [SerializeField] TowerData towerData3;
    void Start()
    {
        IsBuilding = false;
       
    }
    void Update()
    {
        if (!IsBuilding)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Building(towerData1);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Building(towerData2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Building(towerData3);
            }
            
                
            
        }
        
        

        if (IsBuilding)
        {
            RotateTower();
            //Debug.Log(tower.Tier);
            if (tower != null)
            {
                Vector3 newPos = transform.position + (transform.forward) * 10;
                newPos.y = transform.position.y;
                tower.transform.position = newPos;
                
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                IsBuilding = false;
                Destroy(tower.gameObject);
                tower = null;
                
                

            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                IsBuilding = false;
                tower.TowerIsBuilt(true);
                MaterialChange(PlacedMat);
                TowerEvents.GemSpent(defaultTowerData.Cost);
                TowerEvents.TowerBuilt(defaultTowerData);
                
            }
        }
    }

    void Building(TowerData towerData)
    {
        if (PlayerGemPickUp.GemCounter < towerData.Cost) return;
        defaultTowerData = towerData;
        Vector3 TowerPos = transform.position+(transform.forward)*10;
        TowerPos.y = transform.position.y;
        tower = Instantiate(towerData.TowerPrefab, TowerPos, Quaternion.identity);
        tower.SetCost(towerData.Cost);
        Cost = towerData.Cost;

        MaterialChange(BuildingMat);

        IsBuilding = true;

    }

    void MaterialChange(Material mat)
    {
        var renderers = tower.GetComponentsInChildren<Renderer>();
        if (renderers != null)
        {
            foreach (var render in renderers)
            {
                render.material = mat;
            }
        }
    }

    void RotateTower()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            tower.transform.Rotate(0, RotateAmount, 0);

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            tower.transform.Rotate(0, -RotateAmount, 0);
    }
}
