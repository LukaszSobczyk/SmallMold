using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionController : MonoBehaviour {

    int infectionLevel;
    float infectionTimer = 2.0f;
    float timer = 0;
    // Use this for initialization
    void Start () {
        infectionLevel = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    public void Infect()
    {
        if (infectionTimer > timer)
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            timer = 0;
            Debug.Log("infeted");
        }
    }
}
