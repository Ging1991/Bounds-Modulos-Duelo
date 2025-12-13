using System.Collections.Generic;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Tinteros;
using Ging1991.Relojes;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Paneles {

	public class RecompensaDuelo : MonoBehaviour {

		private IEjecutable accion;
		public List<GameObject> objetosDesactivables;
		public List<GameObject> objetosConTinta;
		public List<GameObject> objetosConTintaClara;
		public Text rarezaProbabilidad;
		private Color colorTinta;
		private bool fueCobrada;

		public void Inicializar(int cartaID, string rareza, Color tinta, Color fondo, IEjecutable accion, int probabilidad,
				DatosDeCartas datos, IlustradorDeCartas ilustrador, ITintero tintero) {
			fueCobrada = false;

			GetComponentInChildren<CartaFrente>().Inicializar(datos, ilustrador, tintero);
			GetComponentInChildren<CartaFrente>().Mostrar(cartaID, "A", rareza);
			this.accion = accion;
			colorTinta = tinta;
			foreach (var objeto in objetosConTinta)
				objeto.GetComponent<Image>().color = tinta;
			foreach (var objeto in objetosConTintaClara)
				objeto.GetComponent<Image>().color = fondo;
			rarezaProbabilidad.text = $"Poder {probabilidad}%";
		}

		public void OnMouseDown() {
			if (!fueCobrada) {
				foreach (var objeto in objetosDesactivables)
					objeto.SetActive(false);
				//GetComponentInChildren<EfectoVisual>().Animar("VAPOR", colorTinta);
				accion.Ejecutar();
				fueCobrada = true;
			}
		}

	}

}