using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : LivingAbstractClass
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] TextMeshProUGUI HealthText;
    

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
        HealthText.text = $"{health}";
    }

    private void Update()
    {
        HealthText.text = $"{health}";
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
        
        health = Mathf.Clamp(hero.CurrentHealth, 0, StartingHealth);
        HealthText.text = $"{health}";
        Die();
    }

    public override void TakeDamage(int damage)
    {

        base.TakeDamage(damage);
        GetComponent<CharacterChanging>().CurrentHero.SetHealth(health);
    }


}
