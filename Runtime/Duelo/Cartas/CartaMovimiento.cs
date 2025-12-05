using Bounds.Cartas;
using Bounds.Modulos.Cartas;
using Ging1991.Animaciones.Efectos;
using Ging1991.Relojes;
using UnityEngine;

namespace Bounds.Duelo.Carta {

	public class CartaMovimiento : MonoBehaviour {

		public bool estaGirado = false;
		public int velocidad;
		public Rotar rotarGirar;
		public Rotar rotarEnderezar;


		public void Desplazar(Vector3 direccion, IEjecutable accion = null) {
			GetComponent<MoverVelocidad>().Inicializar(transform.localPosition, direccion, velocidad, accionFinal: accion);
		}


		public void Girar() {
			if (!estaGirado) {
				rotarGirar.Inicializar();
				estaGirado = true;
			}
		}


		public void Enderezar() {
			if (estaGirado) {
				rotarEnderezar.Inicializar();
				estaGirado = false;
			}
		}


	}

}