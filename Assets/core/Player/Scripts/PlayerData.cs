using System;
using System.Collections;
using System.Collections.Generic;
using core.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerData : MonoBehaviour, IDamageable
{
    public static PlayerData instance;

    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;
    }
    
    [Header("Shoot Settings")]
    
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float BulletSpeed = 10f;
    public float FireRate = 0.5f;
    public float NumberOfBullets = 1;
    public float FirePower = 1f;
    [SerializeField] private TMP_Text lvlText;
    
    public AudioClip shootSound; // Dźwięk strzału
    private AudioSource _audioSource; // Komponent do odtwarzania dźwięków

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
        lvlText.text = currentLevel.ToString();
        currentHealth = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = currentHealth;
        
        // Pobierz komponent AudioSource (upewnij się, że jest dodany do obiektu)
        _audioSource = GetComponent<AudioSource>();
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
        // Odtwarzanie dźwięku strzału
        if (shootSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(shootSound);
        }

        for (int i = 0; i < NumberOfBullets; i++)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            var bulletScript = bulletInstance.GetComponent<bullet>();
            bulletScript.aimDirection = aimDirection;
            bulletScript.firePower = FirePower;
            bulletScript.bulletSpeed = BulletSpeed;
        }
    }

    public void GetExp(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= maxExperience)
        {
            currentExperience -= maxExperience;
            currentLevel++;
            LvlUP();
        }
    }
    
    private void LvlUP()
    {
        int i = Random.Range(0, 5);
        switch(i)
        {
            case 0:
                BulletSpeed *= 1.5f;
                break;
            case 1:
                FireRate /= 1.5f;
                break;
            case 2:
                maxHealth += 50;
                currentHealth += 15;
                break;
            case 3:
                FirePower += 1;
                break;
            default:
                gameObject.GetComponent<PlayerMovement>().speed += 1;
                break;
        }

        lvlText.text = currentLevel.ToString();
    }
}
