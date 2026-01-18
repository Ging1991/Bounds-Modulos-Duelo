using System.Collections.Generic;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionInvocacionPerfecta : AccionBasica {

		private readonly TieneInvocacionesPerfectas tieneInvocacionesPerfectas;
		private readonly string codigo;

		public AccionInvocacionPerfecta(int prioridad, int jugador) : base(prioridad, jugador) {

			tieneInvocacionesPerfectas = new TieneInvocacionesPerfectas(jugador);
			codigo = $"INVOCACION_PERFECTA_{jugador}_jugadas";
			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(tieneInvocacionesPerfectas);
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL));
		}


		public override void Ejecutar() {
			int cantidadActual = EstadisticasSingleton.Instancia.GetValor(codigo);
			List<GameObject> opciones = tieneInvocacionesPerfectas.GetCartas();
			MetodoPivot(opciones, cantidadActual, codigo);
		}

		private void MetodoPivot(List<GameObject> opciones, int cantidadActual, string codigo) {
			if (opciones.Count > 0) {
				EmblemaSeleccionInvocacionPerfecta perfecta = EmblemaSeleccionInvocacionPerfecta.GetInstancia();
				perfecta.Seleccionar(jugador, opciones[0]);
				SeleccionarMateriales();
				if (EstadisticasSingleton.Instancia.GetValor(codigo) == cantidadActual)
					posponer = true;
			}
		}


		private bool SeleccionarMateriales() {
			Entrada entrada = Entrada.GetInstancia();
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			EmblemaSeleccionMaterial material = EmblemaSeleccionMaterial.GetInstancia();
			foreach (GameObject carta in new List<GameObject>(fisica.TraerCartasEnCampo(1))) {
				material.Seleccionar(carta);
				if (material.EstaCompleto())
					break;
			}
			foreach (GameObject carta in new List<GameObject>(fisica.TraerCartasEnCampo(2))) {
				material.Seleccionar(carta);
				if (material.EstaCompleto())
					break;
			}
			foreach (GameObject carta in new List<GameObject>(fisica.TraerCartasEnMano(2))) {
				material.Seleccionar(carta);
				if (material.EstaCompleto())
					break;
			}
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject campo = buscador.buscarCampoLibre(2);
			if (campo != null) {
				entrada.PresionarCampo(campo);
				return true;
			}
			return false;
		}


	}

}