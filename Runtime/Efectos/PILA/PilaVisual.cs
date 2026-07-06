using System.Collections.Generic;
using Bounds.Cartas;
using UnityEngine;

namespace Bounds.Duelo.Pila {

	public class PilaVisual : MonoBehaviour {

		public List<GameObject> efectos;

		public void SetEfectos(List<PilaEfectos.CartaPila> cartasID) {
			DesactivarEfectos();
			VisualizarEfectos(cartasID);
		}

		private void VisualizarEfectos(List<PilaEfectos.CartaPila> cartasID) {

			int posicion = 0;
			foreach (var carta in cartasID) {
				efectos[posicion].SetActive(true);
				efectos[posicion].GetComponent<CartaImagenID>().MostrarCartaID(carta.cartaID, carta.imagen, "N");
				posicion++;
				if (posicion >= 5)
					break;
			}

		}


		private void DesactivarEfectos() {
			foreach (GameObject efecto in efectos)
				efecto.SetActive(false);
		}

	}

}