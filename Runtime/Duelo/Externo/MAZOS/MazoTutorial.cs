using Ging1991.Persistencia.Direcciones;

namespace Bounds.Global.Mazos {

	public class MazoTutorial : MazoRecursos {

		private static readonly string DIRECCION = "MAZOS/TUTORIAL";

		public MazoTutorial(string codigo) : base(new DireccionRecursos(DIRECCION, codigo).Generar()) {}

	}

}