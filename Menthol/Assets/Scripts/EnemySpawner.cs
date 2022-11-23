using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool spawnable;
    private GameObject enemyPrefab;

    private void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemys/Enemy");

        StartCoroutine(Delay(2));
    }

    private float GetSpawnPeriod()
    {
        // Calc by difficulty.
        return 2.0f;
    }

    private void Update()
    {
        if (spawnable)
        {
            Spawn();
            StartCoroutine(Delay(GetSpawnPeriod()));
        }
    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.transform.position = transform.position;
        enemy.transform.Rotate(0, Random.Range(0.0f, 1.0f) * 360, 0);
        enemy.transform.Translate(enemy.transform.forward * 20, Space.World);

        spawnable = false;
    }
    protected IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnable = true;
    }
}
