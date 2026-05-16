using UnityEngine;

public class BuildingTowers : MonoBehaviour
{
    bool IsBuilding;
    float RotateAmount = 50f;
    Tower tower;
    [SerializeField] Tower TowerPrefab;
    [SerializeField] Material BuildingMat;
    [SerializeField] Material PlacedMat;
    void Start()
    {
        IsBuilding = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) && !IsBuilding)
            Building();


        if (IsBuilding)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                tower.transform.Rotate(0, RotateAmount, 0);

            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                tower.transform.Rotate(0, -RotateAmount, 0);

            if (Input.GetKeyDown(KeyCode.N))
            {
                IsBuilding = false;
                tower.ChangeIsBuilt(true);
                foreach (Transform child in tower.transform)
                {
                    if (child.GetComponent<Renderer>() != null)
                    {
                        child.GetComponent<Renderer>().material = PlacedMat;
                    }
                }
            }
                
        }

        void Building()
        {
            Vector3 TowerPos = transform.position;
            TowerPos.x += 10;
            TowerPos.y = -1;
            tower = Instantiate(TowerPrefab, TowerPos, Quaternion.identity);
            
            foreach (Transform child in tower.transform)
            {
                if (child.GetComponent<Renderer>() != null)
                {
                    child.GetComponent<Renderer>().material = BuildingMat;
                }
            }
            IsBuilding = true;

        }

        
    }
}
