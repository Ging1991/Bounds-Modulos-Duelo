using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubColocarContador : ISubSobreCarta {

		private readonly string tipo;
		private readonly int cantidad;

		public SubColocarContador(string tipo, int cantidad) {
			this.tipo = tipo;
			this.cantidad = cantidad;
		}


		public void AplicarEfecto(GameObject carta) {
			EmblemaColocarContador.Colocar(carta, tipo, cantidad);
		}


	}

}