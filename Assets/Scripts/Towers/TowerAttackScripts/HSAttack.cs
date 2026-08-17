using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class HSAttack : TowerAttack
{

    Animator animator;
    [SerializeField] BoxCollider Spikes;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        TowerEvents.OnTowerBuilt += SetTowerData;
        TowerEvents.OnTowerUpgraded += SetDataAfterUpgrade;
        TowerEvents.OnHiddenSpikeReadyToAttack += PlayAttackAnim;
    }

    protected override void OnDisable()
    {
        TowerEvents.OnTowerBuilt -= SetTowerData;
        TowerEvents.OnTowerUpgraded -= SetDataAfterUpgrade;
        TowerEvents.OnHiddenSpikeReadyToAttack -= PlayAttackAnim;
    }
    protected override void SetTowerData(TowerData td, GameObject sender)
    {

        if (sender.transform.GetChild(0).gameObject != gameObject) return;
        towerData = td;
        
        damage = towerData.Damage;
        coolDown = towerData.CoolDown;
        currentCoolDown = coolDown;
        
    }

    protected override void SetDataAfterUpgrade(Tower tower, GameObject sender)
    {

        if (gameObject != sender.transform.GetChild(0).gameObject) return;
        towerData = tower.towerData;
        damage = towerData.Damage + (10 * (tower.Tier));

        coolDown = towerData.CoolDown - (0.1f * tower.Tier);
        if (coolDown <= 0)
            coolDown = 0.1f;

        currentCoolDown = coolDown;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage(damage);
        }
    }

    void PlayAttackAnim()
    {
        if(currentCoolDown <= 0f)
        {
            animator.SetTrigger("Attack");
            currentCoolDown = CoolDown;
        }
            
    }

    public void EnableSpikes()
    {
        Spikes.enabled = true;
    }

    public void DisableSpikes()
    {
        Spikes.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(damage);
    }
}
