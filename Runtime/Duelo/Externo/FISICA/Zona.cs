using UnityEngine;
using Bounds.Duelo.Carta;

namespace Bounds.Modulos.Duelo.Fisicas {

	public class Zona {

		protected Vector3 posicion;

		public Zona(Vector3 posicion) {
			this.posicion = posicion;
		}


		public bool Agregar(GameObject carta) {
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			movimiento.Desplazar(posicion);

			return true;
		}


	}

}