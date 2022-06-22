using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour {
	public GameObject hijo;
	public int []item;
	public int []itemsNum;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("jugador");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col){

		if (col.gameObject.tag == "jugador") {
			//print ("entro");
			if (Input.GetKeyDown (KeyCode.O)) {
				print ("abrir");
				hijo.transform.Rotate (0, 0, 170);
				for (int k = 0; k < item.Length; k++) {
					player.GetComponent<Inventario> ().AddItem (item [k], itemsNum [k]);
					item[k] = 0;
				}
			}
			if (Input.GetKeyDown (KeyCode.P)) {
				print ("cerrar");
				hijo.transform.Rotate (0, 0,-170);
			}

		}



	}



}
