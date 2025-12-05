using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Fila.Fases.Subfases {

	public class SubfaseCalculoDeDaño : IFase {


		public SubfaseCalculoDeDaño() { }

		void IFase.Resolver() {
			EmblemaAtaque.ResolverCombate();
		}

	}

}