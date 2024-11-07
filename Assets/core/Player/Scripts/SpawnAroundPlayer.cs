using System.Collections;
using System.Collections.Generic;
using core.Scripts.enemy_ai;
using UnityEngine;

public class SpawnAroundPlayer : MonoBehaviour
{
    [SerializeField] private float radius = 5;

    [SerializeField] private float cooldown = 1f;

    private float _timeSinceSpawn;

    [SerializeField] private GameObject[] enemies;

    [Header("bullet")] [SerializeField] private GameObject bulletEnemy;
 
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _timeSinceSpawn + cooldown)
        {
            _timeSinceSpawn = Time.time;

            var enemiesToSpawn = Random.Range(3, 5);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var randomInsideCircle = transform.position +  (Vector3)Random.insideUnitCircle * radius;
                var randomEnemyToSpawn = Random.Range(0, enemies.Length);
                
                var enemy =Instantiate(enemies[randomEnemyToSpawn], randomInsideCircle, Quaternion.identity);
               
                if (enemy.TryGetComponent(out EnemyAI enemyAI))
                {
                    enemyAI.playerToFollow = transform;
                    if (enemyAI.TryGetComponent(out shooting_ghost shootingGhost))
                    {
                        shootingGhost.target = transform;
                        shootingGhost.bullet =  bulletEnemy;
                    }
                }
            }
            
            
        }
    }
}
