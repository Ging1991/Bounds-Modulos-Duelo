using Bounds.Duelo.Carta;
using UnityEngine;
using Ging1991.Animaciones;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblema {

	public class EmblemaDescartarCarta {


		public static void Descartar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			fisica.EnviarHaciaDescarte(carta, info.propietario);
			//			carta.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
		}


	}

}