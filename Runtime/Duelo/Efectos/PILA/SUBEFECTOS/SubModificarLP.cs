using System.Collections.Generic;
using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubModificarLP : ISubSobreJugador {

		private readonly int cantidad;

		public SubModificarLP(int cantidad) {
			this.cantidad = cantidad;
		}


		public void AplicarEfecto(int jugador, List<string> etiquetas) {
			if (cantidad > 0) {
				EmblemaVida.AumentarVida(jugador, cantidad, "REVITALIZAR");
			}
			if (cantidad < 0) {
				EmblemaVida.DisminuirVida(jugador, -cantidad, etiquetas.Contains("EXPLOSION") ? "EXPLOSION" : "GOLPE");
			}
		}

    }

}