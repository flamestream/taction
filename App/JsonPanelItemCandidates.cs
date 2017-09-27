using System;

namespace ArtTouchPanel {

	internal class JsonPanelItemCandidatesAttribute : Attribute {

		public Type[] values { get; set; }

		public JsonPanelItemCandidatesAttribute(params Type[] values) {

			this.values = values;
		}
	}
}
