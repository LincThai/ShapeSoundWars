using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemySpawners;
    public List<float> enemySpawnerInactiveTimers;
    public List<float> enemySpawnerActiveTimers;
    public List<float> enemySpawnerOffsetTimers;

    [HideInInspector] public float timePassed;

    void Start()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].SetActive(false);
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        for (int i = 0; i < enemySpawners.Count; i++)
        {
            if (timePassed > enemySpawnerOffsetTimers[i])
            {
                enemySpawners[i].SetActive(true);

                if (((timePassed - enemySpawnerOffsetTimers[i]) % (enemySpawnerActiveTimers[i] + enemySpawnerInactiveTimers[i])) < enemySpawnerInactiveTimers[i])
                    enemySpawners[i].SetActive(false);
            }
        }
    }
}
