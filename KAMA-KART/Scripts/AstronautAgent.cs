using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;


public class AstronautAgent : Agent
{
    //Aciones 
    const int VOTAR1 = 0;
    const int VOTAR2 = 1;
    const int VOTAR3 = 2;
    const int VOTAR4 = 3;
    bool SE_PUEDE_VOTAR = true;

    //INICIALIZACI?N

    //Personalidad 
    public int personalidad;

    //Desempe?os
    public Entrenador entrenador;
    float desempenoViejo;
    float desempenoActual;
    float[] desempenosFinales = { 0, 0, 0, 0 };
    float desempenoEspViejo;
    float desempenoEspActual;

    //Barras 
    public const float Max = 100.0f; //Todas tienen un valor m?ximo de 100%

    //Personalidades 
    int[] personalidades = { 0, 0, 0, 0 }; //Flem?tico 1, Sanguineo 2, Melanc?lico 3, Col?rito 4
    float[] tolerancias = {0.2f, 0.5f, 0.7f, 1}; //Arreglo de tolerancias

    //Especialidad
    int[] especialidades = { 0, 0, 0, 0 }; //Motor Propulsor Navegacion Carrocer?a -> Velocidad  Aceleracion  Manejo  Protecci?n
    public int especialidad;

    OptionButton[] optionButtons; //Arreglo para obtener referencias de los par?metros de las opciones

    //Referencias
    public Image fillbar1;
    public float Actual1; //ACTUAL 1 SIEMPRE ES EL PROPIO AGENTE
    Image fillbar2;
    public float Actual2; //Otro agente
    Image fillbar3;
    public float Actual3; //Otro agente
    Image fillbar4;
    public float Actual4; //Otro agente
    string all = "1234"; //String para saber qu? agente ocupa el Script y por lo tanto saber los agentes restantes
    char numeroAgente; //N?mero del agente
    int numDelAgente; //Numero (en Int) del agente

    //Etapa y c?lculos
    int etapa;

    //Caracteristicas
    int[] arreglo1 = {1,2,3,4 };

    //AstronautAgent[] otherAgents; //Arreglo para guardar los otros agentes restantes
    AstronautAgent[] otherAgents = new AstronautAgent[3]; //Arreglo para guardar los otros agentes restantes
    
    public int piezaSeleccionada; //N?mero de la pieza selecciono por el agente en cada etapa;
    public int piezaGanadora; //N?mero de la pieza ganadora en cada etapa;

    Image Image;
    int voto;

    GameObject Barrita;

    MostrarVoto txtVoto; //Para cambiar e texto y mostrar por qu? componente est?n votando

    public bool EsJugador = false;


    int NextAction;

    public override void Initialize()
    {
        //Referencias para obtener desempe?os
        entrenador = GameObject.Find("Entrenador").GetComponent<Entrenador>();
        desempenoViejo = entrenador.desempenofinal;
     
        //Referencias para obtener etapa nueva
        etapa = entrenador.etapa;

        if (!EsJugador)
        {
            numeroAgente = this.name[this.name.Length - 1]; //Obtiene el n?mero que acompa?a al nombre
            numDelAgente = (int)char.GetNumericValue(numeroAgente);



            // F I L L B A R
            //fillbar propio
            fillbar1 = GameObject.Find("FillBar" + numeroAgente).GetComponent<Image>(); //Obtiene referencia para mostrar la barra
                                                                                        //Debug.Log(numeroAgente);
            Barrita = GameObject.Find("FillBar" + numeroAgente);
            //Debug.Log(Barrita.name);
            Actual1 = Max;//fillbar1.fillAmount; // "Vida" Actual
            all = all.Replace("" + numeroAgente, ""); ////Quitar el agente registrado
                                                      //Referencia de los otros agentes (no se incluye al propio agente)
        }

        for (int y = 0; y < 3; y++)
        {
            otherAgents[y] = GameObject.Find("Astronauta" + all[y]).GetComponent<AstronautAgent>();
        }

        //fillbar de los otros agentes
        fillbar2 = GameObject.Find("FillBar" + all[0]).GetComponent<Image>(); //Obtiene referencia para mostrar la barra de "vida" de alg?n agente
        Actual2 = fillbar2.fillAmount; // "Vida" Actual de alg?n agente
        fillbar3 = GameObject.Find("FillBar" + all[1]).GetComponent<Image>(); //Obtiene referencia para mostrar la barra de "vida" de alg?n agente
        Actual3 = fillbar3.fillAmount; // "Vida" Actual de alg?n agente
        fillbar4 = GameObject.Find("FillBar" + all[2]).GetComponent<Image>(); //Obtiene referencia para mostrar la barra de "vida" de alg?n agente
        Actual4 = fillbar4.fillAmount; // "Vida" Actual de alg?n agente

        //Obtener de option button 
        optionButtons = FindObjectsOfType<OptionButton>();

        if (!EsJugador)
        {
            txtVoto = GameObject.Find("txtVotacion" + numDelAgente).GetComponent<MostrarVoto>();
            //votacion.text = "0";
            //GameObject.Find("txtVotacion" + numDelAgente).GetComponent<Text>().text = "0";
            Image = GameObject.Find("CaritaFeliz" + numDelAgente).GetComponent<Image>();
        }
        personalidad = 0;

        //Actual1 = 100.0f;
        //Barrita.GetComponent<ManejoBarra>().Barritas();

    }





    //El episodio que inicia
    public override void OnEpisodeBegin() 
    {
        //Barrita = GameObject.Find("FillBar" + numeroAgente);
        
        //Barrita.GetComponent<ManejoBarra>().Actual = 100.0f;
        //Barrita.GetComponent<ManejoBarra>().Reinicio();
        //Actual1 = fillbar1.fillAmount * 100;
        /*
        while (fillbar1.fillAmount != 1)
        {
            fillbar1.fillAmount = 1;
            Actual1 = 100.0f;
            Barrita.GetComponent<ManejoBarra>().Actual = 100.0f;
        }
        */
        if (!EsJugador)
        {
            //Debug.Log(this.name);
            //Debug.Log("ANTES: " + this.name + " " + Actual1);

            Actual1 = Max; //Rellenado de las barras de vida
            fillbar1.fillAmount = 1;
            //Debug.Log("Barrita: " + fillbar1.fillAmount + " Actual: " + Actual1);
            //entrenador.reinicio(); //Llama a funci?n "reinicio" ubicado en Entrenador (Para inicializar las variables)
        }

        SE_PUEDE_VOTAR = true; //Siempre al inicio se puede votar

        desempenoViejo = entrenador.desempenofinal; // Asignaci?n de desempe?o "viejo" de la nave
        etapa = entrenador.etapa;//Referencia de etapa ;

        //Arreglo de personalidades
        if (!EsJugador)
        {
            for (int i = 0; i < 4; i++)
            {
                personalidades[i] = 0;
            }
            personalidad = 0;
            personalidad = Random.Range(1, 5); // Personalidad Random
            personalidades[personalidad - 1] = 1; //Se determina la personalidad dentro de un arreglo
            Image = GameObject.Find("CaritaFeliz" + numDelAgente).GetComponent<Image>();
            if (personalidad == 1) Image.color = new Color32(70, 247, 54, 255); //Flem?tico
            else if (personalidad == 2) Image.color = new Color32(247, 244, 54, 255); //Sanguineo
            else if (personalidad == 3) Image.color = new Color32(70, 54, 247, 255); //Melancolico
            else if (personalidad == 4) Image.color = new Color32(255, 0, 0, 255); //Colerico


            //Arreglo de especialidades
            for (int i = 0; i < 4; i++)
            {
                especialidades[i] = 0;
            }
            especialidades[numDelAgente - 1] = 1;
            especialidad = numDelAgente;
        }
        
        for (int x=0; x < 4; x++)
        {
            desempenosFinales[x] = entrenador.desempenosFinales[x];
        }


        //piezaSeleccionada = -1; //Reinicio del valor de su pieza seleccionada
        //piezaGanadora = -1; //Reinicio del valor de la pieza ganadora
        if (!EsJugador)
        {
            desempenoEspActual = desempenosFinales[numDelAgente - 1];
            desempenoEspViejo = desempenoEspActual;

            txtVoto.votoSeleccionado("0");
        }
    }





    public override void CollectObservations(VectorSensor sensor)
    {
        etapa = entrenador.etapa;

        Actual2 = fillbar2.fillAmount;
        Actual3 = fillbar3.fillAmount;
        Actual4 = fillbar4.fillAmount;

        //Personalidad - Tolerancia
        //Arreglo de personalidades
        
        for(int i = 0; i < 4; i++)
        {
            sensor.AddObservation(personalidades[i]);//4
        }
        

        //Etapa 
        sensor.AddObservation(etapa);//5

        //Especialidad 
        for (int i = 0; i < 4; i++)
        {
            sensor.AddObservation(especialidades[i]);//9
        }

        //Barras actuales
        if (!EsJugador)
        {
            sensor.AddObservation(Actual1);//10
        }
        else
        {
            sensor.AddObservation(0.0f);
        }
        sensor.AddObservation(Actual2);//11
        sensor.AddObservation(Actual3);//12
        sensor.AddObservation(Actual4);//13

        //Estadisticas de cada opci?n activa
        foreach (OptionButton op in optionButtons) //29
        {
            sensor.AddObservation(op.velocidad);
            sensor.AddObservation(op.aceleracion);
            sensor.AddObservation(op.manejo);
            //sensor.AddObservation(op.peso);
            sensor.AddObservation(op.proteccion);
            //sensor.AddObservation(op.gasolina);
        }
    }





    //Funci?n para realizar la actualizaci?n de la barra basada en la personalidad;
    public void ajusteMedidor()
    {
        //Debug.Log("Se ajusta");
        for (int numPer = 0; numPer < 4; numPer++)
        {
            if (personalidad == numPer+1) //N?mero de la personalidad 
            {
                Actual1 = Actual1 - (45 * tolerancias[numPer]);
                if (Actual1 <= 0) //?La Barra es nula?
                {
                    //Debug.Log(this.name + " Se murio?");
                    // Penalizaci?n a los otros agentes si uno muere (no se penaliza al agente que muere);
                    // Tambi?n se termina el episodio para todos los agentes
                    //entrenador.reinicio();
                    
                    for (int agent = 0; agent < 3; agent++)
                    {
                        //Debug.Log(otherAgents[agent].name);
                        otherAgents[agent].murioAlguien();
                    }
                    entrenador.reinicio();
                    //Carga escena 
                    SceneManager.LoadScene("Ending"); //PARA JUGAR
                    EndEpisode();
                }
                break;
            }
        }
    }

    // funci?n para penalizar y terminar los episodios a los agentes si uno muere
    public void murioAlguien()
    {
        SetReward(-3.0f);
        EndEpisode();
    }





   public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        actionMask.SetActionEnabled(0, VOTAR1, SE_PUEDE_VOTAR);
        actionMask.SetActionEnabled(0, VOTAR2, SE_PUEDE_VOTAR);
        actionMask.SetActionEnabled(0, VOTAR3, SE_PUEDE_VOTAR);
        actionMask.SetActionEnabled(0, VOTAR4, SE_PUEDE_VOTAR);
    }





    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int action = actionBuffers.DiscreteActions[0];
        //Acciones
        if (action==VOTAR1)
        {
            //Debug.Log(this.name + " Votar 1");
            voto = 1;
            piezaSeleccionada = 0;
            entrenador.contadorVotos(0, this.name);
        }
        else if (action == VOTAR2)
        {
            //Debug.Log(this.name + " Votar 2");
            voto = 2;
            piezaSeleccionada = 1;
            entrenador.contadorVotos(1, this.name);
        }
        else if (action == VOTAR3)
        {
            //Debug.Log(this.name + " Votar 3");
            voto = 3;
            piezaSeleccionada = 2;
            entrenador.contadorVotos(2, this.name);
        }
        else if (action == VOTAR4)
        {
            //Debug.Log(this.name + " Votar 4");
            voto = 4;
            piezaSeleccionada = 3;
            entrenador.contadorVotos(3, this.name);
        }
        //entrenador.contadorVotos(voto - 1);
        piezaSeleccionada = voto - 1;
        if (!EsJugador)
        {
            txtVoto.votoSeleccionado("" + (piezaSeleccionada + 1));
        }
    }



    public void calcDesempenoEspecialidad()
    {
        //Si el desempe?o de la especialidad ha mejorado
        if(desempenoEspActual > desempenoEspViejo) 
        {
            if (desempenoEspActual >= 15*etapa) AddReward(+0.6f);
            else if (desempenoEspActual >= 12.5*etapa) AddReward(+0.3f);
            else AddReward(+0.15f);
        }
        else 
        {
            if (desempenoEspActual >= 15*etapa) AddReward(-0.1f); 
            else if (desempenoEspActual >= 12.5*etapa) AddReward(-0.2f);
            else AddReward(-0.4f);
        }
        desempenoViejo = desempenoActual;
    }

    


    public void calcDesempenoEspecialidadFinal()
    {
        //Si el desempe?o de la especialidad ha mejorado
        if (desempenoEspActual > desempenoEspViejo)
        {
            //Debug.Log("aumento especialidad");
            if (desempenoEspActual >= 60) AddReward(+20.0f);
            else if (desempenoEspActual >= 50) AddReward(+5.0f);
            else AddReward(+1.0f);
        }
        else
        {
            //Debug.Log("Disminuyo especialidad");
            if (desempenoEspActual >= 60) AddReward(-1.0f);
            else if (desempenoEspActual >= 50) AddReward(-5.0f);
            else AddReward(-10.0f);
        }
        desempenoViejo = desempenoActual;
    }




    public void revisionRewards()
    {
        //Debug.Log(voto);
        //piezaSeleccionada = voto - 1;
        //Debug.Log("pieza"+piezaSeleccionada);
        piezaGanadora = GameObject.Find("Entrenador").GetComponent<Entrenador>().piezaSeleccionada;

        etapa = GameObject.Find("Entrenador").GetComponent<Entrenador>().etapa;
        //if (etapa == 1) etapa = 2;
        //Debug.Log("Pieza Win: " + piezaGanadora + "Mi pieza: " + piezaSeleccionada);
        if (!EsJugador)
        {
            numeroAgente = this.name[this.name.Length - 1]; //Obtiene el n?mero que acompa?a al nombre
            int ELnumDelAgente = (int)char.GetNumericValue(numeroAgente);

            if (piezaSeleccionada != piezaGanadora) ajusteMedidor();
        }
        //Barrita.GetComponent<ManejoBarra>().Barritas();

        if (!EsJugador)
        {
            for (int x = 0; x < 4; x++)
            {
                desempenosFinales[x] = GameObject.Find("Entrenador").GetComponent<Entrenador>().desempenosFinales[x];
            }
            desempenoEspActual = desempenosFinales[especialidad - 1];
        }
        desempenoActual = GameObject.Find("Entrenador").GetComponent<Entrenador>().desempenofinal;
        //Debug.Log(this.name + " " + desempenoEspActual);
        if (!EsJugador)
        {
            calcDesempenoEspecialidad();
        }
        if (desempenoViejo > desempenoActual) //Si el desempe?o viejo es menor
        {
            //C?lculo (positivo y negativo) del desempe?o al final de la contrucci?n  de la nave 
            if (etapa == 4)
            {
                //Debug.Log(this.name + " " + desempenoEspActual);
                if (desempenoActual >= 85 ) //Si el desempe?o es igual o mayor a 70 tiene mayor recompensa
                {
                    Debug.Log("Termina perfecto (mal): " + desempenoActual); //Perfecto
                    SetReward(-1.0f);
                }
                else if (desempenoActual >= 65 && desempenoActual < 85) //Si el desempe?o es igual o mayor a 70 tiene mayor recompensa
                {
                    Debug.Log("Termina mejor (mal): " + desempenoActual); //Perfecto
                    SetReward(-2.0f);
                }
                else
                {
                    Debug.Log("Termina mal: " + desempenoActual);
                    SetReward(-5.0f); //Recompesa negativa si es una nave ineficiente
                }
                if (!EsJugador)
                {
                    calcDesempenoEspecialidadFinal();
                }
                //entrenador.reinicio();

                //Carga escena
                SceneManager.LoadScene("Ending"); //PARA JUGAR

                EndEpisode();
            }
            else
            {
                AddReward(-0.10f);
            }
        }

        //C?lculo (positivo) del desempe?o al final de la contrucci?n  de la nave 
        else if (etapa == 4)
        {
            //Debug.Log(this.name + " " + desempenoEspActual);
            //C?lculo de desempe?o al final de la contrucci?n  de la nave 
            if (desempenoActual >= 85) //Si el desempe?o es igual o mayor a 70 tiene mayor recompensa
            {
                Debug.Log("Termina perfecto (bien): " + desempenoActual); //Perfecto
                SetReward(+5.0f);
            }
            else if (desempenoActual >= 65 && desempenoActual < 85) //Si el desempe?o es igual o mayor a 70 tiene mayor recompensa
            {
                Debug.Log("Termina mejor (bien): " + desempenoActual); //Perfecto
                //SetReward(+1.5f);
            }
            else
            {
                Debug.Log("Termina bien: " + desempenoActual); // Bien
                //SetReward(+1.0f);
            }
            if (!EsJugador)
            {
                calcDesempenoEspecialidadFinal();
            }
            //entrenador.reinicio();

            //Carga escena
            SceneManager.LoadScene("Ending");  //PARA JUGAR

            EndEpisode();
        }

        else { AddReward(+0.20f);}
        //Actualizaci?n de desempe?os y etapas
    }



    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActionOut = actionsOut.DiscreteActions;
        discreteActionOut[0] = NextAction;

    }


    void Start()
    {
        
    }

    void Update()
    {
        etapa = entrenador.etapa;
        if (etapa == 1)
        {
            Actual1 = 100.0f;
        }
        if (!EsJugador)
        {
            fillbar1.fillAmount = Actual1 / Max;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) NextAction = VOTAR1;
            if (Input.GetKeyDown(KeyCode.Alpha2)) NextAction = VOTAR2;
            if (Input.GetKeyDown(KeyCode.Alpha3)) NextAction = VOTAR3;
            if (Input.GetKeyDown(KeyCode.Alpha4)) NextAction = VOTAR4;
        }
    }
}