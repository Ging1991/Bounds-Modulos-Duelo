using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Ging1991.Core;
using Bounds.Cartas;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas.Jugar {

	public class EmblemaJuegoActivar : EmblemaPadre {

		public static bool PuedeJugar(int jugador, GameObject carta, GameObject lugar) {
			if (carta == null)
				return false;

			CartaInfo info = carta.GetComponent<CartaInfo>();
			int controlador = info.controlador;
			Fisica fisica = Fisica.Instancia;
			EmblemaTurnos turnos = EmblemaTurnos.GetInstancia();

			Campo campo = lugar.GetComponent<Campo>();
			if (campo.EstaOcupado())
				return false;

			if (campo.jugador != jugador)
				return false;

			if (jugador != turnos.jugadorActivo)
				return false;

			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return false;

			if (!fisica.TraerCartasEnMano(controlador).Contains(carta))
				return false;

			return true;
		}


		private static void ColocarSobreElCampo(int jugador, GameObject carta, GameObject lugar, string clase) {
			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxLanzar");
			Seleccionador seleccionador = Seleccionador.Instancia;
			Fisica fisica = Fisica.Instancia;
			fisica.EnviarHaciaCampo(jugador, carta, lugar);
			bool bocaAbajo = clase == "TRAMPA";

			if (bocaAbajo)
				carta.GetComponent<CartaGeneral>().ColocarBocaAbajo();
			else {
				carta.GetComponent<CartaGeneral>().ColocarBocaArriba();
				ActivarEfectosDeActivacion(carta);
			}

			seleccionador.SeleccionarParaJugar();
			CartaArrastrar.jugado = true;
			ControlDuelo.Instancia.HabilitarInvocacionPerfecta();
			Estadisticas.Instancia.ModificarValor($"{clase}_{jugador}_jugadas", 1);
			if (bocaAbajo)
				ActivarTrampas(jugador, carta);
		}


		public static void Jugar(int jugador, GameObject carta, GameObject lugar, bool forzarSeleccion = false) {

			Seleccionador seleccionador = Seleccionador.Instancia;
			if (PuedeJugar(jugador, carta, lugar) || forzarSeleccion) {
				ColocarSobreElCampo(jugador, carta, lugar, carta.GetComponent<CartaInfo>().original.clase);
				//carta.GetComponentInChildren<CartaFisica>().Alejar();

			}
			else {
				ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxRebote");
				seleccionador.SeleccionarParaJugar();
			}
		}


		private static void ActivarTrampas(int jugador, GameObject trampa) {
			int adversario = EmblemaPadre.Adversario(jugador);

			foreach (var tramapAdversario in TraerTrampasBocaAbajo(adversario)) {

				if (tramapAdversario.GetComponent<CartaInfo>().original.datoTrampa.tipo == "ROMPE_TRAMPA") {
					EmblemaTrampa.ActivarTrampa(tramapAdversario);
					EmblemaEfectos.Activar(new EfectoSobreCarta(tramapAdversario, new SubDestruir(), trampa));
					break;
				}
			}

		}



	}

}