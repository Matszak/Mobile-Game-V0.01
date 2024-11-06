using System;
using System.Collections;
using System.Collections.Generic;
using core.Scripts;
using UnityEngine;

public class EnemyBaseBullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    protected float bulletSpeed;

    [SerializeField] protected float damage;
    [SerializeField] protected float lifeTime;
 

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody2D.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(DestroyBullet());
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out PlayerData player))
        {
            player.TakeDamage(damage);
        }
        
        Destroy(this);
    }
 
}
