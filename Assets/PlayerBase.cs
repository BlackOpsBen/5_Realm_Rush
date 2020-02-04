using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    GameObject explosion;
    [SerializeField] int health = 10;
    [SerializeField] Text healthText;

    Material baseMat;
    Material hitMat;
    private void Start()
    {
        healthText.text = health.ToString();
        explosion = Resources.Load<GameObject>("BaseExplosion");
        baseMat = Resources.Load<Material>("Voxel Material");
        hitMat = Resources.Load<Material>("HitFlashMat");
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

    public void TakeDamage(EnemyMover enemy)
    {
        health -= enemy.attackPower;
        healthText.text = health.ToString();
        StartCoroutine(FlashHit());
        if (health <= 0)
        {
            DestroyBase();
        }
    }

    private void DestroyBase()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
