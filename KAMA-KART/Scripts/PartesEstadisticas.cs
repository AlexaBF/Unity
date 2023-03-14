using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartesEstadisticas : MonoBehaviour
{
    public string nombre = " "; // Nombre de la parte
    // Estadisticas que tendr? cada una de las piezas

    public float peso;
    public float velocidad; 
    public float aceleracion;
    public float manejo;
    public float proteccion;
    public float gasolina;


    char numeroParte; // Determina el n?mero que acompa?a al nombre ejemplo: "Carrocer?a2"

    OptionButton optionButton; // Referencia sobre el boton donde est? la respectiva pieza
    // Siempre la pieza n est? en la opcion n (ej.: Motor 1 est? con Opni?n 1)

    void Start()
    {

    }
    void Update()
    {
        numeroParte = this.name[this.name.Length-1]; //Obtiene el n?mero que acompa?a al nombre

        //Si el componente est? activo 
        if (this.GetComponent<MeshRenderer>().enabled==true)
        {
            optionButton=GameObject.Find("Opcion" + numeroParte).GetComponent<OptionButton>(); //Encuentra referencia del nombre del bot?n
            optionButton.ValoresOpciones(this.gameObject); //Llama a la funci?n "ValoresOpciones" de "OptionButton" para obtener la referencia del bot?n
        }
    }
}