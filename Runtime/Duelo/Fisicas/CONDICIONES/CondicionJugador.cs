using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionJugador : CondicionCarta {
		
		private readonly int propietario;
		private readonly int controlador;

		public CondicionJugador(int propietario = -1, int controlador = -1) {
			this.propietario = propietario;
			this.controlador = controlador;
		}


		public override bool Cumple(GameObject carta) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			Acumulador acumulador = new Acumulador();
			acumulador.AgregarVerificacion(propietario != -1, info.propietario == propietario);
			acumulador.AgregarVerificacion(controlador != -1, info.controlador == controlador);
			return acumulador.valor;
		}


	}

}