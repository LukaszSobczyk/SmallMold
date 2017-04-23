using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public float bodyMoveSpeed=1.0f;
    public float rotationSpeed=1.0f;
   
    private Rigidbody rigi;
    private GameObject lHand;
    private GameObject rHand;


    private float distanceFromPoint;
    private Vector3 lhzeropos;
    private Vector3 rhzeropos;
    //LineRenderer dupa;
    void Start ()
    {
        //dupa = gameObject.AddComponent<LineRenderer>();
        rigi = this.GetComponent<Rigidbody>();
        lHand = GameObject.Find("LHand").gameObject;
        rHand = GameObject.Find("RHand").gameObject;
        SaveHandsPos();
        distanceFromPoint = ((lHand.transform.position + rHand.transform.position) / 2.0f - transform.position).magnitude;
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
            //rHand.transform.position = this.transform.position+rhzeropos;
            //rHand.transform.localRotation = rhzerotation;
            //todo pozycja prawej reki
            Transform buff = rHand.transform.parent;
            rHand.transform.SetParent(this.transform);
            rHand.transform.localPosition = rhzeropos;
            rHand.transform.SetParent(buff);
        }
        if (!(Input.GetMouseButton(0)) && !(Input.GetMouseButtonDown(0)))
        {
            //lHand.transform.position = this.transform.position+lhzeropos;
            //lHand.transform.localRotation = lhzerotation;
            //todo pozycja lewej reki
            Transform buff = lHand.transform.parent;
            lHand.transform.SetParent(this.transform);
            lHand.transform.localPosition = lhzeropos;
            lHand.transform.SetParent(buff);
        }

        HandRotation(lHand);
        HandRotation(rHand);
        RotationCorrection();
    }
    void SaveHandsPos()
    {
        Transform buff = lHand.transform.parent;
        lHand.transform.SetParent(this.transform);
        lhzeropos = lHand.transform.localPosition;
        lHand.transform.SetParent(buff);
        buff = rHand.transform.parent;
        rHand.transform.SetParent(this.transform);
        rhzeropos = rHand.transform.localPosition;
        rHand.transform.SetParent(buff);
    }
    void RotationCorrection()
    {
        Vector3 targetDir;
        if ((Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)))
        {
            targetDir = lHand.transform.position;
        }
        else if (!(Input.GetMouseButton(0)) && (Input.GetMouseButton(1)))
        {
            targetDir = rHand.transform.position;
        }
        else
        {
            targetDir = (lHand.transform.position + rHand.transform.position) / 2.0f;
        }
        if (rigi.useGravity)
        {
            targetDir.y = this.transform.position.y;
        }
        transform.LookAt(targetDir);
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
