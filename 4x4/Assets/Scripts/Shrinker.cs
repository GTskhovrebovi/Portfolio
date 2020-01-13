using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
    public float shrinkspeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale - new Vector3 (1,1,1) * shrinkspeed * Time.deltaTime;
        if (transform.localScale.x < 0) Destroy(this.gameObject);
    }
}
