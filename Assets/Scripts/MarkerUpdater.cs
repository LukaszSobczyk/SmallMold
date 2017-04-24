using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerUpdater : MonoBehaviour {

    public GameObject marker;
    GameObject currentTarget;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Infectable") && marker != null)
        {
            if(other.gameObject.transform.parent!=null)
            {
                if (other.gameObject.transform.parent.name == "pizza")
                {
                    currentTarget = other.gameObject.transform.parent.gameObject;
                }
                else
                {
                    currentTarget = other.gameObject;
                }
                ActualizeMarker();
            }
            else
            {
                currentTarget = other.gameObject;
            }
        }
    }
        
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Infectable")&&marker!=null)
        {
            marker.SetActive(true);
            ActualizeMarker();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Infectable") && marker != null)
        {
            marker.SetActive(false);

        }
    }
    void ActualizeMarker()
    {
        RectTransform canvasRect = GameObject.Find("Miedzymordzie").GetComponent<RectTransform>();
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(currentTarget.transform.position);
        marker.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                           ((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)
                           - marker.GetComponent<RectTransform>().rect.width * 0.6f * Mathf.Sign(viewportPosition.x - 0.5f)),

                           ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
                           - marker.GetComponent<RectTransform>().rect.height * 0.6f * Mathf.Sign(viewportPosition.y - 0.5f)));
        InfectionController infoSource=currentTarget.GetComponent<InfectionController>();
        if(infoSource!=null)
        {
            marker.GetComponentInChildren<Text>().text = infoSource.LevelToInfect.ToString();
        }
    }
}
