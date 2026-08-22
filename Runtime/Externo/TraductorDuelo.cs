using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Idiomas;
using UnityEngine;

namespace Bounds.Modulos.Duelo {

	public class TraductorDuelo : MonoBehaviour, ITraductorEspecial {

		public EmblemaTurnos emblemaTurnos;

		public string Traducir(string clave, string traduccionParcial) {
			if (clave == "TURNO_N")
				return traduccionParcial.Replace("[N]", $"{emblemaTurnos.turnos}");
			if (clave == "VIDA_N_1")
				return traduccionParcial.Replace("[N]", $"{BloqueJugador.getInstancia("BloqueJugador1").vida}");
			if (clave == "VIDA_N_2")
				return traduccionParcial.Replace("[N]", $"{BloqueJugador.getInstancia("BloqueJugador2").vida}");
			if (clave == "MAZO_N_1")
				return traduccionParcial.Replace("[N]", $"{new SubCartasEnMazo(1).Generar().Count}");
			if (clave == "MAZO_N_2")
				return traduccionParcial.Replace("[N]", $"{new SubCartasEnMazo(2).Generar().Count}");
			if (clave == "DESCARTE_N_1")
				return traduccionParcial.Replace("[N]", $"{new SubCartasEnCementerio(1).Generar().Count}");
			if (clave == "DESCARTE_N_2")
				return traduccionParcial.Replace("[N]", $"{new SubCartasEnCementerio(2).Generar().Count}");
			if (clave == "MATERIALES_N_1")
				return traduccionParcial.Replace("[N]", $"{Fisica.Instancia.TraerCartasEnMateriales(1).Count}");
			if (clave == "MATERIALES_N_2")
				return traduccionParcial.Replace("[N]", $"{Fisica.Instancia.TraerCartasEnMateriales(2).Count}");

			return "Falta traduccion especial.";
		}

	}

}