using Bounds.Duelo.Emblemas.Fases;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseCambioDeTurno : IFase {

		private readonly int jugador;

		public SubfaseCambioDeTurno(int jugador) {
			this.jugador = jugador;
		}


		void IFase.Resolver() {
			EmblemaTerminarTurno.TerminarTurno(jugador);
		}


	}

}