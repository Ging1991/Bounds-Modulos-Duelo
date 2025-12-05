using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.CPU;
using Bounds.Duelo.Fila;
using Bounds.Duelo.Fila.Fases;
using Bounds.Duelo.Emblemas.Jugar;
using Ging1991.Core;
using Ging1991.Interfaces;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaTurnos : MonoBehaviour {

		public int jugadorActivo = 1;
		public enum Fase {
			FASE_DE_BATALLA,
			FASE_PRINCIPAL,
			FASE_FINAL,
			FASE_MANTENIMIENTO
		}
		public Fase fase;
		public int turnos = 1;


		public void SetFase(Fase fase) {
			this.fase = fase;
			SetColorFase("FaseMantenimiento", Color.black, Color.gray, Color.black);
			SetColorFase("FasePrincipal", Color.black, Color.gray, Color.black);
			SetColorFase("FaseBatalla", Color.black, Color.gray, Color.black);
			SetColorFase("FaseFinal", Color.black, Color.gray, Color.black);

			Color colorActivo = (jugadorActivo == 1) ? Color.green : Color.red;
			if (fase == Fase.FASE_MANTENIMIENTO) {
				SetColorFase("FaseMantenimiento", Color.black, colorActivo, Color.black);
				SetBotonAvance(colorActivo, "ESPERAR: Mantenimiento", false);
			}
			if (fase == Fase.FASE_PRINCIPAL) {
				SetColorFase("FasePrincipal", Color.black, colorActivo, Color.black);
				SetBotonAvance(colorActivo, "CONTINUAR: Ir a batalla", jugadorActivo == 1);
			}
			if (fase == Fase.FASE_DE_BATALLA) {
				SetColorFase("FaseBatalla", Color.black, colorActivo, Color.black);
				SetBotonAvance(colorActivo, "CONTINUAR: Terminar turno", jugadorActivo == 1);
			}
			if (fase == Fase.FASE_FINAL) {
				SetColorFase("FaseFinal", Color.black, colorActivo, Color.black);
				SetBotonAvance(colorActivo, "ESPERAR: Fin de turno", false);
			}
		}


		public void Deseleccionar() {
			EmblemaSeleccionarEquipar equipar = EmblemaSeleccionarEquipar.GetInstancia();
			EmblemaSeleccionMaterial material = EmblemaSeleccionMaterial.GetInstancia();
			EmblemaSeleccionInvocacionPerfecta perfecta = EmblemaSeleccionInvocacionPerfecta.GetInstancia();

			equipar.Deseleccionar();
			material.Deseleccionar();
			perfecta.Deseleccionar();
			EmblemaJuegoSeleccionar.Deseleccionar();
		}


		public void CambiarFase() {

			if (CuadroFinalizarDuelo.ExistenCuadros())
				return;

			Deseleccionar();

			if (fase == Fase.FASE_MANTENIMIENTO) {
				SetFase(Fase.FASE_PRINCIPAL);
				return;
			}

			if (fase == Fase.FASE_PRINCIPAL) {
				SetFase(Fase.FASE_DE_BATALLA);

				MarcarPotencialesAtaques();
				MarcarMuro();
				Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
				instanciador.CrearTextoCreciente("¡Fase de batalla!");
				return;
			}

			if (fase == Fase.FASE_DE_BATALLA) {
				SetFase(Fase.FASE_FINAL);
				FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
				fila.Agregar(new FaseFinal(jugadorActivo));
				return;
			}

			if (fase == Fase.FASE_FINAL) {
				SetFase(Fase.FASE_MANTENIMIENTO);
				return;
			}

		}


		private void SetColorFase(string nombre, Color colorTexto, Color colorRelleno, Color colorBorde) {
			MarcoConTexto fase = GameObject.Find(nombre).GetComponent<MarcoConTexto>();
			fase.SetColorTexto(colorTexto);
			fase.SetColorBorde(colorBorde);
			fase.SetColorRelleno(colorRelleno);
		}


		public void IniciarTurno() {

			FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
			fila.Agregar(new FaseMantenimiento(jugadorActivo));
			SetFase(Fase.FASE_MANTENIMIENTO);

			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();

			if (jugadorActivo == 2) {
				CPUReloj cpuReloj = GameObject.Find("CPU").GetComponent<CPUReloj>();
				cpuReloj.ComenzarTurno();
				instanciador.CrearTextoCreciente("¡Turno del oponente!");
				GestorDeSonidos gestor = FindAnyObjectByType<GestorDeSonidos>();
				gestor.ReproducirSonido("FxAdquisicion");
			}
			else {
				instanciador.CrearTextoCreciente("¡Es tu turno!");
			}

			Text contador = GameObject.Find("ContadorTurnos").GetComponentInChildren<Text>();
			contador.text = "Turno " + turnos;
		}


		public void SetBotonAvance(Color color, string texto, bool estaActivo) {
			GameObject boton = GameObject.Find("BotonAvanzar").transform.GetChild(0).gameObject;
			boton.GetComponentInChildren<Text>().text = texto;
			boton.GetComponent<Image>().color = color;
			boton.GetComponent<Button>().interactable = estaActivo;
		}


		private void MarcarPotencialesAtaques() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(jugadorActivo));
			foreach (GameObject carta in cartas) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();

				if (info.original.clase == "CRIATURA" && /*!info.tieneHabilidad("encadenado") &&*/!movimiento.estaGirado) {
					CartaFX fX = carta.GetComponent<CartaFX>();
					fX.PotencialAtacante(true);
				}
			}

		}


		private void MarcarMuro() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(JugadorDuelo.Adversario(jugadorActivo)));
			foreach (GameObject carta in cartas) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				if (info.GetComponent<CartaEfecto>().TieneClave("MURO")) {
					CartaFX fX = carta.GetComponent<CartaFX>();
					fX.EfectoMuro(true);
				}
			}

		}


		public static void DesmarcarPotencialesAtaques(int jugadorActivo) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(jugadorActivo));
			foreach (GameObject carta in cartas) {
				CartaFX fX = carta.GetComponent<CartaFX>();
				fX.PotencialAtacante(false);
			}
		}


		public static void DesmarcarMuro(int jugador) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(JugadorDuelo.Adversario(jugador)));
			foreach (GameObject carta in cartas) {
				CartaFX fX = carta.GetComponent<CartaFX>();
				fX.EfectoMuro(false);
			}
		}


		public static EmblemaTurnos GetInstancia() {
			GameObject instancia = GameObject.Find("EmblemaTurnos");
			return instancia.GetComponent<EmblemaTurnos>();
		}


	}

}