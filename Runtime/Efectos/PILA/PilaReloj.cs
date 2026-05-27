using Ging1991.Core.Interfaces;
using Ging1991.Relojes;
using UnityEngine;

namespace Bounds.Duelo.Pila {

	public class PilaReloj : MonoBehaviour, IEjecutable {

		private bool estaPausado = true;

		public void Pausar() {
			if (!estaPausado) {
				GetComponent<Reloj>().Desuscribir(this);
				estaPausado = true;
			}
		}

		public void Reanudar() {
			if (estaPausado) {
				GetComponent<Reloj>().segundos.Suscribir(this);
				GetComponent<Reloj>().Reiniciar();
				estaPausado = false;
			}
		}

		public void Ejecutar() {
			GetComponent<PilaEfectos>().Resolver();
		}

	}

}