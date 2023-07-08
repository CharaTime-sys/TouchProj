using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject dead_panel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void Dead()
    {
        if (dead_panel!=null)
        {
            dead_panel.SetActive(true);
        }
        else
        {
            Instantiate(dead_panel);
        }
        Time.timeScale = 0;
    }
}
