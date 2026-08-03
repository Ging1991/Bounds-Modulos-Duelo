using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;
using Bounds.Cartas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaDescarte {


		public static void EnviarDesdeCampo(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			fisica.EnviarHaciaDescarte(carta, info.propietario);
			info.restablecer();
			//cartaEfecto.Restablecer();

			CartaFisica componente = carta.GetComponentInChildren<CartaFisica>();
			componente.ColocarBocaArriba();
			//Emblema.ActivarEfectosDeDejarElCampo(carta);

		}


	}

}