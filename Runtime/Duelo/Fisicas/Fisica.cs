using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Ging1991.Core;

namespace Bounds.Modulos.Duelo.Fisicas {

	public class Fisica : SingletonMonoBehaviour<Fisica> {

		public ZonaCampo campo1;
		public ZonaCampo campo2;
		public GameObject panel;
		public ListadorDeZonas listador;

		public void OrganizarMano(int jugador, List<GameObject> cartas) {
			Vector3 posicionBase = Constantes.VECTOR_MANO1;
			if (jugador == 2)
				posicionBase = Constantes.VECTOR_MANO2;
			for (int i = 0; i < cartas.Count; i++) {
				Vector3 posicion = posicionBase + new Vector3(142 * i, 0, 0);
				GameObject carta = cartas[i];
				carta.GetComponent<CartaMovimiento>().Desplazar(posicion);
			}
		}


		public void Inicializar() {
			listador = new ListadorDeZonas();
			listador.Inicializar();

			campo1 = new ZonaCampo(1);
			campo2 = new ZonaCampo(2);
		}


		public void EnviarHaciaMazo(GameObject carta, int jugador) {
			listador.AgregarCarta(carta, jugador, ListadorDeZonas.Zona.MAZO);
			QuitarDelCampo(carta, jugador);
			if (jugador == 1)
				carta.GetComponent<CartaMovimiento>().Desplazar(Constantes.VECTOR_MAZO1);
			if (jugador == 2)
				carta.GetComponent<CartaMovimiento>().Desplazar(Constantes.VECTOR_MAZO2);
			ActualizarTextoMazo(jugador);
			carta.GetComponent<CartaGeneral>().ColocarBocaAbajo();
		}


		public void EnviarHaciaMano(GameObject carta, int jugador) {
			listador.AgregarCarta(carta, jugador, ListadorDeZonas.Zona.MANO);
			QuitarDelCampo(carta, jugador);
			OrganizarMano(jugador, listador.GenerarLista(jugador, ListadorDeZonas.Zona.MANO));
			CartaInfo info = carta.GetComponent<CartaInfo>();
			info.controlador = jugador;
			ColocarBocaArriba(carta, jugador == 1);
		}


		public void EnviarHaciaCampo(int jugador, GameObject carta, GameObject campo) {
			QuitarDelCampo(carta, jugador);
			listador.AgregarCarta(carta, jugador, ListadorDeZonas.Zona.CAMPO);


			if (campo1.ContieneCampo(campo))
				campo1.Agregar(carta, campo);
			if (campo2.ContieneCampo(campo))
				campo2.Agregar(carta, campo);

			CartaGeneral componente = carta.GetComponent<CartaGeneral>();
			componente.ColocarBocaArriba();

			ActualizarTextoMazo(1);
			ActualizarTextoMazo(2);
		}


		public void EnviarHaciaDescarte(GameObject carta, int jugador) {
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			movimiento.Enderezar();
			movimiento.Desplazar(jugador == 1 ? Constantes.VECTOR_CEMENTERIO1 : Constantes.VECTOR_CEMENTERIO2);
			listador.AgregarCarta(carta, jugador, ListadorDeZonas.Zona.CEMENTERIO);
			QuitarDelCampo(carta, jugador);
			ActualizarContadorCementerio(jugador);
		}


		public void QuitarDelCampo(GameObject carta, int jugador) {
			campo1.QuitarDeCampo(carta);
			campo2.QuitarDeCampo(carta);
		}


		public void EnviarHaciaMateriales(GameObject carta, int jugador) {
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			movimiento.Enderezar();
			movimiento.Desplazar(jugador == 1 ? Constantes.VECTOR_CEMENTERIO1 : Constantes.VECTOR_CEMENTERIO2);

			listador.AgregarCarta(carta, jugador, ListadorDeZonas.Zona.MATERIALES);
			QuitarDelCampo(carta, jugador);
			ActualizarContadorMateriales(jugador);
		}


		public GameObject RobarCarta(int jugador) {
			int cantidad = listador.GenerarLista(jugador, ListadorDeZonas.Zona.MANO).Count;
			GameObject carta = listador.SiguienteCarta(jugador, ListadorDeZonas.Zona.MAZO);

			if (cantidad < 5 && carta != null) {
				EnviarHaciaMano(carta, jugador);
				ColocarBocaArriba(carta, jugador == 1);
			}
			else {
				carta = null;
			}

			ActualizarTextoMazo(jugador);
			return carta;
		}


		private void ColocarBocaArriba(GameObject carta, bool estaBocaArriba) {
			if (estaBocaArriba)
				carta.GetComponent<CartaGeneral>().ColocarBocaArriba(true);
			else
				carta.GetComponent<CartaGeneral>().ColocarBocaAbajo(true);
		}


		public void MolerCarta(int jugador) {
			GameObject carta = listador.SiguienteCarta(jugador, ListadorDeZonas.Zona.MAZO);
			if (carta != null) {
				EnviarHaciaDescarte(carta, jugador);
				ColocarBocaArriba(carta, jugador == 1);
			}
			ActualizarTextoMazo(jugador);
			ActualizarContadorCementerio(jugador);
		}


		public List<GameObject> TraerSiguientesCartasEnMazo(int jugador, int cantidad) {
			List<GameObject> cartas = new List<GameObject>();

			for (int i = 1; i <= cantidad; i++) {
				GameObject carta = listador.SiguienteCarta(jugador, ListadorDeZonas.Zona.MAZO, i);
				if (carta != null)
					cartas.Add(carta);
				else
					break;
			}
			return cartas;
		}


		public List<GameObject> TraerCartasEnMazo(int jugador) {
			return listador.GenerarLista(jugador, ListadorDeZonas.Zona.MAZO);
		}


		public List<GameObject> TraerCartasEnMano(int jugador) {
			return listador.GenerarLista(jugador, ListadorDeZonas.Zona.MANO);
		}


		public List<GameObject> TraerCartasEnCampo(int jugador) {
			return listador.GenerarLista(jugador, ListadorDeZonas.Zona.CAMPO);
		}


		public List<GameObject> TraerCartasEnCementerio(int jugador) {
			return listador.GenerarLista(jugador, ListadorDeZonas.Zona.CEMENTERIO);
		}


		public List<GameObject> TraerCartasEnMateriales(int jugador) {
			return listador.GenerarLista(jugador, ListadorDeZonas.Zona.MATERIALES);
		}


		public List<GameObject> TraerCampos(int jugador) {
			if (jugador == 1)
				return campo1.campos;
			if (jugador == 2)
				return campo2.campos;
			return null;
		}


		public void ActualizarTextoMazo(int jugador) {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + jugador);
			bloque.setMazo(TraerCartasEnMazo(jugador).Count);
		}


		public void ActualizarContadorCementerio(int jugador) {
			ZonaJugador zona = ZonaJugador.GetInstancia("ZonaJugador" + jugador);
			zona.SetDescarte(TraerCartasEnCementerio(jugador).Count);
		}


		public void ActualizarContadorMateriales(int jugador) {
			ZonaJugador zona = ZonaJugador.GetInstancia("ZonaJugador" + jugador);
			zona.SetMateriales(TraerCartasEnMateriales(jugador).Count);
		}


		public void BloquearCartasEnCampo(bool bloquear) {/*
			foreach(GameObject carta in TraerCartasEnCampo(1))
				carta.GetComponent<CanvasGroup>().blocksRaycasts = bloquear;
			foreach(GameObject carta in TraerCartasEnMano(1))
				carta.GetComponent<CanvasGroup>().blocksRaycasts = bloquear;
			foreach(GameObject carta in TraerCartasEnCampo(2))
				carta.GetComponent<CanvasGroup>().blocksRaycasts = bloquear;
			foreach(GameObject carta in TraerCartasEnMano(2))
				carta.GetComponent<CanvasGroup>().blocksRaycasts = bloquear;*/
		}


		public void Mulligan() {
			List<GameObject> cartas = new List<GameObject>(TraerCartasEnMazo(1));
			for (int i = 0; i < 5; i++) {
				GameObject carta = cartas[i];
				EnviarHaciaMazo(carta, 1);
			}
		}


	}

}