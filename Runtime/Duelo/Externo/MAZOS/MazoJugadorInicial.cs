using Ging1991.Persistencia.Direcciones;

namespace Bounds.Global.Mazos {

	public class MazoJugadorInicial : MazoRecursos {

		private static readonly string DIRECCION = "MAZOS";

		public MazoJugadorInicial(string codigo) : base(new DireccionRecursos(DIRECCION, codigo).Generar()) {}


	}

}