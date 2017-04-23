using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPickup : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<SeedScoreSystem>().AddSeed();
            Destroy(gameObject);
        }
    }
}
