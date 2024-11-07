using System;
using System.Collections;
using System.Collections.Generic;
using core.Scripts.enemy_ai;
using UnityEngine;
using UnityEngine.Serialization;

public class bullet : MonoBehaviour
{
    public Vector2 aimDirection;
    public float firePower = 1f;
    public float bulletSpeed;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float damage;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _rigidbody2D.AddForce(aimDirection  * bulletSpeed  , ForceMode2D.Impulse );
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out EnemyAI enemyAI))
        {
            enemyAI.TakeDamage(firePower);
            Destroy(gameObject);
        }
         
    }
    
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this);
    }

}
