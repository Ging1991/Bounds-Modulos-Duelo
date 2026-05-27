using System.Collections.Generic;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;

namespace Bounds.Duelo.CPU.Acciones {
	
	public class AccionFaseBatalla : AccionBasica {


		public AccionFaseBatalla(int prioridad, int jugador) : base(prioridad, jugador) {
			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_PRINCIPAL));
        }


        public override void Ejecutar() {
			Entrada entrada = Entrada.GetInstancia();
			entrada.PresionarBotonFase();
		}


    }

}