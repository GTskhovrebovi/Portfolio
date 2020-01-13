using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float movementSpeed;
    [SerializeField] float attackRange;

    void Start()
    {
        movementSpeed *= Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        if (target != null)
        {
            AutoMove();
        }
    }

    private void AutoMove()
    {
        if ((target.position - transform.position).magnitude > attackRange)
        {
            transform.Translate((target.position - transform.position).normalized * movementSpeed * Time.deltaTime);
        }
        else
        {
            
        }

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
