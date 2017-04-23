using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject cameraPivot;
    GameObject player;
    float pitchMin = -80.0f;
    float pitchMax = 80.0f;
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        float angle = 0.0f;
        if (Input.GetAxis("Mouse X") != 0)
        {
            float x = 5 * Input.GetAxis("Mouse X");
            cameraPivot.transform.Rotate(0, x, 0, Space.World);
            //player.transform.Rotate(0, x, 0);
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            float y = 5 * -Input.GetAxis("Mouse Y");
            angle = Vector3.Angle(transform.forward, Vector3.Scale(transform.forward, new Vector3(1, 0, 1)));
            if (transform.forward.y > 0) angle = -angle;

            y = Mathf.Clamp(y, pitchMin - angle, pitchMax - angle);

            if ((angle < pitchMax && Input.GetAxis("Mouse Y") < 0) || (angle > pitchMin && Input.GetAxis("Mouse Y") > 0))
            {
                cameraPivot.transform.Rotate(y, 0, 0, Space.Self);
            }
        }
    }
}
