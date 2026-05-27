using UnityEngine;
using UnityEngine.EventSystems;

public class Arrastrable : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

	private bool estaArrastrando = false;


	public void OnBeginDrag(PointerEventData eventData) {
		estaArrastrando = true;
	}


	public void OnEndDrag(PointerEventData eventData) {
		estaArrastrando = false;
	}


	public bool EstaArrastrando() {
		return estaArrastrando;
	}


}