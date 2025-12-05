using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Emblemas;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Tinteros;
using Bounds.Modulos.Duelo.Fisicas;

public class PanelZona : MonoBehaviour {

	public List<GameObject> cartas, opciones;
	public int pagina, paginaMax, paginaMin;
	public int jugador;
	public GameObject cartaSeleccionada;
	public bool estaMostrandoMateriales;

	public void iniciar(int jugador, List<GameObject> cartas, string texto = "Panel de visualización") {

		// Seteo el texto para el titulo
		Cartel cartel = transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Cartel>();
		cartel.setTexto(texto);

		this.jugador = jugador;

		// Inicializa las variables de paginacion
		pagina = 1;
		paginaMin = 1;
		paginaMax = cartas.Count / 5;
		if (cartas.Count % 5 > 0 || cartas.Count == 0)
			paginaMax++;

		// Muestro las cartas por primera vez
		this.cartas = cartas;
		mostrar();
	}


	public void botonCerrar() {
		Destroy(gameObject);
	}


	public void botonSiguiente() {
		pagina++;
		if (pagina > paginaMax)
			pagina = paginaMin;
		mostrar();
	}


	public void botonAnterior() {
		pagina--;
		if (pagina < paginaMin)
			pagina = paginaMax;
		mostrar();
	}


	public void botonDescarte() {
		estaMostrandoMateriales = false;
		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		iniciar(jugador, fisica.TraerCartasEnCementerio(jugador), "Visualizar cartas en el descarte");
	}


	public void botonMateriales() {
		estaMostrandoMateriales = true;
		//iniciar(jugador, ZonaMateriales.GetInstancia(jugador).cartas, "Visualizar cartas en materiales");
	}


	public void mostrar() {

		// deshabilito mis opciones
		foreach (GameObject opcion in opciones)
			opcion.SetActive(false);

		// muestro solo las opciones correspondientes a la pagina actual
		int desplazamiento = (pagina - 1) * 5;

		for (int i = 0; i < 5; i++) {

			// Obtengo la carta que voy a mostrar de la lista
			int posicion = i + desplazamiento;
			if (posicion >= cartas.Count)
				break;
			GameObject carta = cartas[posicion];

			// Obtengo la opcion de visualizacion
			GameObject opcion = opciones[i];
			opcion.SetActive(true);
			OpcionVisualizacion scr = opcion.GetComponentInChildren<OpcionVisualizacion>();
			scr.Iniciar(carta);
			scr.padre = this;
		}

	}


	public void Inicializar(DatosDeCartas datosDeCartas, IlustradorDeCartas ilustradorDeCartas, ITintero tintero) {

		foreach (GameObject opcion in opciones)
			opcion.GetComponentInChildren<CartaFrente>().Inicializar(datosDeCartas, ilustradorDeCartas, tintero);

	}


	public void seleccionarCarta(GameObject carta) {
		if (estaMostrandoMateriales)
			return;
		cartaSeleccionada = carta;
		CartaInfo info = carta.GetComponent<CartaInfo>();
		GameObject boton = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).gameObject;
		//boton.SetActive(info.tieneHabilidad("recobrar"));
	}


	public void botonActivar() {
		if (estaMostrandoMateriales)
			return;

		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		fisica.EnviarHaciaMateriales(cartaSeleccionada, 1);
		EmblemaRobo.RobarCartas(1, 1);
		botonCerrar();
	}


	public static PanelZona getInstancia() {
		GameObject instancia = GameObject.Find("PanelZonas");
		if (instancia != null)
			return instancia.GetComponent<PanelZona>();
		else
			return null;
	}

}