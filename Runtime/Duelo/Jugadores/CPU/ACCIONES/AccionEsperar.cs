using System.Collections.Generic;
using Bounds.Duelo.CPU.Condiciones;

namespace Bounds.Duelo.CPU.Acciones {
	
	public class AccionEsperar : AccionBasica {
        
		public AccionEsperar(int prioridad, int jugador) : base(prioridad, jugador) {
			condiciones = new List<ICondicionDeJuego>();
        }


        public override void Ejecutar() {
			posponer = true;
		}


	}

}