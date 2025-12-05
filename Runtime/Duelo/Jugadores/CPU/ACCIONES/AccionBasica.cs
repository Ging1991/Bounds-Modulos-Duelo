using System;
using System.Collections.Generic;
using Bounds.Duelo.CPU.Condiciones;

namespace Bounds.Duelo.CPU.Acciones {
	
	public abstract class AccionBasica : ICPUAccion {
		
		protected TieneLugarLibre tieneLugar;
		protected int prioridad, jugador;
		protected List<ICondicionDeJuego> condiciones;
		protected bool posponer;

		public AccionBasica(int prioridad, int jugador) {
			this.prioridad = prioridad;
			this.jugador = jugador;
			tieneLugar = new TieneLugarLibre(jugador, 1);
			posponer = false;
		}


		public int GetPrioridad() {
			return prioridad;
		}


		public virtual bool PuedeEjecutar() {
			bool ret = true;
			foreach (ICondicionDeJuego condicion in condiciones) {
				ret = ret && condicion.SeCumple();
			}
			return ret && !posponer;
		}


		public abstract void Ejecutar();


        public void SetPosponer(bool posponer) {
			this.posponer = posponer;
        }


    }

}