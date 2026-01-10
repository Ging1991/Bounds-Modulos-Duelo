using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Paneles.Seleccion;

namespace Bounds.Duelo.Paneles {

	public class PanelSeleccion : MonoBehaviour {

		private ISeleccionarCarta acciones;
		public GameObject seleccionada;
		public List<GameObject> opciones;
		public List<GameObject> cartas;
		public int pagina, contador, maxPagina;


		public void Iniciar(ISeleccionarCarta acciones, string texto) {
			pagina = 1;
			this.acciones = acciones;
			transform.GetChild(2).GetComponentInChildren<Text>().text = texto;
		}


		public void PresionarSeleccionar(GameObject carta) {
			foreach (GameObject opcion in opciones) {
				OpcionSeleccion componente = opcion.GetComponent<OpcionSeleccion>();
				componente.Deseleccionar();
			}
			seleccionada = carta;
			Aceptar();
		}


		public void Aceptar() {
			if (acciones != null && seleccionada != null) {
				acciones.Seleccionar(seleccionada);
				Destroy(gameObject);
			}
			if (seleccionada != null) {
				Destroy(gameObject);
			}
		}


		public void AgregarOpciones(List<GameObject> cartas) {
			this.cartas = new List<GameObject>(cartas);
			foreach (GameObject carta in this.cartas)
				AgregarOpcion(carta);
			Organizar();
			if (cartas.Count == 1) {
				PresionarSeleccionar(cartas[0]);
			}
		}


		private void AgregarOpcion(GameObject carta) {
			if (opciones.Count > 4)
				return;

			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
			GameObject opcion = instanciador.CrearOpcionSeleccion(gameObject.transform);
			OpcionSeleccion componente = opcion.GetComponentInChildren<OpcionSeleccion>();
			//componente.Iniciar(carta);
			opciones.Add(opcion);
		}


		private void Organizar() {
			for (int i = 0; i < opciones.Count; i++) {
				Vector3 posicion = Constantes.VECTOR_SELECCION_CARTAS + new Vector3(160 * i, 0, -1);
				GameObject opcion = opciones[i];
				opcion.GetComponent<RectTransform>().localPosition = posicion;
			}
		}


		public void Siguiente() {
			// calculo la pagina maxima
			maxPagina = cartas.Count / 5;
			if (cartas.Count % 5 > 0)
				maxPagina++;

			// calculo la pagina
			pagina += 1;
			if (pagina > maxPagina)
				pagina = 1;

			foreach (GameObject carta in this.opciones)
				Destroy(carta);
			opciones = new List<GameObject>();

			contador = 0;
			foreach (GameObject carta in this.cartas) {
				if (contador >= (pagina - 1) * 5)
					AgregarOpcion(carta);
				contador++;
			}

			Organizar();
		}


		public void Anterior() {
			// calculo la pagina maxima
			maxPagina = cartas.Count / 5;
			if (cartas.Count % 5 > 0)
				maxPagina++;

			// calculo la pagina
			pagina -= 1;
			if (pagina < 1)
				pagina = maxPagina;

			foreach (GameObject carta in this.opciones)
				Destroy(carta);
			opciones = new List<GameObject>();

			contador = 0;
			foreach (GameObject carta in this.cartas) {
				if (contador >= (pagina - 1) * 5)
					AgregarOpcion(carta);
				contador++;
			}

			Organizar();
		}


	}

}