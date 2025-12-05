using Bounds.Duelo.Emblema;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bounds.Duelo.Carta {

	public class CartaArrastrar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

		private Vector3 ultimaPosicion;
		public static GameObject carta;
		public static bool jugado = false;
		private CanvasGroup lienzo;

		void Awake() {
			lienzo = GetComponent<CanvasGroup>();
		}

		public void OnBeginDrag(PointerEventData eventData) {

			if (CuadroFinalizarDuelo.ExistenCuadros())
				return;

			jugado = false;
			ultimaPosicion = transform.localPosition;
			carta = gameObject;
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			fisica.BloquearCartasEnCampo(false);
			lienzo.blocksRaycasts = false; // 🔑 la carta deja de interceptar raycasts
		}


		public void OnDrag(PointerEventData eventData) {
			if (CuadroFinalizarDuelo.ExistenCuadros())
				return;

			Vector3 punto = Input.mousePosition;
			punto.z = 100.0f;
			transform.position = Camera.main.ScreenToWorldPoint(punto);
		}


		public void OnEndDrag(PointerEventData eventData) {
			lienzo.blocksRaycasts = true; // vuelve a bloquear raycasts
			if (CuadroFinalizarDuelo.ExistenCuadros())
				return;

			carta = null;
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			fisica.BloquearCartasEnCampo(true);
			if (!jugado) {
				CartaMovimiento movimiento = GetComponent<CartaMovimiento>();
				movimiento.Desplazar(ultimaPosicion);
			}
		}


	}

}