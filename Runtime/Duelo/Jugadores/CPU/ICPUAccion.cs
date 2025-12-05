namespace Bounds.Duelo.CPU {
	
	public interface ICPUAccion {

		int GetPrioridad();

		void SetPosponer(bool posponer);

		bool PuedeEjecutar();

		void Ejecutar();

	}

}