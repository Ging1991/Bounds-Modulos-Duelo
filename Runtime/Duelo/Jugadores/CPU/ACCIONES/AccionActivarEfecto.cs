using System.Collections.Generic;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionActivarEfecto : AccionBasica {
		public AccionActivarEfecto(int prioridad, int jugador) : base(prioridad, jugador) {
			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL));
		}

		public override void Ejecutar() {
			posponer = true;
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			GameObject carta = Tutor.MejorCriaturaEfecto(fisica.TraerCartasEnCampo(2));
			Entrada entrada = Entrada.GetInstancia();
			if (carta != null) {
				entrada.PresionarCarta(2, carta);
			}
		}

		public override bool PuedeEjecutar() {
			if (posponer)
				return false;
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			return Tutor.MejorCriaturaEfecto(fisica.TraerCartasEnCampo(2)) != null;
		}

	}

}