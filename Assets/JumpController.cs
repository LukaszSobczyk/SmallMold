using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(Input.GetButton("Jump"))
        {
            this.GetComponent<Rigidbody>().AddForce(0, 30, 0);
        }
        float moveHorizontal = Input.GetAxis("Horizontal") * 10.0f;
        float moveVertical = Input.GetAxis("Vertical") * 10.0f;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(moveHorizontal != 0 || moveVertical !=0)
            this.GetComponent<Rigidbody>().AddForce(movement);
    }
}
