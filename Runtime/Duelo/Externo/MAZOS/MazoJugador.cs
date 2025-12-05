using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using Ging1991.Persistencia.Lectores.Directos;
using UnityEngine;

namespace Bounds.Global.Mazos {

	public class MazoJugador : MazoDinamico {

		private static readonly string DIRECCION = "MAZOS";

		public MazoJugador(string codigo) : base(new DireccionDinamica(DIRECCION, $"{codigo}.json").Generar(), codigo) {}

		public override void InicializarDesdeRecursos(string codigo) {
			Mazo mazo = new MazoJugadorInicial(codigo);
			Debug.Log($"5 - Mazo inicial creado: {mazo}");
			Inicializar(mazo.Serializar());
			Guardar();
		}


		public static string GetPredeterminado() {
			Direccion direccionCadena = new DireccionDinamica("MAZOS", "PREDETERMINADO.json");
			Direccion direccionInicial = new DireccionRecursos("MAZOS", "PREDETERMINADO.json");
			LectorCadena lectorCadena = new LectorCadena(direccionCadena.Generar(), TipoLector.DINAMICO);
			lectorCadena.InicializarDesdeRecursos(direccionInicial.Generar());
			return lectorCadena.Leer().valor;
		}
	
	
	}

}