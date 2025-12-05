using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.CPU.Condiciones {

	public class TieneLugarLibre : ICondicionDeJuego {
		
		private readonly int jugador, cantidad;
		private readonly BuscadorCampo buscador;

		public TieneLugarLibre(int jugador, int cantidad) {
			this.jugador = jugador;
			this.cantidad = cantidad;
			buscador = BuscadorCampo.getInstancia();
		}


		public bool SeCumple() {
			return buscador.buscarCampoLibreN(jugador, cantidad).Count >= cantidad;
		}


		public List<GameObject> GetLugares() {
			return buscador.buscarCampoLibreN(jugador, cantidad);
		}


	}

}