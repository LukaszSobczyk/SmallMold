using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float bodyMoveSpeed = 1.0f;
    public float handMoveSpeed = 3.0f;
    public float lrrotationOffset = 1.0f;//y
    //public float udrotationOffset = 1.0f;//x
    //public float jumpForce = 60.0f;
    public float catchDistance = 1.0f;

    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    private Rigidbody rigi;
    private GameObject lHand;
    private GameObject rHand;

    private bool isWalking = false;

    private float distanceFromPoint;
    //LineRenderer dupa;
    private float ydist;
    private float flatdist;
    void Start()
    {
        //dupa = gameObject.AddComponent<LineRenderer>();
        rigi = this.GetComponent<Rigidbody>();
        lHand = GameObject.Find("LHand").gameObject;
        rHand = GameObject.Find("RHand").gameObject;
        distanceFromPoint = ((lHand.transform.position + rHand.transform.position) / 2.0f - transform.position).magnitude;
        ydist = Mathf.Abs(transform.position.y - rHand.transform.position.y);
        flatdist = Mathf.Abs(new Vector3(transform.position.x - rHand.transform.position.x, 0.0f, transform.position.z - rHand.transform.position.z).magnitude);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputResult(lHand);
            rigi.useGravity = false;
            this.GetComponent<AudioSource>().Play();
            CatchAnim();
        }
        if (Input.GetMouseButtonDown(1))
        {
            InputResult(rHand);
            rigi.useGravity = false;
            this.GetComponent<AudioSource>().Play();
            CatchAnim();
        }
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !rigi.useGravity)
        {
            PositionCorrection();
        }

        else if (!(Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)))
        {
            rigi.useGravity = true;
            IdleAnim();
        }
        if ((Input.GetMouseButtonUp(0)) && !(Input.GetMouseButton(1)) || (Input.GetMouseButtonUp(1)) && !(Input.GetMouseButton(0)))
        {
            rigi.velocity = Vector3.zero;
        }
        #region powrot pozycji dloni
        if (!(Input.GetMouseButton(1)) && !(Input.GetMouseButtonDown(1)))
        {
            rHand.transform.position = Vector3.MoveTowards(rHand.transform.position, transform.FindChild("RHandPos").position, handMoveSpeed * Time.fixedDeltaTime);
        }
        if (!(Input.GetMouseButton(0)) && !(Input.GetMouseButtonDown(0)))
        {
            lHand.transform.position = Vector3.MoveTowards(lHand.transform.position, transform.FindChild("LHandPos").position, handMoveSpeed * Time.fixedDeltaTime);
        }
        #endregion
        HandRotation(lHand);
        HandRotation(rHand);
        RotationCorrection();

        #region chodzenie
        if(Input.GetKey(KeyCode.W))
        {
            rigi.AddForce(transform.forward * movementSpeed);
            rigi.rotation = Quaternion.Lerp(rigi.rotation, Camera.main.transform.rotation, Time.deltaTime * rotationSpeed);
            transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Direction", false);
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigi.AddForce(-Camera.main.transform.forward * movementSpeed);
            rigi.rotation = Quaternion.Lerp(rigi.rotation, Camera.main.transform.rotation, Time.deltaTime);
            transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Direction", true);
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigi.AddForce(-Camera.main.transform.right * movementSpeed);
            rigi.rotation = Quaternion.Lerp(rigi.rotation, Camera.main.transform.rotation, Time.deltaTime);
            transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Direction", false);
            isWalking = true;

        }
        if (Input.GetKey(KeyCode.D))
        {
            rigi.AddForce(Camera.main.transform.right * movementSpeed);
            rigi.rotation = Quaternion.Lerp(rigi.rotation, Camera.main.transform.rotation, Time.deltaTime);
            transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Direction", true);
            isWalking = true;
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isWalking = false;
        }
        WalkAnim(isWalking);
        #endregion
    }
    void RotationCorrection()
    {
        Vector3 targetDir;
        if ((Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)))
        {
            //lewa
            targetDir = lHand.transform.position;
            transform.LookAt(targetDir);
            transform.Rotate(new Vector3(0.0f, lrrotationOffset, 0.0f));
        }
        else if (!(Input.GetMouseButton(0)) && (Input.GetMouseButton(1)))
        {
            //prawa
            targetDir = rHand.transform.position;
            transform.LookAt(targetDir);
            transform.Rotate(new Vector3(0.0f, -lrrotationOffset, 0.0f));
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
            target = rHand.transform.position + (this.transform.position - rHand.transform.position).normalized * distanceFromPoint;
        }
        else
        {
            target = (lHand.transform.position + rHand.transform.position) / 2.0f - (lHand.transform.forward + rHand.transform.forward).normalized * distanceFromPoint;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, bodyMoveSpeed * Time.fixedDeltaTime);
    }
    void HandRotation(GameObject hand)
    {
        Vector3 targetDir = (hand.transform.position - gameObject.transform.position).normalized + hand.transform.position;
        hand.transform.LookAt(targetDir);
        hand.transform.rotation = Quaternion.Euler(hand.transform.rotation.eulerAngles.x, hand.transform.rotation.eulerAngles.y, 0);
    }

    void InputResult(GameObject hand)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enviroment") || hit.collider.CompareTag("Infectable"))
            {
                //Debug.Log("First: " + hit.collider.gameObject);
                Ray ray = new Ray(transform.FindChild("Eye").position, hit.point - transform.FindChild("Eye").position);
                Vector3 firsthit = hit.point;
                if (Physics.Raycast(ray, out hit, catchDistance))
                //if (Physics.Raycast(transform.FindChild("Eye").position, hit.point-transform.FindChild("Eye").position, out hit))
                {
                    if ((hit.collider.CompareTag("Enviroment") || hit.collider.CompareTag("Infectable")) && ((firsthit - hit.point).magnitude < catchDistance)
                        &&(this.transform.position-Camera.main.transform.position).magnitude < (hit.point - Camera.main.transform.position).magnitude)
                    {
                        //Debug.Log("Second: " + hit.collider.gameObject);
                        hand.transform.position = hit.point;
                    }
                }
            }
        }

    }
    void CatchAnim()
    {
        transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Holding", true);
    }
    void IdleAnim()
    {
        transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Holding", false);
    }
    void WalkAnim(bool walking)
    {
        transform.FindChild("mold_hero").GetComponent<Animator>().SetBool("Walking", walking);
    }
}
