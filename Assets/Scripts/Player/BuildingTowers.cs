using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class BuildingTowers : MonoBehaviour
{
    bool IsBuilding;
    float RotateAmount = 50f;
    Tower tower;
    [SerializeField] Tower TowerPrefab;
    [SerializeField] Material BuildingMat;
    [SerializeField] Material PlacedMat;
    [SerializeField] TowerData towerData;
    void Start()
    {
        IsBuilding = false;
    }
    void Update()
    {
        Debug.Log(transform.forward);
        if (Input.GetKeyDown(KeyCode.B) && !IsBuilding)
            Building();


        if (IsBuilding)
        {
            RotateTower();

            if (Input.GetKeyDown(KeyCode.I))
            {
                IsBuilding = false;
                Destroy(tower.gameObject);
                tower = null;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                IsBuilding = false;
                tower.ChangeIsBuilt(true);
                MaterialChange(PlacedMat);
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
