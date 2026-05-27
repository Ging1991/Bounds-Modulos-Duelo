using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Duelo.Condiciones;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;
using Bounds.Persistencia;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaRobo : EmblemaPadre {


		public static void RobarCartas(int jugador, int cantidad) {

			for (int i = 0; i < cantidad; i++)
				RobarCarta(jugador);

		}


		private static void RobarCarta(int jugador) {

			Fisica fisica = Fisica.Instancia;

			if (fisica.listador.SiguienteCarta(jugador, ListadorDeZonas.Zona.MAZO) == null)
				DerrotaDescarte(jugador);

			GameObject carta = fisica.RobarCarta(jugador);
			if (carta == null)
				return;

			List<GameObject> cartasLadron = fisica.TraerCartasEnCampo(jugador);
			foreach (GameObject cartaLadron in cartasLadron) {
				if (cartaLadron.GetComponent<CartaEfecto>().TieneClave("LADRON"))
					EmblemaVida.DisminuirVida(JugadorDuelo.Adversario(jugador), 500);
			}

			int adversario = JugadorDuelo.Adversario(jugador);
			ActivarTrampas(adversario);
			ActivarVacios(jugador);
			ActivarEfectos(jugador, carta);
		}


		private static void ActivarVacios(int jugador) {
			int adversario = JugadorDuelo.Adversario(jugador);

			foreach (GameObject vacio in new SubCartasControladas(0, new CondicionClase("VACIO")).Generar()) {
				CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();
				if (infoVacio.original.datoVacio.tipo == "NIDO") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, adversario, new SubModificarLP(-300), "VENENO"));
				}
			}

		}


		private static void ActivarEfectos(int jugador, GameObject cartaRobada) {
			int adversario = JugadorDuelo.Adversario(jugador);
			CartaInfo infoRobada = cartaRobada.GetComponent<CartaInfo>();

			foreach (GameObject carta in new SubCartasControladas(jugador).Generar()) {
				if (carta.GetComponent<CartaEfecto>().TieneClave("ROBO_PALADINFINITO") && infoRobada.original.nivel == 8) {
					//EmblemaDescartarCarta.Descartar(cartaRobada);
					EmblemaEfectos.Activar(new EfectoSobreJugador(carta, adversario, new SubModificarLP(-500), "VENENO"));
					EmblemaEfectos.Activar(new EfectoSobreJugador(carta, jugador, new SubRobar(1), "NUBE"));
				}
			}

		}


		private static void ActivarTrampas(int adversario) {

			Fisica fisica = Fisica.Instancia;

			foreach (GameObject trampa in TraerTrampasBocaAbajo(adversario)) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();

				if (infoTrampa.original.datoTrampa.tipo == "CONTRATAQUE_PEGASO") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, adversario, new SubRobar(1)));
					Condicion condicion = new Condicion(tipoCarta: "TRAMPA", textoParcial: "Contrataque");
					List<GameObject> cartas = condicion.CumpleLista(fisica.TraerCartasEnMazo(infoTrampa.controlador));
					if (cartas.Count > 0)
						EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarBocaAbajo(adversario), cartas[0]));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "ROBO_X_ROBO_N") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, adversario, new SubRobar(infoTrampa.original.datoTrampa.cantidad)));
				}
			}

		}


		private static void DerrotaDescarte(int jugador) {

			TerminarJuego terminar = GameObject.Find("TerminarJuego").GetComponent<TerminarJuego>();
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			ControlDuelo controlDuelo = GameObject.FindAnyObjectByType<ControlDuelo>();
			Billetera billetera = GameObject.FindAnyObjectByType<ControlDuelo>().billetera;

			bool haGanado = false;
			foreach (GameObject carta in fisica.TraerCartasEnCampo(jugador)) {
				haGanado = haGanado || carta.GetComponent<CartaEfecto>().TieneClave("VICTORIA_VACIO");
			}

			if (!haGanado) {
				if (jugador == 1) {
					terminar.Terminar(false);
					billetera.GanarOro(100);
				}
				else {
					BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + 1);
					billetera.GanarOro(bloque.vida / 10);
					terminar.Terminar(true);
				}
			}
			else {
				if (jugador == 1) {
					BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + 1);
					billetera.GanarOro(bloque.vida / 10);
					terminar.Terminar(true);
				}
				else {
					terminar.Terminar(false);
					billetera.GanarOro(100);
				}
			}

		}


	}

}