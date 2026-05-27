using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionMultiple : CondicionCarta {
		
		public enum Tipo {Y, O}
		
		private List<CondicionCarta> condiciones;
		private Tipo tipo;

		public CondicionMultiple(Tipo tipo) {
			this.tipo = tipo;
			condiciones = new List<CondicionCarta>();
		}


		public void AgregarCondicion(CondicionCarta condicion) {
			condiciones.Add(condicion);
		}


		public override bool Cumple(GameObject carta) {
			if (tipo == Tipo.Y)
				return CumpleY(carta);
			if (tipo == Tipo.O)
				return CumpleO(carta);
			return false;
		}
		

		private bool CumpleY(GameObject carta) {
			bool todasSeCumplen = true;
			foreach (CondicionCarta condicion in condiciones) {
				todasSeCumplen = todasSeCumplen && condicion.Cumple(carta);
			}
			return todasSeCumplen;
		}


		private bool CumpleO(GameObject carta) {
			bool algunaSeCumple = false;
			foreach (CondicionCarta condicion in condiciones) {
				algunaSeCumple = algunaSeCumple || condicion.Cumple(carta);
			}
			return algunaSeCumple;
		}


	}

}
