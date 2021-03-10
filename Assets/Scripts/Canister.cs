﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canister : MonoBehaviour
{
    public float fullness = 1;
    public GameObject needle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        needle.transform.localRotation = Quaternion.identity;
        needle.transform.Rotate(0, 0, (1-fullness)*180);
    }
}