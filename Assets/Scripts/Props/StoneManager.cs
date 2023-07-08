using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    public static StoneManager instance;

    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
