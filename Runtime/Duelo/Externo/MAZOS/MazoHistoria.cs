using Ging1991.Persistencia.Direcciones;

namespace Bounds.Global.Mazos {

	public class MazoHistoria : MazoRecursos {

		private static readonly string DIRECCION = "MAZOS/HISTORIA";

		public MazoHistoria(string codigo) : base(new DireccionRecursos(DIRECCION, codigo).Generar()) {}

	}

}