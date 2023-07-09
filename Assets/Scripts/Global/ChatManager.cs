using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    public int ChatCount = 0;

    public GameObject QPlayer;
    public GameObject ChatPop1;
    public GameObject ChatPop2;
    public GameObject ChatPop3;

    public System.Action KeepInLevel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Register_Level();
    }

    private void Register_Level()
    {
        switch (SceneController.Instance.Get_Level_name())
        {
            case "Introduction":
                KeepInLevel += (() => { SceneController.Instance._LoadScene(); });
                break;
            case "Level_1_0":
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && QPlayer != null)
        {
            QPlayer.GetComponent<Animator>().SetTrigger("SpeakTrigger");
        }
    }

    public void Chat()
    {
        if(ChatCount == 0)
        {
            ChatPop1.SetActive(true);
            ChatCount++;
        }
        else if (ChatCount == 1)
        { 
            ChatPop1.SetActive(false);
            ChatPop2.SetActive(true);
            ChatCount++;
        }
        else if (ChatCount == 2)
        {
            ChatPop2.SetActive(false);
            ChatPop3.SetActive(true);
            ChatCount++;
        }
        else if (ChatCount == 3)
        {
            ChatPop3.SetActive(false);
            QPlayer.GetComponent<Animator>().SetTrigger("QuitTrigger");
            //¹Ø¿¨Âß¼­¼ÌÐø
            KeepInLevel();
        }
    }
}
