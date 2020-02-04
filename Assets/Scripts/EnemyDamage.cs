using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int health = 30; // TODO move so that different types of enemies can use this mover script
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;
    GameObject deathFX;
    GameObject hitFX;
    Transform parent;
    bool isExploded = false;

    Material baseMat;
    Material hitMat;

    private void Start()
    {
        deathFX = Resources.Load<GameObject>("deathFX");
        hitFX = Resources.Load<GameObject>("HitFX");
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
        GetComponent<AudioSource>().PlayOneShot(hitSFX);
        Turret turretType = attacker.GetComponentInParent<Turret>();
        health -= turretType.GetBaseDamage();
        StartCoroutine(FlashHit());

        GameObject fx = Instantiate(hitFX, transform.position + new Vector3(0f,9f,0f), Quaternion.identity);
        fx.transform.parent = GameObject.Find("SpawnedAtRuntime").transform;

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
        gameObject.GetComponentInChildren<Renderer>().material = mat;
    }

    private void KillEnemy()
    {
        if (!isExploded)
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            isExploded = true;
            FindObjectOfType<EnemySpawner>().UpdateEnemyCount();
        }
        Destroy(gameObject);
    }
}
