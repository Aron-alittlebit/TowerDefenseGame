using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class CharacterChanging : MonoBehaviour
{
    [SerializeField] GameObject Knight;
    [SerializeField] GameObject Mage;
    [SerializeField] GameObject Ranger;
    [SerializeField] GameObject Engineer;
    GameObject Visual;
    

    private void Start()
    {
        Visual = transform.GetComponentInChildren<PlayerHealth>().gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Change(Knight);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Change(Mage);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Change(Ranger);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Change(Engineer);
        }
    }

    private void Change(GameObject hero)
    {
        DestroyImmediate(Visual);
        Visual = Instantiate(
            hero,
            transform.position,
            transform.rotation,
            transform
        );
    }
}
