using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2021.Main;

namespace AOC2021 {
	public class Day2 {
		public class Instruction {
			private int value;
			private Direction direction;

			public int Value => value;

			public Direction Direction => direction;

			public Instruction(int value, Direction direction) {
				this.value = value;
				this.direction = direction;
			}
		}

		public enum Direction {
			forward,
			back,
			down,
			up
		}

		public static List<Instruction> ParseInstructionData(string data) {
			List<Instruction> instructions = new List<Instruction>();
			List<string> rows = data.StringToRows();
			foreach (string row in rows) {
				string[] cols = row.Split(' ');
				if (Direction.TryParse(cols[0], out Direction dir) && int.TryParse(cols[1], out int value)) {
					instructions.Add(new Instruction(value, dir));
				}
			}

			return instructions;
		}

		public static int CalculatePosition(string data) {

			int horizontal = 0;
			int depth = 0;

			foreach (Instruction instruction in ParseInstructionData(data)) {
				switch ( instruction.Direction) {
					case Direction.forward:
						horizontal += instruction.Value;
						break;
					case Direction.down:
						depth += instruction.Value;
						break;
					case Direction.up:
						depth -= instruction.Value;
						break;
					default:
						break;
				}	
			}

			return horizontal * depth;
		}

		public static int CalculateAim( string data ) {

			int horizontal = 0;
			int depth = 0;
			int aim = 0;
			foreach ( Instruction instruction in ParseInstructionData(data) ) {
				switch ( instruction.Direction ) {
					case Direction.forward:
						horizontal += instruction.Value;
						depth += aim * instruction.Value;
						break;
					case Direction.down:
						aim += instruction.Value;
						break;
					case Direction.up:
						aim -= instruction.Value;
						break;
					default:
						break;
				}
			}

			return horizontal * depth;
		}

	}
}