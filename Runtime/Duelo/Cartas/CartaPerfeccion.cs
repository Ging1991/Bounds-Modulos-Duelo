using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Carta {

	public class CartaPerfeccion : MonoBehaviour {

		public bool esPefecta;
		private static Color32 colorApagado = new Color(0.196f, 0.3137f, 0.4705f);
		public CartaFrente cartaFrente;

		public bool CalcularPerfeccion() {
			if (!EsPerfeccionable())
				esPefecta = true;
			else {
				CartaInfo info = GetComponent<CartaInfo>();
				Fisica fisica = Fisica.Instancia;
				if (info.original.datoCriatura.perfeccion == "REFLEJO")
					esPefecta = ListaCompletaMaterialesOBJ(
						info.original.materiales,
						fisica.TraerCartasEnMateriales(JugadorDuelo.Adversario(info.controlador))
					);
				else
					esPefecta = ListaCompletaMaterialesOBJ(
						info.original.materiales,
						fisica.TraerCartasEnMateriales(info.controlador)
					);
			}
			cartaFrente.SetIlustracionColor((!esPefecta) ? colorApagado : Color.white);
			return esPefecta;
		}


		public bool PuedeInvocar() {


			if (!EsPerfeccionable()) {
				return false;
			}
			else {
				CartaInfo cartaInfo = GetComponent<CartaInfo>();

				if (GetComponent<CartaEfecto>().TieneClave("INVOCACION_UNICA") &&
					JugadorDuelo.GetInstancia(cartaInfo.controlador).invocacionesRestringidas.Contains(cartaInfo.cartaID)) {
					return false;
				}

				Fisica fisica = EmblemaConocimiento.getInstancia().traerFisica();
				return ListaCompletaParaInvocar(
					fisica.TraerCartasEnCampo(cartaInfo.controlador),
					fisica.TraerCartasEnCampo(JugadorDuelo.Adversario(cartaInfo.controlador)),
					fisica.TraerCartasEnMano(cartaInfo.controlador)
				);
			}
		}


		public bool EsPerfecta() {
			return esPefecta;
		}


		public bool EsPerfeccionable() {

			CartaInfo info = GetComponent<CartaInfo>();

			if (info.original.clase != "CRIATURA")
				return false;

			if (info.original.datoCriatura.perfeccion == "BASICO")
				return false;

			if (info.original.datoCriatura.perfeccion == "FICHA")
				return false;

			return true;
		}


		public bool ListaCompletaParaInvocar(
				List<GameObject> campoJugador,
				List<GameObject> campoAdversario,
				List<GameObject> manoJugador) {

			CartaInfo info = GetComponent<CartaInfo>();
			if (info.original.datoCriatura.perfeccion == "REFLEJO")
				return ListaCompletaMaterialesOBJ(info.original.materiales, campoAdversario);
			if (info.original.datoCriatura.perfeccion == "ROMPECABEZAS") {
				List<GameObject> nuevaLista = new();
				nuevaLista.AddRange(manoJugador);
				nuevaLista.Remove(gameObject);
				return ListaCompletaMaterialesOBJ(info.original.materiales, nuevaLista);
			}
			return ListaCompletaMaterialesOBJ(info.original.materiales, campoJugador);
		}


		private bool ListaCompletaMaterialesOBJ(List<MaterialBD> materialesOBJ, List<GameObject> cartasEnCampo) {
			List<GameObject> materialesConsiderados = new List<GameObject>();

			foreach (var materialOBJ in materialesOBJ) {

				foreach (GameObject cartaEnCampo in cartasEnCampo) {

					if (!materialesConsiderados.Contains(cartaEnCampo)) {

						if (materialOBJ.tipo == "TIPO") {
							if (cartaEnCampo.GetComponent<CartaTipo>().ContieneTipo(materialOBJ.parametroTipo)) {
								materialesConsiderados.Add(cartaEnCampo);
								break;
							}
						}
						if (materialOBJ.tipo == "CLASE") {
							if (cartaEnCampo.GetComponent<CartaInfo>().original.clase == materialOBJ.parametroClase) {
								materialesConsiderados.Add(cartaEnCampo);
								break;
							}
						}
						if (materialOBJ.tipo == "CARTA_ID") {
							if (ExtenderID(cartaEnCampo).Contains(materialOBJ.parametroID)) {
								materialesConsiderados.Add(cartaEnCampo);
								break;
							}
						}
						if (materialOBJ.tipo == "VECTOR_ATK") {
							if (cartaEnCampo.GetComponent<CartaInfo>().calcularAtaque() == materialOBJ.parametroATK) {
								materialesConsiderados.Add(cartaEnCampo);
								break;
							}
						}
						if (materialOBJ.tipo == "VECTOR_DEF") {
							if (cartaEnCampo.GetComponent<CartaInfo>().calcularDefensa() == materialOBJ.parametroDEF) {
								materialesConsiderados.Add(cartaEnCampo);
								break;
							}
						}
					}
				}
			}

			return materialesConsiderados.Count == materialesOBJ.Count;
		}


		public static List<int> ExtenderID(GameObject carta) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			List<int> listaID = new List<int>();
			listaID.Add(info.original.cartaID);
			if (carta.GetComponent<CartaEfecto>().TieneClave("ALIAS"))
				listaID.Add(carta.GetComponent<CartaEfecto>().GetEfecto("ALIAS").parametroID);
			return listaID;
		}


	}

}