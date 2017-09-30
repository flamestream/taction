using System;

namespace Taction.CustomAttribute {

	internal class JsonStringTypeValueAttribute : Attribute {

		public string value { get; set; }

		public JsonStringTypeValueAttribute(string value) {

			this.value = value;
		}
	}
}
