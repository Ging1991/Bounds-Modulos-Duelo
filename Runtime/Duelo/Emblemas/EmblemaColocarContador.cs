using Bounds.Duelo.Carta;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Visuales;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaColocarContador {

		public static void Colocar(GameObject carta, string tipo, int cantidad) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			cartaInfo.ColocarContador(tipo, cantidad);
			if (tipo == "debilidad") {
				carta.GetComponentInChildren<GestorVisual>().Animar("SANGRE");
			}

			if (cartaInfo.TraerContadores("veneno") >= cartaInfo.original.nivel) {
				EmblemaEfectos.Activar(new EfectoSobreCarta(carta, new SubDestruir(), carta));
				return;
			}

			CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();

			if (cartaEfecto.TieneClave("MISION_N")) {
				EfectoBD efecto = cartaEfecto.GetEfecto("MISION_N");
				if (cartaInfo.TraerContadores("mision") == efecto.parametroN) {
					if (cartaEfecto.TieneClave("MISION_LP_CUMPLIDA")) {
						EmblemaEfectos.Activar(
							new EfectoSobreJugador(carta, cartaInfo.controlador, new SubModificarLP(1000))
						);
					}
				}
			}
		}

	}

}