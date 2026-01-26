using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Utiles {

	public class Tutor {


		public static GameObject MejorCriaturaInvocacionNormal(List<GameObject> cartas) {

			GameObject ret = null;
			bool esBasico = false;
			int ataque = -1;
			Condicion condicion = new Condicion(tipoCarta: "CRIATURA");

			foreach (GameObject carta in condicion.CumpleLista(cartas)) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				if (info.original.datoCriatura.perfeccion != "BASICO" && esBasico)
					continue;

				// una basica reemplaza a una no basica
				if (info.original.datoCriatura.perfeccion == "BASICO" && !esBasico) {
					ret = carta;
					esBasico = true;
					ataque = info.calcularAtaque();
					continue;
				}
				// una basica reemplaza a una basica de menor ataque
				if (info.original.datoCriatura.perfeccion == "BASICO" && info.calcularAtaque() > ataque) {
					ret = carta;
					esBasico = true;
					ataque = info.calcularAtaque();
					continue;
				}
				// una no basica reemplaza a una no basica de menor ataque
				if (info.original.datoCriatura.perfeccion != "BASICO" && info.calcularAtaque() > ataque) {
					ret = carta;
					esBasico = true;
					ataque = info.calcularAtaque();
					continue;
				}
			}

			return ret;
		}


		public static GameObject MejorCriatura(List<GameObject> cartas) {
			Condicion condicion = new Condicion(tipoCarta: "CRIATURA");
			List<GameObject> criaturas = condicion.CumpleLista(cartas);
			if (criaturas.Count > 0)
				return criaturas[0];
			return null;
		}


		public static GameObject MejorCriaturaEfecto(List<GameObject> cartas) {
			Condicion condicion = new Condicion(tipoCarta: "CRIATURA");
			List<GameObject> criaturas = condicion.CumpleLista(cartas);
			GameObject ret = null;
			foreach (GameObject carta in criaturas) {
				if (EmblemaActivarHabilidad.TieneHabilidadActivada(carta))
					ret = carta;
			}
			return ret;
		}


	}

}