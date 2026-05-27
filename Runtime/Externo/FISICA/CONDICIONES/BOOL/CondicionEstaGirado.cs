using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionEstaGirado : CondicionBool {

		public CondicionEstaGirado(bool estaAbajo = true) : base(valor: estaAbajo) { }


		public override bool GetValor(GameObject carta) {
			return carta.GetComponent<CartaMovimiento>().estaGirado;
		}


	}

}