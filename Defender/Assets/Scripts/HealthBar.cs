using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] Image damagedBarImage;

    [SerializeField] float shrinkSpeed;
    [SerializeField] float shrinkTime;
    float shrinkTimer;

    private void Start() 
    {
        //shrinkTimer = shrinkTime;
    }

    private void Update() 
    {
        shrinkTimer -= Time.deltaTime;
        if (shrinkTimer <= 0)
        {
            if (barImage.fillAmount < damagedBarImage.fillAmount)
            {
                damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }    
    }

    public void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
        shrinkTimer = shrinkTime;
    }

}
