using System.Collections.Generic;

namespace Bounds.Persistencia.Datos {

	[System.Serializable]
	public class MazoBD {
		
		public string codigo;
		public string nombre;
		public string formato;
		public List<string> cartas;
		public string principalVacio;
		public string emblema;
		public string protector;

	}

}