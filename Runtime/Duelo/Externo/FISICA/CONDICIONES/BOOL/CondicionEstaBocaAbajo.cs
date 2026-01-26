using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionEstaBocaAbajo : CondicionBool {

		public CondicionEstaBocaAbajo(bool estaAbajo = true) : base(valor: estaAbajo) { }


		public override bool GetValor(GameObject carta) {
			return !carta.GetComponent<CartaGeneral>().bocaArriba;
		}


	}

}