using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    public AudioSource audiosource;

    public AudioClip clip;
    #region “Ù–ß
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void Play_Sfx(string name)
    {
        AudioClip target_clip = clip;
        //AudioClip target_clip = AssetDatabase.LoadAssetAtPath("Assets/Audio/" + name+".wav", typeof(AudioClip)) as AudioClip;
        audiosource.PlayOneShot(target_clip);
    }
}
