using System.Collections.Generic;
using Bounds.Modulos.Duelo.Visuales;
using UnityEngine;

namespace Bounds.Duelo.Carta {

	public class CartaContador : MonoBehaviour {

		public GameObject claseContador;
		Dictionary<string, GameObject> contadores = new Dictionary<string, GameObject>();

		public void SetContador(string tipo, int cantidad) {

			if (!contadores.ContainsKey(tipo)) {

				GameObject instancia = Instantiate(claseContador);
				instancia.transform.SetParent(transform);

				instancia.transform.localPosition = new Vector3(60, 80 - contadores.Keys.Count * 50, 0);
				instancia.transform.localScale = new Vector3(1, 1, 1);

				Quaternion rotacion = Quaternion.Euler(0, 0, 0);
				CartaMovimiento mov = GetComponent<CartaMovimiento>();
				if (mov.estaGirado)
					rotacion = Quaternion.Euler(0, 0, 0);

				instancia.transform.localRotation = rotacion;

				instancia.GetComponent<ContadorBounds>().SetTipo(tipo);
				contadores.Add(tipo, instancia);
			}

			GameObject contador = contadores[tipo];
			if (cantidad == 0) {
				contadores.Remove(tipo);
				Destroy(contador);

			}
			else {
				contador.GetComponent<ContadorBounds>().SetCantidad(cantidad);
			}
		}


	}

}