using Bounds.Cofres;
using Bounds.Duelo.Paneles;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Tinteros;
using Ging1991.Core;
using Ging1991.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Duelo.Utiles
{

	public class CuadroFinalizarDuelo : MonoBehaviour, IEjecutable
	{

		public IEjecutable accion;
		public GameObject boton;
		public GameObject tituloOBJ;
		public GameObject descripcionOBJ;
		public GameObject recompensaOBJ;
		private int cartaID = 1;
		private string rareza = "N";


		public void Iniciar(IEjecutable accion, bool gano, DatosDeCartas datos, IlustradorDeCartas ilustrador)
		{
			Bloqueador.BloquearGrupo("GLOBAL", true);
			gameObject.name = "CuadroFinalizar";
			this.accion = accion;

			if (!gano)
			{
				Ejecutar();
				recompensaOBJ.gameObject.SetActive(false);
				return;
			}

			int probabilidad = Azar<int>.GenerarEnteroEntre(1, 100);
			if (probabilidad <= 50)
				rareza = "N";
			else if (probabilidad <= 80)
				rareza = "PLA";
			else if (probabilidad <= 95)
				rareza = "ORO";
			else
				rareza = "MIT";

			cartaID = Azar<int>.GenerarEnteroEntre(1, 440);
			ITintero tintero = new TinteroBounds();
			Color tinta = tintero.GetColor($"TINTA_{rareza}");
			Color fondo = tintero.GetColor($"NIVEL_{rareza}");
			GetComponentInChildren<RecompensaDuelo>().Inicializar(cartaID, rareza, tinta, fondo, this, probabilidad, datos, ilustrador, tintero);
		}


		public void Iniciar(string titulo, string texto)
		{
			tituloOBJ.GetComponentInChildren<Text>().text = titulo;
			descripcionOBJ.GetComponentInChildren<Text>().text = texto;
		}


		public void Aceptar()
		{
			Cofre cofre = new Cofre();
			LineaReceta linea = new LineaReceta($"{cartaID}_A_{rareza}_1");
			cofre.AgregarCarta(linea);
			cofre.Guardar();

			if (accion != null)
				accion.Ejecutar();
			Destroy(gameObject);
		}


		public static bool ExistenCuadros()
		{
			return GameObject.Find("CuadroFinalizar") != null;
		}


		public void Ejecutar()
		{
			boton.SetActive(true);
		}


	}

}