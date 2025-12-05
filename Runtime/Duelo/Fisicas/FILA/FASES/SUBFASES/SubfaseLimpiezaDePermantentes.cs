using Bounds.Duelo.Emblemas.Fases;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseLimpiezaDePermantentes : IFase {

		private readonly int jugador;

		public SubfaseLimpiezaDePermantentes(int jugador) {
			this.jugador = jugador;
		}

		void IFase.Resolver() {
			EmblemaLimpiezaDePermantentes.Limpiar(jugador);
		}

	}

}