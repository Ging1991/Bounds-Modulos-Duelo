using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public abstract class CondicionEntero : CondicionCarta {
		
		private readonly int valor;
		private readonly List<int> valores;
		private readonly int minimo;
		private readonly int maximo;
		
		public CondicionEntero(int valor = -1, List<int> valores = null, int minimo = -1, int maximo = -1) {
			this.valor = valor;
			this.minimo = minimo;
			this.maximo = maximo;
			this.valores = valores ?? new List<int>();
			precondiciones = new List<CondicionCarta>();
		}


		public override bool Cumple(GameObject carta) {
			
			if (!CumplePrecondiciones(carta))
				return false;

			Acumulador acumulador = new Acumulador();
			acumulador.AgregarVerificacion(valor != -1, GetValor(carta) == valor);
			acumulador.AgregarVerificacion(minimo != -1, GetValor(carta) >= minimo);
			acumulador.AgregarVerificacion(maximo != -1, GetValor(carta) <= maximo);
			acumulador.AgregarVerificacion(valores.Count > 0, valores.Contains(GetValor(carta)));
			return acumulador.valor;
		}


		public abstract int GetValor(GameObject carta);


	}

}