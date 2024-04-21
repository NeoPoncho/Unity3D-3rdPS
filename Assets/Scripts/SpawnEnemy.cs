using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public Enemy enemy;
    private List<Enemy> enemies;

    [Range(0, 100)]

    public int EnemyCount = 25;
    private readonly float range = 70.0f;

    void Start()
    {
        enemies = new List<Enemy>();

        for (int index = 0; index < EnemyCount; index++)
        {
            Enemy spawned = Instantiate(enemy, RandomNavmeshLocation(range), Quaternion.identity) as Enemy;
            enemies.Add(spawned);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
}
