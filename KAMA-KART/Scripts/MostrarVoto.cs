using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarVoto : MonoBehaviour
{
    Entrenador entrenador;
    AstronautAgent agent;

    int etapa;
    char numTxt;

    // Start is called before the first frame update
    void Start()
    {
        entrenador = GameObject.Find("Entrenador").GetComponent<Entrenador>();
        etapa = entrenador.etapa;
        numTxt = this.name[this.name.Length - 1];
        agent = GameObject.Find("Astronauta" + numTxt).GetComponent<AstronautAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<Text>().text = "" + agent.piezaSeleccionada;
        /*
        if (entrenador.etapa != etapa)
        {
            this.GetComponent<Text>().text = ""+etapa;
            etapa = entrenador.etapa;
        }
        */
    }

    public void votoSeleccionado(string voto)
    {
        this.GetComponent<Text>().text = voto;
    }

}
