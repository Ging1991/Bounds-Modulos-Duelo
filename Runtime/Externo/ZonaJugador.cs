using Bounds.Duelo.Utiles;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core.Interfaces;
using Ging1991.Interfaces.Personalizacion;
using UnityEngine;

public class ZonaJugador : MonoBehaviour {

	public int jugador;
	public IProveedor<int, CartaBD> proveedorCartas;
	public IlustradorDeCartas ilustradorDeCartas;
	public TextoUI materialesOBJ;
	public TextoUI descarteOBJ;

	void OnMouseDown() {
		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		PanelZona panelZona = fisica.panelZona;
		panelZona.Inicializar();
		panelZona.Visualizar(jugador, fisica.TraerCartasEnCementerio(jugador), "Visualizar cartas en el descarte");
	}


	public void SetDescarte(int cantidad) {
		descarteOBJ.SetTexto($"Descarte\n{cantidad}");
	}


	public void SetMateriales(int cantidad) {
		materialesOBJ.SetTexto($"Materiales\n{cantidad}");
	}


	public static ZonaJugador GetInstancia(string nombre) {
		GameObject instancia = GameObject.Find(nombre);
		return instancia.GetComponent<ZonaJugador>();
	}


}