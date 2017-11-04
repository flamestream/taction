using System.Collections.Generic;
using WindowsInput.Native;

namespace Taction {

	public class KeyCommand {

		public List<VirtualKeyCode> KeyCodes { get; set; }

		public override string ToString() {

			return string.Join("+", KeyCodes);
		}
	}
}
