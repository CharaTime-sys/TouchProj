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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
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
            ChatPop2.SetActive(true);
            ChatCount++;
        }
        else if (ChatCount == 2)
        {
            ChatPop3.SetActive(true);
            ChatCount++;
        }
        else if (ChatCount == 3)
        {
            //³¡¾°Ìø×ª
        }
    }
}
