using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bounds.Infraestructura.Constantes {

	[System.Serializable]
	public class Aura {

		[JsonConverter(typeof(StringEnumConverter))]
		public enum SubTipo {
			[EnumMember(Value="ESPIRITU")]
				ESPIRITU,
			[EnumMember(Value="ESTADISTICA")]
				ESTADISTICA,
			[EnumMember(Value = "TIPO")]
				TIPO,
			[EnumMember(Value="HABILIDAD")]
				HABILIDAD,
			INVOCACION
		}

	}

}