using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int health = 30; // TODO move so that different types of enemies can use this mover script
    GameObject deathFX;
    Transform parent;
    bool isExploded = false;

    Material baseMat;
    Material hitMat;

    private void Start()
    {
        deathFX = Resources.Load<GameObject>("deathFX");
        parent = GameObject.Find("SpawnedAtRuntime").transform;
        baseMat = Resources.Load<Material>("Voxel Material");
        hitMat = Resources.Load<Material>("HitFlashMat");
    }

    private void OnParticleCollision(GameObject attacker)
    {
        TakeDamage(attacker);
    }

    private void TakeDamage(GameObject attacker)
    {
        Turret turretType = attacker.GetComponentInParent<Turret>();
        health -= turretType.GetBaseDamage();
        StartCoroutine(FlashHit());

        if (health <= 0)
        {
            KillEnemy();
        }
    }

    IEnumerator FlashHit()
    {
        ChangeMat(hitMat);
        yield return new WaitForSeconds(0.05f);
        ChangeMat(baseMat);
    }

    private void ChangeMat(Material mat)
    {
        gameObject.GetComponent<Renderer>().material = mat;
    }

    private void KillEnemy()
    {
        if (!isExploded)
        {
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            isExploded = true;
        }
        Destroy(gameObject);
    }
}
