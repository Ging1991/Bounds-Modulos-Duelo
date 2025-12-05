using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Fila.Fases.Subfases;
using UnityEngine;

namespace Bounds.Duelo.Fila.Fases {

	public class FaseCombate : IFase {

		private readonly GameObject atacante;
		private readonly GameObject atacado;

		public FaseCombate(GameObject atacante, GameObject atacado) {
			this.atacante = atacante;
			this.atacado = atacado;
		}

		void IFase.Resolver() {
			FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
			fila.Agregar(new SubfaseCalculoDeDaño());
		}

	}

}