using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySound : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
