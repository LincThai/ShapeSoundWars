using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public GameState gameState;

    public List<GameObject> enableOnGameStart;
    public List<GameObject> disableOnGameStart;
    public List<GameObject> enableOnDeath;

    private Gamepad gamepad = Gamepad.current;
    private GameObject player;
    private GameObject enemyManager;

    [HideInInspector] public int highScore;
    [HideInInspector] public int score;


    void Start()
    {
        gameState = GameState.preGame;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");

        for (int i = 0; i < enableOnGameStart.Count; i++)
            enableOnGameStart[i].SetActive(false);
    }

    void Update()
    {
        if (gameState == GameState.dead)
        {
            for (int i = 0; i < enableOnDeath.Count; i++)
                enableOnDeath[i].SetActive(true);
        }

        if ((gameState == GameState.preGame) || (gameState == GameState.dead))
            if (gamepad.aButton.isPressed)
            {
                player.GetComponent<Player>().isDead = false;
                enemyManager.GetComponent<EnemyManager>().timePassed = 0f;
                score = 0;

                for (int i = 0; i < disableOnGameStart.Count; i++)
                    disableOnGameStart[i].SetActive(false);

                for (int i = 0; i < enableOnGameStart.Count; i++)
                    enableOnGameStart[i].SetActive(true);

                gameState = GameState.game;
            }
            if (gamepad.bButton.isPressed)
            {
                Application.Quit();
            }
    }
}

public enum GameState { preGame, game, dead };
