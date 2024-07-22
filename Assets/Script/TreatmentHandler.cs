using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreatmentHandler : MonoBehaviour
{
    public ObjectSelectManager selectManager;
    
    public TMP_Dropdown injuryDropdown;

    public GameObject injuriesParent;
    public TMP_Text injuryText;
    public TMP_Text bloodLossText;
    public TMP_Text burnText;
    public TMP_Text successText;

    public int treatmentType;

    public bool injuryTreated;
    public bool bloodTreated;
    public bool burnTreated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (treatmentType == 0)
            {
                TreatInjury(treatmentType);
            } else if (treatmentType == 1)
            {
                TreatInjury(treatmentType);
            }
            else if (treatmentType == 2)
            {
                TreatInjury(treatmentType);
            }
        }
    }

    public void BeginTreatment()
    {
        treatmentType = injuryDropdown.value;
        
        injuriesParent.SetActive(false);

        switch (treatmentType)
        {
            case 0:
                injuryText.gameObject.SetActive(true);
                break;
            case 1:
                bloodLossText.gameObject.SetActive(true);
                break;
            case 2:
                burnText.gameObject.SetActive(true);
                break;
        }
    }

    void TreatInjury(int treatment)
    {
        if (treatment == 0)
            injuryTreated = true;
        else if (treatment == 1)
            bloodTreated = true;
        else if (treatment == 2)
            burnTreated = true;
        
        if (injuryTreated && bloodTreated && burnTreated)
        {
            TreatmentComplete();
            return;
        }
        
        injuryText.gameObject.SetActive(false);
        bloodLossText.gameObject.SetActive(false);
        burnText.gameObject.SetActive(false);
        injuriesParent.SetActive(true);
        injuriesParent.transform.GetChild(0).gameObject.SetActive(false);
        injuriesParent.transform.GetChild(1).gameObject.SetActive(true);
    }

    void TreatmentComplete()
    {
        Debug.Log("Treatment Complete");

        body.GetComponent<SkinnedMeshRenderer>().material.color = Color.green;
        treatmentType = -1;
        
        injuryText.gameObject.SetActive(false);
        bloodLossText.gameObject.SetActive(false);
        burnText.gameObject.SetActive(false);
        injuriesParent.gameObject.SetActive(false);
        
        successText.gameObject.SetActive(true);
    }
}
