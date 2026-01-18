using System.Collections.Generic;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Tinteros;
using UnityEngine;

namespace Bounds.Duelo.Pila {

	public class PilaVisual : MonoBehaviour {

		public List<GameObject> efectos;
		public DatosDeCartas datosDeCartas;
		public IlustradorDeCartas ilustradorDeCartas;


		public void SetEfectos(List<PilaEfectos.CartaPila> cartasID) {
			DesactivarEfectos();
			VisualizarEfectos(cartasID);
		}


		private void VisualizarEfectos(List<PilaEfectos.CartaPila> cartasID) {

			int posicion = 0;
			foreach (var carta in cartasID) {
				efectos[posicion].SetActive(true);
				efectos[posicion].GetComponent<CartaFrente>().Mostrar(carta.cartaID, carta.imagen);
				posicion++;
				if (posicion >= 5)
					break;
			}

		}


		private void DesactivarEfectos() {
			foreach (GameObject efecto in efectos)
				efecto.SetActive(false);
		}

		public void InicializarVisuales() {
			ITintero tintero = new TinteroBounds();

			foreach (GameObject efecto in efectos)
				efecto.GetComponent<CartaFrente>().Inicializar(datosDeCartas, ilustradorDeCartas, tintero);
		}


	}

}