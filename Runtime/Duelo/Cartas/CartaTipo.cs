using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Carta {

	public class CartaTipo : MonoBehaviour {

		public readonly List<string> tipos = new List<string>();


		public void AgregarTipo(string tipo) {
			if (!tipos.Contains(tipo))
				tipos.Add(tipo);
		}


		public void RemoverTipo(string tipo) {
			if (tipos.Contains(tipo))
				tipos.Remove(tipo);
		}


		public void RemoverTodo() {
			tipos.Clear();
		}


		public bool ContieneTipo(string tipo) {
			return tipos.Contains(tipo);
		}


	}

}