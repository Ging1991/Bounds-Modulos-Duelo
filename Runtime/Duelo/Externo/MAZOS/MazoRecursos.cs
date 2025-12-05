namespace Bounds.Global.Mazos {

	public abstract class MazoRecursos : Mazo {

		public MazoRecursos(string direccion) {
			LectorRecursos lector = new LectorRecursos(direccion);
			Inicializar(lector.Leer());
		}


	}

}