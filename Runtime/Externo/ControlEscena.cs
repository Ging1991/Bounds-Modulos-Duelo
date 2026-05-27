using UnityEngine.SceneManagement;

namespace Bounds.Infraestructura {

	public class ControlEscena {

		private static ControlEscena instancia;
		
		private ControlEscena() {}


		public static ControlEscena GetInstancia(){
			if (instancia == null)
				instancia = new ControlEscena();
			return instancia;
		}


		public void CambiarEscena_menu() {
			SceneManager.LoadScene("MENU");
		}


		public void CambiarEscena_tienda() {
			SceneManager.LoadScene("TIENDA COMPRAR");
		}


		public void CambiarEscena_duelo() {
			SceneManager.LoadScene("CargaDuelo");
		}


		public void CambiarEscena_entrenamiento() {
			SceneManager.LoadScene("Entrenamiento");
		}


		public void CambiarEscena(string escena) {
			SceneManager.LoadScene(escena);
		}


	}

}