using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Condiciones {

	public class EsFaseDeTurno : ICondicionDeJuego {
		
		private EmblemaTurnos.Fase fase;

		public EsFaseDeTurno(EmblemaTurnos.Fase fase) {
			this.fase = fase;
		}


		public bool SeCumple() {
			return GetFase() == fase;
		}


		public EmblemaTurnos.Fase GetFase() {
			EmblemaTurnos emblemaTurnos = GameObject.Find("EmblemaTurnos").GetComponent<EmblemaTurnos>();
			return emblemaTurnos.fase;
		}


	}

}