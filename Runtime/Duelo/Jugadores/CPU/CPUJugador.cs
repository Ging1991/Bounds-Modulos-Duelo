using System.Collections.Generic;
using Bounds.Duelo.CPU.Acciones;
using Bounds.Duelo.Fila;
using Bounds.Duelo.Pila;
using UnityEngine;

namespace Bounds.Duelo.CPU {

	public class CPUJugador : MonoBehaviour {
		
		private List<ICPUAccion> acciones;
		private PilaEfectos pila;
		private FilaFases fila;


		public void CargarAcciones() {
			int prioridad = 0;
			int jugador = 2;
			pila = GameObject.Find("Pila").GetComponent<PilaEfectos>();
			fila = GameObject.Find("Fila").GetComponent<FilaFases>();
			acciones = new List<ICPUAccion>
            {
                new AccionEsperar(prioridad, jugador),
                new AccionInvocacionPerfecta(prioridad, jugador),
                new AccionInvocacionNormal(prioridad, jugador),
                new AccionJugarCarta(prioridad, jugador, "EQUIPO"),
                new AccionJugarCarta(prioridad, jugador, "MISION"),
                new AccionEquipar(prioridad, jugador),
                new AccionJugarCarta(prioridad, jugador, "TRAMPA"),
                new AccionJugarCarta(prioridad, jugador, "VACIO"),
                new AccionJugarCarta(prioridad, jugador, "HECHIZO"),
                new AccionJugarAura(prioridad, jugador),
                new AccionActivarEfecto(prioridad, jugador),
                new AccionFaseBatalla(prioridad, jugador),
                new AccionAtacar(prioridad, jugador),
                new AccionTerminarTurno(prioridad, jugador)
            };
		}


		public void Jugar() {
			if (pila.EstaVacia() && fila.EstaVacia()) {
				foreach (ICPUAccion accion in acciones) {
					if (accion.PuedeEjecutar()) {
						accion.Ejecutar();
						break;
					}
				}
			}
		}


		public void ReiniciarAcciones() {
			foreach (ICPUAccion accion in acciones) {
				accion.SetPosponer(false);
			}
		}


	}

}