using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text highScoreText;
    public Text scoreText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        scoreText.text = gameManager.score.ToString();

        if (gameManager.gameState == GameState.dead)
        {

            if (gameManager.score > gameManager.highScore)
            {
                highScoreText.text = gameManager.score.ToString();
            }
        }
    }
}
