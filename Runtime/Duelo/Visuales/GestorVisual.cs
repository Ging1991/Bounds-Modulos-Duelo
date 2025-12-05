using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Visuales {

	public class GestorVisual : MonoBehaviour {
		
		public List<Visual> visuales;

		public void Animar(string codigo) {
			foreach (var visual in visuales) {
				if (visual.codigo == codigo) {
					visual.gameObject.SetActive(true);
					visual.Iniciar();
					break;
				}
			}
		}

	}

}