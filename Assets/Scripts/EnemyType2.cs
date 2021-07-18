using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : MonoBehaviour
{
    public float speed;
    public GameObject deathParticles;
    public float deathParticlesKillTime;

    public float wallcollisionRange;
    private GameManager gameManager;
    private Player player;

    public float lagTime;
    private Vector3 aimAtLocation = new Vector3();
    private Vector3 prevAimAtLocation = new Vector3();
    private float timePassed;

    public int pointValue;

    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        timePassed = lagTime;
        audioSource = GetComponent<audioSource>();
    }

    void Update()
    {
        if (timePassed >= lagTime)
        {
            prevAimAtLocation = aimAtLocation;
            aimAtLocation = player.gameObject.transform.position;
            timePassed = 0f;
        }
        Vector3 tempLookAt = Vector3.Lerp(prevAimAtLocation, aimAtLocation, 0.5f);

        transform.LookAt(tempLookAt);
        timePassed += Time.deltaTime;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        DetectWall();

        if (gameManager.gameState != GameState.game)
            Kill(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Kill();
        }
        if (other.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);
            Kill(true);
        }
    }

    public void Kill(bool giveScore)
    {
        if (giveScore)
            gameManager.score += pointValue;
        GameObject temp = Instantiate(deathParticles, transform.position, Quaternion.identity) as GameObject;
        Destroy(temp, deathParticlesKillTime);
        audioSource.Play();
        Destroy(gameObject);
    }

    void DetectWall()
    {
        RaycastHit hitInfo;

        Debug.DrawRay(transform.position, transform.forward * wallcollisionRange, Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, wallcollisionRange))
        {
            if (hitInfo.collider.gameObject.tag == "Wall")
            {
                Debug.DrawRay(hitInfo.point, Vector3.Reflect(transform.forward, hitInfo.normal) * wallcollisionRange, Color.yellow);
                Reorient(Vector3.Reflect(transform.forward, hitInfo.normal));
            }
        }
    }

    void Reorient(Vector3 newDirection)
    {
        transform.rotation = Quaternion.LookRotation(newDirection);

        //this is honestly a bit of a hack to stop the "getting out of the world" bug
        //for some reason "LookRotation" sometimes results in non-zero values on the x or z axes
        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
    }
}
