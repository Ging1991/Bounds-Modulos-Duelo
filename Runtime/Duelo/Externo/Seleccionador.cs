using Bounds.Duelo.Carta;
using Ging1991;
using Ging1991.Core;
using UnityEngine;

public class Seleccionador : SingletonMonoBehaviour<Seleccionador> {

	public GameObject cartaParaJugar;
	public GameObject cartaParaVincular;
	public GameObject atacante;
	public GameObject atacado;
	public bool combateCancelado;

	public void SeleccionarParaCombate(GameObject atacante, GameObject atacado) {
		this.atacante = atacante;
		this.atacado = atacado;
		atacante.GetComponent<CartaFX>().seleccionar();
		atacado.GetComponent<CartaFX>().seleccionar();
	}


	public void SeleccionarParaJugar(GameObject carta) {
		cartaParaJugar = carta;
		cartaParaJugar.GetComponent<CartaFX>().seleccionar();
	}


	public void SeleccionarParaVincular(GameObject carta) {
		cartaParaVincular = carta;
		cartaParaVincular.GetComponent<CartaFX>().seleccionar();
	}

	public void SeleccionarParaVincular() {
		if (cartaParaVincular != null) {
			cartaParaVincular.GetComponent<CartaFX>().deseleccionar();
			cartaParaVincular = null;
		}
	}


	public void SeleccionarParaJugar() {
		if (cartaParaJugar != null) {
			cartaParaJugar.GetComponent<CartaFX>().deseleccionar();
			cartaParaJugar = null;
		}
	}

	public void SeleccionarParaCombate() {
		if (atacante != null) {
			atacante.GetComponent<CartaFX>().deseleccionar();
			atacante.GetComponent<CartaFX>().PotencialAtacante(false);
			atacante = null;
		}
		if (atacado != null) {
			atacado.GetComponent<CartaFX>().deseleccionar();
			atacado = null;
		}
	}

}