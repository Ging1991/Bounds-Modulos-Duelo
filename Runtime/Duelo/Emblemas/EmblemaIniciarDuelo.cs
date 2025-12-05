using UnityEngine;

namespace Bounds.Duelo.Emblema {

	public class EmblemaIniciarDuelo {


		public static void IniciarMulligan() {
			Mulligan mulligan = GameObject.FindAnyObjectByType<Mulligan>();
			mulligan.Iniciar();
		}


		public static void SetAvatar(int jugador, Sprite personaje) {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.miniatura.sprite = personaje;
		}


		public static void SetLP(int jugador, int LP) {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.SetVida(LP);
		}


		public static void SetNombre(int jugador, string nombre) {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.SetNombre(nombre);
		}


	}

}