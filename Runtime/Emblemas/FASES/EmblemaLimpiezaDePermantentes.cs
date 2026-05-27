using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblemas.Fases {

	public class EmblemaLimpiezaDePermantentes {

		public static void Limpiar(int jugador) {

			Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();

			List<GameObject> cartasParaLimpiar = new List<GameObject>();
			foreach (GameObject carta in fisica.TraerCartasEnCampo(jugador)) {
				if (DebeLimpiar(carta)) {
					cartasParaLimpiar.Add(carta);
				}
			}

			foreach (GameObject carta in cartasParaLimpiar) {
				EmblemaEnviarAlCementerio.DesdeElCampo(carta);
			}

		}


		private static bool DebeLimpiar(GameObject carta) {

			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (info.original.clase == "HECHIZO")
				return true;

			if (info.original.clase == "TRAMPA" && carta.GetComponent<CartaGeneral>().bocaArriba)
				return true;

			if (info.original.clase == "MISION") {
				EfectoBD efecto = carta.GetComponent<CartaEfecto>().GetEfecto("MISION_N");
				if (info.TraerContadores("mision") >= efecto.parametroN) {
					return true;
				}
			}
			return false;
		}


	}
}