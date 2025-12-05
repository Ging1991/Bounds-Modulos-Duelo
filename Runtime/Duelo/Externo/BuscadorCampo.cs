using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo {

	public class BuscadorCampo {

		private static BuscadorCampo instancia;

		private BuscadorCampo() { }


		public static BuscadorCampo getInstancia() {
			if (instancia == null)
				instancia = new BuscadorCampo();
			return instancia;
		}


		public GameObject buscarCampoLibre(int jugador) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			GameObject libre = null;
			foreach (GameObject campo in fisica.TraerCampos(jugador)) {
				Campo componente = campo.GetComponent<Campo>();
				if (!componente.EstaOcupado())
					libre = campo;
			}
			return libre;
		}


		public List<GameObject> buscarCampoLibreN(int jugador, int cantidad) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			List<GameObject> libres = new List<GameObject>();
			foreach (GameObject campo in fisica.TraerCampos(jugador)) {
				Campo componente = campo.GetComponent<Campo>();
				if (!componente.EstaOcupado()) {
					libres.Add(campo);
					if (libres.Count >= cantidad)
						break;
				}
			}
			return libres;
		}


	}

}