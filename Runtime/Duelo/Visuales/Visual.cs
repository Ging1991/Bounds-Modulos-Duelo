using Ging1991.Animaciones.Efectos;
using UnityEngine;

namespace Bounds.Visuales {


	public class Visual : MonoBehaviour {

		public string codigo;

		public void Iniciar() {
			GetComponent<Escalar>()?.Inicializar();
			GetComponent<Rotar>()?.Inicializar();
			GetComponent<Colorizar>()?.Inicializar();
			GetComponent<Trasparentar>()?.Inicializar();
			GetComponent<Mover>()?.Inicializar();
			GetComponent<AudioSource>()?.Play();

		}

	}

}