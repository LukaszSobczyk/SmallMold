using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public float bodyMoveSpeed=1.0f;
    public float lrrotationOffset=1.0f;//y
    public float udrotationOffset = 1.0f;//x

    private Rigidbody rigi;
    private GameObject lHand;
    private GameObject rHand;


    private float distanceFromPoint;
    //private Vector3 lhzeropos;
    //private Vector3 rhzeropos;
    //LineRenderer dupa;
    private float ydist;
    private float flatdist;
    void Start ()
    {
        //dupa = gameObject.AddComponent<LineRenderer>();
        rigi = this.GetComponent<Rigidbody>();
        lHand = GameObject.Find("LHand").gameObject;
        rHand = GameObject.Find("RHand").gameObject;
        //SaveHandsPos();
        distanceFromPoint = ((lHand.transform.position + rHand.transform.position) / 2.0f - transform.position).magnitude;
        ydist = Mathf.Abs(transform.position.y - rHand.transform.position.y);
        flatdist = Mathf.Abs(new Vector3(transform.position.x - rHand.transform.position.x, 0.0f, transform.position.z - rHand.transform.position.z).magnitude);
    }
    
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputResult(lHand);
            //PositionCorrection();
            rigi.useGravity = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            InputResult(rHand);
            //PositionCorrection();
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
        if (!(Input.GetMouseButton(1)) && !(Input.GetMouseButtonDown(1)))
        {
            //todo pozycja prawej reki
            //Transform buff = rHand.transform.parent;
            //rHand.transform.SetParent(this.transform);
            //Vector3 oldpos = rHand.transform.position;
            //rHand.transform.localPosition = rhzeropos;
            //Vector3 newpos = rHand.transform.position;
            //rHand.transform.position = oldpos;
            //rHand.transform.SetParent(buff);
            //Vector3 target = (transform.forward + transform.right).normalized * flatdist - transform.up.normalized * ydist;
            //rHand.transform.position = Vector3.MoveTowards(rHand.transform.position, transform.position+target, bodyMoveSpeed * Time.fixedDeltaTime);
            rHand.transform.position = Vector3.MoveTowards(rHand.transform.position, transform.FindChild("RHandPos").position, bodyMoveSpeed * Time.fixedDeltaTime);
        }
        if (!(Input.GetMouseButton(0)) && !(Input.GetMouseButtonDown(0)))
        {

            //todo pozycja lewej reki
            //Transform buff = lHand.transform.parent;
            //lHand.transform.SetParent(this.transform);
            //Vector3 oldpos = lHand.transform.position;
            //lHand.transform.localPosition = rhzeropos;
            //Vector3 newpos = lHand.transform.position;
            //lHand.transform.position = oldpos;
            //lHand.transform.SetParent(buff);
            //Vector3 target = (transform.forward - transform.right).normalized * flatdist - transform.up.normalized*ydist;
            //lHand.transform.position = Vector3.MoveTowards(lHand.transform.position, transform.position + target, bodyMoveSpeed * Time.fixedDeltaTime);
            //lHand.transform.position = Vector3.MoveTowards(oldpos, newpos, bodyMoveSpeed * Time.fixedDeltaTime);
            lHand.transform.position = Vector3.MoveTowards(lHand.transform.position, transform.FindChild("LHandPos").position, bodyMoveSpeed * Time.fixedDeltaTime);
        }

        HandRotation(lHand);
        HandRotation(rHand);
        RotationCorrection();
    }
    //void SaveHandsPos()
    //{
    //    Transform buff = lHand.transform.parent;
    //    lHand.transform.SetParent(this.transform);
    //    lhzeropos = lHand.transform.localPosition;
    //    lHand.transform.SetParent(buff);
    //    buff = rHand.transform.parent;
    //    rHand.transform.SetParent(this.transform);
    //    rhzeropos = rHand.transform.localPosition;
    //    rHand.transform.SetParent(buff);
    //}
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
    //void OnTriggerStay(Collider coll)
    //{
    //    if(coll.CompareTag("Enviroment")&& Input.GetButton("Jump"))
    //    {
    //        this.GetComponent<Rigidbody>().AddForce(0, 30, 0);
    //    }
    //}
    void HandRotation(GameObject hand)
    {
        Vector3 targetDir = (hand.transform.position - gameObject.transform.position).normalized+ hand.transform.position;
        hand.transform.LookAt(targetDir);
        hand.transform.rotation = Quaternion.Euler(hand.transform.rotation.eulerAngles.x, hand.transform.rotation.eulerAngles.y, 0);
    }

    void InputResult(GameObject hand)
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit);
        
        //dupa.SetPosition(0, gameObject.transform.position);
        //dupa.SetPosition(1, hit.point);
        if (hit.collider.CompareTag("Enviroment"))
        {
            hand.transform.position = hit.point;
            //HandRotation(hand);
        }
    }
}
