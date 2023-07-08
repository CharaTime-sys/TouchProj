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

    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;


    void Update()
    {
        if(stype == StoneType.L1)
        {
            if(Input.GetKeyUp(KeyCode.D))
            {
                if (isPicked)
                {
                    isPicked = false;
                    EventManager.instance.PlayerStoneOff(1);
                    EventManager.instance.PlayerStoneOff(3);
                }
                else
                {
                    EventManager.instance.PlayerStoneOff(1);
                    EventManager.instance.PlayerStoneOff(3);
                    isPicked = true;
                    s2.GetComponent<Stone>().isPicked = false;
                    var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone2.prefab", typeof(GameObject)) as GameObject;
                    EventManager.instance.PlayerStoneOn(1, go);
                    EventManager.instance.PlayerStoneOn(3, go);
                }
            }
        }
        else if(stype == StoneType.L2)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (isPicked)
                {
                    isPicked = false;
                    EventManager.instance.PlayerStoneOff(1);
                    EventManager.instance.PlayerStoneOff(3);
                }
                else
                {
                    EventManager.instance.PlayerStoneOff(1);
                    EventManager.instance.PlayerStoneOff(3);
                    isPicked = true;
                    s1.GetComponent<Stone>().isPicked = false;
                    var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone1.prefab", typeof(GameObject)) as GameObject;
                    EventManager.instance.PlayerStoneOn(1, go);
                    EventManager.instance.PlayerStoneOn(3, go);
                }
            }
        }
        else if (stype == StoneType.R1)
        {
            if (Input.GetKeyUp(KeyCode.J))
            {
                if (isPicked)
                {
                    isPicked = false;
                    EventManager.instance.PlayerStoneOff(2);
                    EventManager.instance.PlayerStoneOff(4);
                }
                else
                {
                    EventManager.instance.PlayerStoneOff(2);
                    EventManager.instance.PlayerStoneOff(4);
                    isPicked = true;
                    s4.GetComponent<Stone>().isPicked = false;
                    var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone3.prefab", typeof(GameObject)) as GameObject;
                    EventManager.instance.PlayerStoneOn(2, go);
                    EventManager.instance.PlayerStoneOn(4, go);
                }
            }
        }
        else if (stype == StoneType.R2)
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                if (isPicked)
                {
                    isPicked = false;
                    EventManager.instance.PlayerStoneOff(2);
                    EventManager.instance.PlayerStoneOff(4);
                }
                else
                {
                    EventManager.instance.PlayerStoneOff(2);
                    EventManager.instance.PlayerStoneOff(4);
                    isPicked = true;
                    s3.GetComponent<Stone>().isPicked = false;
                    var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone4.prefab", typeof(GameObject)) as GameObject;
                    EventManager.instance.PlayerStoneOn(2, go);
                    EventManager.instance.PlayerStoneOn(4, go);
                }
            }
        }
    }
}
