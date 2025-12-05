using Bounds.Duelo.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubBarajar : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;

			fisica.EnviarHaciaMazo(carta, cartaInfo.propietario);

			if (cartaInfo.propietario == 1) {
				carta.GetComponent<CartaGeneral>().ColocarBocaArriba();
			}
			if (cartaInfo.propietario == 2) {
				carta.GetComponent<CartaGeneral>().ColocarBocaAbajo();
			}
		}

	}

}