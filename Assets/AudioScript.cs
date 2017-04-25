using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
