using System;

namespace Taction.Attribute {

	internal class JsonStringTypeValueAttribute : System.Attribute {

		public string Value { get; set; }

		public JsonStringTypeValueAttribute(string value) {

			Value = value;
		}
	}
}
