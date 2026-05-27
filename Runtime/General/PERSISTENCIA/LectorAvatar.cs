using Ging1991.Dialogos;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using Ging1991.Persistencia.Lectores.Demandas;
using UnityEngine;

namespace Bounds.Persistencia {

	public class LectorAvatar {

		private readonly LectorPorDemandaImagen lectorAvatar;
		private readonly LectorPorDemandaImagen lectorMiniatura;
		private static LectorAvatar instancia;

		public static LectorAvatar GetInstancia() {
			if (instancia == null)
				instancia = new LectorAvatar();
			return instancia;
		}


		private LectorAvatar() {
			lectorAvatar = new LectorPorDemandaImagen(new DireccionRecursos("IMAGENES\\AVATAR"), TipoLector.RECURSOS);
			lectorMiniatura = new LectorPorDemandaImagen(new DireccionRecursos("IMAGENES\\MINIATURA"), TipoLector.RECURSOS);
		}


		public Sprite GetMiniatura(Personaje personaje) {
			return lectorMiniatura.Leer(personaje.ToString().ToLower());
		}


		public Sprite GetAvatar(Personaje personaje) {
			return lectorAvatar.Leer(personaje.ToString().ToLower());
		}


	}

}