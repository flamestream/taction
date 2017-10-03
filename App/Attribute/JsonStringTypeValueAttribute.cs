using System;

namespace Taction.Attribute {

	internal class JsonStringTypeValueAttribute : System.Attribute {

		public string value { get; set; }

		public JsonStringTypeValueAttribute(string value) {

			this.value = value;
		}
	}
}
