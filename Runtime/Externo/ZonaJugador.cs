using Bounds.Duelo.Utiles;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core.Interfaces;
using UnityEngine;

public class ZonaJugador : MonoBehaviour {

	public int jugador;
	public IProveedor<int, CartaBD> proveedorCartas;
	public IlustradorDeCartas ilustradorDeCartas;


	void OnMouseDown() {
		Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
		GameObject panel = instanciador.CrearPanelVisualizacionV2();
		PanelZona scr = panel.GetComponent<PanelZona>();
		scr.Inicializar();
		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		scr.iniciar(jugador, fisica.TraerCartasEnCementerio(jugador), "Visualizar cartas en el descarte");
	}


	public void SetDescarte(int cantidad) {
		Cartel cartel = gameObject.transform.GetChild(1).GetComponent<Cartel>();
		cartel.setTexto("Descarte\n" + cantidad);
	}


	public void SetMateriales(int cantidad) {
		Cartel cartel = gameObject.transform.GetChild(0).GetComponent<Cartel>();
		cartel.setTexto("Materiales\n" + cantidad);
	}


	public static ZonaJugador GetInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<ZonaJugador>();
	}


}