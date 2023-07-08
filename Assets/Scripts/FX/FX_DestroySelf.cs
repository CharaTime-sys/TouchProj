using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_DestroySelf : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 2.0f);
    }
}
