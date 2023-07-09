using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    #region 按钮
    [SerializeField] Button replay_btn;
    [SerializeField] Button quit_btn;
    [SerializeField] Button pause_start_btn;
    [SerializeField] Button pause_quit_btn;
    [SerializeField] GameObject start_text;
    [SerializeField] TextMeshProUGUI tip_text;
    #endregion
    #region 画板
    [SerializeField] GameObject dead_panel;
    [SerializeField] GameObject guest_panel;
    [SerializeField] GameObject pause_panel;
    [SerializeField] GameObject next_panel;
    [SerializeField] GameObject tip_panel;
    #endregion
    [SerializeField] GameObject canvas;
    public Custom custom;
    public AnimationClip intro_clip;

    bool if_paused;
    bool if_started = false;
    bool if_animed = false;
    bool if_next = false;

    public GameObject GuestBornPos;
    public GameObject stage;

    public Image intro_img;
    public TextMeshProUGUI intro_txt;
    [TextArea]
    public string intro;
    public Sprite sprite;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init_UI();
        Set_Btn();
        custom = Instantiate(custom, GuestBornPos.transform);
        InActive_Scene();
        //直接invoke，不用动画事件
        Invoke(nameof(Set_Animed), intro_clip.length);
    }

    private void Update()
    {
        if (if_animed && !if_paused)
        {
            Game_Ready();
        }
        if (!if_started)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (if_paused)
            {
                Game_Continued();
            }
            else
            {
                Game_Paused();
            }
            if_paused = !if_paused;
        }
    }

    #region 初始化相关
    private void Init_UI()
    {
        canvas = Instantiate(canvas);
        dead_panel = Instantiate(dead_panel, canvas.transform);
        guest_panel = Instantiate(guest_panel, canvas.transform);
        pause_panel = Instantiate(pause_panel, canvas.transform);
        next_panel = Instantiate(next_panel, canvas.transform);
        tip_panel = Instantiate(tip_panel, canvas.transform);

        canvas.name = "Canvas";
        dead_panel.name = "Dead_Panel";
        guest_panel.name = "Guest_Panel";
        pause_panel.name = "Pause_Panel";
        next_panel.name = "Next_Panel";
        tip_panel.name = "Tip_Panel";

        dead_panel.SetActive(false);
        pause_panel.SetActive(false);
        next_panel.SetActive(false);
        tip_panel.SetActive(false);
    }

    private void Set_Btn()
    {
        replay_btn = GameObject.Find("Canvas").transform.Find("Dead_Panel/Panel/replay_btn").GetComponent<Button>();
        quit_btn = GameObject.Find("Canvas").transform.Find("Dead_Panel/Panel/quit_btn").GetComponent<Button>();
        pause_start_btn = GameObject.Find("Canvas").transform.Find("Pause_Panel/Panel/replay_btn").GetComponent<Button>();
        pause_quit_btn = GameObject.Find("Canvas").transform.Find("Pause_Panel/Panel/quit_btn").GetComponent<Button>();

        start_text = GameObject.Find("Canvas").transform.Find("Guest_Panel/start_text").gameObject;
        intro_img = GameObject.Find("Canvas").transform.Find("Guest_Panel/People").GetComponent<Image>();
        intro_txt = GameObject.Find("Canvas").transform.Find("Guest_Panel/Intro").GetComponent<TextMeshProUGUI>();
        intro_txt.text = intro;
        intro_img.sprite = sprite;
        intro_img.SetNativeSize();
        tip_text = GameObject.Find("Canvas").transform.Find("Tip_Panel/Image/tip_text").GetComponent<TextMeshProUGUI>();

        replay_btn.onClick.AddListener(() => { if (SceneController.Instance == null) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); return; } SceneController.Instance._LoadScene(SceneManager.GetActiveScene().name);  });
        quit_btn.onClick.AddListener(() => { if (SceneController.Instance == null){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); return; } SceneController.Instance.LoadScene("Start_Game"); });
        pause_start_btn.onClick.AddListener(() => { Game_Continued(); if_paused = !if_paused; if (SoundController.Instance == null) return; SoundController.Instance.Play_Sfx("click"); });
        pause_quit_btn.onClick.AddListener(() => { Debug.Log(SceneController.Instance.name); SceneController.Instance._LoadScene("Start_Game");});
    }

    /// <summary>
    /// 初始化以及暂停场景
    /// </summary>
    private void InActive_Scene()
    {
        custom.gameObject.GetComponent<Animator>().enabled = false;
        custom.enabled = false;
        GameObject.Find("Player").GetComponent<Animator>().enabled = false;
        if (GameObject.Find("Butterfly"))
        {
            GameObject.Find("Butterfly").GetComponent<Animator>().enabled = false;
        }
    }
    private void Start_Scene()
    {
        custom.gameObject.GetComponent<Animator>().enabled = true;
        custom.enabled = true;
        GameObject.Find("Player").GetComponent<Animator>().enabled = true;
        if (GameObject.Find("Butterfly"))
        {
            GameObject.Find("Butterfly").GetComponent<Animator>().enabled = true;
        }
        GameObject.Find("Player").SetActive(true);
        guest_panel.SetActive(false);
        stage.SetActive(true);
        Time.timeScale = 1;
    }
    #endregion

    #region UI事件相关
    public void Dead()
    {
        if (dead_panel != null)
        {
            dead_panel.SetActive(true);
        }
        else
        {
            dead_panel = Instantiate(dead_panel);
        }
        Time.timeScale = 0;
        GameObject.Find("Player").GetComponent<Animator>().enabled = false;
    }

    public void Game_Paused()
    {
        pause_panel.SetActive(true);
        InActive_Scene();
        Time.timeScale = 0;
    }

    public void Game_Continued()
    {
        pause_panel.SetActive(false);
        Start_Scene();
    }

    public void Game_Ready()
    {
        if (Input.anyKeyDown)
        {
            Start_Scene();
            if_started = true;
            if (SoundController.Instance == null)
            {
                return;
            }
            SoundController.Instance.Play_Sfx("click");
        }
    }

    public void Game_Next()
    {
        if (SceneController.Instance == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneController.Instance._LoadScene();
        }
    }

    public void Set_Animed()
    {
        start_text.SetActive(true);
        if_animed = true;
    }
    #endregion
}
