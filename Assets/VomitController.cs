using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitController : MonoBehaviour {

    GameObject product;
	// Use this for initialization
	void Start () {
        product = null;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Infectable")
            product = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        product = null;
    }
    
    private void FixedUpdate()
    {
        if (product != null)
        {
            product.gameObject.GetComponent<InfectionController>().Infect();
        }
    }
    void StartVomit()
    {
        //TO DO Particle play
    }
}
