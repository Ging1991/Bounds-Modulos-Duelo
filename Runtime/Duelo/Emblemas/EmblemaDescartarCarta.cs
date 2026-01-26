using Bounds.Duelo.Carta;
using UnityEngine;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Visuales;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblema {

	public class EmblemaDescartarCarta {

		public static void Descartar(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			fisica.EnviarHaciaDescarte(carta, info.propietario);
			carta.GetComponentInChildren<GestorVisual>().Animar("VENENO");
		}

	}

}