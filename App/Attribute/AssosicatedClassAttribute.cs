using System;

namespace Taction.Attribute {

	internal class AssociatedClassAttribute : System.Attribute {

		public Type value { get; private set; }

		public AssociatedClassAttribute(Type value) {

			this.value = value;
		}
	}
}
