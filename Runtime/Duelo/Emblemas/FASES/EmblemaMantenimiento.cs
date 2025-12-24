using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Fases {

	public class EmblemaMantenimiento {

		public static void ActivarEfectos(int jugador) {

			Fisica fisica = Fisica.Instancia;
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));

			foreach (GameObject carta in cartas) {
				CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();

				if (cartaEfecto.TieneClave("PAGO_SANGRE")) {
					EmblemaEfectos.Activar(new EfectoSobreCarta(carta, new SubPagoEnSangre(), carta));
				}
			}

			ActivarVacios(jugador);
		}


		private static void ActivarVacios(int jugador) {

			CondicionClase condicionVacio = new(clase: "VACIO");
			CondicionMultiple condicionFichaPez = new(CondicionMultiple.Tipo.Y);
			condicionFichaPez.AgregarCondicion(new CondicionJugador(controlador: jugador));
			condicionFichaPez.AgregarCondicion(new CondicionCartaID(cartaID: 146));

			List<GameObject> cartasEnCampo = new SubCartasControladas(0).Generar();
			bool tieneFichas = condicionFichaPez.CumpleLista(cartasEnCampo).Count > 0;

			foreach (GameObject vacio in condicionVacio.CumpleLista(cartasEnCampo)) {
				CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();

				if (infoVacio.original.datoVacio.tipo == "OCEANO" && !tieneFichas) {
					EmblemaEfectos.Activar(new EfectoCrearFicha(vacio, jugador, 146, 1));
				}
				if (infoVacio.original.datoVacio.tipo == "CEMENTERIO" && !tieneFichas) {
					EmblemaEfectos.Activar(new EfectoCrearFicha(vacio, jugador, 586, 1));
				}
				if (infoVacio.original.datoVacio.tipo == "DIAMANTE") {
					EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, jugador, new SubRobar(1), "ROBAR"));
				}
			}

		}


	}

}