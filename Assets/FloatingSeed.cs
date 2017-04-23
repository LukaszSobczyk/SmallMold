using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSeed : MonoBehaviour {

    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;
    public float maxLifetime = 15;
    float timer = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if(maxLifetime < timer)
        {
            Destroy(gameObject);
        }
    }
}
