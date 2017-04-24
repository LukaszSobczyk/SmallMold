using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCorrector : MonoBehaviour {
    public Transform player;
    private Vector3 offset;
    // Update is called once per frame

    private void Awake()
    {
        offset = transform.position - player.position;
    }

    void Update ()
    {
        transform.position = player.transform.position + offset;

	}
}
