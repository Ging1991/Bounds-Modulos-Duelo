using System.Collections.Generic;
using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubEnviarMateriales : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			EmblemaEnviarMaterial.EnviarMateriales(new List<GameObject>() { carta });
		}

	}

}