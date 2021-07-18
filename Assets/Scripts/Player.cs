using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;

    [HideInInspector] public bool isDead;

    private CharacterController playerController;

    private Vector3 previousRotationDirection = Vector3.forward;
    private Vector3 movementVector = new Vector3();
    private Vector3 directionVector = new Vector3();

    private Gamepad gamepad = Gamepad.current;
    public float stopValue;

    private GameManager gameManager;

    public GameObject deathParticles;
    public float deathParticlesKillTime;
    public float spawnProximity;

    private Vector3 playerStartLocation;

    public AudioSource audioMove;
    public AudioSource audioDeath;


    void Start()
    {
        isDead = false;
        playerController = gameObject.GetComponent<CharacterController>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerStartLocation = transform.position;
    }

    void Update()
    {
        if (gameManager.gameState == GameState.game)
        {
            //------------------------------------------------------------------------------ movement

            float moveX = gamepad.leftStick.ReadValue().x;
            float moveZ = gamepad.leftStick.ReadValue().y;

            Vector3 moveXVector = Vector3.right * moveX;
            Vector3 moveZVector = Vector3.forward * moveZ;

            movementVector = moveXVector + moveZVector;
            movementVector.y = 0f;

            if (movementVector.magnitude < stopValue)
                movementVector = Vector3.zero;
            else
                movementVector.Normalize();

            playerController.Move(movementVector * Time.deltaTime * moveSpeed);
            audioMove.Play();

            //------------------------------------------------------------------------------ rotation

            directionVector = new Vector3(gamepad.rightStick.ReadValue().x, 0f, gamepad.rightStick.ReadValue().y);

            if (directionVector.magnitude < 0.1f)
            {
                directionVector = previousRotationDirection;
            }

            directionVector = directionVector.normalized;
            previousRotationDirection = directionVector;

            transform.rotation = Quaternion.LookRotation(directionVector);
        }
    }

    public void Kill()
    {
        GameObject temp = Instantiate(deathParticles, transform.position, Quaternion.identity) as GameObject;
        audioDeath.Play();
        Destroy(temp, deathParticlesKillTime);
        gameManager.gameState = GameState.dead;
        isDead = true;
        transform.position = playerStartLocation;
    }
}
