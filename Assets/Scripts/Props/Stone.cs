using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum StoneType
{
    L1,
    L2,
    R1,
    R2
}


public class Stone : MonoBehaviour
{
    public StoneType stype;

    public bool isPicked = false;

    void Update()
    {
    }
}
