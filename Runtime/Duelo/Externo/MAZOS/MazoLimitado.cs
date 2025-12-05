using Ging1991.Persistencia.Direcciones;

namespace Bounds.Global.Mazos {

	public class MazoLimitado : MazoDinamico {

		private static readonly string DIRECCION = "MAZOS/OPONENTES/";

		public MazoLimitado(string codigo) : base(new DireccionDinamica(DIRECCION, codigo).Generar()) {}

        public override void InicializarDesdeRecursos(string direccion)
        {
            throw new System.NotImplementedException();
        }
    }

}