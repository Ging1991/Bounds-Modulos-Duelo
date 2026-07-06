using UnityEngine;
using Bounds.Duelo.Carta;
using Ging1991.Core.Interfaces;
using Bounds.Visor;
using Bounds.Fisicas.Carta;
using System.Collections.Generic;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using UnityEngine.UI;

namespace Bounds.Infraestructura.Visores {

	public class VisorDuelo : MonoBehaviour {

		public VisorContador visorContador;
		public VisorCartaID visorCartaID;
		public VisorGenerador visorGenerador;
		public GameObject reversoOBJ;

		public void SetBocaAbajo(Sprite reverso) {
			reversoOBJ.GetComponent<Image>().sprite = reverso;
			reversoOBJ.SetActive(true);
		}

		public void Mostrar(GameObject carta) {

			reversoOBJ.SetActive(false);

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
			Color tintaGeneral = visorGenerador.proveedorColor.GetElemento($"TINTA_{info.rareza}");

			visorCartaID.primitiva.SetIlustracionImagen(visorGenerador.GetImagen(info.cartaID, info.imagen));

			visorCartaID.primitiva.nivel.SetValor(info.original.nivel);
			visorCartaID.primitiva.nivel.SetColorBorde(tintaGeneral);
			visorCartaID.primitiva.nivel.SetColorTexto(tintaGeneral);
			visorCartaID.primitiva.nivel.SetColorRelleno(visorGenerador.proveedorColor.GetElemento($"NIVEL_{info.rareza}"));

			// NOMBRE
			string nombre = info.original.nombre;
			try {
				string piv = visorGenerador.proveedorNombres.GetElemento(info.cartaID);
				if (piv != null)
					nombre = piv;
			}
			catch (System.Exception) {
				Debug.LogWarning($"No se encontró el nombre {info.cartaID}");
			}
			visorCartaID.primitiva.SetNombre(nombre, tintaGeneral);
			visorCartaID.primitiva.SetCartaID(info.cartaID, tintaGeneral);
			string claseExtendida = info.original.clase != "CRIATURA" ? info.original.clase : info.original.datoCriatura.perfeccion;
			visorCartaID.primitiva.SetBordeColor(tintaGeneral);
			visorCartaID.primitiva.SetRellenoColor(visorGenerador.proveedorColor.GetElemento($"RELLENO_{claseExtendida}"));
			visorCartaID.primitiva.SetRellenoTextoColor(visorGenerador.proveedorColor.GetElemento($"RELLENO_CLARO_{claseExtendida}"));

			if (info.original.clase == "EQUIPO")
				visorCartaID.primitiva.SetEstadisticas($"________________\nDEF {info.calcularDefensa()}");
			else if (info.original.clase == "CRIATURA")
				visorCartaID.primitiva.SetEstadisticas($"________________\nATK {info.calcularAtaque()} / DEF {info.calcularDefensa()}");
			else
				visorCartaID.primitiva.SetEstadisticas("");


			string encabezado = (info.original.clase != "CRIATURA") ? visorGenerador.GenerarEncabezado(info.original.clase) :
				visorGenerador.GenerarEncabezado(
					info.original.clase,
					info.original.datoCriatura.perfeccion,
					cartaTipo.tipos
				);


			string materiales = "";
			if (info.original.clase == "CRIATURA") {
				materiales += visorGenerador.GenerarMateriales(info.original.materiales);
			}

			string textoEfecto = GetNombre(visorGenerador.proveedorEfectos, info.cartaID, info.original.efecto);
			if (string.IsNullOrWhiteSpace(info.original.efecto))
				textoEfecto = "";

			if (info.original.datoCriatura.perfeccion == "BASICO") {
				visorCartaID.primitiva.SetAmbientacion($"<i>{GetNombre(visorGenerador.proveedorAmbientacion, info.cartaID, info.original.ambientacion)}</i>");
			}
			else {
				visorCartaID.primitiva.SetAmbientacion("");
			}

			List<EfectoBD> efectos = new List<EfectoBD>(info.original.efectos);
			efectos.AddRange(carta.GetComponent<CartaEfecto>().efectos);
			if (info.original.clase == "CRIATURA" && info.original.datoCriatura.efectos != null)
				efectos.AddRange(info.original.datoCriatura.efectos);


			visorCartaID.primitiva.SetDescripcion(
				visorGenerador.GenerarDescripcion(
					encabezado,
					materiales,
					visorGenerador.GenerarEfectos(efectos),
					textoEfecto
				)
			);

			// contadores
			visorContador.SetContador("supervivencia", info.TraerContadores("supervivencia"));
			visorContador.SetContador("veneno", info.TraerContadores("veneno"));
			visorContador.SetContador("poder", info.TraerContadores("poder"));
			visorContador.SetContador("debilidad", info.TraerContadores("debilidad"));
		}

		public static string GetNombre(IProveedor<int, string> selector, int clave, string defecto) {
			string pivot = selector.GetElemento(clave);
			if (pivot != null)
				return pivot;
			return defecto;
		}

	}

}