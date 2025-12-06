using System.Collections.Generic;
using Bounds.Global.Mazos;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDuelo {

	private static GlobalDuelo instancia;

	private void SetEntrenamiento() {
		modo = "ENTRENAMIENTO";

		jugadorLP1 = 3000;
		jugadorLP2 = 3000;

		jugadorNombre1 = "Jugador 1";
		jugadorNombre2 = "Jugador 2";

		jugadorMiniatura1 = "LAUNIX";
		jugadorMiniatura2 = "PESADILLA";

		Bounds.Global.Mazo mazoJugador = new MazoJugador(MazoJugador.GetPredeterminado());
		mazo1 = mazoJugador.cartas;
		mazo2 = mazoJugador.cartas;
		mazoVacio1 = mazoJugador.principalVacio;
		mazoVacio2 = mazoJugador.principalVacio;
		finalizarDuelo = new Fin();
	}


	private GlobalDuelo() {
		SetEntrenamiento();
	}

	public static GlobalDuelo GetInstancia() {
		if (instancia == null)
			instancia = new GlobalDuelo();
		return instancia;
	}

	// PARAMETROS
	public string modo;
	public int jugadorLP1;
	public int jugadorLP2;
	public Sprite protector1;
	public Sprite protector2;
	public string jugadorMiniatura1;
	public string jugadorMiniatura2;
	public string jugadorNombre1;
	public string jugadorNombre2;
	public List<CartaMazo> mazo1;
	public List<CartaMazo> mazo2;
	public CartaMazo mazoVacio1;
	public CartaMazo mazoVacio2;
	public IFinalizarDuelo finalizarDuelo;

	public class Fin : IFinalizarDuelo {

		public void FinalizarDuelo(int jugadorGanador) {
			Debug.Log($"Ha ganado el jugador {jugadorGanador}");
			SceneManager.LoadScene("Test");
		}
	}

}