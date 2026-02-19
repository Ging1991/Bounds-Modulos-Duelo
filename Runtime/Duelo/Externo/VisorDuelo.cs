using UnityEngine;
using Bounds.Duelo.Carta;
using System.Collections.Generic;
using Bounds.Modulos.Cartas.Tinteros;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Visor;
using Bounds.Modulos.Visor.Persistencia;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Fisicas.Carta;
using Ging1991.Core.Interfaces;
using Bounds.Duelo;

namespace Bounds.Infraestructura.Visores {

	public class VisorDuelo : MonoBehaviour {

		public DatosDeCartas datosDeCartas;
		public DatosDeEfectos datosDeEfectos;
		private ITintero tintero;
		public VisorGeneral visorGeneral;
		public VisorContador visorContador;
		protected ISelector<int, string> selectorNombres;
		protected ISelector<string, string> selectorClases;
		protected ISelector<string, string> selectorTipos;
		protected ISelector<string, string> selectorInvocaciones;


		public void Inicializar(ISelector<int, string> selectorNombres, ISelector<string, string> selectorClases,
				ISelector<string, string> selectorTipos, ISelector<string, string> selectorInvocaciones, ISelector<string, Sprite> ilustrador) {

			datosDeCartas.Inicializar();
			datosDeEfectos.Inicializar();
			tintero = new TinteroBounds();
			this.selectorNombres = selectorNombres;
			this.selectorClases = selectorClases;
			this.selectorTipos = selectorTipos;
			this.selectorInvocaciones = selectorInvocaciones;

			visorGeneral.Inicializar(
				datosDeCartas, datosDeEfectos, ilustrador, tintero, selectorClases,
				selectorTipos, selectorInvocaciones, selectorNombres
			);
		}


		public void Mostrar(GameObject carta) {

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
			Color tintaGeneral = tintero.GetColor($"TINTA_{info.rareza}");

			visorGeneral.SetImagen(info.cartaID, info.imagen, tintaGeneral);

			visorGeneral.visorTitulo.SetNivel(info.original.nivel, tintero.GetColor($"NIVEL_{info.rareza}"), tintaGeneral);

			// NOMBRE
			string nombre = info.original.nombre;
			try {
				string piv = selectorNombres.GetElemento(info.cartaID);
				if (piv != null)
					nombre = piv;
			}
			catch (System.Exception) {
				Debug.LogWarning($"No se encontró el nombre {info.cartaID}");
			}
			visorGeneral.visorTitulo.SetNombre(nombre, tintaGeneral);


			visorGeneral.visorTitulo.SetCartaID(info.cartaID, tintaGeneral);

			string claseExtendida = info.original.clase != "CRIATURA" ? info.original.clase : info.original.datoCriatura.perfeccion;
			visorGeneral.SetFondo(tintero.GetColor($"RELLENO_{claseExtendida}"), tintaGeneral);
			visorGeneral.SetSubFondo(tintero.GetColor($"RELLENO_CLARO_{claseExtendida}"), tintaGeneral);

			if (info.original.clase == "EQUIPO")
				visorGeneral.visorDescripcion.SetEstadisticas(info.original.defensa);
			else if (info.original.clase == "CRIATURA")
				visorGeneral.visorDescripcion.SetEstadisticas(info.calcularAtaque(), info.calcularDefensa());
			else
				visorGeneral.visorDescripcion.SetEstadisticas();

			string encabezado = (info.original.clase != "CRIATURA") ? visorGeneral.GenerarEncabezado(info.original.clase) :
				visorGeneral.GenerarEncabezado(
					info.original.clase,
					info.original.datoCriatura.perfeccion,
					cartaTipo.tipos
				);

			List<EfectoBD> efectos = new List<EfectoBD>(info.original.efectos);
			efectos.AddRange(carta.GetComponent<CartaEfecto>().efectos);
			if (info.original.clase == "CRIATURA" && info.original.datoCriatura.efectos != null)
				efectos.AddRange(info.original.datoCriatura.efectos);

			string materiales = "";
			if (info.original.clase == "CRIATURA") {
				materiales += visorGeneral.GenerarMateriales(info.original.materiales);
				if (info.original.datoCriatura.perfeccion == "BASICO") {
					materiales += $"<i>{info.original.ambientacion}</i>";
				}
			}

			visorGeneral.SetDescripcion(encabezado, materiales, visorGeneral.GenerarEfectos(efectos), info.original.efecto);

			// contadores
			visorContador.SetContador("supervivencia", info.TraerContadores("supervivencia"));
			visorContador.SetContador("veneno", info.TraerContadores("veneno"));
			visorContador.SetContador("poder", info.TraerContadores("poder"));
			visorContador.SetContador("debilidad", info.TraerContadores("debilidad"));
		}

	}

}