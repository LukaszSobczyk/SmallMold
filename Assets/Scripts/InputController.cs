using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public float bodyMoveSpeed=1.0f;
    public float lrrotationOffset=1.0f;//y
    public float udrotationOffset = 1.0f;//x
    public float jumpForce = 60.0f;

    private Rigidbody rigi;
    private GameObject lHand;
    private GameObject rHand;


    private float distanceFromPoint;
    LineRenderer dupa;
    private float ydist;
    private float flatdist;
    void Start ()
    {
        dupa = gameObject.AddComponent<LineRenderer>();
        rigi = this.GetComponent<Rigidbody>();
        lHand = GameObject.Find("LHand").gameObject;
        rHand = GameObject.Find("RHand").gameObject;
        distanceFromPoint = ((lHand.transform.position + rHand.transform.position) / 2.0f - transform.position).magnitude;
        ydist = Mathf.Abs(transform.position.y - rHand.transform.position.y);
        flatdist = Mathf.Abs(new Vector3(transform.position.x - rHand.transform.position.x, 0.0f, transform.position.z - rHand.transform.position.z).magnitude);
    }
    
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputResult(lHand);
            rigi.useGravity = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            InputResult(rHand);
            rigi.useGravity = false;
        }
        if ((Input.GetMouseButton(0)|| Input.GetMouseButton(1))&& !rigi.useGravity)
        {
            PositionCorrection();
        }

        else if (!(Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)))
        {
            rigi.useGravity = true;
        }
        if ((Input.GetMouseButtonUp(0)) && !(Input.GetMouseButton(1)) || (Input.GetMouseButtonUp(1)) && !(Input.GetMouseButton(0)))
        {
            rigi.velocity = Vector3.zero;
        }
        #region powrot pozycji dloni
        if (!(Input.GetMouseButton(1)) && !(Input.GetMouseButtonDown(1)))
        {
            rHand.transform.position = Vector3.MoveTowards(rHand.transform.position, transform.FindChild("RHandPos").position, bodyMoveSpeed * Time.fixedDeltaTime);
        }
        if (!(Input.GetMouseButton(0)) && !(Input.GetMouseButtonDown(0)))
        {
            lHand.transform.position = Vector3.MoveTowards(lHand.transform.position, transform.FindChild("LHandPos").position, bodyMoveSpeed * Time.fixedDeltaTime);
        }
        #endregion
        HandRotation(lHand);
        HandRotation(rHand);
        RotationCorrection();
    }
    void RotationCorrection()
    {
        Vector3 targetDir;
        if ((Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)))
        {
            //lewa
            targetDir = lHand.transform.position;
            transform.LookAt(targetDir);
            //transform.Rotate(new Vector3(-udrotationOffset, 0.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, lrrotationOffset, 0.0f));
        }
        else if (!(Input.GetMouseButton(0)) && (Input.GetMouseButton(1)))
        {
            //prawa
            targetDir = rHand.transform.position;
            transform.LookAt(targetDir);
            //transform.Rotate(new Vector3(-udrotationOffset, 0.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, -lrrotationOffset,0.0f));
        }
        else
        {
            //obie
            targetDir = (lHand.transform.position + rHand.transform.position) / 2.0f;
            if (rigi.useGravity)
            {
                targetDir.y = this.transform.position.y;
            }
            transform.LookAt(targetDir);
            //if(!rigi.useGravity)
            //{
                //transform.Rotate(new Vector3(-udrotationOffset, 0.0f, 0.0f));
            //}
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

    }
    void PositionCorrection()
    {
        Vector3 target;
        if ((Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)))
        {
            target = lHand.transform.position + (this.transform.position - lHand.transform.position).normalized * distanceFromPoint;
        }
        else if (!(Input.GetMouseButton(0)) && (Input.GetMouseButton(1)))
        {
            target = rHand.transform.position+(this.transform.position - rHand.transform.position).normalized*distanceFromPoint;
        }
        else
        {
            target = (lHand.transform.position + rHand.transform.position) / 2.0f - (lHand.transform.forward + rHand.transform.forward).normalized * distanceFromPoint;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, bodyMoveSpeed * Time.fixedDeltaTime);
    }
    void OnTriggerStay(Collider coll)
    {
        if ((coll.CompareTag("Enviroment")||coll.CompareTag("Infectable")) && Input.GetButton("Jump"))
        {

            this.GetComponent<Rigidbody>().AddForce((transform.forward+transform.up)*jumpForce);
        }
    }
    void HandRotation(GameObject hand)
    {
        Vector3 targetDir = (hand.transform.position - gameObject.transform.position).normalized+ hand.transform.position;
        hand.transform.LookAt(targetDir);
        hand.transform.rotation = Quaternion.Euler(hand.transform.rotation.eulerAngles.x, hand.transform.rotation.eulerAngles.y, 0);
    }

    void InputResult(GameObject hand)
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit))
        {
            if (hit.collider.CompareTag("Enviroment")|| hit.collider.CompareTag("Infectable"))
            {
                Debug.Log("First: " + hit.collider.gameObject);
                //if (Physics.Raycast(transform.FindChild("Eye").position, hit.point-transform.FindChild("Eye").position, out hit))
                {
                    //if (hit.collider.CompareTag("Enviroment") || hit.collider.CompareTag("Infectable"))
                    {
                        Debug.Log("Second: " + hit.collider.gameObject);
                        hand.transform.position = hit.point;
                    }
                }
            }
        }
        
    }
}
