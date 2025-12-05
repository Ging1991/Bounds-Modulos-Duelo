using System.Collections.Generic;

namespace Bounds.Duelo.Pila.Subefectos {

	public interface ISubSobreJugador {

		public void AplicarEfecto(int jugador, List<string> etiquetas);

	}

}