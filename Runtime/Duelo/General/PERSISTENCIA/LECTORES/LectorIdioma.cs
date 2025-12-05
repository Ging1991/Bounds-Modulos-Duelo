using System.Collections.Generic;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;

namespace Bounds.Persistencia.Lectores {

	public class LectorIdioma : LectorGenerico<LectorIdioma.ListaDatoBD> {
		
		private static readonly string DIRECCION_BASE = "IDIOMAS/SISTEMA";

		public LectorIdioma(string idioma) : base(new DireccionRecursos(DIRECCION_BASE, idioma).Generar(), TipoLector.RECURSOS) {}


		[System.Serializable]
		public class DatoBD {
			public string clave;
			public string valor;
		}

		[System.Serializable]
		public class ListaDatoBD {
			public List<DatoBD> valores;
		}

	}

}