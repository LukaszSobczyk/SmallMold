using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float forcefactor = 30.0f;
    private Rigidbody rigi;
    // Use this for initialization
    void Start ()
    {
        rigi = this.GetComponent<Rigidbody>();
    }
    private Vector3 GetMeshColliderNormal(RaycastHit hit)
    {
        MeshCollider collider = (MeshCollider)hit.collider;
        Mesh mesh = collider.sharedMesh;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;


        Vector3 n0 = normals[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[hit.triangleIndex * 3 + 2]];
        Vector3 baryCenter = hit.barycentricCoordinate;
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        interpolatedNormal.Normalize();
        interpolatedNormal = hit.transform.TransformDirection(interpolatedNormal);
        return interpolatedNormal;

    }
    void Update ()
    {
        //Vector3 moveDirection;
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection = transform.TransformDirection(moveDirection);
        //this.rigidbody
        Vector3 movement=new Vector3();
        if(Input.GetAxis("Horizontal")!=0)
        {
            if(Input.GetAxis("Horizontal")* rigi.velocity.x<0)
            {
                rigi.velocity = new Vector3(0.0f, rigi.velocity.y, rigi.velocity.z);
            }
            movement.x = Input.GetAxis("Horizontal");
        }
        else
        {
            rigi.velocity = new Vector3(0.0f,rigi.velocity.y, rigi.velocity.z);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") * rigi.velocity.z < 0)
            {
                rigi.velocity = new Vector3(rigi.velocity.x, rigi.velocity.y, 0.0f);
            }
            movement.z = Input.GetAxis("Vertical");
        }
        else
        {
            rigi.velocity = new Vector3(rigi.velocity.x, rigi.velocity.y, 0.0f);
        }
        rigi.AddForce(movement * forcefactor*Time.fixedDeltaTime);
        //Debug.Log(movement);
    }
    void OnTriggerStay(Collider coll)
    {
        if(coll.CompareTag("Enviroment")&& Input.GetButton("Jump"))
        {

            this.GetComponent<Rigidbody>().AddForce(0, 30, 0);
        }
    }
    void InputResult(Vector3 inputPos)
    {
        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(inputPos);
        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
    }
}
