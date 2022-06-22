using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CofreController : MonoBehaviour
{
    Animator parentAnimator;
    GameObject canvas;
    public string message;
    AudioSource audioSource;
    public bool autoDestroid = false;
    public float tiempo = 4.0f;
    public bool disappearHintPanel = true;

    // Start is called before the first frame update
    void Start()
    {
       
        parentAnimator = transform.parent.GetComponent<Animator>();
        audioSource = transform.parent.GetComponent<AudioSource>();
        canvas = GameObject.Find("Canvas");
    }
  
    // Update is called once per frame
    void Update()
    {

    }
    public void DoAction()
    {
        //Envía señal al animator controller
        parentAnimator.SetTrigger("OpenG");
        audioSource.Play();
        //Envía señal al cofre
        Invoke("Chesti", .5f);

    }
    void Chesti()
    {
        if (disappearHintPanel)
            canvas.transform.Find("HintPanel").gameObject.SetActive(false);
        canvas = GameObject.Find("Canvas");
        canvas.transform.Find("TextPanel").gameObject.SetActive(true);
        canvas.transform.Find("TextPanel").Find("Text").GetComponent<Text>().text = message;
        Invoke("HidePanel", tiempo);
    }
    void HidePanel()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        if (canvas.transform.Find("TextPanel").GetComponentInChildren<Text>().text == message)
            canvas.transform.Find("TextPanel").gameObject.SetActive(false);

        if (autoDestroid)
        {
            Destroy(this.gameObject);
        }
    }
}
