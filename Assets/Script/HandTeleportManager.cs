using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandTeleportManager : MonoBehaviour
{
    public Transform body;
    
    public InputActionProperty teleportAction;

    private bool Teleport => teleportAction.action.triggered;

    public Vector3 destinationPosition;
    
    public LineRenderer line;
    public Gradient validGradient;
    public Gradient neutralGradient;
    
    public Transform rightHandTransform;
    
    private void Update()
    {
        Vector3 p0 = rightHandTransform.position;
        Vector3 p1 = rightHandTransform.rotation * Vector3.forward;

        line.colorGradient = neutralGradient;
        line.positionCount = 2;
        line.SetPosition(0, p0);
        line.SetPosition(1, p0 + p1 * 7f);

        if (Physics.Raycast(rightHandTransform.position, rightHandTransform.forward, out RaycastHit hit, 7f))
        {
            line.colorGradient = validGradient;
            destinationPosition = hit.point;
        }
        else
        {
            line.colorGradient = neutralGradient;
        }

        if (Teleport)
        {
            body.position = destinationPosition;
        }
    }
}
