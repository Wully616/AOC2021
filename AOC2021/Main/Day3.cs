using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2021.Main;

namespace AOC2021 {
	public class Day3 {

		public static int[][] ParseDiagnosticDataFlipped(string data) {
			List<string> dataList = data.StringToRows();
			int[][] dataGrid = new int[dataList[0].Length][];
			//initialize arrays
			for (int i = 0; i < dataList[0].Length; i++) {
				dataGrid[i] = new int[dataList.Count];
			}

			//process data so columns and rows are flipped
			for (int i = 0; i < dataList.Count; i++) {
				int idx = 0;
				foreach (var col in dataList[i]) {
					if (int.TryParse(col.ToString(), out int value)) {
						//insert it into the grid the opposite way around,
						//so we have swapped rows with columns 
						dataGrid[idx][i] = value;
						idx++;
					}
				}
			}

			return dataGrid;
		}

		public static int[][] ParseDiagnosticData(string data) {
			List<string> dataList = data.StringToRows();
			int[][] dataGrid = new int[dataList.Count][];
			//initialize arrays
			for (int i = 0; i < dataList.Count; i++) {
				dataGrid[i] = new int[dataList[0].Length];
			}

			//process data so columns and rows are flipped
			for (int i = 0; i < dataList.Count; i++) {
				int idx = 0;
				foreach (var col in dataList[i]) {
					if (int.TryParse(col.ToString(), out int value)) {
						//insert it into the grid the opposite way around,
						//so we have swapped rows with columns 
						dataGrid[i][idx] = value;
						idx++;
					}
				}
			}
			// for (int i = 0; i < dataGrid.Length; i++) {
			// 	Console.WriteLine(string.Join("",dataGrid[i]));
			// }

			return dataGrid;
		}

		public static long CalculatePowerConsumption(string data) {
			int[][] dataGridFlipped = ParseDiagnosticDataFlipped(data);
			//dataGrid is flipped, so we only need to count characters in each row, rather than in the columns

			string gamma = string.Empty;
			string epsilon = string.Empty;
			for (int i = 0; i < dataGridFlipped.Length; i++) {
				//count the occurances of 1s and 0s in the row
				Tuple<int, int> leastMost = GetLeastAndMostCommonBits(dataGridFlipped[i]);
				gamma += leastMost.Item2; //gamma is most common
				epsilon += leastMost.Item1; //epsilon is least common
			}

			long gammaLong = Convert.ToInt64(gamma, 2);
			long epsilonLong = Convert.ToInt64(epsilon, 2);
			long powerConsumption = gammaLong * epsilonLong;
			Console.WriteLine($"Gamma: {gamma} - {gammaLong}. Power: {powerConsumption}");

			return powerConsumption;
		}

		public static long CalculateLifeSupportRating(string data) {
			int[][] dataGrid = ParseDiagnosticData(data);
			int[][] dataGridFlipped = ParseDiagnosticDataFlipped(data); //rows follow bit order

			//set of the dataGrid rows which match the data for o2/co2
			HashSet<int> oxygen = new HashSet<int>();
			HashSet<int> co2 = new HashSet<int>();
			//add all indexes to start with
			for (int i = 0; i < dataGrid.Length; i++) {
				oxygen.Add(i);
				co2.Add(i);
			}

			//loop through each bit
			for (int i = 0; i < dataGridFlipped.Length; i++) {
				//count the occurances of 1s and 0s in the row
				//Got the least and most values for this bit

				if (oxygen.Count > 1) {
					int[] o2bits = new int[oxygen.Count];
					int r = 0;
					foreach (int o2Idx in oxygen) {
						//get all the bits in position i for each bitstring row 
						o2bits[r++] = dataGridFlipped[i][o2Idx];
					}
					//Console.WriteLine(string.Join("",o2bits));
					Tuple<int, int> leastMost = GetLeastAndMostCommonBits(o2bits);
					int o2Flag = leastMost.Item1 == leastMost.Item2 ? 1 : leastMost.Item2;
					oxygen.RemoveWhere(o2Idx => oxygen.Count > 1 && dataGrid[o2Idx][i] != o2Flag);
				}

				if (co2.Count > 1) {
					int[] co2bits = new int[co2.Count];
					int r = 0;
					foreach ( int co2Idx in co2 ) {
						//get all the bits in position i for each bitstring row 
						co2bits[r++] = dataGridFlipped[i][co2Idx];
					}

					Tuple<int, int> leastMost = GetLeastAndMostCommonBits(co2bits);
					int co2Flag = leastMost.Item1 == leastMost.Item2 ? 0 : leastMost.Item1;
					co2.RemoveWhere(co2Idx => co2.Count > 1 && dataGrid[co2Idx][i] != co2Flag);
				}

				// Console.WriteLine($"BIT - {i}");
				// Console.WriteLine($"Oxygen");
				// foreach ( var o2idx in oxygen ) {
				//
				// 	Console.WriteLine(string.Join("", dataGrid[o2idx]));
				// }
				// Console.WriteLine($"CO2");
				// foreach ( var co2idx in co2 ) {
				//
				// 	Console.WriteLine(string.Join("", dataGrid[co2idx]));
				// }
				if (oxygen.Count == 1 && co2.Count == 1) {
					break;
				}
			}
			//look up the datagrid for our binary string using the index we found

			string oxygenStr = string.Join("", dataGrid[oxygen.ToList()[0]]);
			string co2Str = string.Join("", dataGrid[co2.ToList()[0]]);

			long oxygenLong = Convert.ToInt64(oxygenStr, 2);
			long co2Long = Convert.ToInt64(co2Str, 2);
			Console.WriteLine($"Oxygen: {oxygenStr} - {oxygenLong}. Co2: {co2Str} - {co2Long}");

			return oxygenLong * co2Long;
		}

		public static Tuple<int, int> GetLeastAndMostCommonBits(int[] bits) {
			Dictionary<int, int> counts = bits
				.GroupBy(item => item)
				.ToDictionary(item => item.Key, item => item.Count());

			int mostCommon = 0;
			int leastCommon = 0;
			int zeros = 0;
			int ones = 0;
			counts.TryGetValue(0, out zeros);
			counts.TryGetValue(1, out ones);
			if ( ones >= zeros ) {
				mostCommon = 1;
			}
			if ( ones <= zeros ) {
				leastCommon = 1;
			}
			//if both are 1, there are equal occurances
			return new Tuple<int, int>(leastCommon, mostCommon);
		}
	}
}