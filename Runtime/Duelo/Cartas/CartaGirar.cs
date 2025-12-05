using Bounds.Modulos.Cartas;
using Ging1991.Relojes;
using UnityEngine;

namespace Bounds.Cartas.Transformaciones {

	public class CartaGirar : IEjecutable {

		private readonly GameObject carta;
		private readonly bool enderezar;

		public CartaGirar(GameObject carta, bool enderezar = false) {
			this.carta = carta;
			this.enderezar = enderezar;
		}

		public void Ejecutar() {/*
			if (enderezar)
				carta.GetComponent<CartaFisica>().Enderezar();
			else
				carta.GetComponent<CartaFisica>().Girar();*/
		}
	}

}