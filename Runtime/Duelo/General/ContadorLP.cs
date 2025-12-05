using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Utiles {

	public class ContadorLP : MonoBehaviour {

		public string nombre;
		public int puntos;
		public bool seteado = false;


		public void Iniciar(string nombre, int puntos) {
			this.nombre = nombre;
			this.puntos = puntos;
			Actualizar();
		}


		public void Disminuir(int cantidad) {
			puntos -= cantidad;
			if (puntos < 0)
				puntos = 0;
			Actualizar();
		}


		public void Aumentar(int cantidad) {
			puntos += cantidad;
			Actualizar();		
		}


		private void Actualizar() {
			Text visorContador = gameObject.transform.GetChild(1).GetComponent<Text>();
			Text visorNombre = gameObject.transform.GetChild(0).GetComponent<Text>();
			visorContador.text = "LP " + puntos;
			visorNombre.text = nombre;
		}


	}

}