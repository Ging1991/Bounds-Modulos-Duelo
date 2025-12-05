using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Seleccion;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Emblemas.Vinculos;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblema {

	public class EmblemaJugarCarta {

		private static void JugarAura(GameObject aura, GameObject lugar) {
			CartaInfo infoAura = aura.GetComponent<CartaInfo>();
			if (aura.GetComponent<CartaEfecto>().TieneClave("VINCULO_INVOCACION")) {
				JugarAuraConInvocacion(infoAura.controlador, aura);
			}
			else {
				JugarAuraDirecto(infoAura.controlador, aura, lugar);
			}
		}


		private static void JugarAuraDirecto(int jugador, GameObject aura, GameObject lugar) {
			Campo campoCriatura = lugar.GetComponent<Campo>();
			if (!campoCriatura.EstaOcupado())
				return;
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject campoLibre = buscador.buscarCampoLibre(jugador);
			if (campoLibre == null)
				return;

			EmblemaJuegoSeleccionar.Seleccionar(aura);
			EmblemaJuegoSeleccionar.SeleccionarParaVincular(campoCriatura.carta);
			EmblemaJuegoJugarAura.Jugar(jugador, campoLibre);
		}


		private static void JugarAuraConInvocacion(int jugador, GameObject aura) {
			CartaInfo infoAura = aura.GetComponent<CartaInfo>();

			List<GameObject> camposLibres = BuscadorCampo.getInstancia().buscarCampoLibreN(jugador, 2);
			if (camposLibres.Count != 2)
				return;

			EfectoBD efectoInvocacion = aura.GetComponent<CartaEfecto>().GetEfecto("VINCULO_INVOCACION");
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> opcionesDesdeZona = new List<GameObject>();
			List<GameObject> opcionesQueCumplen = new List<GameObject>();

			if (efectoInvocacion.parametroZona == "MAZO")
				opcionesDesdeZona.AddRange(fisica.TraerCartasEnMazo(jugador));
			if (efectoInvocacion.parametroZona == "MANO")
				opcionesDesdeZona.AddRange(fisica.TraerCartasEnMano(jugador));
			if (efectoInvocacion.parametroZona == "DESCARTE")
				opcionesDesdeZona.AddRange(fisica.TraerCartasEnCementerio(jugador));

			CondicionCarta condicion = new CondicionClase("CRIATURA");
			foreach (GameObject opcion in condicion.CumpleLista(opcionesDesdeZona)) {
				if (EmblemaVinculo.CumpleRestricciones(aura, opcion))
					opcionesQueCumplen.Add(opcion);
			}

			if (opcionesQueCumplen.Count == 0)
				return;

			fisica.panel.SetActive(true);
			PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
			panel.Iniciar(
				opcionesQueCumplen,
				new SeleccionarCriaturaVinculada(jugador, infoAura.gameObject, camposLibres[0], camposLibres[1]),
				texto: "Selecciona una carta para vincular. Sera invocada al campo."
			);
		}


		public static void Jugar(GameObject carta, GameObject lugar) {

			EmblemaJuegoSeleccionar.Deseleccionar();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			if (info.original.clase == "TRAMPA") {
				EmblemaJuegoSeleccionar.Seleccionar(carta);
				EmblemaJuegoActivar.Jugar(info.controlador, carta, lugar);
			}

			if (info.original.clase == "HECHIZO") {
				EmblemaJuegoSeleccionar.Seleccionar(carta);
				EmblemaJuegoActivarHechizo.Jugar(info.controlador, lugar);
			}

			if (info.original.clase == "CRIATURA") {
				EmblemaJuegoSeleccionar.Seleccionar(carta);
				EmblemaInvocacionNormal.Invocar(info.controlador, lugar);
			}

			if (info.original.clase == "EQUIPO") {
				EmblemaJuegoSeleccionar.Seleccionar(carta);
				EmblemaJuegoActivar.Jugar(info.controlador, carta, lugar);
			}

			if (info.original.clase == "VACIO") {
				EmblemaJuegoSeleccionar.Seleccionar(carta);
				EmblemaJuegoActivar.Jugar(info.controlador, carta, lugar);
			}

			if (info.original.clase == "AURA") {
				JugarAura(carta, lugar);
			}

		}


	}

}