using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarAniquilacion : ISeleccionarCarta {

		public void Seleccionar(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;
			fisica.EnviarHaciaDescarte(carta, cartaInfo.controlador);
			EmblemaEfectos.Activar(new EfectoSobreJugador(carta, cartaInfo.controlador, new SubModificarLP(-1000)));
		}

	}

}