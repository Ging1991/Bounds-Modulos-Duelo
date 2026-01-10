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
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Duelo.Efectos;

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
			ActivarHabilidades(jugador, carta);
			ActivarEfectosDeActivacion(carta);
			ActivarTrampas(carta);
			conocimiento.traerDuelo().HabilitarInvocacionPerfecta();
			//carta.GetComponentInChildren<CartaFisica>().Alejar();
		}


		private static void ActivarHabilidades(int jugador, GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int adversario = JugadorDuelo.Adversario(info.controlador);

			foreach (var otraCarta in new SubCartasControladas(jugador).Generar()) {
				if (otraCarta == carta) {
					continue;
				}
				if (otraCarta.GetComponent<CartaEfecto>().TieneClave("RECLUTADOR_PALADINFINITO") && info.original.nivel == 8) {
					EmblemaEfectos.Activar(new EfectoBarajarFicha(otraCarta, jugador, info.cartaID, 5));
				}
			}

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

				if (infoTrampa.original.datoTrampa.tipo == "DESPERTAR_PALADINFINITO") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoBusqueda(trampa, adversario, 1, new List<Zonas> { Zonas.MAZO }, new CondicionNivel(8)));
					EmblemaEfectos.Activar(new EfectoBarajarFicha(trampa, adversario, 593, 1));
					break;
				}

			}

		}

	}


}