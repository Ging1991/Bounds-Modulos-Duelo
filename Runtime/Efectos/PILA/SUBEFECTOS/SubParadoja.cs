using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubParadoja : ISubSobreCarta {

		private readonly int jugador;
		private readonly int material;

		public SubParadoja(int jugador, int material) {
			this.jugador = jugador;
			this.material = material;
		}

		public void AplicarEfecto(GameObject carta) {
			EmblemaParadoja.Aplicar(jugador, carta, material);
		}


	}

}