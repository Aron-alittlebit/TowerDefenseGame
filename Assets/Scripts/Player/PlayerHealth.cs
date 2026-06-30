using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : LivingAbstractClass
{
    [SerializeField] Transform SpawnPoint;
    HeroData heroData;

    private void OnEnable()
    {
        PlayerEvents.OnHeroChanged += SetHealth;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHeroChanged -= SetHealth;
    }

    protected override void Start()
    {
        base.Start();
        transform.position = SpawnPoint.position;
    }
    protected override void Die()
    {
        if (health <= 0)
        {

            health = StartingHealth;
            transform.position = SpawnPoint.position;

        }
    }

    public void SetHealth(HeroData hero)
    {
        heroData = hero;
        health = Mathf.Clamp(hero.Health, 0, StartingHealth);
        Die();
    }

    public override void TakeDamage(int damage)
    {

        health -= damage;
        heroData.SetHealth(health); 
        Die();
    }
}
