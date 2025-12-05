using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionJugarAura : AccionBasica {

		private readonly TieneCartasEnZona tieneAuras;
		private readonly TieneCartasEnZona tieneCriaturas;
		private readonly string codigo;
		private readonly Estadisticas estadisticas;

		public AccionJugarAura(int prioridad, int jugador) : base(prioridad, jugador) {

			CondicionClase condicionAura = new("AURA");
			CondicionClase condicionCriatura = new("CRIATURA");

			tieneAuras = new TieneCartasEnZona(jugador, condicionAura, Zonas.MANO, 1);
			tieneCriaturas = new TieneCartasEnZona(jugador, condicionCriatura, Zonas.CAMPO, 1);
			codigo = $"AURA_{jugador}_jugadas";
			estadisticas = Estadisticas.Instancia;

			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(tieneAuras);
			condiciones.Add(tieneCriaturas);
			condiciones.Add(tieneLugar);
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL));
		}


		public override void Ejecutar() {
			int cantidadActual = estadisticas.GetValor(codigo);

			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			List<GameObject> auras = tieneAuras.GetCartas();
			if (auras.Count > 0) {
				Entrada entrada = Entrada.GetInstancia();
				entrada.PresionarCarta(2, auras[0]);
				GameObject carta = Tutor.MejorCriatura(fisica.TraerCartasEnCampo(2));
				if (carta != null && carta.GetComponent<CartaPerfeccion>().EsPerfecta()) {
					entrada.PresionarCarta(2, carta);
					BuscadorCampo buscador = BuscadorCampo.getInstancia();
					GameObject campo = buscador.buscarCampoLibre(2);
					if (campo != null) {
						entrada.PresionarCampo(campo);
					}
				}
			}

			if (estadisticas.GetValor(codigo) == cantidadActual)
				posponer = true;
		}


	}

}