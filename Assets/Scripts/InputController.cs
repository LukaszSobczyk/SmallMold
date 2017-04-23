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
    private Quaternion lhzerotation;
    private Quaternion rhzerotation;
    //LineRenderer dupa;
    void Start ()
    {
        //dupa = gameObject.AddComponent<LineRenderer>();
        rigi = this.GetComponent<Rigidbody>();
        lHand = GameObject.Find("LHand").gameObject;
        rHand = GameObject.Find("RHand").gameObject;
        lhzeropos = lHand.transform.position - this.transform.position;
        rhzeropos = rHand.transform.position - this.transform.position;
        lhzerotation = lHand.transform.localRotation;
        rhzerotation = rHand.transform.localRotation;
        distanceFromPoint = ((lHand.transform.position + rHand.transform.position) / 2.0f - transform.position).magnitude;
    }
    
    void Update ()
    {
        RotationCorrection();
        if (Input.GetMouseButtonDown(0))
        {
            InputResult(lHand);
            rigi.useGravity = false;
        }
        else if(!(Input.GetMouseButton(0)) && !(Input.GetMouseButtonDown(0)))
        {
            lHand.transform.position = this.transform.position+lhzeropos;
            //lHand.transform.localRotation = lhzerotation;
        }
        if (Input.GetMouseButtonDown(1))
        {
            InputResult(rHand);
            PositionCorrection();
            rigi.useGravity = false;
        }
        else if (!(Input.GetMouseButton(1))&& !(Input.GetMouseButtonDown(1)))
        {
            rHand.transform.position = this.transform.position+rhzeropos;
            //rHand.transform.localRotation = rhzerotation;
        }
        if (Input.GetMouseButton(0)|| Input.GetMouseButton(1)&& !rigi.useGravity)
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
        HandRotation(lHand);
        HandRotation(rHand);
    }
    void RotationCorrection()
    {
        Vector3 targetDir = (lHand.transform.position + rHand.transform.position) / 2.0f - transform.position;
        if (!rigi.useGravity)
        {
            targetDir.y = this.transform.position.y;
        }
        float step = rotationSpeed * Time.fixedDeltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void PositionCorrection()
    {
        Vector3 target = (lHand.transform.position + rHand.transform.position) / 2.0f - (lHand.transform.forward+ rHand.transform.forward).normalized*distanceFromPoint;
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
