using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public abstract class CondicionBool : CondicionCarta {
		
		private readonly bool valor;
		
		public CondicionBool(bool valor = true) {
			this.valor = valor;
			precondiciones = new List<CondicionCarta>();
		}


		public override bool Cumple(GameObject carta) {
			if (!CumplePrecondiciones(carta))
				return false;
			return GetValor(carta) == valor;
		}


		public abstract bool GetValor(GameObject carta);


	}

}