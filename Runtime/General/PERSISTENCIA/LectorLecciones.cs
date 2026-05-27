using System.Collections.Generic;
using Ging1991.Persistencia.Lectores;
using UnityEngine;

namespace Bounds.Persistencia {

	public class LectorLecciones : LectorGenerico<LectorLecciones.DatoLecciones> {

		private readonly DatoLecciones datos;

		public LectorLecciones() : base("datos\\tutorial", TipoLector.RECURSOS) {
			datos = Leer();
		}


		public DatoLeccion TraerLeccion(int leccionID) {
			DatoLeccion ret = null;
			foreach (DatoLeccion dato in datos.lecciones)
				if (dato.indice == leccionID)
					ret = dato;
			return ret;
		}


		[System.Serializable]
		public class DatoLecciones {

			public List<DatoLeccion> lecciones;

		}

		[System.Serializable]
		public class DatoLeccion {

			public List<DatoMensaje> mensajes;
			public List<int> cartas1;
			public List<int> cartas2;
			public string titulo;
			public int indice;
			public int LP;

		}


		[System.Serializable]
		public class DatoMensaje {

			public int indice;
			public string contenido;
			
		}


	}

}