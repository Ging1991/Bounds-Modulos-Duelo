using Ging1991.Interfaces.Personalizacion;
using UnityEngine;
using UnityEngine.UI;

public class BloqueJugador : MonoBehaviour {

	public int vida;
	public Image miniatura;
	public TextoUI nombreOBJ;
	public TextoUI mazoOBJ;
	public TextoUI vidaOBJ;

	public void SetNombre(string nombre) {
		nombreOBJ.SetTexto(nombre);
	}


	public void SetVida(int vida) {
		this.vida = vida;
		if (this.vida < 0) {
			this.vida = 0;
		}
		vidaOBJ.SetTexto($"Vida {this.vida}");
	}


	public void setMazo(int cantidad) {
		mazoOBJ.SetTexto($"Mazo {cantidad}");
	}


	public static BloqueJugador getInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<BloqueJugador>();
	}


}