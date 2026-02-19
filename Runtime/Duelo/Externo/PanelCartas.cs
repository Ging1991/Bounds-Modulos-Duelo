using System.Collections.Generic;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Tinteros;
using Ging1991.Core;
using Ging1991.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Paneles {

	public class PanelCartas : MonoBehaviour, ISeleccionarCarta {

		public List<GameObject> cartas, opciones, cartasSeleccionadas;
		public int pagina, paginaMax, paginaMin;
		private ISeleccionarCarta accion;
		public int cantidad;
		public bool permanecer = false;
		public GameObject tituloOBJ;


		public void Iniciar(List<GameObject> cartas, ISeleccionarCarta accion, int cantidad = 1, string texto = "Panel de visualización") {
			gameObject.SetActive(true);
			Bloqueador.BloquearGrupo("GLOBAL", true);

			tituloOBJ.GetComponentInChildren<Text>().text = texto;
			cartasSeleccionadas = new List<GameObject>();

			pagina = 1;
			paginaMin = 1;
			paginaMax = cartas.Count / 5;
			if (cartas.Count % 5 > 0 || cartas.Count == 0)
				paginaMax++;

			this.cartas = cartas;
			this.accion = accion;
			this.cantidad = cantidad;
			Mostrar();
			permanecer = false;
		}


		public void AutoSeleccionar() {
			if (cartas.Count == 1) {
				Seleccionar(cartas[0]);
			}
		}


		public void BotonCerrar() {
			Bloqueador.BloquearGrupo("GLOBAL", false);
			gameObject.SetActive(false);
			EmblemaSeleccionInvocacionPerfecta.GetInstancia().Deseleccionar();
			EmblemaSeleccionMaterial.GetInstancia().Deseleccionar();
			GameObject.FindAnyObjectByType<Invocador>().InvocacionCompletada();
		}


		public void RetirarPanel() {
			if (!permanecer) {
				Bloqueador.BloquearGrupo("GLOBAL", false);
				gameObject.SetActive(false);
				permanecer = false;
			}
		}


		public void BotonSiguiente() {
			pagina++;
			if (pagina > paginaMax)
				pagina = paginaMin;
			Mostrar();
		}


		public void BotonAnterior() {
			pagina--;
			if (pagina < paginaMin)
				pagina = paginaMax;
			Mostrar();
		}


		public void Mostrar() {

			foreach (GameObject opcion in opciones)
				opcion.SetActive(false);

			int desplazamiento = (pagina - 1) * 5;

			for (int i = 0; i < 5; i++) {

				int posicion = i + desplazamiento;
				if (posicion >= cartas.Count)
					break;

				GameObject carta = cartas[posicion];

				GameObject opcion = opciones[i];
				opcion.SetActive(true);
				PanelOpcionCarta scr = opcion.GetComponentInChildren<PanelOpcionCarta>();
				scr.Iniciar(carta, this);
			}

		}


		public void InicializarVisuales(DatosDeCartas datosDeCartas, ISelector<string, Sprite> ilustradorDeCartas, ITintero tintero) {
			foreach (GameObject opcion in opciones)
				opcion.GetComponentInChildren<CartaFrente>().Inicializar(datosDeCartas, ilustradorDeCartas, tintero);
		}


		public void Seleccionar(GameObject carta) {
			if (cartasSeleccionadas.Contains(carta)) {
				cartasSeleccionadas.Remove(carta);
			}
			else {
				cartasSeleccionadas.Add(carta);
				if (cartasSeleccionadas.Count == cantidad) {
					accion.Seleccionar(carta);
					RetirarPanel();
				}
			}
		}

	}

}