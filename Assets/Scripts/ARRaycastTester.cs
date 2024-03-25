using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ARRaycastTester : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] GameObject raycastMidPoint;
    [SerializeField] GameObject raycastHitPoint;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    private Vector2 pointer;

    private void Start()
    {
        if (raycastManager == null)
        {
            raycastManager = FindObjectOfType<ARRaycastManager>();
        }
    }

    private void Update()
    {
        Vector2 screenMidPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        if (raycastManager.Raycast(screenMidPoint, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            raycastMidPoint.SetActive(true);
            raycastMidPoint.transform.position = raycastHits[0].pose.position;
            raycastMidPoint.transform.rotation = raycastHits[0].pose.rotation;
        }
        else
        {
            raycastMidPoint.SetActive(false);
        }
    }

    private void OnPointer(InputValue value)
    {
        pointer = value.Get<Vector2>();
    }

    private void OnTouch(InputValue value)
    {
        if (raycastManager.Raycast(pointer, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            Vector3 pos = raycastHits[0].pose.position;
            Quaternion rot = raycastHits[0].pose.rotation;
            GameObject instance = Instantiate(raycastHitPoint, pos, rot);
        }
    }
}
