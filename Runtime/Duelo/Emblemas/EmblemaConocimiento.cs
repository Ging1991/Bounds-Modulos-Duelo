using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Infraestructura.Visores;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblema {

	public class EmblemaConocimiento {

		private static EmblemaConocimiento instancia;

		private EmblemaConocimiento() { }


		public static EmblemaConocimiento getInstancia() {
			if (instancia == null)
				instancia = new EmblemaConocimiento();
			return instancia;
		}


		public ControlDuelo traerDuelo() {
			return GameObject.Find("Control").GetComponent<ControlDuelo>();
		}


		public EmblemaTurnos traerControlTurnos() {
			return GameObject.Find("EmblemaTurnos").GetComponent<EmblemaTurnos>();
		}


		public Fisica traerFisica() {
			return GameObject.Find("Fisica").GetComponent<Fisica>();
		}


		public Instanciador traerInstanciador() {
			return GameObject.Find("Instanciador").GetComponent<Instanciador>();
		}


		public VisorDuelo traerVisor() {
			return GameObject.Find("VisorDuelo").GetComponent<VisorDuelo>();
		}


	}

}