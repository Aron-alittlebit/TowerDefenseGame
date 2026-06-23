using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class BuildingTowers : MonoBehaviour
{
    bool IsBuilding;
    float RotateAmount = 5f;
    
    Tower tower;
    TowerData defaultTowerData;
    
    [SerializeField] Material BuildingMat;
    Material PlacedMat;
    [SerializeField] TowerData towerData1;
    [SerializeField] TowerData towerData2;
    [SerializeField] TowerData towerData3;
    [SerializeField] TowerData towerData4;

    KeyCode selectedTowerKey;
    void Start()
    {
        IsBuilding = false;
    }
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DecideToBuildOrCancel(KeyCode.Alpha1, towerData1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DecideToBuildOrCancel(KeyCode.Alpha2, towerData2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DecideToBuildOrCancel(KeyCode.Alpha3, towerData3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DecideToBuildOrCancel(KeyCode.Alpha4, towerData4);
        }

        if (IsBuilding)
        {
            RotateTower();
           
            if (tower != null)
            {
                Vector3 newPos = transform.position + (transform.forward) * 10;
                newPos.y = transform.position.y-1;
                tower.transform.position = newPos;
                
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                IsBuilding = false;
                tower.TowerIsBuilt();
                
                MaterialChange(PlacedMat);
                TowerEvents.GemSpent(defaultTowerData.Cost);
                TowerEvents.TowerBuilt(defaultTowerData, tower.transform.GetChild(0).gameObject);
                
            }
        }
    }

    void Building(TowerData towerData)
    {
        if (PlayerGemPickUp.GemCounter < towerData.Cost) return;
        
        defaultTowerData = towerData;

        Vector3 TowerPos = transform.position + (transform.forward) * 10;
        TowerPos.y = transform.position.y - 1;
        tower = Instantiate(towerData.TowerPrefab, TowerPos, Quaternion.identity);

        tower.SetCost(towerData.Cost);

        var renderers = tower.GetComponentsInChildren<Renderer>();
        if (renderers != null)
        {
            foreach (var render in renderers)
            {
                PlacedMat = render.sharedMaterial;
            }
        }

       

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

    void CancelBuilding()
    {
        if(tower != null)
        {
            
            Destroy(tower.gameObject);
            tower = null;
        }
        IsBuilding = false;
        
    }

    void DecideToBuildOrCancel(KeyCode key, TowerData td)
    {
        if(selectedTowerKey == key && IsBuilding)
        {
            CancelBuilding();
        }
        else
        {
            CancelBuilding();
            Building(td);
            selectedTowerKey = key;
        }
    }

   
}
