﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public int attackPower = 1;
    [SerializeField] float movementPeriod = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        GameObject.Find("Friendly_Base").GetComponent<PlayerBase>().TakeDamage(this);
        Destroy(gameObject);
    }
}
