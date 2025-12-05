using System.Collections.Generic;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using UnityEngine;

namespace Bounds.Duelo.Carta {

	public class CartaEfecto : MonoBehaviour {

		public CartaBD datosOriginales;
		public List<EfectoBD> efectos = new List<EfectoBD>();


		public void Inicializar(CartaBD datosOriginales) {
			this.datosOriginales = datosOriginales;
			Restablecer();
		}


		public void Restablecer() {
			efectos = new List<EfectoBD>();
		}


		public void ColocarEfecto(EfectoBD efecto) {
			bool estaContenido = false;

			foreach (var efectoActual in efectos) {
				estaContenido = estaContenido || efectoActual.clave == efecto.clave;
			}

			foreach (var efectoActual in datosOriginales.efectos) {
				estaContenido = estaContenido || efectoActual.clave == efecto.clave;
			}

			if (GetComponent<CartaPerfeccion>().esPefecta && datosOriginales.clase == "CRIATURA") {
				foreach (var efectoActual in datosOriginales.datoCriatura.efectos) {
					estaContenido = estaContenido || efectoActual.clave == efecto.clave;
				}
			}

			if (!estaContenido)
				efectos.Add(efecto);
		}


		public void RemoverEfecto(EfectoBD efecto) {
			if (efectos.Contains(efecto))
				efectos.Remove(efecto);
		}


		public bool TieneEfecto(EfectoBD efecto) {
			return efectos.Contains(efecto);
		}


		public bool TieneClave(string clave) {
			bool tieneClave = false;
			foreach (var efecto in datosOriginales.efectos) {
				if (efecto.clave == clave) {
					tieneClave = true;
				}
			}
			foreach (var efecto in efectos) {
				if (efecto.clave == clave) {
					tieneClave = true;
				}
			}
			if (datosOriginales.datoCriatura != null && datosOriginales.datoCriatura.efectos != null) {
				if (GetComponent<CartaPerfeccion>().EsPerfecta())
					foreach (var efecto in datosOriginales.datoCriatura.efectos) {
						if (efecto.clave == clave) {
							tieneClave = true;
						}
					}
			}
			return tieneClave;
		}


		public EfectoBD GetEfecto(string clave) {
			EfectoBD ret = null;
			foreach (var efecto in datosOriginales.efectos) {
				if (efecto.clave == clave) {
					ret = efecto;
				}
			}
			foreach (var efecto in efectos) {
				if (efecto.clave == clave) {
					ret = efecto;
				}
			}
			if (datosOriginales.datoCriatura != null && datosOriginales.datoCriatura.efectos != null) {
				if (GetComponent<CartaPerfeccion>().EsPerfecta())
					foreach (var efecto in datosOriginales.datoCriatura.efectos) {
						if (efecto.clave == clave) {
							ret = efecto;
						}
					}
			}
			return ret;
		}


		public List<string> GetClaves() {
			List<string> claves = new List<string>();

			foreach (var efecto in datosOriginales.efectos) {
				if (efecto.clave != "") {
					claves.Add(efecto.clave.ToLower());
				}
			}
			return claves;
		}


	}


}