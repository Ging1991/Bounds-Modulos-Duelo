using System.Collections.Generic;
using Bounds.Duelo;
using Bounds.Duelo.Carta;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Core;
using UnityEngine;

public class Invocador : MonoBehaviour, ISeleccionarCarta {

	private string tipoSeleccion;
	public GameObject criatura;
	public List<GameObject> materiales;

	public void IniciarInvocacionPerfecta() {
		EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
		EmblemaTurnos turnos = conocimiento.traerControlTurnos();

		if (turnos.jugadorActivo != 1 || turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
			return;

		if (!CrearPanelInvocacion()) {
			ControlDuelo.Instancia.GetComponent<GestorDeSonidos>().ReproducirSonido("FxRebote");
		}

	}


	public void Seleccionar(GameObject carta) {
		if (tipoSeleccion == "INVOCACION") {
			criatura = carta;
			EmblemaSeleccionInvocacionPerfecta.GetInstancia().Seleccionar(1, carta);
			CrearPanelMateriales();
		}
		else {
			materiales.Add(carta);
			EmblemaSeleccionMaterial.GetInstancia().Seleccionar(carta);
			CrearPanelMateriales();
		}
	}


	public void InvocacionCompletada() {
		criatura = null;
		materiales = new List<GameObject>();
	}


	public void CrearPanelMateriales() {
		if (criatura == null)
			return;

		int jugador = 1;
		if (criatura.GetComponent<CartaInfo>().original.datoCriatura.perfeccion == "REFLEJO")
			jugador = 2;

		List<GameObject> opciones = new List<GameObject>();
		List<GameObject> opcionesDisponibles = new SubCartasControladas(jugador).Generar();

		if (criatura.GetComponent<CartaInfo>().original.datoCriatura.perfeccion == "ROMPECABEZAS") {
			opcionesDisponibles = new SubCartasEnMano(jugador).Generar();


		}

		foreach (var opcionDisponible in opcionesDisponibles) {
			bool resultado = EsEnesimoMaterial(opcionDisponible, materiales.Count);

			if (materiales.Contains(opcionDisponible))
				continue;

			if (opcionDisponible == criatura)
				continue;

			if (resultado) {
				opciones.Add(opcionDisponible);
			}
		}
		CrearPanelSeleccion(opciones, "Selecciona el material a utilizar.", "MATERIAL", true);
	}


	private bool EsEnesimoMaterial(GameObject carta, int posicion) {
		CartaInfo info = criatura.GetComponent<CartaInfo>();
		return EsEnesimoMaterialOBJ(carta, posicion, info.original.materiales);
	}


	private bool EsEnesimoMaterialOBJ(GameObject carta, int posicion, List<MaterialBD> materialesOBJ) {
		MaterialBD material = materialesOBJ[posicion];
		if (material.tipo == "CLASE" && carta.GetComponent<CartaInfo>().original.clase == material.parametroClase)
			return true;
		if (material.tipo == "TIPO" && carta.GetComponent<CartaTipo>().ContieneTipo(material.parametroTipo))
			return true;
		if (material.tipo == "CARTA_ID" && CartaPerfeccion.ExtenderID(carta).Contains(material.parametroID))
			return true;
		if (material.tipo == "VECTOR_ATK" && carta.GetComponent<CartaInfo>().calcularAtaque() == material.parametroATK)
			return true;
		if (material.tipo == "VECTOR_DEF" && carta.GetComponent<CartaInfo>().calcularDefensa() == material.parametroDEF)
			return true;
		return false;
	}


	public bool CrearPanelInvocacion() {
		TieneInvocacionesPerfectas invocaciones = new TieneInvocacionesPerfectas(1);
		List<GameObject> lista = invocaciones.GetCartas();
		if (lista.Count > 0) {
			CrearPanelSeleccion(lista, "Selecciona la criatura que quieres invocar.", "INVOCACION", false);
			return true;
		}
		return false;
	}


	private void CrearPanelSeleccion(List<GameObject> opciones, string texto, string tipo, bool permanecer) {
		Fisica fisica = Fisica.Instancia;
		fisica.panel.GetComponent<PanelCartas>().Iniciar(opciones, this, 1, texto);
		fisica.panel.GetComponent<PanelCartas>().permanecer = permanecer;
		tipoSeleccion = tipo;
		if (tipo == "MATERIAL")
			fisica.panel.GetComponent<PanelCartas>().AutoSeleccionar();
	}


}