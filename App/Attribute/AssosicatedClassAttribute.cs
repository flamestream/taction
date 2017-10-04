using System;

namespace Taction.Attribute {

	internal class AssociatedClassAttribute : System.Attribute {

		public Type Value { get; private set; }

		public AssociatedClassAttribute(Type value) {

			Value = value;
		}
	}
}
