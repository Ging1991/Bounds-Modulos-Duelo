using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubRobar : ISubSobreJugador {

		private readonly int cantidad;

		public SubRobar(int cantidad) {
			this.cantidad = cantidad;
		}

        public void AplicarEfecto(int jugador, List<string> etiquetas) {
			if (jugador == 0) {
				EmblemaRobo.RobarCartas(1, cantidad);
				EmblemaRobo.RobarCartas(2, cantidad);	
			} else
				EmblemaRobo.RobarCartas(jugador, cantidad);
		}


	}

}