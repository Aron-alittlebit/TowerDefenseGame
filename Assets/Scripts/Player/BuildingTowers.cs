using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class BuildingTowers : MonoBehaviour
{
    bool IsBuilding;
    float RotateAmount = 50f;
    int BPressed;
    Tower tower;
    [SerializeField] Tower TowerPrefab;
    [SerializeField] Material BuildingMat;
    [SerializeField] Material PlacedMat;
    [SerializeField] TowerData towerData;
    void Start()
    {
        IsBuilding = false;
        BPressed = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BPressed++;
        }
        
        if (Input.GetKeyDown(KeyCode.B) && !IsBuilding && BPressed == 1)
        {
            Building();
            
        }

        Debug.Log(BPressed);

        if (IsBuilding)
        {
            RotateTower();

            if (Input.GetKeyDown(KeyCode.B) && BPressed == 2)
            {
                IsBuilding = false;
                Destroy(tower.gameObject);
                tower = null;
                BPressed = 0;
                

            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                IsBuilding = false;
                tower.ChangeIsBuilt(true);
                MaterialChange(PlacedMat);
                TowerEvents.GemSpent(towerData.Cost);
                BPressed = 0;
            }
        }
    }

    void Building()
    {
        if (PlayerGemPickUp.GemCounter < towerData.Cost) return;
        Vector3 TowerPos = transform.position+(transform.forward)*10;
        TowerPos.y = -1;
        tower = Instantiate(TowerPrefab, TowerPos, Quaternion.identity);

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
