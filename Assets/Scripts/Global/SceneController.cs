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
    public Texture2D cursor_tex;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.SetCursor(cursor_tex, new Vector2(0f, 0f), CursorMode.Auto);
    }

    public void _LoadScene(string name)
    {
        SoundController.Instance.Play_Sfx("click");
        StartCoroutine(LoadScene(name));
    }

    public void _LoadScene()
    {
        SoundController.Instance.Play_Sfx("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadScene(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;
        GameObject.Find("Canvas").SetActive(false);

        while (!asyncOperation.isDone)
        {
            progress_sp.material.SetFloat("_Load", asyncOperation.progress/0.9f);
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public static void ExitGame()
    {
        Application.Quit();
    }

    public string Get_Level_name()
    {
        return SceneManager.GetActiveScene().name;
    }
}
