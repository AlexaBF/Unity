using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

public class LoginManager : MonoBehaviour
{
    Text user;
    Text passwd;
    Button login;
    float duracion;
    GameObject canvas;
    GameObject rueda;
    bool verifica;
    String done;
    bool donee = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        canvas = GameObject.Find("Canvas");
        rueda = canvas.transform.Find("rueda").gameObject;
        rueda.SetActive(false);
        //StartCoroutine(Wheel());
        canvas.transform.Find("Text").gameObject.SetActive(false);
        canvas.transform.Find("cargando").gameObject.SetActive(false);
        user = GameObject.Find("UserInputField").transform.Find("Text").GetComponent<Text>();
        passwd = GameObject.Find("PasswdInputField").transform.Find("Text").GetComponent<Text>();
        login = GameObject.Find("LoginButton").GetComponent<Button>();
        login.onClick.AddListener(LoginAction);
        String fecha_inicio = System.DateTime.Now.ToString("yyyy-MM-dd"); //Revisar documentación
        PlayerPrefs.SetString("fecha_inicio", fecha_inicio);
        Debug.Log(fecha_inicio);
        String hora_inicial = DateTime.Now.ToString("HH:mm:ss");
        PlayerPrefs.SetString("hora_inicio", hora_inicial);
        Debug.Log(hora_inicial);
    }

    public void LoginAction()
    {
        Debug.Log(user.text + "," + passwd.text);
        //WebRequest al módulo web por medio de una URL
        //SceneManager.LoadScene("Scene 1");
        StartCoroutine(DoLogin());
        rueda.SetActive(true);
        canvas.transform.Find("cargando").gameObject.SetActive(true);
        //StartCoroutine(Wheel());
    }



    IEnumerator DoLogin(){
        Dictionary<string, string> body = new Dictionary<string, string>();
        body.Add("user", user.text);
        body.Add("password", passwd.text);
        UnityWebRequest www = UnityWebRequest.Post("http://kamanode-env.eba-qfr55mks.us-east-1.elasticbeanstalk.com/api/login", body);
        //www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            rueda.SetActive(false);
            canvas.transform.Find("cargando").gameObject.SetActive(false);
            Debug.LogError(www.error);          
        }
        else
        {
            try
            {
                Dictionary<string, string> bodyy = new Dictionary<string, string>();
                bodyy.Add("user", user.text);
                Debug.Log("Respuesta"+www.downloadHandler.text);
                Dictionary<string, string> values =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(www.downloadHandler.text);
                done = values["done"];

                if (done == "true")
                {
                    PlayerPrefs.SetString("correo", user.text);
                    canvas.transform.Find("Text").gameObject.SetActive(false);
                    string message = values["message"];
                    Debug.Log(message);
                    string token = values["token"];
                    PlayerPrefs.SetString("token", token);
                    duracion= Time.timeSinceLevelLoad; //Tiempo transcurrido
                    PlayerPrefs.SetFloat("duracion", duracion);
                    Debug.Log(duracion);
                    SceneManager.LoadScene("Scene 1");
                    SceneManager.LoadScene("Inventario",LoadSceneMode.Additive);
                    SceneManager.LoadScene("AlisonScene1", LoadSceneMode.Additive);
                    SceneManager.LoadScene("KarlaScene1", LoadSceneMode.Additive);
                    SceneManager.LoadScene("TonyScene1",LoadSceneMode.Additive);
                }
                else
                {
                    rueda.SetActive(false);
                    canvas.transform.Find("cargando").gameObject.SetActive(false);
                    string message = values["message"];
                    canvas.transform.Find("Text").gameObject.SetActive(true);
                    Debug.Log(message);
                }
            }
            catch (Exception e)
            {
                rueda.SetActive(false);
                canvas.transform.Find("cargando").gameObject.SetActive(false);
                Debug.Log(e.Message);
            }
        }
    }


    IEnumerator DoLog()
    {
        Dictionary<string, string> body = new Dictionary<string, string>();
        body.Add("user", user.text);
        //body.Add("password", passwd.text);
        UnityWebRequest www = UnityWebRequest.Post("http://kamanode-env.eba-qfr55mks.us-east-1.elasticbeanstalk.com/api/correo", body);
        //www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        Debug.Log(user.text);
        if (www.isNetworkError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            try
            {
                Debug.Log("Respuesta" + www.downloadHandler.text);
                Dictionary<string, string> values =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(www.downloadHandler.text);
                string message = values["message"];
                string id = values["id"];
                PlayerPrefs.SetString("id", id);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        donee = true;
    }

    IEnumerator Wheel()
    {
        float target = 360;
        while (target > 0)
        {
            float ammount = 40 * Time.deltaTime;
            rueda.transform.Rotate(0, 0, ammount);
            target -= ammount;
            if (target <= 5)
                target = 360;
            yield return null;
        }
    }
        // Update is called once per frame
        void Update()
    {
        /*if (done == "true")
        {
            rueda.SetActive(true);
            canvas.transform.Find("cargando").gameObject.SetActive(true);
        }*/
    }

}
