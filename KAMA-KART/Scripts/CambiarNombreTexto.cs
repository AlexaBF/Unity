using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarNombreTexto : MonoBehaviour
{
    //Declaramos variables para obtener las referencias de:
    GameObject parte1; // Motor.
    GameObject parte2; // Propulsor.
    GameObject parte3; // Navegacion.
    GameObject parte4; // Carrocer?a.
    char numeroOpcion; // N?mero que hace referencia de la ubicaci?n del texto (opcion1 <- 1).
    public int etapa; // N?mero de la etapa (depende de la etapa en la que nos encontremos).
    GameObject optionButton; // Referencia a la opci?n de la que es titulo este GameObject.

    // Start is called before the first frame update
    void Start()
    {
        
        etapa = GameObject.Find("Entrenador").GetComponent<Entrenador>().etapa; //Obtiene referencia de la etapa en la que se encontraron
        numeroOpcion = this.name[this.name.Length-5];  //Obtiene el n?mero que acompa?a al nombre
        
        //Referencias
        //Referencias a las parte 1
        parte1 = GameObject.Find("Motor"+numeroOpcion);
        //Referencias a las parte 2
        parte2 = GameObject.Find("Propulsor"+numeroOpcion);
        //Referencias a las parte 3
        parte3 = GameObject.Find("Navegacion"+numeroOpcion);
        //Referencias a las parte 4
        parte4 = GameObject.Find("Carroceria"+numeroOpcion);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void cambiarNombre()
    {
        etapa = GameObject.Find("Entrenador").GetComponent<Entrenador>().etapa; // Obtiene referencia de la etapa en la que se encontraron
        
        // Validaciones para obtener determinado componente dependiendo de la etapa
        //** Esto es para obtener la variable p?blica llamada "nombre" para ponerlo como t?tulo

        if (parte1.GetComponent<MeshRenderer>().enabled==true && etapa==1)
        {
            this.GetComponent<Text>().text = parte1.GetComponent<PartesEstadisticas>().nombre; // Etapa 1: S?lo motores
        }
        else if (parte2.GetComponent<MeshRenderer>().enabled==true && etapa==2)
        {
            this.GetComponent<Text>().text = parte2.GetComponent<PartesEstadisticas>().nombre; // Etapa 2: S?lo propulsores
        }
        else if (parte3.GetComponent<MeshRenderer>().enabled==true && etapa==3)
        {
            this.GetComponent<Text>().text = parte3.GetComponent<PartesEstadisticas>().nombre; // Etapa 3: S?lo Navegaci?n
        }
        else if (parte4.GetComponent<MeshRenderer>().enabled==true && etapa==4)
        {
            this.GetComponent<Text>().text = parte4.GetComponent<PartesEstadisticas>().nombre; // Etapa 4: S?lo Carrocer?a
        }
    }
}