using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgramManager : MonoBehaviour
{
    [SerializeField] private GameObject planeMP;

    private ARRaycastManager arrm;


    void Start()
    {
        arrm = FindObjectOfType<ARRaycastManager>();
        planeMP.SetActive(false);
    }

    
    void Update()
    {
        ShowMarker();
    }
    void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arrm.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if(hits.Count>0)
        {
            planeMP.transform.position = hits[0].pose.position;
            planeMP.SetActive(true);
        }
    }
}
