using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//these to be added
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public GameObject prefabObject; //gameboject that will be spawned 
    private GameObject spawnedObject; //object that is spawned
    private Vector2 touchPosition;   //position in phone that is touched
    public ARRaycastManager _arRaycastManager;   //cast the ray towards detected plane and tells where it hits 
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); //contain the hits 

    void Update()
    {
        if (Input.touchCount > 0)	
        {
            Touch touch = Input.GetTouch(0);//stores the first touch data 

            if (touch.phase == TouchPhase.Began) //began phase occur when a new touch is detected
                touchPosition = touch.position;

            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose; //pose = position + rotation

                if (spawnedObject == null)//if no object spawned then it will be spwaned otherwise position be changed 
                    spawnedObject = Instantiate(prefabObject, hitPose.position, hitPose.rotation);
                else
                    spawnedObject.transform.position = hitPose.position;
            }
    	}
    }
}

