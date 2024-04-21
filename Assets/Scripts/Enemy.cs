using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public NavMeshAgent agent;
    public float speed = 5.0f;
    public int enemyHealth = 20;

    void Start()
    {
        SetKinematic(true);
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerArmature");
    }

    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
      
        if (enemyHealth <= 0)
        {
            SetKinematic(false);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 5.0f);
        }
    }
}
