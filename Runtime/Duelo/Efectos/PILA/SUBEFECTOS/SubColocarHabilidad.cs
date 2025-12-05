using Bounds.Duelo.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubColocarHabilidad : ISubSobreCarta {

		private readonly EfectoBD efecto;

		public SubColocarHabilidad(EfectoBD efecto) {
			this.efecto = efecto;
		}


		public void AplicarEfecto(GameObject carta) {
			carta.GetComponent<CartaEfecto>().ColocarEfecto(efecto);
			if (efecto.clave == "ENVENENADO") {
				//carta.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
			}
		}


	}

}