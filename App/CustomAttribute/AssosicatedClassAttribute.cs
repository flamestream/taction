using System;

namespace Taction.CustomAttribute {

	internal class AssociatedClassAttribute : Attribute {

		public Type value { get; private set; }

		public AssociatedClassAttribute(Type value) {

			this.value = value;
		}
	}
}
