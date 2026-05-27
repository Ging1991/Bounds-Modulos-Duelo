using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Infraestructura.Cartas {

	public class CartaPresionable : MonoBehaviour {

		public GameObject carta;
		public List<ICartaPresionable> observadores = new List<ICartaPresionable>();

		public void OnMouseDown() {
			foreach (ICartaPresionable observador in observadores) {
				observador.PresionarCarta(carta);
			}
		}


	}

}