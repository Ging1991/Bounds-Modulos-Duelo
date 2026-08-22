using Bounds.Duelo;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Sistema.Ilustradores;
using Ging1991.Core.Interfaces;
using Ging1991.Idiomas;
using UnityEngine;

public class ZonaJugador : MonoBehaviour {

	public int jugador;
	public IProveedor<int, CartaBD> proveedorCartas;
	public IlustradorDeCartas ilustradorDeCartas;
	public Traduccion materialesOBJ;
	public Traduccion descarteOBJ;

	void OnMouseDown() {
		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		PanelZona panelZona = fisica.panelZona;
		panelZona.Inicializar();
		panelZona.Visualizar(
			jugador,
			fisica.TraerCartasEnCementerio(jugador),
			ControlDuelo.Instancia.selectorSistema.GetElemento("VISUALIZAR_DESCARTE").Replace("[N]", $"{jugador}")
		);
	}


	public void SetDescarte(int cantidad) {
		Traductor.Instancia.Traducir(descarteOBJ);
	}


	public void SetMateriales(int cantidad) {
		Traductor.Instancia.Traducir(materialesOBJ);
	}


	public static ZonaJugador GetInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<ZonaJugador>();
	}


}