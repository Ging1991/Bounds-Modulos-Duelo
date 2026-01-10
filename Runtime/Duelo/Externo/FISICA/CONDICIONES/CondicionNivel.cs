using System.Collections.Generic;
using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionNivel : CondicionEntero {
		
		public CondicionNivel(int nivel = -1, List<int> niveles = null, int minimo = -1, int maximo = -1) :
				base(nivel, niveles, minimo, maximo) {}


		public override int GetValor(GameObject carta) {
			return carta.GetComponent<CartaInfo>().original.nivel;
		}


	}

}