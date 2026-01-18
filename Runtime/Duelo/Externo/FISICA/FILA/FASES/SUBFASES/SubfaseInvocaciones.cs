using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseInvocaciones : IFase {

		private readonly int jugador;

		public SubfaseInvocaciones(int jugador) {
			this.jugador = jugador;
		}

		void IFase.Resolver() {
			JugadorDuelo jugadorDuelo = JugadorDuelo.GetInstancia(jugador);
			jugadorDuelo.invocaciones_normales = 1;
			jugadorDuelo.invocacionesRestringidas.Clear();
			EmblemaTurnos.GetInstancia().CambiarFase();
		}

	}

}