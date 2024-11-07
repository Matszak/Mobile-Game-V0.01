using System;
using System.Collections;
using System.Collections.Generic;
using core.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour, IDamageable
{
    [Header("Shoot Settings")]
    
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float BulletSpeed  = 10f;
    public float FireRate  = 0.5f;
    public float NumberOfBullets  = 1;
    public float FirePower = 1f;

    private float _nextFireTime = 0f;
    
    [Header("Health Settings")]
    
    [SerializeField] private int currentHealth,
        maxHealth,
        currentExperience,
        maxExperience,
        currentLevel;

    public Slider HealthBar;
    
    

    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        HealthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TryShoot(Vector2 aimDirection)
    {
        if (aimDirection.magnitude > 0.2f && Time.time > _nextFireTime)
        {
            Shoot(aimDirection);
            _nextFireTime = Time.time + FireRate;
        }
    }

    private void Shoot(Vector2 aimDirection)
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            var bulletScript = bulletInstance.GetComponent<bullet>();
            bulletScript.aimDirection = aimDirection;
            bulletScript.firePower = FirePower;
            bulletScript.bulletSpeed = BulletSpeed;
        }
    }
}
