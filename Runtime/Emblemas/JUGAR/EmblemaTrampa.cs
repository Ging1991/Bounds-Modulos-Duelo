using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaTrampa {

		public static void ActivarTrampa(GameObject trampa) {
			trampa.GetComponent<CartaGeneral>().ColocarBocaArriba();
			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxEspadas");
			EmblemaPadre.ActivarEfectosDeActivacion(trampa);
		}

		public static void ActivarRobarCartas(GameObject trampa, int jugador, int cantidad) {
			CartaInfo info = trampa.GetComponent<CartaInfo>();
			ActivarTrampa(trampa);

			if (info.original.datoTrampa.tipo == "ROBO_X_ROBO_N") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, info.controlador, new SubRobar(info.original.datoTrampa.cantidad)));
			}

		}

		public static void ActivarDeclararAtaque(GameObject trampa, GameObject atacante, GameObject atacado) {
			ActivarTrampa(trampa);
			CartaInfo info = trampa.GetComponent<CartaInfo>();

			if (info.original.datoTrampa.tipo == "RUGIDO") {
				EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubColocarContador("debilidad", 4), atacante));
			}

		}


	}

}