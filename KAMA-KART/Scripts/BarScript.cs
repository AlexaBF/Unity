using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    //Referencias 
    public Image fillbar1;
    public float Max1;
    public float Actual1;


    public Image fillbar2;
    public float Max2;
    public float Actual2;


    public Image fillbar3;
    public float Max3;
    public float Actual3;


    public Image fillbar4;
    public float Max4;
    public float Actual4;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillbar1.fillAmount = Actual1 / Max1;
        fillbar2.fillAmount = Actual2 / Max2;
        fillbar3.fillAmount = Actual3 / Max3;
        fillbar4.fillAmount = Actual4 / Max4;

    }
}
