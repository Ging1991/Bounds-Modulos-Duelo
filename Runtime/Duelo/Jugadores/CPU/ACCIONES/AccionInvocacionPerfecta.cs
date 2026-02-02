using System.Collections.Generic;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
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
				Debug.Log(perfecta.cartaSeleccionada.name);
				SeleccionarMateriales(opciones[0]);
				if (EstadisticasSingleton.Instancia.GetValor(codigo) == cantidadActual)
					posponer = true;
			}
		}


		private bool SeleccionarMateriales(GameObject criatura) {
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			EmblemaSeleccionMaterial material = EmblemaSeleccionMaterial.GetInstancia();

			string invocacion = criatura.GetComponent<CartaInfo>().original.datoCriatura.perfeccion;

			if (invocacion == "ROMPECABEZAS") {
				foreach (GameObject carta in new List<GameObject>(fisica.TraerCartasEnMano(2))) {
					material.Seleccionar(carta);
					if (material.EstaCompleto())
						break;
				}
				return true;
			}

			if (invocacion == "REFLEJO") {
				foreach (GameObject carta in new List<GameObject>(fisica.TraerCartasEnCampo(1))) {
					material.Seleccionar(carta);
					if (material.EstaCompleto())
						break;
				}
				return true;
			}

			foreach (GameObject carta in new List<GameObject>(fisica.TraerCartasEnCampo(2))) {
				material.Seleccionar(carta);
				if (material.EstaCompleto())
					break;
				return true;
			}
			return false;
		}


	}

}