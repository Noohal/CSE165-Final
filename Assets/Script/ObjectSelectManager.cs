using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelectManager : MonoBehaviour
{
    public struct ObjectTransformStruct
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    public InputActionProperty selectAction;

    private bool Select => selectAction.action.triggered;
    
    public Transform handTransform;

    public Gradient selectedGradient;
    public Gradient neutralGradient;
    
    public LineRenderer line;

    public GameObject selectedObject;
    public GameObject grabbedObject;

    private bool isGrabbing = false;
    private ObjectTransformStruct objectOriginTransform;
    
    private void Awake()
    {
        selectAction.action.performed += ctx => GrabObject();
        selectAction.action.canceled += ctx => ReleaseObject();
    }

    private void OnDestroy()
    {
        selectAction.action.performed -= ctx => GrabObject();
        selectAction.action.canceled -= ctx => ReleaseObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(handTransform.position, handTransform.forward, out RaycastHit hit, 7f))
        {
            if (hit.collider.name is not "Floor")
            {
                line.colorGradient = selectedGradient;
                selectedObject = hit.collider.gameObject;
            }
        }
        else
        {
            line.colorGradient = neutralGradient;
            selectedObject = null;
        }

        OnMoveObject();
    }

    void GrabObject()
    {
        if (selectedObject != null)
        {
            isGrabbing = true;
            
            grabbedObject = selectedObject.gameObject;
                
            objectOriginTransform.position = selectedObject.transform.position;
            objectOriginTransform.rotation = selectedObject.transform.rotation;
        }
    }

    void OnMoveObject()
    {
        if (grabbedObject == null)
            return;

        grabbedObject.transform.position = handTransform.position;
        grabbedObject.transform.rotation = handTransform.rotation;
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            isGrabbing = false;

            grabbedObject.transform.position = objectOriginTransform.position;
            grabbedObject.transform.rotation = objectOriginTransform.rotation;
            
            grabbedObject = null;
        }
    }
}
