using UnityEngine;
using Bounds.Duelo.Pila.Subefectos;
using System.Collections.Generic;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Pila.Efectos {

	public class EfectoSeleccion : EfectoBase, ISeleccionarCarta {

		private readonly ISubSobreCarta subefecto;
		private readonly ISubListaDeCartas lista;
		private readonly string texto;

		public EfectoSeleccion(GameObject fuente, ISubSobreCarta subefecto, ISubListaDeCartas lista, string texto) : base(fuente) {
			this.lista = lista;
			this.subefecto = subefecto;
			this.texto = texto;
		}


		public override void Resolver() {
			GameObject.FindAnyObjectByType<PilaReloj>().Pausar();
			Fisica fisica = Fisica.Instancia;
			List<GameObject> opciones = lista.Generar();
			CartaInfo infoFuente = fuente.GetComponent<CartaInfo>();

			if (opciones.Count > 0) {
				if (infoFuente.controlador == 2) {
					Seleccionar(opciones[0]);
				}
				else {
					PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
					panel.Iniciar(opciones, this, texto: this.texto);
				}

			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
			}

		}


		public void Seleccionar(GameObject carta) {
			subefecto.AplicarEfecto(carta);
			GameObject.FindAnyObjectByType<PilaReloj>().Reanudar();
		}

	}

}