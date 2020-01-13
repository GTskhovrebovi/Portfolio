using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public int damageLevel;
    public int critLevel;
    public int cdReductionLevel;
    public int castTimeLevel;

    public float aoeRadius;
    public float aoeTickInterval;
    public float aoeDuration;
    public float aoeAttackCastTime;

    public int points;

    public int currentLevel;


    public Stats()
    {
        damageLevel = 1;
        critLevel = 1;
        cdReductionLevel = 1;
        castTimeLevel = 1;

        aoeRadius = 2f;
        aoeTickInterval = 0.4f;
        aoeDuration = 4f;
        aoeAttackCastTime = 1f;
        
        points = 0;
        currentLevel = 1;
    }

    public float tapDamage
    {
        get
        {
            return damageLevel ;
        }
    }

    public float heavyTapDamage
    {
        get
        {
            return damageLevel * 4;
        }
    }

    public float aoeTickDamage
    {
        get
        {
            return damageLevel;
        }
    }
    
    public float heavyAttackCooldown
    {
        get
        {
            return 1f;
        }
    }




}
