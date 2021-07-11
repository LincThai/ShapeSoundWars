using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform offsetFromPlayer;

    public float speed;

    void Start()
    {
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offsetFromPlayer.position, speed * Time.deltaTime);
    }
}
