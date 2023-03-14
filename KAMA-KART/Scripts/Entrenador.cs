using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Entrenador : MonoBehaviour
{
    // Gestión del desempeño (Todos se recalculan)
    public float desempenofinal; //Variable donde se guarda el desempeño final

    // Variable donde se guarda el desempeño de los parámetros (Esto se recalcula dependiendo de lo escogido)
    public float desempenoVelocidad;
    public float desempenoAceleracion;
    public float desempenoPeso;
    public float desempenoProteccion;
    public float desempenoManejo;
    public float desempenoGasolina;

    // Estadisticas promediadas de las elegidas para la nave 
    float velocidad;
    float peso;
    float aceleracion;
    float manejo;
    float gasolina;
    float proteccion;

    // Número de la etapa (depende de la etapa en la que nos encontremos).
    public int etapa;

    // Arreglo para saber si todos han realizado la votación  (y qué opciones votaron)
    int[] contadorOpciones = { 0, 0, 0, 0 };

    // Variable que determina el número de pieza seleccionada
    public int piezaSeleccionada = 0;

    // Auxiliar para revisar cúal es la pieza más votada por los agentes
    int auxVotacion = -1;

    // Arreglo de las opciones para saber las estadísticas de la opción escogida
    OptionButton[] optionButtons;

    // Cantidad de clicks
    public int cantidadClick;

    // arreglo de agentes
    AstronautAgent[] Agents;

    //Arreglo de desempenos
    public float[] desempenosFinales = { 0, 0, 0, 0 }; //Motor Propulsor Navegacion Carrocería
    public float[] desempenosFinalesFinales = { 0, 0, 0, 0,0,0 }; //Motor Propulsor Navegacion Carrocería Peso Gasolina
    public float[] desempenoTodos = { 0, 0, 0, 0, 0, 0 };

    CambiarNombreTexto[] cambiarNombreText;
    CambiarDescripcionTexto[] cambiarDescripcionText;

    Text votacion;

    public int contarVotos = 0;

    public bool SeVota = false;

    public bool LosDemasVotan = true;

    public bool SeReinicia;
    void Start()
    {
        SeReinicia = false;
        etapa = 1; //Siempre se inicia en la etapa 1
        optionButtons = FindObjectsOfType<OptionButton>(); //Referencia de OptionButton
        Agents = FindObjectsOfType<AstronautAgent>();
        cambiarNombreText = FindObjectsOfType<CambiarNombreTexto>();
        cambiarDescripcionText = FindObjectsOfType<CambiarDescripcionTexto>();
    }

    //Función que se llama si sólo si se temrina por completo las etapas (si llega a la etapa 4 o si se muere alguno de los agentes) 
    //Esta función iguala a 0 todas las variables utilizadas en el programa
    public void reinicio()
    {
        SeReinicia = true;
        etapa = 1; //Siempre inicia en etapa 1
        cantidadClick = 0; // Reinicio a 0 de la cantidad de votos acumulados

        for (int y = 0; y > 4; y++)  //Iguala a 0 los elementos del arreglo
        {
            contadorOpciones[y] = 0;
        }

        // Reinicio de las estadistocas de la nave a 0
        velocidad = 0;
        aceleracion = 0;
        peso = 0;
        proteccion = 0;
        manejo = 0;
        gasolina = 0;


        piezaSeleccionada = 0; //Reinicio de la variable de la pieza seleccionada
        auxVotacion = -1; //Reinicio de la variable auxiliar que revisa la pieza más votada por los agentes   

        desempenofinal = 0; // Reinicio del desempeño total de la nave a 0
        //Reinicio de los desempeños por especialidades
        desempenoVelocidad = 0;
        desempenoPeso = 0;
        desempenoAceleracion = 0;
        desempenoProteccion = 0;
        desempenoManejo = 0;
        desempenoGasolina = 0;


        //Reinicio de los desempenos finales por categorías 
        for (int t = 0; t < 4; t++)  //Iguala a 0 los elementos del arreglo
        {
            desempenosFinales[t] = 0;
        }

        for (int t = 0; t < 6; t++)  //Iguala a 0 los elementos del arreglo
        {
            desempenosFinalesFinales[t] = 0;
        }

        for (int t = 0; t < 6; t++)  //Iguala a 0 los elementos del arreglo
        {
            desempenoTodos[t] = 0;
        }
        

    }

    public void calculo()//float velocidad, float peso ,float aceleracion, float manejo)
    {

        // Revision para saber cual fue la pieza seleccionada. (más votada)
        // Censurar en caso de que el jugador sea todopoderoso
        for (int a = 0; a < 4; a++)
        {
            //Debug.Log("contadorOpciones[" + a + "]: " + contadorOpciones[a]);
            if (auxVotacion < contadorOpciones[a])
            {
                //Debug.Log("contadorOpciones[a]: " + contadorOpciones[a]);
                auxVotacion = contadorOpciones[a];
                piezaSeleccionada = a;
            }
        }
        //Debug.Log("PiezaGanadora: " + piezaSeleccionada);
        if (etapa == 1)
        {
            // Si Etapa=1, las estadísticas valdrán lo que obtiene como referencia de los parámetros de la pieza seleccionada
            velocidad = optionButtons[piezaSeleccionada].velocidad;
            peso = optionButtons[piezaSeleccionada].peso;
            aceleracion = optionButtons[piezaSeleccionada].aceleracion;
            manejo = optionButtons[piezaSeleccionada].manejo;
            proteccion = optionButtons[piezaSeleccionada].proteccion;
            gasolina = optionButtons[piezaSeleccionada].gasolina;
        }
        else
        {
            // Si etapa!=1, las estadísticas de la nave serán el promedio de (lo que valían + el valor de la pieza seleccionada)
            velocidad = (velocidad + optionButtons[piezaSeleccionada].velocidad) / 2;
            peso = (peso + optionButtons[piezaSeleccionada].peso) / 2;
            aceleracion = (aceleracion + optionButtons[piezaSeleccionada].aceleracion) / 2;
            manejo = (manejo + optionButtons[piezaSeleccionada].manejo) / 2;
            proteccion = (proteccion + optionButtons[piezaSeleccionada].proteccion) / 2;
            gasolina = (gasolina + optionButtons[piezaSeleccionada].gasolina) / 2;
        }


        // C Á L C U L O   D E   L A   N A V E 
        //Velocidad
        /*
        Velocidad: 
        20 pts desempeño → vel = 8
        16 pts desempeño → vel = 7 | vel = 9
        12 pts desempeño → vel = 6 | vel = 10
        8 pts desempeño → vel = 5
        4 pts desempeño → vel = 4
        2 pts desempeño → vel = 3
        1 pts desempeño → vel = 2
        0 pts desempeño → vel = 1

        */
        if (velocidad == 8) desempenoVelocidad = 20;
        else if ((7 <= velocidad && velocidad < 8) || (8 > velocidad && velocidad <= 9)) desempenoVelocidad = 15;
        else if ((velocidad >= 6 && velocidad < 7) || (velocidad > 9 && velocidad <= 10)) desempenoVelocidad = 10;
        else if (velocidad < 6 && velocidad >= 4) desempenoVelocidad = 5;
        else if (velocidad < 4) desempenoVelocidad = 1;


        //Peso
        if (peso == 5) desempenoPeso = 10;
        else if ((peso >= 3 && peso < 5) || (peso > 5 && peso <= 7)) desempenoPeso = 5;
        else if ((peso >= 1 && peso < 3) || (peso > 7 && peso <= 10)) desempenoPeso = 1;



        //Aceleración
        /*
        Aceleración:
        25 pts desempeño → ace = 6
        15 pts desempeño → ace= 5 | ace= 7
        8 pts desempeño → ace= 4 | ace=8
        4 pts desempeño → ace=3 | ace=9
        1 pts desempeño → ace= 2 | ace=10
        0  pts desempeño → ace=1


        */
        if (aceleracion == 6) desempenoAceleracion = 20;
        else if ((aceleracion >= 5 && aceleracion < 6) || (aceleracion > 6 && aceleracion <= 7)) desempenoAceleracion = 15;
        else if ((aceleracion >= 4 && aceleracion < 5) || (aceleracion > 7 && aceleracion <= 8)) desempenoAceleracion = 10;
        else if ((aceleracion >= 3 && aceleracion < 4) || (aceleracion > 8 && aceleracion <= 9)) desempenoAceleracion = 5;
        else if ((aceleracion < 3) || (aceleracion > 9)) desempenoAceleracion = 1;



        //Manejo
        if (manejo == 6) desempenoManejo = 20;
        else if ((5 <= manejo && manejo < 6) || (6 < manejo && manejo <= 7)) desempenoManejo = 15;
        else if ((4 <= manejo && manejo < 5) || (7 < manejo && manejo <= 8)) desempenoManejo = 10;
        else if ((3 <= manejo && manejo < 4) || (8 < manejo && manejo <= 9)) desempenoManejo = 5;
        else if ((1 <= manejo && manejo < 3) || (9 < manejo && manejo <= 10)) desempenoManejo = 1;

        //Gasolina

        if (gasolina == 7) desempenoGasolina = 10;
        else if (4 <= gasolina && gasolina < 7 || 7 < gasolina && gasolina <= 9) desempenoGasolina = 5;
        else if (gasolina < 4 || (9 < gasolina)) desempenoGasolina = 1;


        //Protección
        /*
        15 pts desempeño → protección = 7
        10 pts desempeño → 5 <= protección < 7 | 7 < protección <= 9
        5 pts desempeño → 3 <= protección < 5 | 9 < protección <= 10
        0 pts desempeño →  protección < 3
        */


        if (proteccion == 7) desempenoProteccion = 20;
        else if ((proteccion >= 6 && proteccion < 7) || (proteccion > 7 && proteccion <= 8)) desempenoProteccion = 15;
        else if ((proteccion >= 5 && proteccion < 6) || (proteccion > 8 && proteccion <= 9)) desempenoProteccion = 10;
        else if ((proteccion >= 4 && proteccion < 5) || (proteccion > 9 && proteccion <= 10)) desempenoProteccion = 5;
        else if (proteccion < 4) desempenoProteccion = 1;

        desempenoTodos[0] += desempenoVelocidad;
        desempenoTodos[1] += desempenoAceleracion;
        desempenoTodos[2] += desempenoPeso;
        desempenoTodos[3] += desempenoProteccion;
        desempenoTodos[4] += desempenoManejo;
        desempenoTodos[5] += desempenoGasolina;



        //Cálculo final 
        desempenofinal = (desempenoTodos[0] / etapa) + (desempenoTodos[1] / etapa) + (desempenoTodos[2] / etapa) + (desempenoTodos[3] / etapa) + (desempenoTodos[4] / etapa) + (desempenoTodos[5] / etapa);
        desempenosFinales[0] += desempenoVelocidad;
        desempenosFinales[1] += desempenoAceleracion;
        desempenosFinales[2] += desempenoManejo;
        desempenosFinales[3] += desempenoProteccion;



        //Todo desempeño

        //Finales finales
        desempenosFinalesFinales[0] += optionButtons[piezaSeleccionada].velocidad;
        desempenosFinalesFinales[1] += optionButtons[piezaSeleccionada].aceleracion;
        desempenosFinalesFinales[2] += optionButtons[piezaSeleccionada].manejo;
        desempenosFinalesFinales[3] += optionButtons[piezaSeleccionada].proteccion;
        desempenosFinalesFinales[4] += optionButtons[piezaSeleccionada].peso;
        desempenosFinalesFinales[5] += optionButtons[piezaSeleccionada].gasolina;
        PlayerPrefs.SetFloat("desempenoVelocidad", desempenosFinalesFinales[0]);
        PlayerPrefs.SetFloat("desempenoAceleracion", desempenosFinalesFinales[1]);
        PlayerPrefs.SetFloat("desempenoManejo", desempenosFinalesFinales[2]);
        PlayerPrefs.SetFloat("desempenoProteccion", desempenosFinalesFinales[3]);
        PlayerPrefs.SetFloat("desempenoPeso", desempenosFinalesFinales[4]);
        PlayerPrefs.SetFloat("desempenoGasolina", desempenosFinalesFinales[5]);
        PlayerPrefs.SetInt("etapa", etapa);
        PlayerPrefs.SetFloat("desempenofinal", desempenofinal);
    }

    //Función para realizar el conteo de votos totales  (y por opción)
    public void contadorVotos(int opcion, string name)
    {
        if (name != "Astronauta5")
        {
            contadorOpciones[opcion] = contadorOpciones[opcion] + 1;
        }

        else
        {
            contadorOpciones[opcion] = contadorOpciones[opcion] + 5;
        }
        //Debug.Log("OPCION "+ opcion);

        foreach (AstronautAgent agent in Agents)
        {
            if (agent.name == name)
                agent.piezaSeleccionada = opcion;
            if (agent.EsJugador)
                piezaSeleccionada = opcion;
        }
        contarVotos++;
        //if(contarVotos == 4) { SeVota = true; } //PARA ENTRENAR
        if(contarVotos == 5) { SeVota = true; } //PARA JUGAR
    }

    public void cambioEtapa()
    {
        for (int x = 1; x <= 4; x++)
        {
            if (etapa == 1)
            {

                //Activar y desactivar los componentes para simular el cambio de "escena"
                GameObject.Find("Motor" + x).GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Propulsor" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Navegacion" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Carroceria" + x).GetComponent<MeshRenderer>().enabled = false;


            }
            else if (etapa == 2)
            {

                //Activar y desactivar los componentes para simular el cambio de "escena"
                GameObject.Find("Motor" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Propulsor" + x).GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Navegacion" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Carroceria" + x).GetComponent<MeshRenderer>().enabled = false;


            }
            else if (etapa == 3)
            {

                //Activar y desactivar los componentes para simular el cambio de "escena"
                GameObject.Find("Motor" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Propulsor" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Navegacion" + x).GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Carroceria" + x).GetComponent<MeshRenderer>().enabled = false;

            }
            else if (etapa == 4)
            {

                //Activar y desactivar los componentes para simular el cambio de "escena"
                GameObject.Find("Motor" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Propulsor" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Navegacion" + x).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Carroceria" + x).GetComponent<MeshRenderer>().enabled = true;

            }
        }
        //LLama a Cambiar nombre 
        foreach (CambiarNombreTexto c in cambiarNombreText)
        {
            c.cambiarNombre();
        }
        //LLama a Cambiar nombre 
        foreach (CambiarDescripcionTexto d in cambiarDescripcionText)
        {
            d.cambiarDescripcion();
        }
    }

    public void ReinicioVotos()
    {
        for (int y = 0; y < 4; y++)  //Actualiza el arreglo de las opciones seleccionadas en 0 
        {
            contadorOpciones[y] = 0;
        }
        contarVotos = 0;
        auxVotacion = -1;
        piezaSeleccionada = 0;
    }


    //Funcion de cambio de escena por etapa;
    //-> se carga etapa
    //-> request de acciones a todo agente
    //-> se calcula los resultados de la etapa
    //-> asignación de rewards por resultados de la etapa
    //-> se reinician los votos
    //-> se repite el ciclo;

    void Update()
    {
        //PARA JUGAR
        cambioEtapa();
       // if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
            if (LosDemasVotan)
            {
                cambioEtapa();

                foreach (AstronautAgent agent in Agents)
                {

                    if (!agent.EsJugador) agent.RequestDecision();
                    //Debug.Log("agente: " + agent.name);
                    //Debug.Log(agent.name + agent.piezaSeleccionada);
                }
                LosDemasVotan = false;
            }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //etapa++;
            //Debug.Log("etapa ahora: " + etapa);
            cambioEtapa();

            foreach (AstronautAgent agent in Agents)
            {

                if(agent.EsJugador) agent.RequestDecision();
                //Debug.Log("agente: " + agent.name);
                //Debug.Log(agent.name + agent.piezaSeleccionada);
            }

        }
        
        if (SeVota)
        {
            calculo();
            foreach (AstronautAgent agent in Agents)
            {
                agent.revisionRewards();
                //agent.fillbar1.fillAmount = agent.Actual1 / agent.Max;
            }
            if (etapa != 4)
            {
                if (!SeReinicia) { etapa++; }
                else { SeReinicia = false; }
            }
            else
            {
                reinicio();
                SeReinicia = false;
            }
            ReinicioVotos();
            SeVota = false;
            LosDemasVotan = true;
        }

        /*
        //PARA ENTRENAR
        //etapa++;
        Debug.Log("etapa ahora: " + etapa);
        cambioEtapa();

        foreach (AstronautAgent agent in Agents)
        {
            agent.RequestDecision();
            //Debug.Log("agente: " + agent.name);
            //Debug.Log(agent.name + agent.piezaSeleccionada);
        }
        if (SeVota)
        {
            calculo();
            foreach (AstronautAgent agent in Agents)
            {
                agent.revisionRewards();
                //agent.fillbar1.fillAmount = agent.Actual1 / agent.Max;
            }
            if (etapa != 4)
            {
                if (!SeReinicia) { etapa++; }
                else { SeReinicia = false; }
            }
            else
            {
                reinicio();
                SeReinicia = false;
            }
            ReinicioVotos();
            SeVota = false;
        }
        */
        
    }
}
