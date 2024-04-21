using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;
    public GameObject player;

    Vector3 originalPos;

    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        curHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.transform.position.y < -10)
        {
            DamagePlayer(1);

            if(curHealth == 0)
            {
                player.transform.position = originalPos;
                curHealth = maxHealth;
                healthBar.SetHealth(curHealth);
            }
        }
    }

    public void DamagePlayer(int damage)
    {
        curHealth -= damage;

        healthBar.SetHealth(curHealth);
    }
}
