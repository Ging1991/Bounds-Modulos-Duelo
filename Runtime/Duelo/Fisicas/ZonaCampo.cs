using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;

namespace Bounds.Modulos.Duelo.Fisicas {

	public class ZonaCampo : Zona {

		public List<GameObject> campos;
		private int jugador;


		public ZonaCampo(int jugador) : base(new Vector3(0, 0, 0)) {
			campos = new List<GameObject>();
			this.jugador = jugador;
			GameObject[] objetos = GameObject.FindGameObjectsWithTag("campo");

			for (int i = 0; i < objetos.Length; i++) {
				if (objetos[i].GetComponent<Campo>().jugador == jugador)
					campos.Add(objetos[i]);
			}
		}


		public bool Agregar(GameObject carta, GameObject lugar) {
			if (!ContieneCampo(lugar))
				return false;

			Campo componente = lugar.GetComponent<Campo>();
			if (componente.EstaOcupado())
				return false;

			Agregar(carta);
			componente.Ocupar(carta);
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			Vector3 destino = lugar.transform.localPosition + lugar.transform.parent.localPosition;
			movimiento.Desplazar(destino);
			return true;
		}


		public bool ContieneCampo(GameObject campo) {
			return campos.Contains(campo);
		}


		public bool QuitarDeCampo(GameObject carta) {
			foreach (GameObject campo in campos) {
				Campo componente = campo.GetComponent<Campo>();
				if (componente.carta == carta)
					componente.Desocupar();
			}

			return true;
		}


	}

}