using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;

    private void Start()
    {
        objectToPan = GetComponentInChildren<Turret>().transform;
        print(objectToPan);
        targetEnemy = FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        objectToPan.LookAt(targetEnemy);
    }
}
