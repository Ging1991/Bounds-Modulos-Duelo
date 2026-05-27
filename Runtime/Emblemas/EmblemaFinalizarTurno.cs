using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblema {

	public class EmblemaFinalizarTurno {

		public static List<GameObject> TraerCartasEnCampo(int jugador, CondicionCarta condicion) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			List<GameObject> cartasEnCampo = new List<GameObject>();
			if (jugador == 0) {
				cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(1));
				cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(2));
			}
			else {
				cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(jugador));
			}
			if (condicion != null) {
				return condicion.CumpleLista(cartasEnCampo);
			}
			return cartasEnCampo;
		}


		public static bool Finalizar(int jugadorEnTurno) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartasEnCampoActivo = fisica.TraerCartasEnCampo(jugadorEnTurno);

			// Si el jugador no tiene criaturas pierde 500 LP
			Condicion condicion = new Condicion(tipoCarta: "CRIATURA");
			if (condicion.CumpleLista(cartasEnCampoActivo).Count == 0)
				EmblemaVida.DisminuirVida(jugadorEnTurno, 500);

			// Si el jugador tiene 5 cartas el oponente pierde 500 LP
			if (cartasEnCampoActivo.Count == 5)
				EmblemaVida.DisminuirVida(JugadorDuelo.Adversario(jugadorEnTurno), 500);

			CondicionClase condicionVacio = new CondicionClase(clase: "VACIO");
			foreach (GameObject vacio in TraerCartasEnCampo(0, condicionVacio)) {
				CartaInfo info = vacio.GetComponent<CartaInfo>();

				if (info.original.datoVacio.tipo == "ACABAR_VIDA") {
					EmblemaVida.DisminuirVida(jugadorEnTurno, 500);
				}

			}

			// TRAMPAS
			PilaEfectos pila = GameObject.FindAnyObjectByType<PilaEfectos>();
			int adversario = JugadorDuelo.Adversario(jugadorEnTurno);
			CondicionClase condicionTrampa = new CondicionClase(clase: "TRAMPA");
			foreach (GameObject trampa in TraerCartasEnCampo(adversario, condicionTrampa)) {
				CartaInfo info = trampa.GetComponent<CartaInfo>();
				CartaGeneral trampaGeneral = trampa.GetComponent<CartaGeneral>();
				if (trampaGeneral.bocaArriba)
					continue;

				if (info.original.datoTrampa.tipo == "invoca_evolucion") {
					CondicionMultiple condicionMultiple = new CondicionMultiple(CondicionMultiple.Tipo.Y);
					condicionMultiple.AgregarCondicion(new CondicionEsPerfecta());
					condicionMultiple.AgregarCondicion(new CondicionCriaturaPerfeccion(perfeccion: "EVOLUCION"));
					List<GameObject> cartasEnDescarte = condicionMultiple.CumpleLista(fisica.TraerCartasEnCementerio(adversario));
					if (cartasEnDescarte.Count > 0) {
						trampaGeneral.ColocarBocaArriba();
						foreach (GameObject criatura in cartasEnDescarte) {
							pila.Agregar(new EfectoInvocacionEspecial(trampa, criatura, adversario));
						}
						foreach (GameObject criatura in TraerCartasEnCampo(jugadorEnTurno, condicionMultiple)) {
							pila.Agregar(new EfectoInvocacionEspecial(trampa, criatura, jugadorEnTurno));
						}

						break;
					}
				}
				if (info.original.datoTrampa.tipo == "invoca_fusion") {
					CondicionMultiple condicionMultiple = new CondicionMultiple(CondicionMultiple.Tipo.Y);
					condicionMultiple.AgregarCondicion(new CondicionEsPerfecta());
					condicionMultiple.AgregarCondicion(new CondicionCriaturaPerfeccion(perfeccion: "FUSION"));
					List<GameObject> cartasEnDescarte = condicionMultiple.CumpleLista(fisica.TraerCartasEnCementerio(adversario));
					if (cartasEnDescarte.Count > 0) {
						trampaGeneral.ColocarBocaArriba();
						foreach (GameObject criatura in cartasEnDescarte) {
							pila.Agregar(new EfectoInvocacionEspecial(trampa, criatura, adversario));
						}
						foreach (GameObject criatura in TraerCartasEnCampo(jugadorEnTurno, condicionMultiple)) {
							pila.Agregar(new EfectoInvocacionEspecial(trampa, criatura, jugadorEnTurno));
						}

						break;
					}
				}

			}

			return true;
		}


	}

}