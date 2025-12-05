using Bounds.Duelo.Emblemas.Fases;
using UnityEngine;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseDeclararAtaque : IFase {

		private readonly GameObject atacante;
		private readonly GameObject atacado;

		public SubfaseDeclararAtaque(GameObject atacante, GameObject atacado) {
			this.atacante = atacante;
			this.atacado = atacado;
		}

		void IFase.Resolver() {
			Debug.Log("Declaracion");
			EmblemaDeclararAtaque.Declarar(atacante, atacado);
		}

	}

}