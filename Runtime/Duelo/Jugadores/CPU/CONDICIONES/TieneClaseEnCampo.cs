using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Condiciones {

	public class TieneClaseEnCampo : ICondicionDeJuego {

		private readonly int jugador;
		private readonly string clase;
		private bool soloEnderezados;

		public TieneClaseEnCampo(int jugador, string clase, bool soloEnderezados = false) {
			this.jugador = jugador;
			this.clase = clase;
			this.soloEnderezados = soloEnderezados;
		}


		public bool SeCumple() {
			return GetCartas().Count > 0;
		}


		public List<GameObject> GetCartas() {
			Fisica fisica = EmblemaConocimiento.getInstancia().traerFisica();
			Condicion condicion = new Condicion(tipoCarta: clase, estaEnderezadoRequerido: soloEnderezados);
			return condicion.CumpleLista(fisica.TraerCartasEnCampo(jugador));
		}


	}

}