using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubParadoja : ISubSobreCarta {

		private string invocacion;
		private int jugador;

		public SubParadoja(int jugador, string invocacion) {
			this.jugador = jugador;
			this.invocacion = invocacion;
		}

		public void AplicarEfecto(GameObject carta) {
			EmblemaParadoja.Aplicar(jugador, carta, invocacion);
		}


	}

}