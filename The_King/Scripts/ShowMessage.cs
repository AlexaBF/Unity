using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    public string message;
    public bool autoDestroid=false;
    GameObject canvas;
    public float tiempo = 10.0f;
    public bool haveSound = false;
    AudioSource sound;
    public bool disappearHintPanel = true;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        if (haveSound)
            sound = this.transform.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (disappearHintPanel)
            canvas.transform.Find("HintPanel").gameObject.SetActive(false);
        if (haveSound)
            sound.Play();
        canvas.transform.Find("TextPanel").GetComponentInChildren<Text>().text = message;
        canvas.transform.Find("TextPanel").gameObject.SetActive(true);
        Invoke("HidePanel", tiempo);
    }
    void HidePanel()
    {
        if (canvas.transform.Find("TextPanel").GetComponentInChildren<Text>().text == message)
            canvas.transform.Find("TextPanel").gameObject.SetActive(false);
        
        if (autoDestroid)
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
