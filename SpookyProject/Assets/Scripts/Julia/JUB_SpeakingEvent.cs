using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_SpeakingEvent : MonoBehaviour
{
    public GameObject speakingCanvas;
    public CoolTextScript textScript;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        speakingCanvas.transform.localScale = Vector3.zero;
        textScript = speakingCanvas.GetComponentInChildren<CoolTextScript>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.LogWarning("must appear");
        speakingCanvas.transform.localScale = Vector3.one;
        textScript.Read(text);
    }

}
