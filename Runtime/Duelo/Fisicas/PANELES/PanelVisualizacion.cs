using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Paneles {

	public class PanelVisualizacion : MonoBehaviour {

		public List<GameObject> cartas, opciones;
		public int pagina, paginaMax, paginaMin;


		public void Iniciar(List<GameObject> cartas, string texto = "Panel de visualización") {

			// Seteo el texto para el titulo
			transform.GetChild(2).GetComponentInChildren<Text>().text = texto;

			// Inicializa las variables de paginacion
			pagina = 1;
			paginaMin = 1;
			paginaMax = cartas.Count / 5;
			if (cartas.Count % 5 > 0 || cartas.Count == 0)
				paginaMax++;

			// Muestro las cartas por primera vez
			this.cartas = cartas;
			Mostrar();
		}


		public void botonCerrar() {
			Destroy(gameObject);
		}


		public void botonSiguiente() {
			pagina++;
			if (pagina > paginaMax)
				pagina = paginaMin;
			Mostrar();
		}


		public void botonAnterior() {
			pagina--;
			if (pagina < paginaMin)
				pagina = paginaMax;
			Mostrar();
		}


		public void Mostrar () {

			// deshabilito mis opciones
			foreach (GameObject opcion in opciones)
				opcion.SetActive(false);
				
			// muestro solo las opciones correspondientes a la pagina actual
			int desplazamiento = (pagina-1)*5;

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
			}

		}


	}

}