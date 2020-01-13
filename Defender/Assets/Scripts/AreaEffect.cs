using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float duration;
    public float damage;
    public float damageInterval;

    float damageTimer;

    [SerializeField] ContactFilter2D contactFilter;

    List<Collider2D> contacts;

    private void Awake() 
    {
        damageTimer = 0;
        contacts = new List<Collider2D>();
    }
    void Start()
    {
        Destroy(this.gameObject, duration);
    }

    void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= damageInterval)
        {
            GetComponent<Collider2D>().OverlapCollider(contactFilter,contacts);

            foreach(Collider2D contact in contacts)
            {
                Health health = contact.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }

            damageTimer = 0;
        }
    }


}
