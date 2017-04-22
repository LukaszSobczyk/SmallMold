using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
    }
	
	void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputResult(Input.mousePosition);
        }
    }
    void InputResult(Vector3 inputPos)
    {
        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(inputPos);
        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
    }
}
