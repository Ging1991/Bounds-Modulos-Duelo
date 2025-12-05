using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas.Vinculos;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Jugar {

	public class EmblemaJuegoJugarAura {

		public static void Jugar(int jugador, GameObject lugar, bool fueraDeFase = false) {


			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			GameObject aura = Seleccionador.Instancia.cartaParaJugar;
			GameObject criatura = Seleccionador.Instancia.cartaParaVincular;
			Fisica fisica = conocimiento.traerFisica();

			Campo campo = lugar.GetComponent<Campo>();
			if (campo.EstaOcupado())
				return;

			if (!fisica.TraerCampos(jugador).Contains(lugar))
				return;

			if (aura == null || criatura == null)
				return;

			if (jugador != turnos.jugadorActivo && !fueraDeFase)
				return;

			CartaInfo infoCriatura = criatura.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = criatura.GetComponent<CartaTipo>();
			CartaMovimiento movimientoCriatura = criatura.GetComponent<CartaMovimiento>();
			CartaInfo infoAura = aura.GetComponent<CartaInfo>();

			if (!EmblemaVinculo.CumpleRestricciones(aura, criatura)) {
				ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxRebote");
				EmblemaJuegoSeleccionar.Deseleccionar();
				return;
			}

			ControlDuelo.Instancia.GetComponent<GestorDeSonidos>().ReproducirSonido("FxLanzar");

			fisica.EnviarHaciaCampo(jugador, aura, lugar);
			EmblemaJuegoSeleccionar.Deseleccionar();

			CartaArrastrar.jugado = true;
			infoAura.criaturaEquipada = criatura;

			EmblemaVinculo.Vincular(aura, criatura);

			//Visor visor = GameObject.Find("Visor").GetComponent<Visor>();
			Estadisticas.Instancia.ModificarValor($"AURA_{jugador}_jugadas", 1);
			//visor.Mostrar(criatura);
			ControlDuelo duelo = conocimiento.traerDuelo();
			duelo.HabilitarInvocacionPerfecta();
			EmblemaPadre.ActivarEfectosDeActivacion(aura);

			// ************************************************ EFECTOS *******************************************************
			List<GameObject> cartasEnCampo = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));
			int adversario = JugadorDuelo.Adversario(jugador);
			PilaEfectos pila = GameObject.Find("Pila").GetComponent<PilaEfectos>();

			foreach (GameObject cartaEnCampo in cartasEnCampo) {
				CartaInfo infoCartaEnCampo = cartaEnCampo.GetComponent<CartaInfo>();
				CartaEfecto cartaEfectoEncampo = cartaEnCampo.GetComponent<CartaEfecto>();

				if (cartaEfectoEncampo.TieneClave("APRENDER_C")) {
					EfectoBD efecto = cartaEfectoEncampo.GetEfecto("APRENDER_C");
					if (efecto.parametroClase == "AURA")
						EmblemaEfectos.Activar(new EfectoSobreJugador(cartaEnCampo, jugador, new SubRobar(1)));

				}
				if (cartaEfectoEncampo.TieneClave("ENSEÑAR_C")) {
					EfectoBD efecto = cartaEfectoEncampo.GetEfecto("ENSEÑAR_C");
					if (efecto.parametroClase == "AURA") {
						EfectoBase efectoBase = new EfectoSobreJugador(cartaEnCampo, adversario, new SubModificarLP(-500));
						efectoBase.AgregarEtiqueta("VENENO");
						EmblemaEfectos.Activar(efectoBase);
					}
				}
			}

		}


	}


}