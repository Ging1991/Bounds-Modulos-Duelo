using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionAtaque : CondicionEntero {


		public CondicionAtaque(int ataque = -1, List<int> ataques = null, int minimo = -1, int maximo = -1) :
				base(ataque, ataques, minimo, maximo) {

			precondiciones.Add(new CondicionClase(clase: "CRIATURA"));
		}


		public override int GetValor(GameObject carta) {
			return carta.GetComponent<CartaInfo>().calcularAtaque();
		}


	}

}