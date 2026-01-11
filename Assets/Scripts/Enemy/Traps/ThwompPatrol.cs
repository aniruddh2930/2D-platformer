using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompPatrol : MonoBehaviour
{
    [SerializeField] private Transform patrolPoint1;
    [SerializeField] private Transform patrolPoint2;
    [SerializeField] private Transform patrolPoint3;
    [SerializeField] private Transform patrolPoint4;
    [SerializeField] private float speed;
    [SerializeField] private float delay;
    private bool waiting;
    private int currentPatrolPoint=0;
    private Transform nextPatrolPoint;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    public Vector2 center;
    public Vector2 size;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        nextPatrolPoint = patrolPoint1;
    }
    private void Update()
    {
        if (waiting)
            return;
        if (Vector2.Distance(nextPatrolPoint.position, transform.position) < 0.1f)
        {
            box.enabled = true;
            waiting = true;
            rb.linearVelocity = Vector2.zero;
            currentPatrolPoint = (currentPatrolPoint + 1) % 4;
            ChoosePoint();
            StartCoroutine(EnablePatrol());
        }
        else
        {
            moveToNextPatrolPoint();
        }

    }

    private void ChoosePoint()
    {
        if (currentPatrolPoint == 0)
        {
            nextPatrolPoint = patrolPoint1;
        }
        else if (currentPatrolPoint == 1)
        {
            nextPatrolPoint = patrolPoint2;
        }
        else if (currentPatrolPoint == 2)
        {
            nextPatrolPoint = patrolPoint3;
        }
        else
        {
            nextPatrolPoint = patrolPoint4;
        }
    }

    private void moveToNextPatrolPoint()
    {
        rb.linearVelocity = (nextPatrolPoint.position - transform.position).normalized * speed;
    }
    
    private IEnumerator EnablePatrol()
    {
        yield return new WaitForSeconds(delay);
        waiting = false;
    }

    private void OnEnable()
    {
        center = box.bounds.center;
        size = box.bounds.size;
        box.enabled = false;
    }
}
