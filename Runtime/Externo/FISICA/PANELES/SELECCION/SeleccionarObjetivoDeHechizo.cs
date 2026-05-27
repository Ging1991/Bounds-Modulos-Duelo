using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarObjetivoDeHechizo : ISeleccionarCarta {

		private readonly int jugador;
		private readonly GameObject lugar;

		public SeleccionarObjetivoDeHechizo(int jugador, GameObject lugar) {
			this.jugador = jugador;
			this.lugar = lugar;
		}

		public void Seleccionar(GameObject objetivo) {
			EmblemaJuegoActivarHechizo.LanzarObjetivo(jugador, lugar, objetivo);
		}

	}

}