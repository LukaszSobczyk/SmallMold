using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckerCup : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); 

        if (Physics.Raycast(transform.position, forward, 10))
        {

        }
    }
}
