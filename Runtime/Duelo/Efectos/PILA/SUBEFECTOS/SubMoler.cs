using System.Collections.Generic;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Visuales;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubMoler : ISubSobreJugador {

		private readonly int cantidad;

		public SubMoler(int cantidad) {
			this.cantidad = cantidad;
		}

		public void AplicarEfecto(int jugador, List<string> etiquetas) {
			Fisica fisica = Fisica.Instancia;
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.GetComponentInChildren<GestorVisual>().Animar("VENENO", "FxVeneno");

			for (int i = 0; i < cantidad; i++)
				fisica.MolerCarta(jugador);
		}


	}

}