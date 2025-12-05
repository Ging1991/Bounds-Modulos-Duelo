using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Visuales;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Global;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaVida : EmblemaPadre {


		public static void DisminuirVida(int jugador, int cantidad, string visual = "GOLPE") {

			if (VidaActual(jugador) <= 0)
				return;

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CondicionClase condicionClase = new CondicionClase(clase: "TRAMPA");
			List<GameObject> trampas = condicionClase.CumpleLista(fisica.TraerCartasEnCampo(jugador));

			foreach (GameObject trampa in trampas) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
				CartaGeneral generalTrampa = trampa.GetComponent<CartaGeneral>();

				if (!generalTrampa.bocaArriba) {

					if (infoTrampa.original.datoTrampa.tipo == "gana_vida") {
						cantidad = cantidad * (-1);
						generalTrampa.ColocarBocaArriba();
					}

					if (infoTrampa.original.datoTrampa.tipo == "niega_vida") {
						generalTrampa.ColocarBocaArriba();
						return;
					}

				}

			}

			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.SetVida(bloque.vida - cantidad);
			bloque.GetComponentInChildren<GestorVisual>().Animar(visual);

			TerminarJuego terminar = GameObject.Find("TerminarJuego").GetComponent<TerminarJuego>();
			Configuracion configuracion = new Configuracion();

			if (bloque.vida < 1) {
				if (jugador == 1) {
					configuracion.GanarOro(100);
					terminar.terminar(false);

				}
				else {
					configuracion.GanarOro(bloque.vida / 10);
					terminar.terminar(true);
				}
			}
		}


		public static void AumentarVida(int jugador, int cantidad, string visual = "REVITALIZAR") {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.SetVida(bloque.vida + cantidad);
			bloque.GetComponentInChildren<GestorVisual>().Animar(visual);

			foreach (var carta in new SubCartasControladas(jugador).Generar()) {
				if (carta.GetComponent<CartaEfecto>().TieneClave("MISION_LP")) {
					EfectoBD efecto = carta.GetComponent<CartaEfecto>().GetEfecto("MISION_N");
					if (carta.GetComponent<CartaInfo>().TraerContadores("mision") < efecto.parametroN)
						EmblemaEfectos.Activar(new EfectoSobreCarta(carta, new SubColocarContador("mision", 1), carta));
				}
			}

			ActivarTrampas(jugador, cantidad);
		}


		private static void ActivarTrampas(int jugador, int cantidad) {

			int adversario = Adversario(jugador);

			foreach (GameObject trampa in EmblemaPadre.TraerTrampasBocaAbajo(adversario)) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();

				if (infoTrampa.original.datoTrampa.tipo == "REACCION_NEGATIVA") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, jugador, new SubModificarLP(cantidad * (-2))));
				}

			}


			foreach (GameObject trampa in EmblemaPadre.TraerTrampasBocaAbajo(jugador)) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();

				if (infoTrampa.original.datoTrampa.tipo == "VIDA_DAÑO") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, adversario, new SubModificarLP(-cantidad)));
					break;
				}

			}

		}



		public static int VidaActual(int jugador) {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			return bloque.vida;
		}


	}

}