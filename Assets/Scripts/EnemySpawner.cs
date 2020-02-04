using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 3.0f;
    [SerializeField] int enemyCount = 10;
    int waveEnemyCount;
    [SerializeField] Text enemyCountText;

    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        waveEnemyCount = enemyCount;
        enemyCountText.text = waveEnemyCount.ToString();
        enemy = Resources.Load<GameObject>("Enemy");
        StartCoroutine(SpawnEnemy(enemy));
    }

    IEnumerator SpawnEnemy(GameObject enemy)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject spawn = Instantiate(enemy, transform.position, Quaternion.identity);
            spawn.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    public void UpdateEnemyCount()
    {
        waveEnemyCount--;
        enemyCountText.text = waveEnemyCount.ToString();
    }
}
