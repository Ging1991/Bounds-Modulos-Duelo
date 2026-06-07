using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaTrampa {

		public static void ActivarTrampa(GameObject trampa) {
			trampa.GetComponent<CartaGeneral>().ColocarBocaArriba();
			EmblemaPadre.ActivarEfectosDeActivacion(trampa);
		}

		public static void ActivarRobarCartas(GameObject trampa, int jugador, int cantidad) {
			CartaInfo info = trampa.GetComponent<CartaInfo>();

			trampa.GetComponent<CartaGeneral>().ColocarBocaArriba();
			EmblemaPadre.ActivarEfectosDeActivacion(trampa);
			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxEspadas");

			if (info.original.datoTrampa.tipo == "ROBO_X_ROBO_N") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, info.controlador, new SubRobar(info.original.datoTrampa.cantidad)));
			}

		}


	}

}