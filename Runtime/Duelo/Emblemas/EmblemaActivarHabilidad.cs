using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Ventanas;
using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaActivarHabilidad : IPresionarBoton {

		private static EmblemaActivarHabilidad instancia;
		public GameObject carta;
		public static List<string> HABILIDADES_ACTIVADAS = new List<string>{
			"recuperar",
			"manipulacion",
			"CARBONIZAR",
			"DESTRUIR",
			"ACTIVADO_DESCARTA_ROBO",
			"ACTIVADO_DESCARTA_DAÑO",
			"ACTIVADO_RECUPERA_CRIATURA"
		};

		private EmblemaActivarHabilidad() { }


		public static EmblemaActivarHabilidad GetInstancia() {
			if (instancia == null)
				instancia = new EmblemaActivarHabilidad();
			return instancia;
		}


		public void Activar(GameObject carta) {

			if (CuadroAceptar.existenCuadros())
				return;
			if (GameObject.Find("PanelSeleccionar") != null)
				return;

			this.carta = carta;
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			int controlador = info.controlador;
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();

			// La carta debe estar en campo
			if (!fisica.TraerCartasEnCampo(controlador).Contains(carta))
				return;

			// Si hay un aura seleccionada no puede activar efecto
			if (Seleccionador.Instancia.cartaParaJugar != null)
				return;

			// debe estar enderezada
			if (movimiento.estaGirado)
				return;

			// debe ser turno del controlador
			if (turnos.jugadorActivo != controlador)
				return;

			// debe ser fase principal
			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return;

			// debe tener una habilidad activable
			if (info.original.clase != "CRIATURA" && info.original.clase != "EQUIPO")
				return;

			if (!TieneHabilidadActivada(carta))
				return;

			// instancio un cuadro de confirmacion
			if (info.controlador == 2) {
				Resolver();
				return;
			}

			VentanaControl.CrearVentanaConfirmar("¿Desea activar la habilidad?", this);
		}


		private void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();

			CondicionCarta condicionCriatura = new CondicionClase("CRIATURA");
			int jugador = cartaInfo.controlador;
			int adversario = JugadorDuelo.Adversario(jugador);

			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			movimiento.Girar();

			EmblemaSeleccionarEquipar equipo = EmblemaSeleccionarEquipar.GetInstancia();
			equipo.Deseleccionar();

			/*
						if (cartaInfo.habilidades.Contains("RECUPERAR")) {
							List<GameObject> cartas = fisica.TraerCartasEnCementerio(cartaInfo.controlador);
							if (cartas.Count > 0) {
								if (cartaInfo.controlador == 2) {
									fisica.EnviarHaciaMano(cartas[0], cartaInfo.propietario);
									CartaGeneral scr = cartas[0].GetComponent<CartaGeneral>();
									scr.ColocarBocaArriba();
									return;
								}

								Instanciador instanciador = conocimiento.traerInstanciador();
								PanelSeleccion panel = instanciador.CrearPanelSeleccionarCarta().GetComponent<PanelSeleccion>();
								//panel.Iniciar(new SeleccionRecuperar(cartaInfo.controlador), "Selecciona una carta para traer del descarte a tu mano.");
								panel.AgregarOpciones(cartas);
							}
						}*/
			/*
						if (cartaInfo.habilidades.Contains("MANIPULACION")) {
							List<GameObject> cartas = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(adversario));

							if (cartas.Count > 0) {
								if (cartaInfo.controlador == 2) {
									BuscadorCampo buscador = BuscadorCampo.getInstancia();
									GameObject campo = buscador.buscarCampoLibre(jugador);
									if (campo != null) {
										fisica.EnviarHaciaCampo(jugador, carta, campo);
										CartaInfo infoz = cartas[0].GetComponent<CartaInfo>();
										infoz.controlador = cartaInfo.controlador;
										GameObject grupo = GameObject.Find($"Cartas{2}");
										cartas[0].transform.SetParent(grupo.transform);
									}
									return;
								}

								Instanciador instanciador = conocimiento.traerInstanciador();
								PanelSeleccion panel = instanciador.CrearPanelSeleccionarCarta().GetComponent<PanelSeleccion>();
								//panel.Iniciar(new SeleccionControlar(cartaInfo.controlador), "Selecciona una carta para ganar control sobre ella.");
								panel.AgregarOpciones(cartas);
							}
						}*/

			if (cartaInfo.GetComponent<CartaEfecto>().TieneClave("DESTRUIR")) {
				List<GameObject> cartas = fisica.TraerCartasEnCampo(adversario);
				if (cartas.Count > 0) {
					SeleccionarDestruir seleccionar = new();
					if (cartaInfo.controlador == 2) {
						seleccionar.Seleccionar(cartas[0]);
						return;
					}
					fisica.panel.GetComponent<PanelCartas>().Iniciar(cartas, seleccionar, 1, "Selecciona una carta para destruir.");
				}
			}

			if (cartaInfo.GetComponent<CartaEfecto>().TieneClave("CARBONIZAR")) {
				EfectoBase efecto = new EfectoSobreJugador(carta, adversario, new SubModificarLP(-500));
				efecto.AgregarEtiqueta("EXPLOSION");
				EmblemaEfectos.Activar(efecto);
			}

			if (carta.GetComponent<CartaEfecto>().TieneClave("ACTIVADO_DESCARTA_DAÑO")) {
				List<GameObject> cartas = condicionCriatura.CumpleLista(fisica.TraerCartasEnMano(jugador));
				if (cartas.Count > 0) {
					fisica.EnviarHaciaDescarte(cartas[0], cartaInfo.controlador);
					EfectoBase efecto = new EfectoSobreJugador(carta, adversario, new SubModificarLP(-500));
					efecto.AgregarEtiqueta("EXPLOSION");
					EmblemaEfectos.Activar(efecto);
				}
			}

			if (carta.GetComponent<CartaEfecto>().TieneClave("ACTIVADO_DESCARTA_ROBO")) {
				List<GameObject> cartas = condicionCriatura.CumpleLista(fisica.TraerCartasEnMano(jugador));
				if (cartas.Count > 0) {
					fisica.EnviarHaciaDescarte(cartas[0], cartaInfo.controlador);
					EmblemaEfectos.Activar(new EfectoSobreJugador(carta, jugador, new SubRobar(1)));
				}
			}

			if (carta.GetComponent<CartaEfecto>().TieneClave("ACTIVADO_RECUPERA_CRIATURA")) {
				List<GameObject> cartas = condicionCriatura.CumpleLista(fisica.TraerCartasEnCementerio(jugador));
				if (cartas.Count > 0) {
					fisica.EnviarHaciaMano(cartas[0], cartaInfo.controlador);
				}
			}

		}


		public static bool TieneHabilidadActivada(GameObject carta) {
			bool ret = false;
			foreach (string habilidad in HABILIDADES_ACTIVADAS)
				ret = ret || carta.GetComponent<CartaEfecto>().TieneClave(habilidad);

			return ret;
		}


		public void PresionarBoton(TipoBoton boton) {
			if (boton == TipoBoton.ACEPTAR)
				Resolver();
			if (boton == TipoBoton.CANCELAR)
				EmblemaSeleccionarEquipar.GetInstancia().Deseleccionar();
		}


	}

}