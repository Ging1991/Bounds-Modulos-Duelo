using Ging1991.Persistencia.Direcciones;

namespace Bounds.Global.Mazos {

	public class MazoEntrenamiento : MazoRecursos {

		private static readonly string DIRECCION = "MAZOS/OPONENTES/";

		public MazoEntrenamiento(string codigo) : base(new DireccionRecursos(DIRECCION, codigo).Generar()) {}

	}

}