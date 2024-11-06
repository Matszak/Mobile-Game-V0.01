using System;
using System.Collections;
using System.Collections.Generic;
using core.Scripts.enemy_ai;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField] private float damage;
    private Collider2D _collider2D;
    // Start is called before the first frame update
    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out EnemyAI enemyAI))
        {
            enemyAI.TakeDamage(damage);
        }
        
    }
    
 
}
