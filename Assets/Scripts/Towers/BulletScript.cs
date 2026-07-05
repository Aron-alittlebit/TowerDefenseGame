using UnityEngine;

public class BulletScript : MonoBehaviour
{
    int Damage;
    [SerializeField] float speed;
    Vector3 Direction;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.position += Direction.normalized * speed * Time.deltaTime;
    }

    public void SetData(TowerData td, Vector3 dir)
    {
        Damage = td.Damage;
        Direction = dir;

    }

    private void OnTriggerEnter(Collider other)
    {
        Entity enemy = other.GetComponent<Entity>();
        if (enemy != null)
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
