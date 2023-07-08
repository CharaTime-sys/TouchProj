using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public static MagicManager instance;

    public float MagicTimer = 3.0f;

    public bool MagicOn = false;

    public float MagicStartTime = 0;

    public GameObject Player;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(MagicOn)
        {
            if(Time.time - MagicStartTime > MagicTimer)
            {
                Player.GetComponent<Animator>().SetTrigger("EndTrigger");
                MagicStop();
            }
        }
    }

    public void MagicStart()
    {
        MagicOn = true;
        MagicStartTime = Time.time;
    }

    public void MagicStop()
    {
        MagicOn = false;
        MagicStartTime = 0;
        Player.GetComponent<PlayerHandInfo>().ClearLeftHand();
        Player.GetComponent<PlayerHandInfo>().ClearRightHand();
    }


}
