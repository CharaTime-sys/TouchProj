using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject Player;

    //控制角色手中石头显隐
    public void PlayerStoneOn(int n, GameObject ShowObj)
    {
        //Player.transform.GetChild(n).gameObject.SetActive(true);
        GameObject.Instantiate(ShowObj,Player.transform.GetChild(n));
    }

    public void PlayerStoneOff(int n)
    {
        Destroy(Player.transform.GetChild(n).GetChild(0).gameObject);
        //Player.transform.GetChild(n).gameObject.SetActive(false);
    }
}
