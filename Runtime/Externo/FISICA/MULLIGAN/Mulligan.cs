using System.Collections.Generic;
using Bounds.Cartas;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Duelo.Fila;
using Bounds.Duelo.Fila.Fases;
using Bounds.Fisicas.Carta;
using Bounds.Mazos;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core.Interfaces;
using UnityEngine;

namespace Bounds.Duelo {

	public class Mulligan : MonoBehaviour {

		public GameObject ventanaMulligan;
		public CartaGenerador cartaGenerador;

		public void Iniciar(CartaGenerador cartaGenerador) {
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			List<GameObject> cartas = fisica.TraerCartasEnMazo(1);
			this.cartaGenerador = cartaGenerador;

			AgregarCarta(cartas[0], 0);
			AgregarCarta(cartas[1], 1);
			AgregarCarta(cartas[2], 2);
			AgregarCarta(cartas[3], 3);
			AgregarCarta(cartas[4], 4);
		}


		private void AgregarCarta(GameObject carta, int posicion) {
			CartaImagenID cartaFrente = transform.GetChild(posicion).GetComponent<CartaImagenID>();
			cartaFrente.gameObject.SetActive(true);
			CartaInfo info = carta.GetComponent<CartaInfo>();
			cartaFrente.generador = cartaGenerador;
			cartaFrente.MostrarCartaID(info.cartaID, info.imagen, info.rareza);
		}


		public void ConservarMano1() {
			EmblemaRobo.RobarCartas(1, 4);
			FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
			fila.Agregar(new FaseMantenimiento(1));
			ActivarVacios();
			Destroy(ventanaMulligan);
		}


		public void CambiarCartas1() {
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			fisica.Mulligan();
			EmblemaRobo.RobarCartas(1, 4);
			FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
			fila.Agregar(new FaseMantenimiento(1));
			ActivarVacios();
			Destroy(ventanaMulligan);
		}


		public void ActivarVacios() {
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			GlobalDuelo duelo = GlobalDuelo.GetInstancia();

			if (duelo.mazoVacio1 != null) {
				JugarVacioPrincipal(1, duelo.mazoVacio1, fisica);
			}

			if (duelo.mazoVacio2 != null) {
				JugarVacioPrincipal(2, duelo.mazoVacio2, fisica);
			}
		}


		private void JugarVacioPrincipal(int jugador, CartaMazo vacio, Fisica fisica) {
			List<GameObject> mazo = new List<GameObject>(fisica.TraerCartasEnMazo(jugador));

			foreach (GameObject carta in mazo) {

				CartaInfo info = carta.GetComponent<CartaInfo>();
				if (info.cartaID == vacio.cartaID) {
					EmblemaJuegoSeleccionar.Seleccionar(carta, forzarSeleccion: true);
					GameObject campo = BuscadorCampo.getInstancia().buscarCampoLibre(jugador);
					EmblemaJuegoActivar.Jugar(jugador, carta, campo, forzarSeleccion: true);
					break;
				}
			}


		}



	}

}