using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class CharacterChanging : MonoBehaviour
{
    [SerializeField] HeroData Knight;
    [SerializeField] HeroData Mage;
    [SerializeField] HeroData Ranger;
    [SerializeField] HeroData Engineer;
     GameObject Visual;
    

    private void Start()
    {
        Visual = transform.GetChild(0).gameObject;
        GetComponent<CharacterMovement>().SetAnimator(Visual.GetComponentInParent<Animator>());
    }

    void Update()
    {
        if (GetComponent<BuildingTowers>().IsBuilding) return;

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

    private void Change(HeroData hero)
    {
        DestroyImmediate(Visual);
        Visual = Instantiate(
            hero.ModelPrefab,
            transform.position,
            transform.rotation,
            transform
        );
        PlayerEvents.HeroChanged(hero);
        transform.GetComponent<Animator>().runtimeAnimatorController = hero.AnimatorController;

    }
}
