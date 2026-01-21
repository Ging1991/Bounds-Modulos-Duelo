using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Ging1991.Animaciones;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaTutor : EmblemaPadre {

		public static void Agregar(int jugador, GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			//if (!fisica.TraerCartasEnMazo(jugador).Contains(carta))
			//return;

			if (fisica.TraerCartasEnMano(jugador).Count > 4)
				return;

			//carta.GetComponentInChildren<EfectoVisual>().Animar("REVITALIZAR");
			info.restablecer();
			fisica.EnviarHaciaMano(carta, jugador);

			carta.GetComponent<CartaMovimiento>().Enderezar();

			CartaGeneral componente = carta.GetComponent<CartaGeneral>();
			if (jugador == 1)
				componente.ColocarBocaArriba();
			else
				componente.ColocarBocaAbajo();
		}


	}

}