﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
    }
}
