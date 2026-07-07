using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : LivingAbstractClass
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] AudioClip RespawnSound;
    

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
        //base.Start();
        transform.position = SpawnPoint.position;
        HealthText.text = $"{health}";
    }

    
    protected override void Die()
    {
        
        
        SoundManager.instance.PlaySound(RespawnSound, SpawnPoint, 50f);
        health = StartingHealth;
        GetComponent<CharacterChanging>().CurrentHero.SetHealth(health);
        HealthText.text = $"{health}";
        CharacterController cc = GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false;
            transform.position = SpawnPoint.position;
            cc.enabled = true;
        }
        else
        {
            transform.position = SpawnPoint.position;
        }
        
    }

    public void SetHealth(HeroData hero)
    {
        StartingHealth = hero.Health;
        health = Mathf.Clamp(hero.CurrentHealth, 0, StartingHealth);
        HealthText.text = $"{health}";
        
    }

    public override void TakeDamage(int damage)
    {

        health -= damage;
        HealthText.text = $"{health}";
        GetComponent<CharacterChanging>().CurrentHero.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
        
    }


}
