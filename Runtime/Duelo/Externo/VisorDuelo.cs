using UnityEngine;
using Bounds.Duelo.Carta;
using System.Collections.Generic;
using Bounds.Modulos.Cartas.Tinteros;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Visor;
using Bounds.Modulos.Visor.Persistencia;
using Bounds.Modulos.Cartas.Persistencia.Datos;

namespace Bounds.Infraestructura.Visores {

	public class VisorDuelo : MonoBehaviour {

		public IlustradorDeCartas ilustradorDeCartas;
		public DatosDeCartas datosDeCartas;
		public DatosDeEfectos datosDeEfectos;
		public TraductorVisor traductorClases;
		public TraductorVisor traductorTipos;
		public TraductorVisor traductorPerfecciones;
		private ITintero tintero;
		public VisorGeneral visorGeneral;


		public void Inicializar() {
			datosDeCartas.Inicializar();
			datosDeEfectos.Inicializar();
			ilustradorDeCartas.Inicializar();
			traductorClases.Inicializar();
			traductorPerfecciones.Inicializar();
			traductorTipos.Inicializar();
			tintero = new TinteroBounds();
			visorGeneral.Inicializar(
				datosDeCartas, datosDeEfectos, ilustradorDeCartas, tintero, traductorClases, traductorTipos, traductorPerfecciones
			);
		}


		public void Mostrar(GameObject carta) {

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
			Color tintaGeneral = tintero.GetColor($"TINTA_{info.rareza}");

			visorGeneral.SetImagen(info.cartaID, info.imagen, tintaGeneral);

			visorGeneral.visorTitulo.SetNivel(info.original.nivel, tintero.GetColor($"NIVEL_{info.rareza}"), tintaGeneral);
			visorGeneral.visorTitulo.SetNombre(info.original.nombre, tintaGeneral);
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
			if (info.original.clase == "CRIATURA")
				materiales += visorGeneral.GenerarMateriales(info.original.materiales);

			visorGeneral.SetDescripcion(encabezado, materiales, visorGeneral.GenerarEfectos(efectos), info.original.efecto);
		}


		public void MostrarDescripcionExtendida() {
			/*
						if (cartaActual == null)
							return;

						CartaInfo info = cartaActual.GetComponent<CartaInfo>();
						VisorDescripcion manager = GetComponent<VisorDescripcion>();
						LectorHabilidades lector = LectorHabilidades.GetInstancia();
						manager.EliminarDescripciones();

						int diferencia = 100;
						int inicial = 220;
						int posX = 130;
						int pos = 0;
						GameObject lienzo = GameObject.Find("Lienzo_efectos");

						EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
						Fisica fisica = conocimiento.traerFisica();
						bool enCampo = false;
						enCampo = enCampo || fisica.TraerCartasEnCampo(1).Contains(info.gameObject);
						enCampo = enCampo || fisica.TraerCartasEnCampo(2).Contains(info.gameObject);
						List<string> habilidades = new List<string>();

						if (enCampo)
							habilidades = info.habilidades;
						else
							habilidades = info.original.datoCriatura.habilidades;

						// Descripcion de habilidades
						if (info.original.clase == "CRIATURA" && habilidades != null)

							foreach (string habilidad in habilidades) {
								if (!habilidad.StartsWith("especial")) {
									GameObject x = manager.CrearDescripcion(lector.GetDescripcion(habilidad), new Vector3(posX, diferencia*pos +inicial, 0), lienzo.transform);
									pos--;
									x.GetComponentInChildren<Image>().color = Color.cyan;
								}
							}

						// Ambientacion
						if (info.original.ambientacion != null) {
							GameObject x = manager.CrearDescripcion(info.original.ambientacion, new Vector3(posX, diferencia*pos +inicial, 0), lienzo.transform);
							pos--;
							x.GetComponentInChildren<Image>().color = Color.green;
						}

						// Contadores de supervivevcia
						if (info.traerContadores("supervivencia") > 0) {
							GameObject x = manager.CrearDescripcion("Contadores de supervivencia: " + info.traerContadores("supervivencia"), new Vector3(posX, diferencia*pos +inicial, 0), lienzo.transform);
							pos--;
							x.GetComponentInChildren<Image>().color = Color.yellow;
						}

						// Contadores de supervivevcia
						if (info.traerContadores("veneno") > 0) {
							GameObject x = manager.CrearDescripcion("Contadores de veneno: " + info.traerContadores("veneno"), new Vector3(posX, diferencia*pos +inicial, 0), lienzo.transform);
							pos--;
							x.GetComponentInChildren<Image>().color = Color.yellow;
						}

						// Contadores de poder
						if (info.traerContadores("poder") > 0) {
							GameObject x = manager.CrearDescripcion("Contadores de poder: " + info.traerContadores("poder"), new Vector3(posX, diferencia*pos +inicial, 0), lienzo.transform);
							pos--;
							x.GetComponentInChildren<Image>().color = Color.yellow;
						}

						// Contadores de poder
						if (info.traerContadores("debilidad") > 0) {
							GameObject x = manager.CrearDescripcion("Contadores de debilidad: " + info.traerContadores("debilidad"), new Vector3(posX, diferencia*pos +inicial, 0), lienzo.transform);
							pos--;
							x.GetComponentInChildren<Image>().color = Color.yellow;
						}*/

		}


	}

}