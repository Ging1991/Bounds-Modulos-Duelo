using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionDefensa : CondicionEntero {


		public CondicionDefensa(int defensa = -1, List<int> defensas = null, int minimo = -1, int maximo = -1) :
			base(defensa, defensas, minimo, maximo) {

			precondiciones.Add(new CondicionClase(clases: new List<string>() { "CRIATURA", "EQUIPO" }));
		}


		public override int GetValor(GameObject carta) {
			return carta.GetComponent<CartaInfo>().calcularDefensa();
		}


	}

}