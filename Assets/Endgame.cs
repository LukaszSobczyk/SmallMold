using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour {

    public GameObject endgame;
	void Update () {
        if(gameObject.GetComponent<SeedParticleSystem>()!=null)
            if (gameObject.GetComponent<SeedParticleSystem>().IsDone())
            {
                endgame.SetActive(true);
            }
	}
}
