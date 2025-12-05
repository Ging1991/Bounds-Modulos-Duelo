using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Animaciones;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubDescartar : ISubSobreJugador {

		private readonly int cantidad;

		public SubDescartar(int cantidad) {
			this.cantidad = cantidad;
		}

		public void AplicarEfecto(int jugador, List<string> etiquetas) {

			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			//bloque.GetComponentInChildren<EfectoVisual>().Animar("VENENO");

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartasEnMano = new List<GameObject>(fisica.TraerCartasEnMano(jugador));

			for (int i = 0; i < cantidad; i++) {
				if (cartasEnMano.Count > 0) {
					GameObject carta = EmblemaAleatorio.carta(cartasEnMano);
					EmblemaDescartarCarta.Descartar(carta);
					cartasEnMano.Remove(carta);
				}
				else {
					break;
				}
			}
		}


	}

}