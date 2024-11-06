using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace core.Scripts.enemy_ai
{
    public class ExplodingGhost : EnemyAI  
    {
        [SerializeField] private float timeToExplosion;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float damageAmount;
        
        protected override void Attack()
        {
            StartCoroutine(WaitAndExplode());
        }

        IEnumerator WaitAndExplode( )
        {
            yield return new WaitForSeconds(timeToExplosion);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (var collider2d in colliders)
            {
                if (collider2d.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damageAmount);
                }
            }
            Destroy(gameObject);
            _currentState = EnemyState.Die;

        }
    }
}