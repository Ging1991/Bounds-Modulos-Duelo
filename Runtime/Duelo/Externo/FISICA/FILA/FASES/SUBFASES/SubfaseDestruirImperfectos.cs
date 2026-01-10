using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseDestruirImperfectos : IFase {

		private readonly int jugador;

		public SubfaseDestruirImperfectos(int jugador) {
			this.jugador = jugador;
		}


		void IFase.Resolver() {
		 	EmblemaDestruccionImperfectos.Destruir(jugador);
		}

	}

}