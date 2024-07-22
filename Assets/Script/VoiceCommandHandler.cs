using System.Collections;
using System.Collections.Generic;
using Oculus.Voice;
using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceCommandHandler : MonoBehaviour
{
    public Transform cube;

    public InputActionProperty commandAction;

    public bool appVoiceActive = false;
    public AppVoiceExperience appVoiceExperience;
    
    // Start is called before the first frame update
    void Awake()
    {
        commandAction.action.performed += ctx => HandleVoiceInput();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        commandAction.action.performed -= ctx => HandleVoiceInput();
    }

    void HandleVoiceInput()
    {
        if (!appVoiceActive)
        {
            appVoiceExperience.Activate();
            appVoiceActive = true;
        }
        else
        {
            appVoiceExperience.Deactivate();
            appVoiceActive = false;
        }
    }

    private void SetColor(Transform t, Color color)
    {
        t.GetComponent<Renderer>().material.color = color;
    }

    public void UpdateColor(string[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            Debug.Log(values[i]);
        }
        
        var colorString = values[0];
        var shapeString = values[1];

        if (!ColorUtility.TryParseHtmlString(colorString, out var color)) return;
        if (string.IsNullOrEmpty(shapeString)) return;

        SetColor(cube, color);

    }
}