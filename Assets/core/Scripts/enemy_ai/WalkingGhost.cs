using System;
using UnityEngine;

namespace core.Scripts.enemy_ai
{
    public class WalkingGhost : EnemyAI
    {
        
        [Header("walking ghost")]
        [SerializeField] private float damageAmount;
        
        public void Awake()
        {
            health = 3f;
            
        }

        protected override void Attack()
        {
            if (_collider2D.TryGetComponent(out PlayerData player))
            {
                player.TakeDamage(damageAmount);
            }

            _currentState = EnemyState.Walk;
            
        }
    }
}