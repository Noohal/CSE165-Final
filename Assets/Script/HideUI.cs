using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    public GameObject helpCanvas;

    public void HideHelp()
    {
        helpCanvas.SetActive(false);
    }
}
