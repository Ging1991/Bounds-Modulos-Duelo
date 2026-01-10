using System.Collections.Generic;
using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionCriaturaPerfeccion : CondicionCadena {
		
		public CondicionCriaturaPerfeccion(string perfeccion = null, List<string> perfecciones = null) :
				base(valor:perfeccion, valores:perfecciones) {

			precondiciones.Add(new CondicionClase(clase:"CRIATURA"));
		}


		public override string GetValor(GameObject carta) {
			return carta.GetComponent<CartaInfo>().original.datoCriatura.perfeccion;
		}

	}

}