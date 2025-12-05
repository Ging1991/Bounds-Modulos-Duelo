using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Ging1991.Animaciones;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaRecuperar : EmblemaPadre {

		public static void Levantar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			if (!fisica.TraerCartasEnCementerio(1).Contains(carta) && !fisica.TraerCartasEnCementerio(2).Contains(carta))
				return;

			if (fisica.TraerCartasEnMano(info.propietario).Count > 4)
				return;

			//carta.GetComponentInChildren<EfectoVisual>().Animar("NUBE");
			fisica.EnviarHaciaMano(carta, info.propietario);
			info.restablecer();

			carta.GetComponent<CartaMovimiento>().Enderezar();

			CartaGeneral componente = carta.GetComponent<CartaGeneral>();
			if (info.controlador == 1)
				componente.ColocarBocaArriba();
			else
				componente.ColocarBocaAbajo();
		}


	}

}