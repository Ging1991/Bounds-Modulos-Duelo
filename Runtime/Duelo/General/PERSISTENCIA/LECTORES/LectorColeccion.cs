using System.Collections.Generic;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;

namespace Bounds.Persistencia.Lectores {

	public class LectorColeccion : LectorGenerico<LectorColeccion.DatoBD>{

		public LectorColeccion(string codigo) : base(new DireccionRecursos("COLECCIONES", codigo).Generar(), TipoLector.RECURSOS) {}

		[System.Serializable]
		public class DatoBD {

			public string codigo;
			public string titulo;
			public string nombre;
			public string emblema;
			public List<string> comunes;
			public List<string> infrecuentes;
			public List<string> raras;
			public List<string> miticas;
		}

	}

}