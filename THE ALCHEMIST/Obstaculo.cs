using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {
	public Inventario inv;
	public int i1;
	public int i2;
	public int i3;
	public int i4;
	public int i5;
	public int i6;
	public int i7;
	public int i8;
	public int i9;
	public int i10;
	public int i11;
	public GameObject explosionObstacle;

	// Use this for initialization
	void Start () {
		inv = GameObject.FindGameObjectWithTag ("jugador").GetComponent<Inventario>();
	}

	void OnTriggerStay(Collider other){
		if (Input.GetKeyDown(KeyCode.K) && other.CompareTag ("jugador")) {
			if (inv.pociones >= i1 && inv.barriles >= i2 && inv.cristales >= i3 && inv.fuego >= i4 && inv.hachas >= i5 && inv.paraguas >= i6 && inv.hongos >= i7 && inv.latas >= i8 && inv.pocionesverdes >= i9 && inv.pocionesmoradas >= i10 && inv.agua >= i11) {
				inv.pociones -= i1;
				inv.barriles -= i2;
				inv.cristales -= i3;
				inv.fuego -= i4;
				inv.hachas -= i5;
				inv.paraguas -= i6;
				inv.hongos -= i7;
				inv.latas -= i8;
				inv.pocionesverdes -= i9;
				inv.pocionesmoradas -= i10;
				inv.agua -= i11;
				inv.UpdateItems ();
				GameObject p = Instantiate (explosionObstacle, transform.position, transform.rotation) as GameObject;
				Destroy (p, 6);
				Destroy (gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
