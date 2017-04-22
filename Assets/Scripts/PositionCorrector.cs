using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCorrector : MonoBehaviour {
    public GameObject pivotObject;
    public float yOffset;
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = new Vector3(pivotObject.transform.position.x, pivotObject.transform.position.y+yOffset, pivotObject.transform.position.z);
	}
}
