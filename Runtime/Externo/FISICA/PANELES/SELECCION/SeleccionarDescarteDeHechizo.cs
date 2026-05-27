using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarDescarteDeHechizo : ISeleccionarCarta {

		private readonly int jugador;
		private readonly GameObject lugar;

		public SeleccionarDescarteDeHechizo(int jugador, GameObject lugar) {
			this.jugador = jugador;
			this.lugar = lugar;
		}

		public void Seleccionar(GameObject carta) {
			EmblemaDescartarCarta.Descartar(carta);
			EmblemaJuegoActivarHechizo.LanzarDescarte(jugador, lugar, carta);
		}

	}

}