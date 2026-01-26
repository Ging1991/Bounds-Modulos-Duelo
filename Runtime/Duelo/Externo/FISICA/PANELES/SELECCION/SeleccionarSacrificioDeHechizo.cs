using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarSacrificioDeHechizo : ISeleccionarCarta {

		private readonly int jugador;
		private readonly GameObject lugar;

		public SeleccionarSacrificioDeHechizo(int jugador, GameObject lugar) {
			this.jugador = jugador;
			this.lugar = lugar;
		}

		public void Seleccionar(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;
			fisica.EnviarHaciaDescarte(carta, cartaInfo.controlador);
			EmblemaJuegoActivarHechizo.LanzarSacrificio(jugador, lugar, carta);
		}

	}

}