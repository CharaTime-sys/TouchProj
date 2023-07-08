using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
}
