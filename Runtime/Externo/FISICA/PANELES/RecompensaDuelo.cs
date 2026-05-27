using System.Collections.Generic;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Cartas.Tinteros;
using Ging1991.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Paneles {

	public class RecompensaDuelo : MonoBehaviour {

		private IEjecutable accion;
		public List<GameObject> objetosDesactivables;
		public List<GameObject> objetosConTinta;
		public List<GameObject> objetosConTintaClara;
		public Text rarezaProbabilidad;
		private bool fueCobrada;

		public void Inicializar(int cartaID, string imagen, string rareza, Color tinta, Color fondo, IEjecutable accion, int probabilidad,
				IProveedor<int, CartaBD> proveedorCartas, IProveedor<string, Sprite> ilustrador, ITintero tintero) {
			fueCobrada = false;

			GetComponentInChildren<CartaFrente>().Inicializar(proveedorCartas, ilustrador, tintero);
			GetComponentInChildren<CartaFrente>().Mostrar(cartaID, imagen, rareza);
			this.accion = accion;
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