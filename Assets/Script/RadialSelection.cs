using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RadialSelection : MonoBehaviour
{
    public bool isToggled = false;
    public InputActionProperty toggleMenu;
    
    [Range(0,15)]
    public int numberOfRadialParts;
    public GameObject radialPrefab;
    public Transform radialCanvas;
    public float anglePadding = 10f;
    public Transform handTransform;

    private readonly List<GameObject> _spawnedParts = new List<GameObject>();
    private int _currentSelectedPart = -1;
    
    // Start is called before the first frame update
    void Awake()
    {
        toggleMenu.action.performed += ctx => ToggleMenu();
    }

    private void OnDestroy()
    {
        toggleMenu.action.performed -= ctx => ToggleMenu();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRadialPart();
        GetSelectedPart();
    }

    void ToggleMenu()
    {
        isToggled = !isToggled;
        radialCanvas.gameObject.SetActive(isToggled);
    }

    void GetSelectedPart()
    {
        Vector3 menuToHand = handTransform.position - radialCanvas.position;
        Vector3 projectedMenuToHand = Vector3.ProjectOnPlane(menuToHand, radialCanvas.forward);
        
        float angle = Vector3.SignedAngle(radialCanvas.up, projectedMenuToHand, -radialCanvas.forward);

        if (angle < 0) 
            angle += 360f;
        
        _currentSelectedPart = (int) angle * numberOfRadialParts / 360;

        for (int i = 0; i < _spawnedParts.Count; i++)
        {
            if (i == _currentSelectedPart)
            {
                _spawnedParts[i].GetComponent<Image>().color = Color.green;
                _spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                _spawnedParts[i].GetComponent<Image>().color = Color.white;
                _spawnedParts[i].transform.localScale = 1f * Vector3.one;
            }
        }
    }

    void SpawnRadialPart()
    {
        foreach (var item in _spawnedParts)
        {
            Destroy(item);
        }

        _spawnedParts.Clear();
        
        for (int i = 0; i < numberOfRadialParts; i++)
        {
            float angle = -i * 360f / numberOfRadialParts - anglePadding / 2f;

            Vector3 radialPartAngleEuler = new Vector3(0, 0, angle);

            GameObject spawnRadial = Instantiate(radialPrefab, radialCanvas);
            spawnRadial.transform.position = radialCanvas.position;
            spawnRadial.transform.localEulerAngles = radialPartAngleEuler;

            spawnRadial.GetComponent<Image>().fillAmount = 1f / (float) numberOfRadialParts - (anglePadding / 360f);
            
            _spawnedParts.Add(spawnRadial);
        }
    }
}
