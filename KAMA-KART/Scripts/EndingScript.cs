using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    //Referencias 
    public float Max = 40.0f;

    //Barra de velocidad
    public Image fillbar1;
    //Barra de aceleraci?n
    public Image fillbar2;
    //Barra de manejo
    public Image fillbar3;
    //Barra de manejo
    public Image fillbar4;
    //Barra de peso
    public Image fillbar5;
    //Barra de gasolina
    public Image fillbar6;

    //Referencias
    Image fondo1;
    Image fondo2;
    Text texto;
    Text desf;
    public float desempenoPeso;
    public float desempenoVelocidad;
    public float desempenoAceleracion;
    public float desempenoManejo;
    public float desempenoProteccion;
    public float desempenoGasolina;
    public float desempenofinal;
    int etapa;

    //audio
    AudioSource audioBien;
    AudioSource audioMal;
    //

    string rango;

    // Start is called before the first frame update
    void Start()
    {
        //audio
        audioBien = GameObject.Find("Bien").GetComponent<AudioSource>();
        audioMal = GameObject.Find("Mal").GetComponent<AudioSource>();
        //

        texto = GameObject.Find("Place").GetComponent<Text>();
        desf = GameObject.Find("Desempeno").GetComponent<Text>();
        fondo1 = GameObject.Find("Fondo").GetComponent<Image>();
        fondo2 = GameObject.Find("Description").GetComponent<Image>();


        //PlayerPrefs
        desempenoVelocidad = PlayerPrefs.GetFloat("desempenoVelocidad", 0);
        desempenoAceleracion = PlayerPrefs.GetFloat("desempenoAceleracion", 0);
        desempenoManejo = PlayerPrefs.GetFloat("desempenoManejo", 0);
        desempenoProteccion = PlayerPrefs.GetFloat("desempenoProteccion", 0);
        desempenoPeso = PlayerPrefs.GetFloat("desempenoPeso", 0);
        desempenoGasolina = PlayerPrefs.GetFloat("desempenoGasolina", 0);

        Debug.Log("desempenoVelocidad " + desempenoVelocidad);
        Debug.Log("desempenoAceleracion " + desempenoAceleracion);
        Debug.Log("desempenoManejo " + desempenoManejo);
        Debug.Log("desempenoProteccion " + desempenoProteccion);
        Debug.Log("desempenoPeso " + desempenoPeso);
        Debug.Log("desempenoGasolina " + desempenoGasolina);


        //Barras
        fillbar1.fillAmount = desempenoVelocidad / 40;
        fillbar2.fillAmount = desempenoAceleracion / 40;
        fillbar3.fillAmount = desempenoManejo / 40;
        fillbar4.fillAmount = desempenoProteccion / 40;
        fillbar5.fillAmount = desempenoPeso / 40;
        fillbar6.fillAmount = desempenoGasolina / 40;

        //Etapa
        etapa = PlayerPrefs.GetInt("etapa", 0);


        if (etapa < 4)
        {
            desempenofinal = (desempenoVelocidad / 4) + (desempenoAceleracion / 4) + (desempenoManejo / 4) + (desempenoProteccion / 4) + (desempenoPeso / 4) + (desempenoGasolina / 4);

            fillbar1.color = new Color32(255, 0, 0, 255);
            fillbar2.color = new Color32(255, 0, 0, 255);
            fillbar3.color = new Color32(255, 0, 0, 255);
            fillbar4.color = new Color32(255, 0, 0, 255);
            fillbar5.color = new Color32(255, 0, 0, 255);
            fillbar6.color = new Color32(255, 0, 0, 255);
            fondo1.color = new Color32(255, 0, 0, 255);
            fondo2.color = new Color32(255, 0, 0, 255);
            GameObject.Find("Engine1-2").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Wing6-1").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Big_Launcher-1").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Cockpit3_WithoutInterior").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Wing2-5").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Blaster-1").GetComponent<MeshRenderer>().enabled = false;


            texto.text = ("Incomplete");
            audioMal.Play();
            desf.text = ("Desempeno final: F");
        }
        else
        {
            desempenofinal = PlayerPrefs.GetFloat("desempenofinal", 0);
            texto.text = ("Well done!");
            audioBien.Play();
            if (desempenofinal >= 75)
            {
                rango = "S";
            }
            else if (desempenofinal <= 74 || desempenofinal <= 50)
            {
                rango = "A";
            }
            else if (desempenofinal <= 49 || desempenofinal <= 25)
            {
                rango = "B";
            }
            else if (desempenofinal <= 24)
            {
                rango = "C";
            }


            desf.text = ("Desempeno final: " + rango);

        }






    }

    // Update is called once per frame una prueba
    void Update()
    {

    }
}