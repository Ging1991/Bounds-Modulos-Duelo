using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblema {

	public class EmblemaSeleccionarEquipar {

		public GameObject cartaSeleccionada;

		private static EmblemaSeleccionarEquipar instancia;

		private EmblemaSeleccionarEquipar() { }


		public static EmblemaSeleccionarEquipar GetInstancia() {
			if (instancia == null)
				instancia = new EmblemaSeleccionarEquipar();
			return instancia;
		}


		public void Seleccionar(int jugador, GameObject carta) {
			// marca el equipo, luego debera marcar el moustro

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			ControlDuelo duelo = conocimiento.traerDuelo();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();

			// Debe estar en la fase principal
			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return;

			// Debe pertenecer al jugador activo
			if (info.controlador != turnos.jugadorActivo)
				return;

			// Debe ser de tipo equipo
			if (info.original.clase != "EQUIPO")
				return;

			// Debe estar en campo
			if (!fisica.TraerCartasEnCampo(jugador).Contains(carta))
				return;

			// Debe estar libre, no equipar ya a alguien
			if (info.criaturaEquipada != null)
				return;

			// debe estar enderezada
			if (movimiento.estaGirado)
				return;

			Deseleccionar();
			CartaFX fx = carta.GetComponent<CartaFX>();
			fx.seleccionar();
			cartaSeleccionada = carta;
		}


		public void Deseleccionar() {
			EmblemaJuegoSeleccionar.Deseleccionar();
			if (cartaSeleccionada != null) {
				CartaFX fx = cartaSeleccionada.GetComponent<CartaFX>();
				fx.deseleccionar();
			}
			cartaSeleccionada = null;
		}


	}

}