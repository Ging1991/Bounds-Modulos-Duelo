using UnityEngine;
using UnityEngine.UI;

public class Cartel : MonoBehaviour {

	public int limite = -1;


	public void setTexto(string texto) {
		Text componente = GetComponentInChildren<Text>();
		componente.text = texto;
	}


	public void setColor(string color) {
		Image componente = GetComponentInChildren<Image>();
		switch (color) {
			case "verde": componente.color = Color.green; break;
			case "rojo":  componente.color = Color.red; break;
			case "gris":  componente.color = Color.gray; break;
			default: Debug.Log("Color no encontrado: " + color); break;
		}
	}


	public static Cartel getInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<Cartel>();
	}


}