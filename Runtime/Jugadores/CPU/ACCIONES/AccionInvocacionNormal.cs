using System.Collections.Generic;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionInvocacionNormal : AccionBasica {

		private TieneCartasEnZona tieneCartasEnZona;

		public AccionInvocacionNormal(int prioridad, int jugador) : base(prioridad, jugador) {

			tieneCartasEnZona = new TieneCartasEnZona(
				jugador,
				new CondicionClase("CRIATURA"),
				Zonas.MANO,
				1
			);

			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(new TieneInvocacionesNormales(2));
			condiciones.Add(tieneLugar);
			condiciones.Add(tieneCartasEnZona);
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL));
		}


		public override void Ejecutar() {
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			GameObject carta = Tutor.MejorCriaturaInvocacionNormal(fisica.TraerCartasEnMano(2));
			Entrada entrada = Entrada.GetInstancia();
			if (carta != null) {
				entrada.PresionarCarta(2, carta);
				List<GameObject> lugares = tieneLugar.GetLugares();
				if (lugares.Count > 0) {
					entrada.PresionarCampo(lugares[0]);
				}
				else {
					Debug.LogError("No había lugares disponibles");
				}
			}
			else {
				Debug.LogError("No había una criatura para invocar.");
			}
		}


	}

}