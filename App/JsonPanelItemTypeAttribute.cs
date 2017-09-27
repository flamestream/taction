using System;

namespace ArtTouchPanel {

	internal class JsonPanelItemTypeAttribute : Attribute {

		public string value { get; set; }

		public JsonPanelItemTypeAttribute(string value) {

			this.value = value;
		}
	}
}
