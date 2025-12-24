using System.Collections.Generic;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionJugarCarta : AccionBasica {

		private readonly TieneCartasEnZona tieneCartasEnZona;
		private readonly EstadisticasSingleton estadisticas;
		private readonly string codigo;


		public AccionJugarCarta(int prioridad, int jugador, string clase) : base(prioridad, jugador) {
			CondicionCarta condicionClase = new CondicionClase(clase);
			tieneCartasEnZona = new TieneCartasEnZona(jugador, condicionClase, Zonas.MANO, 1);
			estadisticas = EstadisticasSingleton.Instancia;
			codigo = $"{clase}_{jugador}_jugadas";

			condiciones = new List<ICondicionDeJuego>
			{
				tieneCartasEnZona,
				tieneLugar,
				new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL)
			};
		}


		public override void Ejecutar() {

			int cantidadActual = estadisticas.GetValor(codigo);

			List<GameObject> cartas = tieneCartasEnZona.GetCartas();
			if (cartas.Count > 0) {
				Entrada entrada = Entrada.GetInstancia();
				entrada.PresionarCarta(2, cartas[0]);
				List<GameObject> lugares = tieneLugar.GetLugares();
				if (lugares.Count > 0) {
					entrada.PresionarCampo(lugares[0]);
				}
				else {
					Debug.LogError("No había lugares disponibles");
				}
			}
			else {
				Debug.LogError("No había cartas para jugar.");
			}

			if (estadisticas.GetValor(codigo) == cantidadActual)
				posponer = true;
		}


	}

}