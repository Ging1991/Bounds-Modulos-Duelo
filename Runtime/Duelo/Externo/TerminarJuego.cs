using UnityEngine;
using Bounds.Modulos.Cartas.Ilustradores;
using System.Collections;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Cartas.Persistencia;
using Ging1991.Core.Interfaces;
using Bounds.Persistencia;

namespace Bounds.Duelo {

	public class TerminarJuego : MonoBehaviour, IEjecutable {

		public ISelector<string, Sprite> ilustradorDeCartas;
		public bool haGanado;

		public void Ejecutar() {
			ControlDuelo.Instancia.finalizarDuelo.FinalizarDuelo(haGanado ? 1 : 2);
		}


		public void Terminar(bool ganar) {
			GameObject cpu = GameObject.Find("CPU");
			if (cpu != null)
				Destroy(cpu);
			StartCoroutine(MostrarCuadroRecompensas(ganar));
		}


		IEnumerator MostrarCuadroRecompensas(bool haGanado) {
			this.haGanado = haGanado;
			ControlDuelo controlDuelo = GameObject.FindAnyObjectByType<ControlDuelo>();
			Billetera billetera = controlDuelo.billetera;
			ilustradorDeCartas = controlDuelo.ilustradorDeCartas;

			yield return new WaitForSeconds(1);
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Instanciador instanciador = conocimiento.traerInstanciador();
			GameObject cuadro = instanciador.CrearCuadroFinalizarDuelo(new Vector3(0, 0, 0));

			if (haGanado) {
				BloqueJugador bloque1 = BloqueJugador.getInstancia("BloqueJugador" + 1);
				cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar("Resultado: VICTORIA", $"Recompensa ${bloque1.vida / 10}");
				cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar(this, true, DatosDeCartas.Instancia, ilustradorDeCartas);
				billetera.GanarOro(bloque1.vida / 10);
			}
			else {
				cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar("Resultado: DERROTA", "Recompensa $100");
				cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar(this, false, DatosDeCartas.Instancia, ilustradorDeCartas);
				billetera.GanarOro(100);
			}
		}

	}

}