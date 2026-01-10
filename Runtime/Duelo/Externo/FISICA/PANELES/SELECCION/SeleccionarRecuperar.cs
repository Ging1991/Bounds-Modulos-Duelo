using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarRecuperar : ISeleccionarCarta {

		private readonly int jugador;

		public SeleccionarRecuperar(int jugador) {
			this.jugador = jugador;
		}

		public void Seleccionar(GameObject carta) {
			EmblemaTutor.Agregar(jugador, carta);
		}

	}

}