using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBomb : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameState != GameState.game)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < allEnemies.Length; i++)
            {
                if (allEnemies[i].GetComponent<EnemyType1>() != null)
                    allEnemies[i].GetComponent<EnemyType1>().Kill(true);
                if (allEnemies[i].GetComponent<EnemyType2>() != null)
                    allEnemies[i].GetComponent<EnemyType2>().Kill(true);
            }

            Destroy(gameObject);
        }
    }
}
