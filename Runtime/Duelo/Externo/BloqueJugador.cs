using UnityEngine;
using UnityEngine.UI;

public class BloqueJugador : MonoBehaviour {

	public int vida;
	public Image miniatura;

	public void SetNombre(string nombre) {
		Cartel cartelNombre = gameObject.transform.GetChild(0).GetComponent<Cartel>();
		cartelNombre.setTexto(nombre);
	}


	public void SetVida(int vida) {
		this.vida = vida;
		if (this.vida < 0) {
			this.vida = 0;
		}
		Cartel cartelNombre = gameObject.transform.GetChild(1).GetComponent<Cartel>();
		cartelNombre.setTexto("Vida " + this.vida);
	}


	public void setMazo(int cantidad) {
		Cartel cartelNombre = gameObject.transform.GetChild(2).GetComponent<Cartel>();
		cartelNombre.setTexto("Mazo " + cantidad);
	}


	public static BloqueJugador getInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<BloqueJugador>();
	}


}