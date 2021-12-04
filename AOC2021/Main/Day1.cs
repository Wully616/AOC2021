using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2021.Main;

namespace AOC2021 {
	public class Day1 {
		static void Main( string[] args ) {
		}

		public static List<int> ParseDepthData(string data) {
			return data.StringToRows().StringToInts();
		}
		public static int GetDepthIncreases(string data) {
			int depth = Int32.MaxValue;
			int count = 0;

			foreach (var i in ParseDepthData(data)) {
				count += i > depth ? 1 : 0;
				depth = i;
			}

			return count;
		}

		public static int GetSlidingWindowDepthIncreases(string data) {
			int depth = Int32.MaxValue;
			int count = 0;
			List<int> depths = ParseDepthData(data);
			for (int i = 0; i <= depths.Count-3; i++) {
				int window = depths[i] + depths[i + 1] + depths[i + 2];
				count += window > depth ? 1 : 0;
				depth = window;
			}

			return count;
		}
	}
}
