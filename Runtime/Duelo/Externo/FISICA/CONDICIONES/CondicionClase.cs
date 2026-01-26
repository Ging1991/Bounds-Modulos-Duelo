using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionClase : CondicionCadena {


		public CondicionClase(string clase = null, List<string> clases = null) : base(valor: clase, valores: clases) { }


		public override string GetValor(GameObject carta) {
			return carta.GetComponent<CartaInfo>().original.clase;
		}


	}

}