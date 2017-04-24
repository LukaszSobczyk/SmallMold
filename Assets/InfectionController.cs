using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionController : MonoBehaviour {

    float infectionLevel;
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
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        infectionLevel+= .01f;
    }

    public void StopInfecting()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
