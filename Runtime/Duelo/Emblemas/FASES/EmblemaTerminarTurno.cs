using Bounds.Duelo.CPU;
using Bounds.Duelo.Emblema;
using Ging1991.Interfaces;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Fases {

	public class EmblemaTerminarTurno {

		public static void TerminarTurno(int jugadorActivo) {
			EmblemaFinalizarTurno.Finalizar(jugadorActivo);

			EmblemaTurnos.DesmarcarPotencialesAtaques(jugadorActivo);
			EmblemaTurnos.DesmarcarMuro(jugadorActivo);

			if (jugadorActivo == 1) {
				GameObject boton = GameObject.Find("BotonInvocacion");
				boton.GetComponent<Boton>().SetColorTexto(Color.black);
				boton.GetComponent<Boton>().SetColorBorde(Color.black);
				boton.GetComponent<Boton>().SetColorRelleno(Color.gray);
				EmblemaTurnos.GetInstancia().jugadorActivo = 2;
			}
			else {
				EmblemaTurnos.GetInstancia().jugadorActivo = 1;
				CPUReloj cpuReloj = GameObject.Find("CPU").GetComponent<CPUReloj>();
				cpuReloj.TerminarTurno();
			}
			EmblemaTurnos.GetInstancia().CambiarFase();
			EmblemaTurnos.GetInstancia().turnos++;
			EmblemaTurnos.GetInstancia().IniciarTurno();
		}


	}

}