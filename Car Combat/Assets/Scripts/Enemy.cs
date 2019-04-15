﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] WayPoint[] waypoints;

    public NavMeshAgent agent;

    int currentWaypoint = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetComponent<EnemyShoot>().Firing());
        waypoints = FindObjectsOfType<WayPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 target = waypoints[currentWaypoint].transform.position;
        Vector3 proximity = target - transform.position;
        agent.SetDestination(target);

        if (proximity.magnitude < 0.5)
        {
            currentWaypoint++;
        }
    }
}
