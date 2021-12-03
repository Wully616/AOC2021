using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2021.Main {
	public static class Utils {
		public static Stream GetEmbeddedResourceStream
			( this Assembly assembly, string relativeResourcePath ) {
			if ( string.IsNullOrEmpty(relativeResourcePath) )
				throw new ArgumentNullException("relativeResourcePath");

			var resourcePath = String.Format("{0}.{1}",
				Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$",
					string.Empty, RegexOptions.IgnoreCase), relativeResourcePath);

			var stream = assembly.GetManifestResourceStream(resourcePath);
			if ( stream == null )
				throw new ArgumentException(String.Format("The specified embedded resource \"{0}\" is not found.", resourcePath));
			return stream;
		}

		public static string ReadResource( string relativeResourcePath ) {
			string output = string.Empty;
			using ( var inputStream =
				Assembly.GetExecutingAssembly().GetEmbeddedResourceStream(relativeResourcePath) ) {
				StreamReader reader = new StreamReader(inputStream);
				output = reader.ReadToEnd();
			}

			return output;
		}
		public static List<string> StringToRows(this string data ) {
			//trim newline at the end
			data = data.Trim();
			return data.Split(
				new string[] { "\r\n", "\r", "\n" },
				StringSplitOptions.None
			).ToList();
		}
	}
}
