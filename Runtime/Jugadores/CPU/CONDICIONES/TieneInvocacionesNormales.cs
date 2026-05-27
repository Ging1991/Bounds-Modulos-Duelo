using Bounds.Duelo.Utiles;

namespace Bounds.Duelo.CPU.Condiciones {

    public class TieneInvocacionesNormales : ICondicionDeJuego {
		
		private readonly int jugador;

		public TieneInvocacionesNormales(int jugador) {
			this.jugador = jugador;
		}


        public bool SeCumple() {
			JugadorDuelo jugadorDuelo = JugadorDuelo.GetInstancia(jugador);
			return jugadorDuelo.invocaciones_normales > 0;
        }


    }

}