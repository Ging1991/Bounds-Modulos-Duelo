using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionEsPerfecta : CondicionBool {
		
		public CondicionEsPerfecta(bool esPerfecta = true) : base(valor: esPerfecta) {}


		public override bool GetValor(GameObject carta) {
			return carta.GetComponent<CartaPerfeccion>().EsPerfecta();
		}


	}

}