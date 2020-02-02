using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 3.0f;
    [SerializeField] int enemyCount = 10;

    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load<GameObject>("Enemy");
        StartCoroutine(spawnEnemy(enemy));
    }

    IEnumerator spawnEnemy(GameObject enemy)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            GameObject spawn = Instantiate(enemy, transform.position, Quaternion.identity);
            spawn.transform.parent = gameObject.transform;
        }
    }
}
