using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ObjectSelectManager : MonoBehaviour
{
    public struct ObjectTransformStruct
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public GameObject body;
    
    public InputActionProperty selectAction;
    
    public Transform handTransform;

    public LineRenderer line;
    
    public Gradient selectedGradient;
    public Gradient neutralGradient;

    public LayerMask ignoreMask;
    public GameObject selectedObject;
    public GameObject grabbedObject;

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
            if (hit.collider.name is not "Floor" || hit.collider.gameObject.layer != ignoreMask)
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
        if (selectedObject != null && selectedObject != body)
        {
            grabbedObject = selectedObject.gameObject;

            if (grabbedObject.TryGetComponent(out Rigidbody rb))
                rb.isKinematic = true;
                
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
            if (grabbedObject.TryGetComponent(out Rigidbody rb))
                rb.isKinematic = false;
            
            //grabbedObject.transform.position = objectOriginTransform.position;
            //grabbedObject.transform.rotation = objectOriginTransform.rotation;
            
            grabbedObject = null;
        }
    }
}
