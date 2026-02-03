using Bounds.Persistencia.Parametros;
using Ging1991.Persistencia.Direcciones;

namespace Bounds.Modulos.Duelo {

	public class ParametrosControlDuelo : ParametrosControl {

		public override void SetParametros() {
			parametros.direcciones["DIRECCION_NOMBRES"] = new DireccionRecursos("Cartas", "Nombres").Generar();
			parametros.direcciones["CONFIGURACION"] = new DireccionDinamica("CONFIGURACION", "CONFIGURACION.json").Generar();
			parametros.direcciones["BILLETERA"] = new DireccionDinamica("CONFIGURACION", "BILLETERA.json").Generar();
		}

	}

}