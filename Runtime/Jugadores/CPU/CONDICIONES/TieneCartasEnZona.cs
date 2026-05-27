using System.Collections.Generic;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Condiciones {

	public class TieneCartasEnZona : ICondicionDeJuego {

		private readonly int jugador, cantidad;
		private readonly Zonas zona;
		private readonly CondicionCarta condicion;

		public TieneCartasEnZona(int jugador, CondicionCarta condicion, Zonas zona, int cantidad) {
			this.jugador = jugador;
			this.condicion = condicion;
			this.zona = zona;
			this.cantidad = cantidad;
		}


		public bool SeCumple() {
			return GetCartas().Count >= cantidad;
		}


		public List<GameObject> GetCartas() {
			List<GameObject> cartas = new List<GameObject>();
			Fisica fisica = EmblemaConocimiento.getInstancia().traerFisica();
			switch (zona) {
				case Zonas.MAZO: cartas = fisica.TraerCartasEnMazo(jugador); break;
				case Zonas.MANO: cartas = fisica.TraerCartasEnMano(jugador); break;
				case Zonas.CAMPO: cartas = fisica.TraerCartasEnCampo(jugador); break;
				case Zonas.DESCARTE: cartas = fisica.TraerCartasEnCementerio(jugador); break;
			}
			return condicion.CumpleLista(cartas);
		}


	}

}