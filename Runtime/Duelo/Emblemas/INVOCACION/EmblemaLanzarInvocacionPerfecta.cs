using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Condiciones;
using Ging1991.Core;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaInvocacionPerfecta : EmblemaPadre {


		public static void Invocar(int jugador, GameObject criatura, List<GameObject> materiales) {

			Fisica fisica = Fisica.Instancia;
			EmblemaEnviarMaterial.EnviarMateriales(materiales);

			CartaInfo info = criatura.GetComponent<CartaInfo>();
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject lugar = buscador.buscarCampoLibre(jugador);

			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxLanzar");

			EmblemaInvocacion.Invocar(jugador, criatura, lugar);
			EmblemaSeleccionInvocacionPerfecta.GetInstancia().Deseleccionar();

			if (info.controlador == 1) {
				fisica.panel.GetComponent<PanelCartas>().permanecer = false;
				fisica.panel.GetComponent<PanelCartas>().BotonCerrar();
				GameObject.FindAnyObjectByType<Invocador>().InvocacionCompletada();
				EmblemaSeleccionMaterial.GetInstancia().Deseleccionar();
			}

			string codigo = $"INVOCACION_PERFECTA_{jugador}_jugadas";
			EstadisticasSingleton.Instancia.ModificarValor(codigo, 1);

			ActivarVacios(criatura);
			ActivarHabilidades(criatura);
			ActivarTrampas(criatura);

			EstadoAura.RevisarEstado();

			ControlDuelo duelo = ControlDuelo.Instancia;
			duelo.HabilitarInvocacionPerfecta();
			GameObject.FindAnyObjectByType<Invocador>().InvocacionCompletada();
		}


		private static void ActivarHabilidades(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int adversario = Adversario(info.controlador);

			// ****************************   Habilidades propias de la carta  ****************************************
			/*
						if (info.habilidades.Contains("especial_zombi2")) {
							List<GameObject> zombis = new List<GameObject>(fisica.TraerCartasEnCampo(info.controlador));
							zombis.AddRange(fisica.TraerCartasEnCampo(adversario));
							Condicion condicionZombi = new Condicion(tipoCarta:"CRIATURA", tipoCriatura:new List<string>{"zombi"});
							zombis = condicionZombi.CumpleLista(zombis);
							info.colocarContador("poder", zombis.Count*2);
						}

						if (info.habilidades.Contains("especial_carta")) {
							List<GameObject> cartasX = new List<GameObject>(fisica.TraerCartasEnCampo(1));
							cartasX.AddRange(fisica.TraerCartasEnCampo(2));
							info.colocarContador("poder", cartasX.Count);
						}

						if (info.habilidades.Contains("especial_angel")) {
							List<GameObject> angeles = new List<GameObject>(fisica.TraerCartasEnMateriales(1));
							angeles.AddRange(fisica.TraerCartasEnMateriales(2));
							Condicion condicionAngel = new Condicion(tipoCarta:"CRIATURA", tipoCriatura:new List<string>{"angel"});
							angeles = condicionAngel.CumpleLista(angeles);
							info.colocarContador("poder", angeles.Count);
						}*/

		}


		private static void ActivarVacios(GameObject criatura) {
			int adversario = Adversario(criatura.GetComponent<CartaInfo>().controlador);

			foreach (GameObject vacio in new SubCartasControladas(0, new CondicionClase("VACIO")).Generar()) {
				CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();
				if (infoVacio.original.datoVacio.tipo == "PERFECCION") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, adversario, new SubModificarLP(-500), "EXPLOSION"));
				}
			}

		}


		private static void ActivarTrampas(GameObject criatura) {

			int controlador = criatura.GetComponent<CartaInfo>().controlador;
			int adversario = Adversario(controlador);
			Fisica fisica = Fisica.Instancia;
			CondicionMultiple condicionTrampa = new(CondicionMultiple.Tipo.Y);
			condicionTrampa.AgregarCondicion(new CondicionClase("TRAMPA"));
			condicionTrampa.AgregarCondicion(new CondicionEstaBocaAbajo());

			List<GameObject> cartasDelAdversario = new List<GameObject>(fisica.TraerCartasEnCampo(adversario));
			CartaInfo infoCriatura = criatura.GetComponent<CartaInfo>();

			foreach (GameObject trampa in condicionTrampa.CumpleLista(cartasDelAdversario)) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
				CartaGeneral generalTrampa = trampa.GetComponent<CartaGeneral>();

				if (infoTrampa.original.datoTrampa.tipo == "INVOCACION_PERFECTA_DESTRUYE") {
					generalTrampa.ColocarBocaArriba();
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criatura));
				}

			}
		}


	}

}