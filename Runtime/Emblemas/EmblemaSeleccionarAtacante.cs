using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblema {

	public class EmblemaSeleccionarAtacante {


		public static void Seleccionar(int jugador, GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			Fisica fisica = conocimiento.traerFisica();

			if (turnos.fase != EmblemaTurnos.Fase.FASE_DE_BATALLA)
				return;

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaMovimiento cartaMovimiento = carta.GetComponent<CartaMovimiento>();

			if (info.controlador != turnos.jugadorActivo)
				return;

			if (info.original.clase != "CRIATURA")
				return;

			if (cartaMovimiento.estaGirado)
				return;

			if (!fisica.TraerCartasEnCampo(jugador).Contains(carta))
				return;

			if (GameObject.Find("cuadro") != null)
				return;

			GameObject.FindAnyObjectByType<Seleccionador>().SeleccionarParaCombate();
			GameObject.FindAnyObjectByType<Seleccionador>().atacante = carta;
			carta.GetComponent<CartaFX>().seleccionar();
		}


	}

}