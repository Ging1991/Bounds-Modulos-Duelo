using System.Collections.Generic;
using Bounds.Persistencia.Datos;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;

namespace Bounds.Persistencia.Lectores {

	public class LectorDescagables : LectorGenerico<LectorDescagables.ListaDescagableBD>{

		public LectorDescagables() : base(new DireccionRecursos("Descargables", "Cartas").Generar(), TipoLector.RECURSOS) {}

		[System.Serializable]
		public class ListaDescagableBD {

			public List<DescagableBD> valor;
		}

	}

}