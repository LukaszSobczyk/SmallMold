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
    public int LevelToInfect = 0;
    int maxSeedSpawn = 200;
    int SpawnRate = 30;
    public ParticleSystem ps;
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
        if (GetComponentInChildren<ParticleSystem>() != null && state == InfectionState.None && !ps.GetComponent<SeedParticleSystem>().IsDone())
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            state = InfectionState.Infecting;
        }
        if(ps.GetComponent<SeedParticleSystem>().IsDone())
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            Debug.Log("Done");
        }


        //infectionLevel+= .01f;
        MoldingScript.ChangeMold(GetComponent<Renderer>(), ps.GetComponent<SeedParticleSystem>().GetSpawnAmount()/100.0f, ps.GetComponent<SeedParticleSystem>().GetSpawnAmount() / 100.0f);
    }

    public void StopInfecting()
    {
        if(GetComponentInChildren<ParticleSystem>() != null)
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        state = InfectionState.None;
    }
}
