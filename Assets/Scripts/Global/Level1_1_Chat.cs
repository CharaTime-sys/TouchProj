using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_1_Chat : MonoBehaviour
{
    public static Level1_1_Chat instance;

    public GameObject QPlayer;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SummonQPlayer()
    {
        QPlayer.SetActive(true);
    }

    private void Update()
    {
        
    }
}
