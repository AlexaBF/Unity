using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejoBarra : MonoBehaviour
{

    public float Max = 100.0f;
    public float Actual = 100.0f;

    char numBarra;
    AstronautAgent agent;

    void Start()
    {
        numBarra = this.name[this.name.Length - 1];
        agent = GameObject.Find("Astronauta" + numBarra).GetComponent<AstronautAgent>();
        Actual = agent.Actual1;
    }

    public void Reinicio()
    {
        while (this.GetComponent<Image>().fillAmount != 1)
         {
            this.GetComponent<Image>().fillAmount = 1;
            agent.Actual1 = 100.0f;
            Actual = 100.0f;
        }
    }

    public void  Barritas()
    {
        Actual = agent.Actual1;
        this.GetComponent<Image>().fillAmount = Actual / Max;
    }

    
    void Update()
    {
        
    }
}
