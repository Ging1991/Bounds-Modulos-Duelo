using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Utiles;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Jugar {

	public class EmblemaJuegoSeleccionar : EmblemaPadre {

		private static bool PuedeJugarPorClase(GameObject carta, int jugador, string clase) {

			if (clase == "AURA")
				return true;

			if (clase == "EQUIPO")
				return true;

			if (clase == "TRAMPA")
				return true;

			if (clase == "HECHIZO")
				return true;

			if (clase == "MISION")
				return true;

			if (clase == "VACIO")
				return true;

			if (clase == "CRIATURA") {
				return JugadorDuelo.GetInstancia(jugador).invocaciones_normales > 0;
			}

			return false;
		}


		public static bool PuedeJugar(GameObject carta) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int controlador = info.controlador;
			Fisica fisica = Fisica.Instancia;
			EmblemaTurnos turnos = EmblemaTurnos.GetInstancia();

			if (!fisica.TraerCartasEnMano(controlador).Contains(carta))
				return false;

			if (fisica.TraerCartasEnCampo(controlador).Count == 5)
				return false;

			if (controlador != turnos.jugadorActivo)
				return false;

			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return false;

			return PuedeJugarPorClase(carta, controlador, info.original.clase);
		}


		public static void Seleccionar(GameObject carta, bool forzarSeleccion = false) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int controlador = info.controlador;
			Fisica fisica = Fisica.Instancia;

			if (Seleccionador.Instancia.cartaParaJugar == carta) {
				Deseleccionar();
				return;
			}

			if (!fisica.TraerCartasEnMano(controlador).Contains(carta) && !forzarSeleccion)
				return;

			if (info.original.clase == "CRIATURA" && JugadorDuelo.GetInstancia(controlador).invocaciones_normales == 0)
				return;

			Deseleccionar();
			if (PuedeJugar(carta) || forzarSeleccion) {
				Seleccionador.Instancia.SeleccionarParaJugar(carta);
			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
			}
		}


		public static void Deseleccionar() {
			Seleccionador.Instancia.SeleccionarParaJugar();
			Seleccionador.Instancia.SeleccionarParaVincular();
		}


		public static void SeleccionarParaVincular(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			GameObject aura = Seleccionador.Instancia.cartaParaJugar;

			if (aura == null)
				return;

			if (aura.GetComponent<CartaInfo>().original.clase != "AURA")
				return;

			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return;

			if (info.original.clase != "CRIATURA")
				return;

			Seleccionador.Instancia.SeleccionarParaVincular(carta);
		}


	}

}