using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Scene_Video : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        SceneController.Instance.audiosource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.time>14f)
        {
            SceneController.Instance._LoadScene();
        }
    }
}
