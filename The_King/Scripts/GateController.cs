using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{
    Animator parentAnimator;
    AudioSource audioSource;
    Timer timer;
    public bool autorizar_puntos=false;
    // Start is called before the first frame update
    void Start()
    {
        parentAnimator = transform.parent.GetComponent<Animator>();
        audioSource = transform.parent.GetComponent<AudioSource>();
        timer = GameObject.FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoAction()
    {
        audioSource.Play();
        parentAnimator.SetTrigger("OpenG");
        if (autorizar_puntos == true)
        {
            //Envía señal al animator controller
            
            
            if (timer != null)
            {
                timer.StopTime();
            }
        }

    }
}
