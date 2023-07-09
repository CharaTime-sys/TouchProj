using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_1_Chat : MonoBehaviour
{
    public static Level1_1_Chat instance;

    public GameObject QPlayer;
    public GameObject Stones;
    public GameObject Player;

    public int tipCount = 0;

    public GameObject tipPop1;
    public GameObject tipPop2;
    public GameObject tipPop3;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        ChatManager.instance.KeepInLevel += LevelAfterChat;
    }

    public void LevelAfterChat()
    {
        //出现石头
        Stones.SetActive(true);
        //出现主角
        Player.SetActive(true);
        //出现提示
        TipsTools();
        
    }

    public void SummonQPlayer()
    {
        QPlayer.SetActive(true);
    }

    public void TipsTools()
    {
        if(tipCount == 0)
        {
            tipPop1.SetActive(true);
            tipCount++;
        }
        else if(tipCount == 1)
        {
            tipPop1.SetActive(false);
            tipPop2.SetActive(true);
            LevelController1_1.Instance.Game_Next();
            tipCount++;
        }
        else if (tipCount == 2)
        {
            tipPop2.SetActive(false);
            tipPop3.SetActive(true);
            tipCount++;
        }
        else if (tipCount == 3)
        {
            tipPop3.SetActive(false);
            //通关
        }
    }

    private void Update()
    {
        
    }
}
