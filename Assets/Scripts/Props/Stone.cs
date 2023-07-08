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
                    isPicked = true;
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
                    isPicked = true;
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

                }
                else
                {

                }
            }
        }
        else if (stype == StoneType.R2)
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                if (isPicked)
                {

                }
                else
                {

                }
            }
        }
    }
}
