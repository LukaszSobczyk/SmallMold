using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionController : MonoBehaviour {

    enum InfectionState
    {
        None,
        Infecting
    }
    InfectionState state;
    float infectionLevel;
    float infectionTimer = 2.0f;
    float timer = 0;
    // Use this for initialization
    void Start () {
        infectionLevel = 0;
        state = InfectionState.None;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    public void Infect()
    {
        //Debug.Log(infectionLevel);
        if (GetComponentInChildren<ParticleSystem>() != null && state == InfectionState.None)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            state = InfectionState.Infecting;
        }

        infectionLevel+= .01f;
        MoldingScript.ChangeMold(GetComponent<Renderer>(), infectionLevel, infectionLevel);
    }

    public void StopInfecting()
    {
        if(GetComponentInChildren<ParticleSystem>() != null)
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        state = InfectionState.None;
    }
}
