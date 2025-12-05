using UnityEngine;
using UnityEngine.UI;

public class TextoCreciente : MonoBehaviour {
    public float limite = 2;
    public float aumento = 0.01f;
    public bool iniciado = false;

    public void iniciar(string texto) {
        GetComponent<Text>().text = texto;
        iniciado = true;
    }

    void FixedUpdate() {
        if (!iniciado)
            return;
        Vector3 posicion = transform.localScale;
        posicion.x += aumento;
        posicion.y += aumento;
        if (posicion.x > limite)
            Destroy(gameObject);
        transform.localScale = posicion;
    }
}
