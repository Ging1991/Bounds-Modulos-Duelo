using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseRobo : IFase {

		private readonly int jugador;

		public SubfaseRobo(int jugador) {
			this.jugador = jugador;
		}

		void IFase.Resolver() {
			EmblemaRobo.RobarCartas(jugador, 1);
		}

	}

}