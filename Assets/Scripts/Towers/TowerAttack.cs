using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] float CoolDown;
    float currentCoolDown;
    [SerializeField] int Damage;
    [SerializeField] LayerMask EntityLayer;

    private void Start()
    {
        currentCoolDown = CoolDown;
    }
    private void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
    }

    private void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
    }
    private void Update()
    {
        currentCoolDown -= Time.deltaTime;
        
    }

    void Attack(Transform firePoint, Vector3 targetPos, float Range)
    {
        if (currentCoolDown <= 0)
        {
            
            Vector3 direction =targetPos - firePoint.position;
            
            if (Physics.Raycast(firePoint.position, direction, out RaycastHit hitInfo,
                Range, EntityLayer))
            {
                Debug.Log("Attacked enemy");
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                enemy.TakeDamage(Damage);
            }
            currentCoolDown = CoolDown;
        }
    }


}
