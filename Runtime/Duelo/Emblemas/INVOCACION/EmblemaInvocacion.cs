using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Cartas;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaInvocacion : EmblemaPadre {

		public static void Invocar(int jugador, GameObject carta, GameObject lugar) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaGeneral componente = carta.GetComponent<CartaGeneral>();
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();

			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxLanzar");
			fisica.EnviarHaciaCampo(jugador, carta, lugar);
			componente.ColocarBocaArriba();
			movimiento.Girar();

			ActivarEfectosDeVacio(jugador, carta);
			ActivarHabilidades(carta);
			ActivarEfectosDeActivacion(carta);
			ActivarTrampas(carta);
			conocimiento.traerDuelo().HabilitarInvocacionPerfecta();
			//carta.GetComponentInChildren<CartaFisica>().Alejar();
		}


		private static void ActivarHabilidades(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int adversario = JugadorDuelo.Adversario(info.controlador);


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
							Debug.Log("angeles en campo: " + angeles.Count);
							info.colocarContador("poder", angeles.Count);
						}*/

		}


		private static void ActivarEfectosDeVacio(int jugador, GameObject carta) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int adversario = Adversario(jugador);

			List<GameObject> cartasEnCampo = new SubCartasControladas(0, new CondicionClase("VACIO")).Generar();
			foreach (var vacio in cartasEnCampo) {
				CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();
				if (infoVacio.original.datoVacio.tipo == "PERPENDICULAR" && info.original.datoCriatura.perfeccion == "VECTOR") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, adversario, new SubModificarLP(-500), "EXPLOSION"));
				}
			}

		}


		private static void ActivarTrampas(GameObject criatura) {

			int controlador = criatura.GetComponent<CartaInfo>().controlador;
			int adversario = Adversario(controlador);
			CartaInfo infoCriatura = criatura.GetComponent<CartaInfo>();

			foreach (GameObject trampa in TraerTrampasBocaAbajo(adversario)) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();

				if (infoCriatura.original.datoCriatura.perfeccion == "MAGICO" && infoTrampa.original.datoTrampa.tipo == "DESTRUYE_PRISMA") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criatura));
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, adversario, new SubRobar(1)));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "INVOCA_FUSION_DAÑO" && infoCriatura.original.datoCriatura.perfeccion == "FUSION") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EfectoBase efectoBase = new EfectoSobreJugador(trampa, controlador, new SubModificarLP(-1000));
					efectoBase.AgregarEtiqueta("VENENO");
					EmblemaEfectos.Activar(efectoBase);
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "ANTI_FUSION" && infoCriatura.original.datoCriatura.perfeccion == "FUSION") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criatura));
					break;
				}

			}

		}

	}


}