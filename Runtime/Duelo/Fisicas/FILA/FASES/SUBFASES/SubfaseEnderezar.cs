using Bounds.Duelo.Emblema;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseEnderezar : IFase {

		private readonly int jugador;

		public SubfaseEnderezar(int jugador) {
			this.jugador = jugador;
		}

		void IFase.Resolver() {
			EmblemaEnderezarPermanentes.Enderezar(jugador);
		}

	}

}