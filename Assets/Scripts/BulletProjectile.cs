using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public GameObject BulletPB;
    private Rigidbody BulletRigidbody;
    public float speed;

    void Awake()
    {
        BulletRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        BulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage((int)Random.Range(5, 7));
        }
        Destroy(gameObject);
    }
}
