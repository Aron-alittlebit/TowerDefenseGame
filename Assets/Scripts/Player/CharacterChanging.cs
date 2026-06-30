using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class CharacterChanging : MonoBehaviour
{
    [SerializeField] HeroData Knight;
    [SerializeField] HeroData Mage;
    [SerializeField] HeroData Ranger;
    [SerializeField] HeroData Engineer;
    public HeroData CurrentHero { get; private set; }
    
     GameObject Visual;
    

    private void Start()
    {
        Visual = transform.GetChild(0).gameObject;
        CurrentHero = Knight;
        GetComponent<CharacterMovement>().SetAnimator(Visual.GetComponentInParent<Animator>());
        GetComponent<PlayerHealth>().SetHealth(Knight);
    }

    private void OnEnable()
    {
        PlayerEvents.OnLevelReloaded += LevelReloaded;
    }

    private void OnDisable()
    {
        PlayerEvents.OnLevelReloaded -= LevelReloaded;
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
        CurrentHero = hero;

    }

    void LevelReloaded()
    {
        GetComponent<PlayerHealth>().SetHealth(Knight);
        CurrentHero.SetHealth(CurrentHero.Health);
    }


}
