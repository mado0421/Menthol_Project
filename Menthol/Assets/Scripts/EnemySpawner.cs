using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool spawnable;
    private GameObject enemyPrefab;
    private GameObject spacialEnemyPrefab;

    [SerializeField]
    private float SpecialEnemySpawnPeriod = 30f;
    private float remainTimeToSpawnSpecial;
    private bool readyToSpawnSpecial;

    private void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemys/Enemy");
        spacialEnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemys/SpecialEnemy");

        StartCoroutine(Delay(2));
        remainTimeToSpawnSpecial = SpecialEnemySpawnPeriod;
        readyToSpawnSpecial = false;
    }

    private float GetSpawnPeriod()
    {
        // Calc by difficulty.
        return 2.0f * (3 - Mathf.Log(2, GameManager.Instance.CurrTime));

        //return 2.0f;
    }

    private void Update()
    {
        remainTimeToSpawnSpecial -= Time.deltaTime;
        if(remainTimeToSpawnSpecial < 0)
        {
            readyToSpawnSpecial = true;
            remainTimeToSpawnSpecial = SpecialEnemySpawnPeriod;
        }

        if (spawnable)
        {
            Spawn();
            StartCoroutine(Delay(GetSpawnPeriod()));
        }
    }

    private void Spawn()
    {
        GameObject enemy;
        if (readyToSpawnSpecial) { enemy = Instantiate(spacialEnemyPrefab, transform); readyToSpawnSpecial = false; }
        else enemy = Instantiate(enemyPrefab, transform);
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
