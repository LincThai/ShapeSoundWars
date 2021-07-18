using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Weapon : MonoBehaviour
{
    public GameObject projectileObject;

    public float refireRate;
    private float timePassed;
    private bool canShoot;

    private Gamepad gamepad = Gamepad.current;
    public float shootThreshold;

    private GameManager gameManager;

    private AudioSource audioSource;

    private void Start()
    {
        canShoot = true;
        timePassed = 0.0f;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameManager.gameState == GameState.game)
        {
            if ((Vector3.Magnitude(gamepad.rightStick.ReadValue()) > shootThreshold) && (canShoot))
            {
                Shoot();
                canShoot = false;
                timePassed = 0.0f;
            }

            timePassed += Time.deltaTime;

            if (timePassed >= refireRate)
            {
                canShoot = true;
            }
        }
    }

    private void Shoot()
    {
        if (!transform.root.GetComponent<Player>().isDead)
        {
            Instantiate(projectileObject, transform.position, transform.rotation);
            audioSource.Play();
        }
    }

}
