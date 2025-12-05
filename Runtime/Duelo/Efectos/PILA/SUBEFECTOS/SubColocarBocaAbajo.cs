using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubColocarBocaAbajo : ISubSobreCarta {

		private readonly int jugador;

		public SubColocarBocaAbajo(int jugador) {
			this.jugador = jugador;
		}


		public void AplicarEfecto(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject campo = buscador.buscarCampoLibre(jugador);
			if (campo != null) {
				fisica.EnviarHaciaCampo(jugador, carta, campo);
				carta.GetComponent<CartaGeneral>().ColocarBocaAbajo();
			}
		}


	}

}