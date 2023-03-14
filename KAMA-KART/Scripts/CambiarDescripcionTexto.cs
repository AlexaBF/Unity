using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarDescripcionTexto : MonoBehaviour
{
    //Declaramos variables para obtener las referencias de:
    GameObject parte1; // Motor.
    GameObject parte2; // Propulsor.
    GameObject parte3; // Navegacion.
    GameObject parte4; // Carrocería.
    char numeroOpcion; // Número que hace referencia de la ubicación del texto (opcion1 <- 1).
    public int etapa; // Número de la etapa (depende de la etapa en la que nos encontremos).
    GameObject optionButton; // Referencia a la opción de la que es titulo este GameObject.

    // Start is called before the first frame update
    void Start()
    {
        
        etapa = GameObject.Find("Entrenador").GetComponent<Entrenador>().etapa; //Obtiene referencia de la etapa en la que se encontraron
        numeroOpcion = this.name[this.name.Length-1];  //Obtiene el número que acompaña al nombre
        
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

    public void cambiarDescripcion()
    {
        etapa = GameObject.Find("Entrenador").GetComponent<Entrenador>().etapa; // Obtiene referencia de la etapa en la que se encontraron
        
        // Validaciones para obtener determinado componente dependiendo de la etapa
        //** Esto es para obtener la variable pública llamada "nombre" para ponerlo como título

        if (parte1.GetComponent<MeshRenderer>().enabled==true && etapa==1)
        {
            // Etapa 1: Sólo motores
            this.GetComponent<Text>().text = "Velocidad: " + parte1.GetComponent<PartesEstadisticas>().velocidad + "  " + 
            "Aceleracion: " + parte1.GetComponent<PartesEstadisticas>().aceleracion + "  " + 
            "Manejo: " + parte1.GetComponent<PartesEstadisticas>().manejo + "  " + 
            "Proteccion: " + parte1.GetComponent<PartesEstadisticas>().proteccion + "  " + 
            "Peso: " + parte1.GetComponent<PartesEstadisticas>().peso + "  " + 
            "Gasolina: " + parte1.GetComponent<PartesEstadisticas>().gasolina;
        }
        else if (parte2.GetComponent<MeshRenderer>().enabled==true && etapa==2)
        {
            // Etapa 2: Sólo propulsores
            this.GetComponent<Text>().text = "Velocidad: " + parte2.GetComponent<PartesEstadisticas>().velocidad + "  " + 
            "Aceleracion: " + parte2.GetComponent<PartesEstadisticas>().aceleracion + "  " + 
            "Manejo: " + parte2.GetComponent<PartesEstadisticas>().manejo + "  " + 
            "Proteccion: " + parte2.GetComponent<PartesEstadisticas>().proteccion + "  " + 
            "Peso: " + parte2.GetComponent<PartesEstadisticas>().peso + "  " + 
            "Gasolina: " + parte2.GetComponent<PartesEstadisticas>().gasolina;
        }
        else if (parte3.GetComponent<MeshRenderer>().enabled==true && etapa==3)
        {
            // Etapa 3: Sólo Navegación
            this.GetComponent<Text>().text = "Velocidad: " + parte3.GetComponent<PartesEstadisticas>().velocidad + "  " + 
            "Aceleracion: " + parte3.GetComponent<PartesEstadisticas>().aceleracion + "  " + 
            "Manejo: " + parte3.GetComponent<PartesEstadisticas>().manejo + "  " + 
            "Proteccion: " + parte3.GetComponent<PartesEstadisticas>().proteccion + "  " + 
            "Peso: " + parte3.GetComponent<PartesEstadisticas>().peso + "  " + 
            "Gasolina: " + parte3.GetComponent<PartesEstadisticas>().gasolina;
        }
        else if (parte4.GetComponent<MeshRenderer>().enabled==true && etapa==4)
        {
            // Etapa 4: Sólo Carrocería
            this.GetComponent<Text>().text = "Velocidad: " + parte4.GetComponent<PartesEstadisticas>().velocidad + "  " + 
            "Aceleracion: " + parte4.GetComponent<PartesEstadisticas>().aceleracion + "  " + 
            "Manejo: " + parte4.GetComponent<PartesEstadisticas>().manejo + "  " + 
            "Proteccion: " + parte4.GetComponent<PartesEstadisticas>().proteccion + "  " + 
            "Peso: " + parte4.GetComponent<PartesEstadisticas>().peso + "  " + 
            "Gasolina: " + parte4.GetComponent<PartesEstadisticas>().gasolina;
        }
    }
}
