using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public abstract class CondicionCadena : CondicionCarta {
		
		protected readonly string valor;
		protected readonly List<string> valores;
		

		public CondicionCadena(string valor = null, List<string> valores = null) {
			this.valor = valor;
			this.valores = valores ?? new List<string>();
			precondiciones = new List<CondicionCarta>();
		}


		public override bool Cumple(GameObject carta) {

			if (!CumplePrecondiciones(carta))
				return false;

			Acumulador acumulador = new Acumulador();
			acumulador.AgregarVerificacion(valor != null, GetValor(carta) == valor);
			acumulador.AgregarVerificacion(valores.Count > 0, valores.Contains(GetValor(carta)));
			return acumulador.valor;
		}


		public abstract string GetValor(GameObject carta);


	}

}