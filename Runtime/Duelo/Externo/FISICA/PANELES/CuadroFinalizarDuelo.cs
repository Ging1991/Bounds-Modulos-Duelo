using Bounds.Cofres;
using Bounds.Duelo.Paneles;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Cartas.Tinteros;
using Bounds.Persistencia;
using Bounds.Persistencia.Datos;
using Ging1991.Core;
using Ging1991.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Utiles {

	public class CuadroFinalizarDuelo : MonoBehaviour, IEjecutable {

		public IEjecutable accion;
		public GameObject boton;
		public GameObject tituloOBJ;
		public GameObject descripcionOBJ;
		public GameObject recompensaOBJ;
		private CartaColeccionBD carta;
		private string rareza;


		public void Iniciar(IEjecutable accion, bool gano, IProveedor<int, CartaBD> proveedorCartas, IProveedor<string, Sprite> ilustrador) {
			Bloqueador.BloquearGrupo("GLOBAL", true);
			gameObject.name = "CuadroFinalizar";
			this.accion = accion;

			if (!gano) {
				Ejecutar();
				recompensaOBJ.gameObject.SetActive(false);
				return;
			}

			Coleccion coleccion = new Coleccion("PRINCIPIANTE", ControlDuelo.Instancia.carpetaColecciones.Generar("PRINCIPIANTE"));
			int probabilidad = Azar<int>.GenerarEnteroEntre(1, 100);

			if (probabilidad <= 40) {
				carta = Azar<CartaColeccionBD>.ValorAleatorio(coleccion.comunes);
				rareza = "N";
			}
			else if (probabilidad <= 75) {
				carta = Azar<CartaColeccionBD>.ValorAleatorio(coleccion.infrecuentes);
				rareza = "PLA";
			}
			else if (probabilidad <= 90) {
				carta = Azar<CartaColeccionBD>.ValorAleatorio(coleccion.raras);
				rareza = "ORO";
			}
			else if (probabilidad <= 99) {
				carta = Azar<CartaColeccionBD>.ValorAleatorio(coleccion.raras);
				rareza = "MIT";
			}
			else {
				carta = Azar<CartaColeccionBD>.ValorAleatorio(coleccion.secretas);
				rareza = "SEC";
			}

			ITintero tintero = new TinteroBounds();
			Color tinta = tintero.GetColor($"TINTA_{rareza}");
			Color fondo = tintero.GetColor($"NIVEL_{rareza}");
			GetComponentInChildren<RecompensaDuelo>().Inicializar(
				carta.cartaID, carta.imagen, rareza, tinta, fondo, this, probabilidad, proveedorCartas, ilustrador, tintero
			);
		}


		public void Iniciar(string titulo, string texto) {
			tituloOBJ.GetComponentInChildren<Text>().text = titulo;
			descripcionOBJ.GetComponentInChildren<Text>().text = texto;
		}


		public void Aceptar() {
			Cofre cofre = ControlDuelo.Instancia.cofre;
			CartaCofreBD linea = new($"{carta.cartaID}_{carta.imagen}_{rareza}_1");
			cofre.AgregarCarta(linea);
			cofre.Guardar();

			if (accion != null)
				accion.Ejecutar();
			Destroy(gameObject);
		}


		public static bool ExistenCuadros() {
			return GameObject.Find("CuadroFinalizar") != null;
		}


		public void Ejecutar() {
			boton.SetActive(true);
		}


	}

}