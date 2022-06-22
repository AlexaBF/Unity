using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour {
	public Image pocionImage;
	public Text pocionText;
	public int pociones;

	public Image barrilImage;
	public Text barrilText;
	public int barriles;

	public Image cristalImage;
	public Text cristalText;
	public int cristales;

	public Image fireImage;
	public Text fireText;
	public int fuego;

	public Image axeImage;
	public Text axeText;
	public int hachas;

	public Image umbImage;
	public Text umbText;
	public int paraguas;

	public Image hongoImage;
	public Text hongoText;
	public int hongos;

	public Image lataImage;
	public Text lataText;
	public int latas;

	public Image greenImage;
	public Text greenText;
	public int pocionesverdes;

	public Image purpleImage;
	public Text purpleText;
	public int pocionesmoradas;

	public Image waterImage;
	public Text waterText;
	public int agua;

	// Use this for initialization
	void Start () {
		pociones = 0;
		barriles = 0;
		cristales = 0;
		fuego = 0;
		hachas = 0;
		paraguas = 0;
		hongos = 0;
		latas = 0;
		pocionesverdes = 0;
		pocionesmoradas = 0;
		agua = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateItems(){
		pocionText.text = pociones.ToString ();
		barrilText.text = barriles.ToString ();
		cristalText.text = cristales.ToString ();
		fireText.text = fuego.ToString ();
		axeText.text = hachas.ToString ();
		umbText.text = paraguas.ToString ();
		hongoText.text = hongos.ToString ();
		lataText.text = latas.ToString ();
		greenText.text = pocionesverdes.ToString ();
		purpleText.text = pocionesmoradas.ToString ();
		waterText.text = agua.ToString ();

	}

	public void AddItem(int item, int num){
		if (item == 1) {
			pocionImage.enabled = true;
			pociones = pociones + num;
			pocionText.text = pociones.ToString ();
		}

		if (item == 2) {
			barrilImage.enabled = true;
			barriles = barriles + num;
			barrilText.text = barriles.ToString ();
		}

		if (item == 3) {
			cristalImage.enabled = true;
			cristales = cristales + num;
			cristalText.text = cristales.ToString ();
		}

		if (item == 4) {
			fireImage.enabled = true;
			fuego = fuego + num;
			fireText.text = fuego.ToString ();
		}
		if (item == 5) {
			axeImage.enabled = true;
			hachas = hachas + num;
			axeText.text = hachas.ToString ();
		}

		if (item == 6) {
			umbImage.enabled = true;
			paraguas = paraguas + num;
			umbText.text = paraguas.ToString ();
		}

		if (item == 7) {
			hongoImage.enabled = true;
			hongos = hongos + num;
			hongoText.text = hongos.ToString ();
		}

		if (item == 8) {
			lataImage.enabled = true;
			latas = latas + num;
			lataText.text = latas.ToString ();
		}
		if (item == 9) {
			greenImage.enabled = true;
			pocionesverdes = pocionesverdes + num;
			greenText.text = pocionesverdes.ToString ();
		}

		if (item == 10) {
			purpleImage.enabled = true;
			pocionesmoradas = pocionesmoradas + num;
			purpleText.text = pocionesmoradas.ToString ();
		}

		if (item == 11) {
			waterImage.enabled = true;
			agua = agua + num;
			waterText.text = agua.ToString ();
		}
	}
	//void OnGUI () {
		//GUI.Box (new Rect (10, 10, 210, 200), "Inventario");
	//}
}
