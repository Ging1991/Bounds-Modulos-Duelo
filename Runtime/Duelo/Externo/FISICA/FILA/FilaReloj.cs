using Ging1991.Core.Interfaces;
using Ging1991.Relojes;
using UnityEngine;

namespace Bounds.Duelo.Fila {

	public class FilaReloj : MonoBehaviour, IEjecutable {

		private bool estaPausado = true;

		public void Pausar() {
			if (!estaPausado) {
				GetComponent<Reloj>().Desuscribir(this);
				estaPausado = true;
			}
		}

		public void Reanudar() {
			if (estaPausado) {
				GetComponent<Reloj>().Reiniciar();
				GetComponent<Reloj>().segundos.Suscribir(this);
				estaPausado = false;
			}
		}

		public void Ejecutar() {
			GetComponent<FilaFases>().Resolver();
		}

	}

}