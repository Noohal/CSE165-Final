using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoiceAnalysis : MonoBehaviour
{
    public Transform body;
    
    public TMP_Text introMessage;
    public TMP_Text consentMessage;
    public TMP_Text loadingMessage;
    public TMP_Text endingMessage;

    public GameObject injuriesParent;

    public int stage = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
        
        introMessage.gameObject.SetActive(true);
        consentMessage.gameObject.SetActive(false);
        loadingMessage.gameObject.SetActive(false);
        endingMessage.gameObject.SetActive(false);
        
        injuriesParent.SetActive(false);
    }

    public void ShowConsentMessage(string[] values)
    {
        var analysisString = values[0];

        if (analysisString.Equals("Analysis") == false)
            return;
        
        if (stage != 0)
            return;
        stage = 1;
        
        introMessage.gameObject.SetActive(false);
        consentMessage.gameObject.SetActive(true);
    }

    public void TryConsent(string[] values)
    {
        if (values[1].Equals("have"))
        {
            ShowLoadingMessage(values);
        }
        else
        {
            stage = -1;
            consentMessage.gameObject.SetActive(false);
            endingMessage.gameObject.SetActive(true);
        }
    }

    private void ShowLoadingMessage(string[] values)
    {
        if (stage != 1)
            return;
        stage = 2;
        
        consentMessage.gameObject.SetActive(false);
        loadingMessage.gameObject.SetActive(true);
        StartCoroutine(WaitSeconds(3f));
        
        loadingMessage.gameObject.SetActive(false);
        injuriesParent.SetActive(true);
        injuriesParent.transform.GetChild(0).gameObject.SetActive(true);
        injuriesParent.transform.GetChild(1).gameObject.SetActive(false);

        body.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
