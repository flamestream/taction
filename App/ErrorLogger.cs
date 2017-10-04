using System;
using System.IO;
using System.Text;

namespace Taction {

	internal class ErrorLogger {

		public string FilePath { get; private set; }
		public int MaxFileSize { get; private set; }
		public int TrimLineCount { get; private set; }

		public ErrorLogger(string filePath, int maxSize, int trimLineCount) {

			 FilePath = filePath;
			 MaxFileSize = maxSize;
			 TrimLineCount = trimLineCount;
		}

		public void Log(string message) {

			using (var file = new StreamWriter( FilePath, true, Encoding.UTF8)) {

				// Add header
				file.WriteLine(string.Format(@"[{0}] {1}", DateTime.Now, message));
				file.Close();
			}

			var fileInfo = new FileInfo( FilePath);
			if (fileInfo.Length > MaxFileSize)
				 Trim();
		}

		protected void Trim() {

			var lastLines = ReadEndTokens( FilePath, TrimLineCount, Encoding.UTF8, Environment.NewLine);
			using (var file = new StreamWriter( FilePath, false, Encoding.UTF8)) {

				file.Write(lastLines);
				file.Close();
			}
		}

		// From https://stackoverflow.com/a/398512/190308
		private static string ReadEndTokens(string path, Int64 numberOfTokens, Encoding encoding, string tokenSeparator) {

			int sizeOfChar = encoding.GetByteCount("\n");
			byte[] buffer = encoding.GetBytes(tokenSeparator);

			using (FileStream fs = new FileStream(path, FileMode.Open)) {
				Int64 tokenCount = 0;
				Int64 endPosition = fs.Length / sizeOfChar;

				for (Int64 position = sizeOfChar; position < endPosition; position += sizeOfChar) {
					fs.Seek(-position, SeekOrigin.End);
					fs.Read(buffer, 0, buffer.Length);

					if (encoding.GetString(buffer) == tokenSeparator) {
						tokenCount++;
						if (tokenCount == numberOfTokens) {
							byte[] returnBuffer = new byte[fs.Length - fs.Position];
							fs.Read(returnBuffer, 0, returnBuffer.Length);
							return encoding.GetString(returnBuffer);
						}
					}
				}

				// handle case where number of tokens in file is less than numberOfTokens
				fs.Seek(0, SeekOrigin.Begin);
				buffer = new byte[fs.Length];
				fs.Read(buffer, 0, buffer.Length);
				return encoding.GetString(buffer);
			}
		}
	}
}
