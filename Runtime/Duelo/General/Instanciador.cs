using UnityEngine;

namespace Bounds.Duelo.Utiles {

	public class Instanciador : MonoBehaviour {

		public GameObject clase_espada;
		public GameObject clase_muro;
		public GameObject claseFondoSeleccion;
		public GameObject claseFondoSeleccion2;
		public GameObject claseAtaque;
		public GameObject claseOpcionCarta;
		public GameObject clasePanelPerfecto;
		public GameObject claseAniTextoRojo;
		public GameObject claseAniTextoVerde;
		public GameObject clasePopupDuelo;
		public GameObject claseAlterText;
		public GameObject claseCuadroConfirmar;
		public GameObject claseCuadroFinalizarDuelo;
		public GameObject clasePopipAtaqueDirecto;
		public GameObject clasePanelSeleccionar;
		public GameObject clasePanelVisualizar;
		public GameObject clasePanelVisualizarV2;
		public GameObject claseOpcionSeleccion;
		public GameObject claseOpcionVisualizacion;
		public GameObject claseTextoCreciente;


		public GameObject CrearTextoCreciente(string texto) {
			GameObject explosion = Instantiate(claseTextoCreciente);
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			explosion.transform.SetParent(lienzo.transform);
			explosion.transform.localPosition = new Vector3(0, 0, 0);
			explosion.transform.localScale = new Vector3(1, 1, 1);
			explosion.GetComponent<TextoCreciente>().iniciar(texto);
			return explosion;
		}


		public GameObject CrearTextoFlotante(Vector3 posicion, Transform padre) {
			GameObject explosion = Instantiate(claseAlterText, posicion, Quaternion.identity);
			explosion.transform.SetParent(padre);
			explosion.transform.localScale = new Vector3(1, 1, 1);
			explosion.transform.localPosition = posicion;
			return explosion;
		}


		public GameObject CrearCuadroConfirmar(Vector3 posicion, Transform padre) {
			GameObject explosion = Instantiate(claseCuadroConfirmar, posicion, Quaternion.identity);
			explosion.transform.SetParent(padre);
			explosion.transform.localScale = new Vector3(1, 1, 1);
			explosion.transform.localPosition = posicion;
			return explosion;
		}


		public GameObject CrearCuadroFinalizarDuelo(Vector3 posicion) {
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			GameObject explosion = Instantiate(claseCuadroFinalizarDuelo, posicion, Quaternion.identity);
			explosion.transform.SetParent(lienzo.transform);
			explosion.transform.localScale = new Vector3(1, 1, 1);
			explosion.transform.localPosition = posicion;
			return explosion;
		}


		public GameObject CrearPopupDuelo(Vector3 posicion) {
			GameObject explosion = Instantiate(clasePopupDuelo, posicion, Quaternion.identity);
			return explosion;
		}


		public GameObject CrearFondoSeleccion(Transform padre, bool usar2 = false) {
			GameObject claseCrear = (usar2) ? claseFondoSeleccion2 : claseFondoSeleccion;
			GameObject fondo = Instantiate(claseCrear, padre.position, Quaternion.identity);
			fondo.transform.SetParent(padre);
			//fondo.transform.Rotate(90 ,0, 0);
			fondo.transform.localScale = new Vector3(1, 1, 1);
			fondo.transform.localPosition = fondo.transform.localPosition + new Vector3(0, 3, 0.2f);
			return fondo;
		}


		public GameObject CrearOpcionCarta(Transform padre) {
			GameObject opcion = Instantiate(claseOpcionCarta, padre.position, Quaternion.identity);
			opcion.transform.SetParent(padre);
			opcion.transform.localScale = new Vector3(1, 1, 1);
			return opcion;
		}


		public GameObject CrearOpcionSeleccion(Transform padre) {
			GameObject opcion = Instantiate(claseOpcionSeleccion, padre.position, Quaternion.identity);
			opcion.transform.SetParent(padre);
			opcion.transform.localScale = new Vector3(1, 1, 1);
			return opcion;
		}


		public GameObject CrearTextoFlotanteRojo(Vector3 posicion, int cantidad) {
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			GameObject ani = Instantiate(claseAniTextoRojo, lienzo.transform.position, Quaternion.identity);
			ani.transform.SetParent(lienzo.transform);
			ani.transform.localPosition = posicion;
			ani.transform.localScale = new Vector3(1, 1, 1);
			AniTexto scr = ani.GetComponent<AniTexto>();
			scr.Iniciar(cantidad);

			return ani;
		}


		public GameObject CrearTextoFlotanteVerde(Vector3 posicion, int cantidad) {
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			GameObject ani = Instantiate(claseAniTextoVerde, lienzo.transform.position, Quaternion.identity);
			ani.transform.SetParent(lienzo.transform);
			ani.transform.localPosition = posicion;
			//ani.transform.position = posicion;
			ani.transform.localScale = new Vector3(1, 1, 1);
			AniTexto scr = ani.GetComponentInChildren<AniTexto>();
			scr.Iniciar(cantidad);
			return ani;
		}


		public GameObject CrearEspadaAtaque(Vector3 posicion) {
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			GameObject ani = Instantiate(clase_espada, lienzo.transform.position, Quaternion.identity);
			ani.transform.SetParent(lienzo.transform);
			ani.transform.localPosition = posicion;
			ani.transform.localScale = new Vector3(2, 2, 1);
			return ani;
		}


		public GameObject CrearMuro(Vector3 posicion) {
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			GameObject ani = Instantiate(clase_muro, lienzo.transform.position, Quaternion.identity);
			ani.transform.SetParent(lienzo.transform);
			Vector3 prueba = new Vector3(posicion.x, posicion.y + 50, 0);
			ani.transform.localPosition = prueba;
			ani.transform.localScale = new Vector3(1.5f, 1.5f, 1);
			return ani;
		}


		public GameObject CrearLienzoSeleccionCarta() {
			GameObject panel = Instantiate(clasePanelPerfecto, new Vector3(0, 150, 0), Quaternion.identity);
			panel.name = "PanelPerfecto";
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			panel.transform.SetParent(lienzo.transform);
			panel.transform.localScale = new Vector3(1, 1, 1);
			panel.transform.localPosition = new Vector3(0, 0, 0);
			return panel;
		}


		public GameObject CrearPanelSeleccionarCarta() {
			GameObject panel = Instantiate(clasePanelSeleccionar, new Vector3(0, 150, 0), Quaternion.identity);
			panel.name = "PanelSeleccionar";
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			panel.transform.SetParent(lienzo.transform);
			panel.transform.localScale = new Vector3(1, 1, 1);
			panel.transform.localPosition = new Vector3(0, 0, 0);
			return panel;
		}


		public GameObject CrearPanelVisualizacion() {
			GameObject panel = Instantiate(clasePanelVisualizar, new Vector3(0, 150, 0), Quaternion.identity);
			panel.name = "PanelVisualizar";
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			panel.transform.SetParent(lienzo.transform);
			panel.transform.localScale = new Vector3(1, 1, 1);
			panel.transform.localPosition = new Vector3(0, 0, 0);
			return panel;
		}


		public GameObject CrearPanelVisualizacionV2() {
			GameObject panel = Instantiate(clasePanelVisualizarV2, new Vector3(0, 150, 0), Quaternion.identity);
			panel.name = "PanelZonas";
			GameObject lienzo = GameObject.Find("Lienzo_efectos");
			panel.transform.SetParent(lienzo.transform);
			panel.transform.localScale = new Vector3(1, 1, 1);
			panel.transform.localPosition = new Vector3(0, 0, 0);
			return panel;
		}


	}

}