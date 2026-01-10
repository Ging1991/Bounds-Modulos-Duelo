using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaInvocacionNormal : EmblemaPadre {

		public static bool PuedeInvocar(int jugador, GameObject carta, GameObject lugar) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			JugadorDuelo jugadorDuelo = JugadorDuelo.GetInstancia(jugador);
			Campo campo = lugar.GetComponent<Campo>();

			if (campo.EstaOcupado())
				return false;

			if (carta == null)
				return false;

			if (carta.GetComponent<CartaInfo>().original.clase != "CRIATURA")
				return false;

			if (campo.jugador != jugador)
				return false;


			if (jugador != turnos.jugadorActivo)
				return false;

			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return false;

			if (jugadorDuelo.invocaciones_normales == 0)
				return false;

			return true;
		}


		public static void Invocar(int jugador, GameObject lugar) {

			GameObject carta = Seleccionador.Instancia.cartaParaJugar;

			if (!PuedeInvocar(jugador, carta, lugar))
				return;

			Fisica fisica = Fisica.Instancia;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			JugadorDuelo jugadorDuelo = JugadorDuelo.GetInstancia(jugador);

			jugadorDuelo.invocaciones_normales--;
			EmblemaInvocacion.Invocar(jugador, carta, lugar);
			EmblemaJuegoSeleccionar.Deseleccionar();


			ActivarHabilidades(carta);
			CartaArrastrar.jugado = true;
			ActivarTrampas(carta);
			ActivarVacios(jugador, carta);

			// TRAMPAS ANTI META
			int adversario = Adversario(jugador);
			GameObject lugarTrampa = BuscadorCampo.getInstancia().buscarCampoLibre(adversario);
			if (lugarTrampa != null) {
				CondicionClase condicionClase = new CondicionClase("TRAMPA");
				List<GameObject> trampasAntimeta = condicionClase.CumpleLista(fisica.TraerCartasEnMazo(adversario));
				foreach (GameObject trampa in trampasAntimeta) {
					CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
					CartaGeneral trampaGeneral = trampa.GetComponent<CartaGeneral>();

					if (infoTrampa.original.clase == "TRAMPA"
							&& infoTrampa.original.datoTrampa.tipo == "invoca_contadores_4000"
							&& info.calcularAtaque() >= 4000) {

						trampaGeneral.ColocarBocaArriba();
						EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("debilidad", 8), carta));
						//EfectosDeSonido.Tocar("FxLanzarCarta");	
						fisica.EnviarHaciaCampo(jugador, trampa, lugarTrampa);
						break;
					}
				}

			}

			ControlDuelo duelo = ControlDuelo.Instancia;
			duelo.HabilitarInvocacionPerfecta();
		}


		private static void ActivarVacios(int jugador, GameObject criatura) {
			CartaInfo info = criatura.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = criatura.GetComponent<CartaTipo>();
			CartaEfecto cartaEfecto = criatura.GetComponent<CartaEfecto>();
			int adversario = Adversario(jugador);
			CondicionClase condicionCriatura = new("CRIATURA");

			foreach (GameObject vacio in new SubCartasControladas(0, new CondicionClase("VACIO")).Generar()) {
				CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();

				if (infoVacio.original.datoVacio.tipo == "MUNDO_VOLCAN" && cartaTipo.ContieneTipo("PYRO") && info.original.datoCriatura.perfeccion == "BASICO") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, adversario, new SubModificarLP(-500), "EXPLOSION"));
				}

				if (infoVacio.original.datoVacio.tipo == "PLANTA_LP" && cartaTipo.ContieneTipo("PLANTA") && info.original.datoCriatura.perfeccion == "BASICO") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, jugador, new SubModificarLP(500), "REVITALIZAR"));
				}

				if (infoVacio.original.datoVacio.tipo == "ODIO" && cartaTipo.ContieneTipo("DEMONIO") && info.original.datoCriatura.perfeccion == "BASICO") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, adversario, new SubDescartar(1), "VENENO"));
				}

				if (infoVacio.original.datoVacio.tipo == "TRUENO" && cartaTipo.ContieneTipo("TRUENO")) {
					CondicionTipoCriatura condicionTrueno = new("trueno");
					EmblemaEfectos.Activar(
						new EfectoSobreCartas(
							vacio,
							new SubColocarContador("debilidad", 1),
							condicionTrueno.NoCumpleLista(
								condicionCriatura.CumpleLista(new SubCartasControladas(0).Generar())
							)
						)
					);
				}

				if (infoVacio.original.datoVacio.tipo == "VENENO" && cartaTipo.ContieneTipo("INSECTO") && info.original.datoCriatura.perfeccion == "BASICO") {
					CondicionTipoCriatura condicionInsecto = new("insecto");

					EmblemaEfectos.Activar(
						new EfectoSobreCartas(
							vacio,
							new SubColocarContador("veneno", 1),
							condicionInsecto.NoCumpleLista(
								condicionCriatura.CumpleLista(new SubCartasControladas(0).Generar())
							)
						)
					);

				}

				if (infoVacio.original.datoVacio.tipo == "ZOMBI" && cartaTipo.ContieneTipo("ZOMBI") && info.original.datoCriatura.perfeccion == "BASICO") {
					CondicionTipoCriatura condicionZombi = new("ZOMBI");

					EmblemaEfectos.Activar(
						new EfectoSobreCartas(
							vacio,
							new SubColocarContador("debilidad", 1),
							condicionZombi.NoCumpleLista(
								condicionCriatura.CumpleLista(new SubCartasControladas(0).Generar())
							)
						)
					);

				}

				if (infoVacio.original.datoVacio.tipo == "REY_DRAGON" && cartaTipo.ContieneTipo("DRAGON") && info.original.datoCriatura.perfeccion == "BASICO") {
					info.ColocarContador("poder", 1);
					EfectoBD efecto = new EfectoBD();
					efecto.clave = "BRUTAL";
					cartaEfecto.ColocarEfecto(efecto);
				}

			}

		}


		private static void ActivarTrampas(GameObject criatura) {

			int controlador = criatura.GetComponent<CartaInfo>().controlador;
			int adversario = Adversario(controlador);

			Fisica fisica = Fisica.Instancia;
			CartaInfo infoCriatura = criatura.GetComponent<CartaInfo>();

			foreach (GameObject trampa in TraerTrampasBocaAbajo(adversario)) {

				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
				CartaGeneral generalTrampa = trampa.GetComponent<CartaGeneral>();

				if (infoTrampa.original.datoTrampa.tipo == "invoca_contadores_4000" && infoCriatura.calcularAtaque() >= 4000) {
					generalTrampa.ColocarBocaArriba();
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("debilidad", 8), criatura));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "camino_dificil") {

					List<GameObject> criaturasJugador = new List<GameObject>(fisica.TraerCartasEnCampo(controlador));
					Condicion condicionCriatura = new Condicion(tipoCarta: "CRIATURA");
					criaturasJugador = condicionCriatura.CumpleLista(criaturasJugador);
					bool enderezado = false;
					foreach (GameObject XX in criaturasJugador) {
						CartaMovimiento movXX = XX.GetComponent<CartaMovimiento>();
						enderezado = enderezado || !movXX.estaGirado;
					}
					if (enderezado) {
						generalTrampa.ColocarBocaArriba();
						//pila.Agregar(new EfectoGirarCriaturas(trampa,info.controlador));
						//pila.Agregar(new EfectoTribulacion(trampa,Adversario(jugador), "planta"));
						break;
					}

				}
				if (infoTrampa.original.datoTrampa.tipo == "anti_fusion" && infoTrampa.original.clase == "FUSION") {
					//EmblemaDestruccion.Destruir(criatura, 0);
					generalTrampa.ColocarBocaArriba();
					break;
				}
				if (infoTrampa.original.datoTrampa.tipo == "CONTRATAQUE_FENIX") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EfectoBase efectoBase = new EfectoSobreJugador(trampa, controlador, new SubModificarLP(-500));
					efectoBase.AgregarEtiqueta("VENENO");
					EmblemaEfectos.Activar(efectoBase);
					Condicion condicion = new Condicion(tipoCarta: "TRAMPA", textoParcial: "Contrataque");
					List<GameObject> cartas = condicion.CumpleLista(fisica.TraerCartasEnMazo(infoTrampa.controlador));
					if (cartas.Count > 0)
						EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarBocaAbajo(adversario), cartas[0]));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "ILUSION" && !criatura.GetComponent<CartaPerfeccion>().EsPerfecta()) {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criatura));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "DESTRUYE_INVOCACION_NORMAL") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criatura));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "TRAMPA_PEZ" && infoCriatura.calcularDefensa() <= 3000) {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, controlador, new SubRobar(1)));
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criatura));
				}


				if (infoTrampa.original.datoTrampa.tipo == "SOBRECARGA_INVOCACION") {
					List<GameObject> cartasDelControlador = new List<GameObject>(fisica.TraerCartasEnCampo(controlador));
					if (cartasDelControlador.Count == 5) {
						EmblemaTrampa.ActivarTrampa(trampa);
						EmblemaEfectos.Activar(new EfectoSobreCartas(trampa, new SubDestruir(), cartasDelControlador));
						break;
					}
				}

				if (infoTrampa.original.datoTrampa.tipo == "NORMAL_X3" && infoCriatura.original.datoCriatura.perfeccion == "BASICO") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("debilidad", 3), criatura));
					break;
				}

			}

		}


		private static void ActivarHabilidades(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			PilaEfectos pila = GameObject.Find("Pila").GetComponent<PilaEfectos>();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
			int jugador = info.controlador;
			int adversario = Adversario(jugador);

			bool activado = false;

			foreach (var cartaEnCementerio in new SubCartasEnCementerio(jugador).Generar()) {
				if (cartaEnCementerio.GetComponent<CartaEfecto>().TieneClave("REFUERZOS_MAS_ALLA")) {
					EmblemaEfectos.Activar(
						new EfectoSobreCartas(
							cartaEnCementerio,
							new SubColocarContador("poder", 1),
							new SubCartasControladas(jugador).Generar()
						)
					);
				}
				if (cartaEnCementerio.GetComponent<CartaEfecto>().TieneClave("EXPLOSION_MAS_ALLA")) {
					EmblemaEfectos.Activar(
						new EfectoSobreJugador(
							cartaEnCementerio,
							adversario,
							new SubModificarLP(-500),
							"EXPLOSION"
						)
					);
				}
			}

			List<GameObject> cartasEnCampo1 = fisica.TraerCartasEnCampo(1);
			foreach (GameObject cartaEnCampo in cartasEnCampo1) {

				if (cartaEnCampo == carta)
					continue;

				CartaInfo infoEnCampo = cartaEnCampo.GetComponent<CartaInfo>();

				if (infoEnCampo.GetComponent<CartaEfecto>().TieneClave("ATADURAS") && turnos.jugadorActivo == 1) {
					int objetivo = 1;
					if (turnos.jugadorActivo == 1)
						objetivo = 2;

					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					EmblemaEfectos.Activar(new EfectoSobreListaDeCartas(
						cartaEnCampo,
						new SubColocarContador("debilidad", 1),
						new SubCartasControladas(objetivo, condicionCriatura)
					));
				}

				if (infoEnCampo.GetComponent<CartaEfecto>().TieneClave("REFUERZOS") && turnos.jugadorActivo == 1) {
					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					EmblemaEfectos.Activar(new EfectoSobreListaDeCartas(
						cartaEnCampo,
						new SubColocarContador("poder", 1),
						new SubCartasControladas(info.controlador, condicionCriatura)
					));
				}

				if (infoEnCampo.GetComponent<CartaEfecto>().TieneClave("BENDICION_T") && turnos.jugadorActivo == 1) {
					EfectoBD efecto = infoEnCampo.GetComponent<CartaEfecto>().GetEfecto("BENDICION_T");
					if (info.GetComponent<CartaTipo>().tipos.Contains(efecto.parametroTipo)) {
						SubColocarContador subefecto = new SubColocarContador("poder", 2);
						EmblemaEfectos.Activar(new EfectoSobreCarta(cartaEnCampo, subefecto, carta));
					}
				}

				if (infoEnCampo.GetComponent<CartaEfecto>().TieneClave("SUPREMACIA_TRUENO") && cartaTipo.ContieneTipo("TRUENO")) {
					pila.Agregar(new EfectoTrueno(cartaEnCampo));
					activado = true;
				}

			}

			List<GameObject> CartasEnCampo2 = fisica.TraerCartasEnCampo(2);
			foreach (GameObject cartaEnCampo in CartasEnCampo2) {

				if (cartaEnCampo == carta)
					continue;

				CartaInfo info1 = cartaEnCampo.GetComponent<CartaInfo>();

				if (info1.GetComponent<CartaEfecto>().TieneClave("ATADURAS") && turnos.jugadorActivo == 2) {
					int objetivo = 1;
					if (turnos.jugadorActivo == 1)
						objetivo = 2;
					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					EmblemaEfectos.Activar(new EfectoSobreListaDeCartas(
						cartaEnCampo,
						new SubColocarContador("debilidad", 1),
						new SubCartasControladas(objetivo, condicionCriatura)
					));
				}

				if (info1.GetComponent<CartaEfecto>().TieneClave("REFUERZOS") && turnos.jugadorActivo == 2) {
					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					EmblemaEfectos.Activar(new EfectoSobreListaDeCartas(
						cartaEnCampo,
						new SubColocarContador("poder", 1),
						new SubCartasControladas(info.controlador, condicionCriatura)
					));
				}

				if (info1.GetComponent<CartaEfecto>().TieneClave("SUPREMACIA_TRUENO") && cartaTipo.ContieneTipo("TRUENO") && !activado)
					pila.Agregar(new EfectoTrueno(cartaEnCampo));
			}

		}


	}

}