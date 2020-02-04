using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Parameters
    [SerializeField] Transform objectToPan;
    [SerializeField] float range = 32.0f; // TODO consolidate tower stats into one script
    [SerializeField] ParticleSystem bullets;

    // State
    [SerializeField] float distanceToTarget;
    public Waypoint stationPoint;
    Transform targetEnemy;
    bool isFiring = false;

    private void Start()
    {
        objectToPan = GetComponentInChildren<Turret>().transform;
        if (FindObjectOfType<EnemyMover>())
        {
            targetEnemy = FindObjectOfType<EnemyMover>().transform;
        }
        bullets = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            distanceToTarget = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
            if (distanceToTarget < range)
            {
                Fire();
            }
            else
            {
                StopFiring();
            }
        }
        else
        {
            StopFiring();
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform closestEnemy, EnemyDamage testEnemy)
    {
        float distToTest = Vector3.Distance(transform.position, testEnemy.transform.position);
        float distToClosest = Vector3.Distance(transform.position, closestEnemy.position);
        if (distToTest < distToClosest)
        {
            return testEnemy.transform;
        }
        else
        {
            return closestEnemy;
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
        if (!isFiring)
        {
            bullets.Play();
            isFiring = true;
        }
    }
}
