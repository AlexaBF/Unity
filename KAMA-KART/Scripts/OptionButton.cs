using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    //Referencia del componente bot�n de si mismo
    Button btnopcion;

    //Referencias del componente activo (Esto pr�ximamente se usa en el c�lculo)
    public float velocidad;
    public float aceleracion;
    public float peso;
    public float proteccion;
    public float manejo;
    public float gasolina;

    //Variable de tipo Entrenador para cambiar la "Escena" (Los componentes mostrados en pantalla)
    public Entrenador Escena;

    // Variable char que hace referencia al n�mero de opcion que (opcion1 <- 1).
    char numeroOpcion;
    // Valor numerico que hace referencia al n�mero de opcion que  (opcion1 <- 1).
    int opcion;

    void Start()
    {
        numeroOpcion = this.name[this.name.Length-1]; //Obtiene el n�mero que acompa�a al nombre
        btnopcion = this.GetComponent<Button>(); //Referencia del bot�n
        //btnopcion.onClick.AddListener(OptionAction); //AddListener
        Escena = FindObjectOfType<Entrenador>(); //Obteniendo referencia de tipo Entrenador
        opcion = (int)char.GetNumericValue(numeroOpcion); // Se pasa a int la variable numeroOpcion
    }
    /*
    // Funci�n para contabilizar las veces que se elige una opci�n y cual es 
    public void OptionAction()
    {
        Escena.contadorVotos(opcion-1); // Se llama a la funcion contadorVotos (que suma la cantidad de votos y de donde son) desde entrenador
    }
    */
    // Funci�n para obtener cada uno de los par�metros/valores de las opciones que se presentan en pantalla (las 4 opciones)
    //Esta referencia se obtiene de "PartesEstadisticas)
    public void ValoresOpciones(GameObject parte)
    {
        velocidad = parte.GetComponent<PartesEstadisticas>().velocidad;
        aceleracion = parte.GetComponent<PartesEstadisticas>().aceleracion;
        peso = parte.GetComponent<PartesEstadisticas>().peso;
        proteccion = parte.GetComponent<PartesEstadisticas>().proteccion;
        manejo = parte.GetComponent<PartesEstadisticas>().manejo;
        gasolina = parte.GetComponent<PartesEstadisticas>().gasolina;
    }

    void Update()
    {
      
    }
}