using Ging1991.Core.Interfaces;
using Ging1991.Relojes;
using UnityEngine;

namespace Bounds.Duelo.CPU {

	public class CPUReloj : MonoBehaviour, IEjecutable {

		private Reloj reloj;
		private CPUJugador jugador;


		public void Inicializacion() {
			reloj = GetComponent<Reloj>();
			jugador = GetComponent<CPUJugador>();
			jugador.CargarAcciones();
		}


		public void ComenzarTurno() {
			reloj.segundos.Suscribir(this);
			jugador.ReiniciarAcciones();
		}


		public void TerminarTurno() {
			reloj.Desuscribir(this);
		}


		public void Ejecutar() {
			jugador.Jugar();
		}

	}

}