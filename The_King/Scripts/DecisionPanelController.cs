using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DecisionPanelController : MonoBehaviour
{
    public string aText;
    public int aPoints = 6;
    public string bText;
    public int bPoints = 1;
    public float time = 2.0f;
    public string key = "ss_etica";
    GameObject canvas;
    public GameObject decisionGroup;
    Transform A_button;
    Transform B_button;
    AudioSource Deci_sound;
    AudioSource A_button_sound;
    AudioSource B_button_sound;
    FirstPersonController fpc;
    public bool hintpaneishon=true;
    int conteoClics = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        A_button = decisionGroup.transform.Find("A Button");
        B_button = decisionGroup.transform.Find("B Button");
        Deci_sound = this.transform.GetComponent<AudioSource>();
        A_button_sound = A_button.GetComponent<AudioSource>();
        B_button_sound = B_button.GetComponent<AudioSource>();
        fpc = GameObject.FindObjectOfType<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hintpaneishon == true)
        {
            canvas.transform.Find("HintPanel").gameObject.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        fpc.enabled = !fpc.enabled;
        decisionGroup.SetActive(true);
        A_button.GetComponentInChildren<Text>().text = aText;
        B_button.GetComponentInChildren<Text>().text = bText;

        A_button.gameObject.SetActive(true);
        B_button.gameObject.SetActive(true);

        Deci_sound.Play();

        Invoke("HideButtons", time);
    }
    
    public void ScoreRecord(string dec)
    {
        if (conteoClics == 0)
        {
            conteoClics++;
            fpc.enabled = !fpc.enabled;
            if (dec == "A")
            {
                A_button_sound.Play();
                if (PlayerPrefs.HasKey(key))
                {
                    int objectCount = PlayerPrefs.GetInt(key);
                    objectCount += aPoints;
                    PlayerPrefs.SetInt(key, objectCount);
                }
                else
                {
                    PlayerPrefs.SetInt(key, aPoints);
                }
            }
            else if (dec == "B")
            {
                B_button_sound.Play();
                if (PlayerPrefs.HasKey(key))
                {
                    int objectCount = PlayerPrefs.GetInt(key);
                    objectCount += bPoints;
                    PlayerPrefs.SetInt(key, objectCount);
                }
                else
                {
                    PlayerPrefs.SetInt(key, bPoints);
                }
            }

            Invoke("HideButtons", 0.001f);
            Debug.Log(key + ": " + PlayerPrefs.GetInt(key));
        }
    }
    

    void HideButtons()
    {
        decisionGroup.SetActive(false);
        A_button.gameObject.SetActive(false);
        B_button.gameObject.SetActive(false);
        canvas.transform.Find("TextPanel").gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Destroy(this.gameObject);
    }
   
    // Update is called once per frame
    void Update()
    {

    }
}