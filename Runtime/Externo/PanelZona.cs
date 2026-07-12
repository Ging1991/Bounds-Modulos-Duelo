using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Emblemas;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;
using Bounds.Duelo;
using Bounds.Cartas;
using Ging1991.Interfaces.Personalizacion;
using Ging1991.Core;

public class PanelZona : MonoBehaviour {

	public List<GameObject> cartas, opciones;
	public int pagina, paginaMax, paginaMin;
	public int jugador;
	public GameObject cartaSeleccionada;
	public bool estaMostrandoMateriales;
	public TextoUI titulo;

	public void Inicializar() {

		foreach (var carta in opciones) {
			carta.GetComponentInChildren<CartaImagenID>().generador = ControlDuelo.Instancia.cartaGenerador;
		}
	}

	public void Visualizar(int jugador, List<GameObject> cartas, string texto = "Panel de visualización") {
		gameObject.SetActive(true);
		Bloqueador.BloquearGrupo("GLOBAL", true);
		this.jugador = jugador;
		titulo.SetTexto(texto);

		// Inicializa las variables de paginacion
		pagina = 1;
		paginaMin = 1;
		paginaMax = cartas.Count / 5;
		if (cartas.Count % 5 > 0 || cartas.Count == 0)
			paginaMax++;

		// Muestro las cartas por primera vez
		this.cartas = cartas;
		MostrarCartas();
	}


	public void BotonCerrar() {
		Bloqueador.BloquearGrupo("GLOBAL", false);
		gameObject.SetActive(false);
	}


	public void BotonSiguiente() {
		pagina++;
		if (pagina > paginaMax)
			pagina = paginaMin;
		MostrarCartas();
	}


	public void BotonAnterior() {
		pagina--;
		if (pagina < paginaMin)
			pagina = paginaMax;
		MostrarCartas();
	}


	public void BotonDescarte() {
		estaMostrandoMateriales = false;
		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		Visualizar(jugador, fisica.TraerCartasEnCementerio(jugador), $"Cartas en el descarte del jugador {jugador}");
	}


	public void BotonMateriales() {
		estaMostrandoMateriales = true;
		Visualizar(jugador, FindAnyObjectByType<Fisica>().TraerCartasEnMateriales(jugador), $"Cartas en materiales del jugador {jugador}");
	}


	public void MostrarCartas() {

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


	public void SeleccionarCarta(GameObject carta) {
		if (estaMostrandoMateriales)
			return;
		cartaSeleccionada = carta;
		CartaInfo info = carta.GetComponent<CartaInfo>();
		GameObject boton = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).gameObject;
		//boton.SetActive(info.tieneHabilidad("recobrar"));
	}


	public void BotonActivar() {
		if (estaMostrandoMateriales)
			return;

		Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
		fisica.EnviarHaciaMateriales(cartaSeleccionada, 1);
		EmblemaRobo.RobarCartas(1, 1);
		BotonCerrar();
	}


	public static PanelZona GetInstancia() {
		GameObject instancia = GameObject.Find("PanelZonas");
		if (instancia != null)
			return instancia.GetComponent<PanelZona>();
		else
			return null;
	}

}