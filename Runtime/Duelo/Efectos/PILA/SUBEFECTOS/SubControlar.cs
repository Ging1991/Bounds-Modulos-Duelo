using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubControlar : ISubSobreCarta {

		private int jugador;

		public SubControlar(int jugador) {
			this.jugador = jugador;
		}

		public void AplicarEfecto(GameObject carta) {
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject campo = buscador.buscarCampoLibre(jugador);
			if (campo != null) {
				EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
				Fisica fisica = conocimiento.traerFisica();
				fisica.EnviarHaciaCampo(jugador, carta, campo);
				CartaInfo info = carta.GetComponent<CartaInfo>();
				info.controlador = jugador;
				GameObject grupo = GameObject.Find($"Cartas{jugador}");
				carta.transform.SetParent(grupo.transform);
			}
		}


	}

}