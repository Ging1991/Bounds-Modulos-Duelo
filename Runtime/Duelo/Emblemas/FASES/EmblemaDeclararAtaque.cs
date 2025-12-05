using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Duelo.Fila;
using Bounds.Duelo.Fila.Fases;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Fases {

	public class EmblemaDeclararAtaque {

		public static void Declarar(GameObject atacante, GameObject atacado) {
			Seleccionador seleccionador = Seleccionador.Instancia;
			seleccionador.SeleccionarParaCombate(atacante, atacado);
			seleccionador.combateCancelado = false;
			atacante.GetComponent<CartaMovimiento>().Girar();
			ActivarEfectos(atacante, atacado);
			ActivarTrampas(atacante, atacado);

			PilaEfectos pila = GameObject.FindAnyObjectByType<PilaEfectos>();
			if (pila.EstaVacia()) {
				EmblemaAtaque.ResolverCombate();

			}
			else {
				FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
				fila.Agregar(new FaseCombate(atacante, atacado));
			}
		}


		private static void ActivarEfectos(GameObject atacante, GameObject atacado) {

			CartaEfecto efectoAtacante = atacante.GetComponent<CartaEfecto>();
			CartaInfo infoAtacante = atacante.GetComponent<CartaInfo>();
			CartaInfo infoAtacado = atacado.GetComponent<CartaInfo>();

			Fisica fisica = Fisica.Instancia;
			int adversario = JugadorDuelo.Adversario(infoAtacante.controlador);

			if (efectoAtacante.TieneClave("ROBAR")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(atacante, infoAtacante.controlador, new SubRobar(1)));
			}

			if (efectoAtacante.TieneClave("ARRUINAR")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(atacante, adversario, new SubMoler(5)));
			}

			foreach (var aura in new SubCartasControladas(infoAtacado.controlador, new CondicionClase("AURA")).Generar()) {
				if (aura.GetComponent<CartaEfecto>().TieneClave("VINCULO_SOMBRA")) {
					GameObject.FindAnyObjectByType<GestorDeSonidos>().ReproducirSonido("FxRebote");
					EmblemaEfectos.Activar(new EfectoSobreCarta(aura, new SubBarajar(), aura));
					Seleccionador.Instancia.combateCancelado = true;
					break;
				}
			}


		}


		private static void ActivarTrampas(GameObject criaturaAtacante, GameObject objetivoAtacado) {

			int jugadorAtacado = objetivoAtacado.GetComponent<CartaInfo>().controlador;
			int jugadorAtacante = criaturaAtacante.GetComponent<CartaInfo>().controlador;

			Seleccionador seleccionador = GameObject.FindAnyObjectByType<Seleccionador>();
			Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
			CartaInfo infoAtacante = criaturaAtacante.GetComponent<CartaInfo>();
			PilaEfectos pila = GameObject.Find("Pila").GetComponent<PilaEfectos>();

			foreach (GameObject trampa in EmblemaPadre.TraerTrampasBocaAbajo(jugadorAtacado)) {

				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
				CartaGeneral generalTrampa = trampa.GetComponent<CartaGeneral>();

				if (infoTrampa.original.datoTrampa.tipo == "CAMBIO_OBJETIVO") {
					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					List<GameObject> criaturasDelAtacante = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(jugadorAtacante));
					criaturasDelAtacante.Remove(criaturaAtacante);

					if (criaturasDelAtacante.Count > 0) {
						generalTrampa.ColocarBocaArriba();
						seleccionador.atacado = criaturasDelAtacante[0];
					}
				}

				if (infoTrampa.original.datoTrampa.tipo == "niega_ataque") {
					generalTrampa.ColocarBocaArriba();
					seleccionador.combateCancelado = true;
				}

				if (infoTrampa.original.datoTrampa.tipo == "CANCELA_ATAQUE") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoCancelarAtaque(trampa));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "CONTRATAQUE_DRAGON") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("debilidad", 2), criaturaAtacante));

					Condicion condicion = new Condicion(tipoCarta: "TRAMPA", textoParcial: "Contrataque");
					List<GameObject> cartas = condicion.CumpleLista(fisica.TraerCartasEnMazo(infoTrampa.controlador));
					if (cartas.Count > 0)
						EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarBocaAbajo(jugadorAtacado), cartas[0]));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "ESQUIVAR") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoCancelarAtaque(trampa));
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, infoTrampa.controlador, new SubRobar(1)));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "LEVANTA_HECHIZO") {
					CondicionClase condicionHechizo = new CondicionClase("HECHIZO");
					List<GameObject> hechizos = condicionHechizo.CumpleLista(fisica.TraerCartasEnCampo(1));
					hechizos.AddRange(condicionHechizo.CumpleLista(fisica.TraerCartasEnCampo(2)));
					if (hechizos.Count > 0) {
						EmblemaTrampa.ActivarTrampa(trampa);
						EmblemaEfectos.Activar(new EfectoSobreCartas(trampa, new SubEnviarManoDescarte(), hechizos));
					}
				}

				if (infoTrampa.original.datoTrampa.tipo == "FICHA_CACHORRO") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoCancelarAtaque(trampa));
					EmblemaEfectos.Activar(new EfectoCrearFicha(trampa, jugadorAtacado, 225, 1));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "RUGIDO") {
					generalTrampa.ColocarBocaArriba();
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("debilidad", 4), criaturaAtacante));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "PODER2") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("poder", 2), objetivoAtacado));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "FATAL") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criaturaAtacante));
					break;
				}

			}

		}

	}

}