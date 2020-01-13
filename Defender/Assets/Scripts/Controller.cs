using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    public Stats stats;

    [SerializeField] LayerMask enemyLayerMask;

    [SerializeField] GameObject heavyAttackPrefab;
    [SerializeField] GameObject areaEffectPrefab;
    
    bool canCastAoe;

    float heavyAttackTimer;
    bool heavyAttackReady;

    Vector3 clickPos;
    float clickDuration = 0f;

    private void Awake() 
    {
                
    }

    void Start()
    {
        heavyAttackTimer = stats.heavyAttackCooldown;
    }

    void Update()
    {
        if (!heavyAttackReady)
        {
            heavyAttackTimer -= Time.deltaTime;
            if (heavyAttackTimer < 0) 
            {
                heavyAttackTimer = 0;
                heavyAttackReady = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            canCastAoe = true;
            clickDuration = 0;
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;
            
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero,100, enemyLayerMask);


            
            if (hit.collider != null)
            {
                if (heavyAttackReady)
                {
                    GameObject heavyAttackParticle = Instantiate(heavyAttackPrefab, clickPos, Quaternion.identity);
                    Destroy(heavyAttackParticle, heavyAttackParticle.GetComponent<ParticleSystem>().main.duration);
                    hit.collider.GetComponent<Health>().TakeDamage(stats.heavyTapDamage);
                    heavyAttackTimer = stats.heavyAttackCooldown;
                    heavyAttackReady = false;
                }
                else
                {
                    hit.collider.GetComponent<Health>().TakeDamage(stats.tapDamage);
                }
            }

 
        }

        if (Input.GetMouseButton(0))
        {
            clickDuration += Time.deltaTime;
            if (clickDuration > stats.aoeAttackCastTime && canCastAoe)
            {
                AreaEffect aoe = Instantiate(areaEffectPrefab,clickPos, Quaternion.identity).GetComponent<AreaEffect>();
                aoe.damage = stats.aoeTickDamage;
                aoe.damageInterval = stats.aoeTickInterval;
                aoe.duration = stats.aoeDuration;
                aoe.transform.localScale *= stats.aoeRadius;
                clickDuration = 0;
                canCastAoe = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickDuration = 0;
            canCastAoe = true;
        }
    }
}
