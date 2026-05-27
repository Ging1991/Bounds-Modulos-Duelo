using System.Collections.Generic;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionEquipar : AccionBasica {

		private TieneCartasEnZona tieneEquipos;
		private TieneCartasEnZona tieneCriaturas;

		public AccionEquipar(int prioridad, int jugador) : base(prioridad, jugador) {

			CondicionClase condicionCriaturas = new("CRIATURA");
			CondicionMultiple condicionEquipos = new CondicionMultiple(CondicionMultiple.Tipo.Y);
			condicionEquipos.AgregarCondicion(new CondicionClase("EQUIPO"));
			condicionEquipos.AgregarCondicion(new CondicionEstaGirado(false));

			tieneEquipos = new TieneCartasEnZona(jugador, condicionEquipos, Zonas.CAMPO, 1);
			tieneCriaturas = new TieneCartasEnZona(jugador, condicionCriaturas, Zonas.CAMPO, 1);

			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(tieneEquipos);
			condiciones.Add(tieneCriaturas);
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL));
		}


		public override void Ejecutar() {
			posponer = true;
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			List<GameObject> equipos = tieneEquipos.GetCartas();
			if (equipos.Count > 0) {
				Entrada entrada = Entrada.GetInstancia();
				entrada.PresionarCarta(2, equipos[0]);
				GameObject carta = Tutor.MejorCriatura(fisica.TraerCartasEnCampo(2));
				if (carta != null) {
					entrada.PresionarCarta(2, carta);
				}
			}
		}


	}

}