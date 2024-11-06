using System;
using System.Collections;
using UnityEngine;

namespace core.Scripts.enemy_ai
{
    public class shooting_ghost : EnemyAI
    {
        [SerializeField] public Transform target;
        [SerializeField] protected float cooldown;
        [SerializeField] protected GameObject bullet;

        private float _lastAttackTime;
        private void Awake()
        {
            health = 10;
            attackRange = 5;
        }

        protected override void Attack()
        {
            var direction = (target.position - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            
            if (Time.time >= _lastAttackTime + cooldown)
            {
                _lastAttackTime = Time.time;

                var bulletToSpawn = bullet;
                Instantiate(bulletToSpawn, transform.position,  transform.rotation);
              
                
            }

            var position = Vector3.Distance(transform.position, target.position);
            if (position >= attackRange)
            {
                _currentState = EnemyState.Walk;
            }
          

        }
        
    }
    
    
}