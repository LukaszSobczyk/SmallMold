using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSeed : MonoBehaviour {

    enum SeedState
    {
        Falling,
        Seeding
    }

    SeedState state;
    Vector3 lockTransform;
    public float maxLifetime = 15;
    float timer = 0;
	// Use this for initialization
	void Start () {
        state = SeedState.Falling;
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Infectable" || other.gameObject.tag == "Enviroment") && state == SeedState.Falling)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            state = SeedState.Seeding;
            lockTransform = gameObject.transform.position;
        }
    }

    public void Update()
    {
        if(state == SeedState.Seeding)
            this.gameObject.transform.position = lockTransform;
        else
        {
            timer += Time.fixedDeltaTime;
            if (timer > maxLifetime)
                Destroy(gameObject);
        }
    }
}
