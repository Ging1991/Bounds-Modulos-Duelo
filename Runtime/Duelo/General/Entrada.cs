using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Infraestructura.Visores;
using Bounds.Cartas;
using Bounds.Duelo.Emblemas.Jugar;
using System.Collections.Generic;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Visor;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Utiles {

	public class Entrada : ICartaObservador {

		private static Entrada instancia;
		public bool panelPerfectoVisible = false;

		private Entrada() { }

		public static Entrada GetInstancia() {
			if (instancia == null)
				instancia = new Entrada();
			return instancia;
		}


		public void PresionarCarta(int jugador, GameObject carta) {

			if (CartaArrastrar.carta != null)
				return;

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();

			// SELECCIONA DEL CAMPO CRIATURA PARA materiea
			EmblemaSeleccionMaterial material = EmblemaSeleccionMaterial.GetInstancia();
			material.Seleccionar(carta);

			// La muestro en el visor
			VisorDuelo visor = conocimiento.traerVisor();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaGeneral cartaGeneral = carta.GetComponent<CartaGeneral>();
			if (jugador == 1 && (info.controlador == 1 || cartaGeneral.bocaArriba)) {
				visor.GetComponentInChildren<VisorGeneral>().ColocarBocaAbajo(false, null);
				visor.Mostrar(carta);
			}

			if (jugador == 1 && info.controlador == 2 && !cartaGeneral.bocaArriba) {
				visor.GetComponentInChildren<VisorGeneral>().ColocarBocaAbajo(true, carta.GetComponentInChildren<CartaFisica>().GetReverso());
			}

			if (panelPerfectoVisible || PanelZona.getInstancia() != null)
				return;

			EmblemaActivarHabilidad habilidad = EmblemaActivarHabilidad.GetInstancia();
			habilidad.Activar(carta);

			// Selecciona la carta de su mano para jugarla
			EmblemaJuegoSeleccionar.Seleccionar(carta);

			// SELECCIONA DEL CAMPO EQUIPO PARA EQUIPAR
			EmblemaSeleccionarEquipar equipar = EmblemaSeleccionarEquipar.GetInstancia();
			equipar.Seleccionar(turnos.jugadorActivo, carta);

			// SELECCIONA DEL CAMPO CRIATURA PARA EQUIPAR
			EmblemaEquipar equipar2 = EmblemaEquipar.GetInstancia();
			equipar2.Seleccionar(carta);

			// SELECCIONA DEL CAMPO CRIATURA PARA aurear
			//Seleccionador.Instancia.SeleccionarParaJugar(carta);
			EmblemaJuegoSeleccionar.SeleccionarParaVincular(carta);

			// SELECCIONA DEL CAMPO CARTA ATACANTE
			EmblemaSeleccionarAtacante.Seleccionar(turnos.jugadorActivo, carta);

			// SELECCIONA DEL CAMPO CARTA ATACADA
			EmblemaSeleccionarAtacado.Seleccionar(carta);
		}


		public void PresionarCampo(GameObject campo) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			Seleccionador seleccionador = Seleccionador.Instancia;
			List<string> lista = new() { "TRAMPA", "EQUIPO", "VACIO", "MISION" };

			if (panelPerfectoVisible || PanelZona.getInstancia() != null)
				return;

			if (seleccionador.cartaParaJugar == null)
				return;

			if (lista.Contains(seleccionador.cartaParaJugar.GetComponent<CartaInfo>().original.clase)) {
				EmblemaJuegoActivar.Jugar(turnos.jugadorActivo, seleccionador.cartaParaJugar, campo);

			}
			else {

				// SELECCIONA UN CAMPO PARA INVOCACION NORMAL
				EmblemaInvocacionNormal.Invocar(turnos.jugadorActivo, campo);

				EmblemaJuegoActivarHechizo.Jugar(turnos.jugadorActivo, campo);

				// SELECCIONA DEL CAMPO PARA LANZAR aura
				EmblemaJuegoJugarAura.Jugar(turnos.jugadorActivo, campo);
			}

		}


		public void PresionarBotonFase() {
			if (panelPerfectoVisible || PanelZona.getInstancia() != null)
				return;
			EmblemaTurnos turnos = GameObject.Find("EmblemaTurnos").GetComponent<EmblemaTurnos>();
			turnos.CambiarFase();
			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxEspadas");
		}


		public void PresionarBotonInvocacion() {
			Invocador invocador = GameObject.FindAnyObjectByType<Invocador>();
			invocador.IniciarInvocacionPerfecta();
		}

	}

}