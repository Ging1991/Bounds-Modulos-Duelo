using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Condiciones;
using Bounds.Persistencia.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Paneles.Seleccion;
using Ging1991.Core;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;
using Bounds.Fisicas.Campos;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaJuegoActivarHechizo {

		public static void Jugar(int jugador, GameObject lugar) {

			GameObject hechizo = Seleccionador.Instancia.cartaParaJugar;

			if (!PuedeJugar(jugador, lugar, hechizo))
				return;

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			if (dato.tipo.Contains("OBJETIVO")) {
				SeleccionarObjetivo(jugador, lugar, hechizo);
				return;
			}

			if (dato.tipo.Contains("SACRIFICIO")) {
				SeleccionarSacrificio(jugador, lugar, hechizo);
				return;
			}

			if (dato.tipo.Contains("DESCARTE")) {
				SeleccionarDescarte(jugador, lugar, hechizo);
				return;
			}

			if (dato.tipo.Contains("COSTE_LP")) {
				PagarLP(jugador, lugar, hechizo);
				return;
			}

			ActivarCartaEnCampo(jugador, lugar);
			ActivarEfectosDeOtrasCartas(hechizo);
			EmblemaHechizoActivacion.Activar(jugador, hechizo);
		}


		private static bool PuedeJugar(int jugador, GameObject lugar, GameObject hechizo) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();

			if (hechizo == null)
				return false;

			CartaInfo info = hechizo.GetComponent<CartaInfo>();
			if (info.original.clase != "HECHIZO")
				return false;

			CampoLugar campo = lugar.GetComponent<CampoLugar>();
			if (campo.carta != null)
				return false;

			if (campo.jugador != jugador)
				return false;

			if (jugador != turnos.jugadorActivo)
				return false;

			return true;
		}


		private static void SeleccionarObjetivo(int jugador, GameObject lugar, GameObject hechizo) {

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			Fisica fisica = Fisica.Instancia;
			CondicionCarta condicion = CondicionMapper.GenerarCondicion(dato.condicionCarta, jugador);

			List<GameObject> opciones = new();

			if (dato.fuentes.Contains("CAMPO")) {
				opciones.AddRange(fisica.TraerCartasEnCampo(1));
				opciones.AddRange(fisica.TraerCartasEnCampo(2));
			}
			if (dato.fuentes.Contains("MAZO")) {
				opciones.AddRange(fisica.TraerCartasEnMazo(1));
				opciones.AddRange(fisica.TraerCartasEnMazo(2));
			}
			if (dato.fuentes.Contains("DESCARTE")) {
				opciones.AddRange(fisica.TraerCartasEnCementerio(1));
				opciones.AddRange(fisica.TraerCartasEnCementerio(2));
			}
			if (dato.fuentes.Contains("MATERIALES")) {
				opciones.AddRange(fisica.TraerCartasEnMateriales(1));
				opciones.AddRange(fisica.TraerCartasEnMateriales(2));
			}

			if (condicion != null)
				opciones = condicion.CumpleLista(opciones);

			if (opciones.Count >= 1) {
				SeleccionarObjetivoDeHechizo accion = new SeleccionarObjetivoDeHechizo(jugador, lugar);
				if (jugador == 1 && opciones.Count > 1) {
					fisica.panel.SetActive(true);
					PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
					panel.Iniciar(opciones, accion, texto: "Selecciona un objetivo para el efecto.");

				}
				else {
					accion.Seleccionar(opciones[0]);
				}

			}
			else {
				ControlDuelo.Instancia.GetComponent<GestorDeSonidos>().ReproducirSonido("FxRebote");
				Seleccionador.Instancia.SeleccionarParaJugar();
			}
		}


		private static void SeleccionarDescarte(int jugador, GameObject lugar, GameObject hechizo) {

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			Fisica fisica = Fisica.Instancia;
			CondicionCarta condicion = CondicionMapper.GenerarCondicion(dato.condicionCarta);
			List<GameObject> cartasEnMano = new List<GameObject>(fisica.TraerCartasEnMano(jugador));
			cartasEnMano.Remove(hechizo);
			if (condicion != null)
				cartasEnMano = condicion.CumpleLista(cartasEnMano);

			if (cartasEnMano.Count > 0) {
				SeleccionarDescarteDeHechizo accion = new(jugador, lugar);
				if (jugador == 1 && cartasEnMano.Count > 1) {
					fisica.panel.SetActive(true);
					PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
					panel.Iniciar(cartasEnMano, accion, texto: "Selecciona una carta para descartar.");
				}
				else {
					accion.Seleccionar(cartasEnMano[0]);
				}

			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
				Seleccionador.Instancia.SeleccionarParaJugar();
			}
		}


		private static void SeleccionarSacrificio(int jugador, GameObject lugar, GameObject hechizo) {

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			Fisica fisica = Fisica.Instancia;
			CondicionCarta condicion = CondicionMapper.GenerarCondicion(dato.condicionCarta);
			List<GameObject> cartasEnCampo = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));
			if (condicion != null)
				cartasEnCampo = condicion.CumpleLista(cartasEnCampo);

			if (cartasEnCampo.Count > 0) {
				SeleccionarSacrificioDeHechizo accion = new(jugador, lugar);
				if (jugador == 1 && cartasEnCampo.Count > 1) {
					fisica.panel.SetActive(true);
					PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
					panel.Iniciar(cartasEnCampo, accion, texto: "Selecciona una carta para sacrificar.");
				}
				else {
					accion.Seleccionar(cartasEnCampo[0]);
				}

			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
				Seleccionador.Instancia.SeleccionarParaJugar();
			}
		}


		private static void PagarLP(int jugador, GameObject lugar, GameObject hechizo) {

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;

			if (EmblemaVida.VidaActual(jugador) > dato.costeLP) {
				EmblemaVida.DisminuirVida(jugador, dato.costeLP, "VENENO");
				LanzarLP(jugador, lugar, dato.costeLP);

			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
				Seleccionador.Instancia.SeleccionarParaJugar();
			}
		}


		public static void LanzarObjetivo(int jugador, GameObject lugar, GameObject objetivo) {
			GameObject hechizo = Seleccionador.Instancia.cartaParaJugar;
			ActivarCartaEnCampo(jugador, lugar);
			ActivarEfectosDeOtrasCartas(hechizo);
			EmblemaHechizoActivacion.ActivarConObjetivo(hechizo, objetivo);
		}


		public static void LanzarSacrificio(int jugador, GameObject lugar, GameObject sacrificio) {
			GameObject hechizo = Seleccionador.Instancia.cartaParaJugar;
			ActivarCartaEnCampo(jugador, lugar);
			ActivarEfectosDeOtrasCartas(hechizo);
			EmblemaHechizoActivacion.ActivarConSacrificio(jugador, hechizo, sacrificio);
		}


		public static void LanzarDescarte(int jugador, GameObject lugar, GameObject descarte) {
			GameObject hechizo = Seleccionador.Instancia.cartaParaJugar;
			ActivarCartaEnCampo(jugador, lugar);
			ActivarEfectosDeOtrasCartas(hechizo);
			EmblemaHechizoActivacion.ActivarConDescarte(jugador, hechizo, descarte);
		}


		public static void LanzarLP(int jugador, GameObject lugar, int costeLP) {
			GameObject hechizo = Seleccionador.Instancia.cartaParaJugar;
			ActivarCartaEnCampo(jugador, lugar);
			ActivarEfectosDeOtrasCartas(hechizo);
			EmblemaHechizoActivacion.ActivarConPagoLP(jugador, hechizo, costeLP);
		}


		private static void ActivarCartaEnCampo(int jugador, GameObject lugar) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			GameObject hechizo = Seleccionador.Instancia.cartaParaJugar;
			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxLanzar");
			fisica.EnviarHaciaCampo(jugador, hechizo, lugar);
			Seleccionador.Instancia.SeleccionarParaJugar();
			CartaArrastrar.jugado = true;
			EmblemaPadre.ActivarEfectosDeActivacion(hechizo);
		}


		private static void ActivarEfectosDeOtrasCartas(GameObject hechizo) {

			CartaInfo info = hechizo.GetComponent<CartaInfo>();
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			int adversario = JugadorDuelo.Adversario(info.controlador);

			List<GameObject> cartasEnCampo = new List<GameObject>(fisica.TraerCartasEnCampo(info.controlador));
			foreach (GameObject cartaEnCampo in cartasEnCampo) {

				CartaEfecto cartaEfectoEncampo = cartaEnCampo.GetComponent<CartaEfecto>();

				if (cartaEfectoEncampo.TieneClave("QUEMADURA_MAGICA")) {
					EfectoBase efectoBase = new EfectoSobreJugador(cartaEnCampo, adversario, new SubModificarLP(-500));
					efectoBase.AgregarEtiqueta("VENENO");
					EmblemaEfectos.Activar(efectoBase);
				}
				if (cartaEfectoEncampo.TieneClave("APRENDER_C")) {
					EfectoBD efecto = cartaEfectoEncampo.GetEfecto("APRENDER_C");
					if (efecto.parametroClase == "HECHIZO")
						EmblemaEfectos.Activar(new EfectoSobreJugador(cartaEnCampo, info.controlador, new SubRobar(1)));
				}
			}


		}


	}

}