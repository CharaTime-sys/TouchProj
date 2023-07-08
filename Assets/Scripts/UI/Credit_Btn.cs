using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit_Btn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { SceneController.Instance._LoadScene("Start_Game"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
