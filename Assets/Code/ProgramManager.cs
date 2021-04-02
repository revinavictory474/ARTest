using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgramManager : MonoBehaviour
{
    [SerializeField] private GameObject planeMP;
    [SerializeField] private Camera ARCamera;

    private ARRaycastManager arrm;
    private Vector2 touchPosition;
    public bool ChooseObject = false;
    public bool rotate;
    public GameObject objSpawn;
    public GameObject _scrollView;
    private GameObject _selectedObject;
    public Quaternion yRotation;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arrm = FindObjectOfType<ARRaycastManager>();
        planeMP.SetActive(false);
        _scrollView.SetActive(false);
    }

    
    void Update()
    {
        if (ChooseObject)
        {
            ShowMarkerAndSetObject();
        }
        MoveObjectAndRotate();
    }
    void ShowMarkerAndSetObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arrm.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if(hits.Count>0)
        {
            planeMP.transform.position = hits[0].pose.position;
            planeMP.SetActive(true);
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(objSpawn, hits[0].pose.position, objSpawn.transform.rotation);
            ChooseObject = false;
            planeMP.SetActive(false);
        }
    }
    void MoveObjectAndRotate()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {

                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Unselected"))
                    {
                        raycastHit.collider.gameObject.tag = "Selected";
                    }
                }
            }

            _selectedObject = GameObject.FindWithTag("Selected");

            if (touch.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                if (rotate)
                {
                    yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                    _selectedObject.transform.rotation = yRotation * _selectedObject.transform.rotation;
                }
                else
                {
                    arrm.Raycast(touchPosition, hits, TrackableType.Planes);
                    _selectedObject.transform.position = hits[0].pose.position;
                }
            }

            if(Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                if(touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    float distanceBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistBetwTouch = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    float delta = distanceBetweenTouches - prevDistBetwTouch;

                    if(Mathf.Abs(delta) > 0)
                    {
                        delta *= 0.1f;
                    }
                    else
                    {
                        distanceBetweenTouches = delta = 0;
                    }
                    yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * delta, 0f);
                    _selectedObject.transform.rotation = yRotation * _selectedObject.transform.rotation;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (_selectedObject.CompareTag("Selected"))
                {
                    _selectedObject.tag = "Unselected";
                }
            }

        }
    }

}
