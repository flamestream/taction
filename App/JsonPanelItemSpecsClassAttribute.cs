using System;

namespace Taction {

	internal class JsonPanelItemSpecsClassAttribute : Attribute {

		public Type value { get; private set; }

		public JsonPanelItemSpecsClassAttribute(Type value) {

			this.value = value;
		}
	}
}
