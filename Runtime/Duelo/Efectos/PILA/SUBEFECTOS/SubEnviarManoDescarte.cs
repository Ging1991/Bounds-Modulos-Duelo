using Bounds.Duelo.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubEnviarManoDescarte : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
			if (fisica.TraerCartasEnMano(cartaInfo.propietario).Count < 5) {
				fisica.EnviarHaciaMano(carta, cartaInfo.propietario);
				if (cartaInfo.propietario == 2) {
					carta.GetComponent<CartaGeneral>().ColocarBocaAbajo();
				}
			}
			else {
				fisica.EnviarHaciaDescarte(carta, cartaInfo.propietario);
			}
		}


	}

}