using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTest : MonoBehaviour
{
    public Transform cube;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
