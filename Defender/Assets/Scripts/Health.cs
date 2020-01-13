using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] HealthBar healthBar;
    [SerializeField] DamageTextSpawner damageTextSpawner;


    public Action<GameObject> Died;

    private void Awake() 
    {
        health = maxHealth; 
    }

    private void Start() 
    {
        healthBar.SetHealth(GetHealthPercent());
    }


    public void TakeDamage(float amount)
    {
        damageTextSpawner.Spawn(amount);
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        healthBar.SetHealth(GetHealthPercent());
    }

    private void Die()
    {
        Died(this.gameObject);
        Destroy(this.gameObject);
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }

}
