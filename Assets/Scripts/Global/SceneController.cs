using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public Image progress_sp;
    public GameObject progress_text;
    public GameObject Load_Panel;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void _LoadScene(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    public IEnumerator LoadScene(string name)
    {
        Load_Panel.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            progress_sp.material.SetFloat("_Load", asyncOperation.progress/0.9f);
            if (asyncOperation.progress >= 0.9f)
            {
                progress_text.SetActive(true);
                if (Input.anyKeyDown)
                {
                    Load_Panel.SetActive(false);
                    progress_text.SetActive(false);
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
}
