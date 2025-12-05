using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo {

	public class ZonaDescarte : MonoBehaviour {

		public string tipo;
		public int jugador;


		void OnMouseDown() {
			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
			GameObject panel = instanciador.CrearPanelVisualizacion();
			PanelVisualizacion componente = panel.GetComponent<PanelVisualizacion>();
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();

			if (tipo == "descarte")
				componente.Iniciar(fisica.TraerCartasEnCementerio(jugador), "Visualizar cartas en el descarte");

			if (tipo == "materiales")
				componente.Iniciar(fisica.TraerCartasEnMateriales(jugador), "Visualizar cartas en materiales");
		}

	}

}