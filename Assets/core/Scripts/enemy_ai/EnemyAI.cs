using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
namespace core.Scripts.enemy_ai

{
    public class EnemyAI : MonoBehaviour, IDamageable
    {
        [Header("AI settings")]
        
        [SerializeField] public Transform playerToFollow;
        [SerializeField] private float speed;
        [SerializeField] protected float walkSpeed = 2f;
        [SerializeField] protected float attackRange = 1.5f;
        [SerializeField] protected float health = 5f;
        [SerializeField] protected float maxHealth = 5f;
        [Space(10)]
        protected Collider2D _collider2D;

        
        
        [Header("Health Bar")] 
        
        [SerializeField] private FloatingHealthBar healthBar;
        
        
        protected enum EnemyState
        {
            Walk,
            Attack,
            Die
        }

        protected EnemyState _currentState;
 
        void Start()
        {
            health = maxHealth;
            healthBar.UpdateHealthBar(health, maxHealth);

            _collider2D = GetComponent<Collider2D>();
            _currentState = EnemyState.Walk;
        }

        private void Awake()
        {
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        }

        // Update is called once per frame
        void Update()
        {
 
            if (healthBarInstance)
                healthBarInstance.transform.position = transform.position + Vector3.up;
 
            switch (_currentState)
            {
                case EnemyState.Walk:
                    Move();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Die:
                    Die();
                    break;
                default:
                    transform.position = transform.position;
                    break;
            }
        }

        protected virtual void Move()
        {
            var direction = (playerToFollow.position - transform.position).normalized;
            transform.position += direction * (walkSpeed * Time.deltaTime);
                
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            var position = Vector3.Distance(transform.position, playerToFollow.position);
            if (position <= attackRange)
            {
                _currentState = EnemyState.Attack;
            }
        }
        
        protected virtual void Attack()
        {
            if (_collider2D.TryGetComponent(out PlayerData player))
            {
                player.TakeDamage(4f);
                _currentState = EnemyState.Die;
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthBar.UpdateHealthBar(health, maxHealth);
            if (health <= 0)
            {
                Destroy(gameObject);
                Die();
            }
        }

        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}
