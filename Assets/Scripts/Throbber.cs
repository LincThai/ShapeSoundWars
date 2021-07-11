using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throbber : MonoBehaviour
{
    public float scaleTime;
    public float maxSclae;

    void Update()
    {
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * maxSclae, Time.time % scaleTime);
    }
}
