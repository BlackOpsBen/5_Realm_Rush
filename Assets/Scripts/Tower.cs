using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float range = 32.0f; // TODO consolidate tower stats into one script
    [SerializeField] float distanceToTarget;
    [SerializeField] ParticleSystem bullets;
    bool isFiring = false;

    private void Start()
    {
        objectToPan = GetComponentInChildren<Turret>().transform;
        targetEnemy = FindObjectOfType<EnemyMover>().transform;
        bullets = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            distanceToTarget = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
            if (distanceToTarget < range)
            {
                Fire();
                Debug.Log("Fire initiated");
            }
            else
            {
                Debug.LogWarning("HOLDING FIRE");
                StopFiring();
            }
        }
        else
        {
            StopFiring();
        }
    }

    private void StopFiring()
    {
        if (isFiring)
        {
            bullets.Stop();
            isFiring = false;
        }
    }

    private void Fire()
    {
        Debug.Log("Firing");
        if (!isFiring)
        {
            bullets.Play();
            isFiring = true;
        }
    }
}
