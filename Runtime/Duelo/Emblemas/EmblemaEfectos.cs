using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaEfectos {


		public static void Activar(IEfecto efecto) {
			PilaEfectos pila = Object.FindAnyObjectByType<PilaEfectos>();
			pila.Agregar(efecto);
			ActivarTrampas(efecto);
		}


		public static void ActivarTrampas(IEfecto efecto) {

			int jugador = efecto.GetFuente().GetComponent<CartaInfo>().controlador;
			int adversario = EmblemaPadre.Adversario(jugador);

			foreach (var trampa in EmblemaPadre.TraerTrampasBocaAbajo(jugador)) {
				CartaGeneral trampaGeneral = trampa.GetComponent<CartaGeneral>();
				if (trampa.GetComponent<CartaInfo>().original.datoTrampa.tipo == "EXPLOSION2" && efecto.GetEtiquetas().Contains("EXPLOSION")) {
					trampaGeneral.ColocarBocaArriba();
					EfectoBase efectoBase = new EfectoSobreJugador(trampa, adversario, new SubModificarLP(-500));
					efectoBase.AgregarEtiqueta("EXPLOSION");
					Activar(efectoBase);
					break;
				}
			}

			foreach (var trampa in EmblemaPadre.TraerTrampasBocaAbajo(adversario)) {
				CartaGeneral trampaGeneral = trampa.GetComponent<CartaGeneral>();
				if (trampa.GetComponent<CartaInfo>().original.datoTrampa.tipo == "CANCELAR_EFECTO") {
					trampaGeneral.ColocarBocaArriba();
					Activar(new EfectoCancelarEfecto(trampa, efecto));
					break;
				}
			}

		}


		public static void Cancelar(IEfecto efecto) {
			PilaEfectos pila = Object.FindAnyObjectByType<PilaEfectos>();
			pila.Remover(efecto);
		}


	}

}