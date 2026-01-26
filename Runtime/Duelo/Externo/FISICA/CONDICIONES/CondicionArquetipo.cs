using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionArquetipo : CondicionCarta {

		protected readonly string valor;

		public CondicionArquetipo(string valor) {
			this.valor = valor;
		}


		public override bool Cumple(GameObject carta) {
			string nombre = carta.GetComponent<CartaInfo>().original.nombre;
			return nombre.ToUpper().Contains(valor.ToUpper());
		}


	}

}