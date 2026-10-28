using Bounds.Duelo.Carta;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Animaciones;
using UnityEngine;

namespace Bounds.Duelo.Emblema {

	public class EmblemaBarajarAlMazo {


		public static void Enviar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			fisica.EnviarHaciaDescarte(carta, info.propietario);
			//carta.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
		}


		public static void DesdeElCampo(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int jugador = info.propietario;
			fisica.EnviarHaciaMazo(carta, jugador);
			//ActivarEfectosDeDejarElCampo(carta);
		}


		public static void DesdeLaMano(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			int jugador = info.propietario;
			fisica.EnviarHaciaMazo(carta, jugador);
			//ActivarEfectosDeDejarElCampo(carta);
		}


		public static void ActivarEfectosDeDejarElCampo(GameObject carta) {

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();

		}


	}

}