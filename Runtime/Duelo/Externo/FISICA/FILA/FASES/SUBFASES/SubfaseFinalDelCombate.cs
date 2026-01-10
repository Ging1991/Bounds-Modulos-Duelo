using Bounds.Duelo.Emblemas.Fases;
using UnityEngine;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseFinalDelCombate : IFase {

		private readonly GameObject atacante;
		private readonly GameObject atacado;

		public SubfaseFinalDelCombate(GameObject atacante, GameObject atacado) {
			this.atacante = atacante;
			this.atacado = atacado;
		}

		void IFase.Resolver() {
			EmblemaFinalDelCombate.Final(atacante, atacado);
		}

	}

}