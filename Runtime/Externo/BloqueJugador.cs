using Ging1991.Idiomas;
using Ging1991.Interfaces.Personalizacion;
using UnityEngine;
using UnityEngine.UI;

public class BloqueJugador : MonoBehaviour {

	public int vida;
	public Image miniatura;
	public Traduccion nombreOBJ;
	public Traduccion mazoOBJ;
	public Traduccion vidaOBJ;

	public void SetNombre(string nombre) {
		nombreOBJ.SetTexto(nombre);
	}


	public void SetVida(int vida) {
		this.vida = vida;
		if (this.vida < 0) {
			this.vida = 0;
		}
		Traductor.Instancia.Traducir(vidaOBJ);
	}


	public void setMazo(int cantidad) {
		Traductor.Instancia.Traducir(mazoOBJ);
	}


	public static BloqueJugador getInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<BloqueJugador>();
	}


}