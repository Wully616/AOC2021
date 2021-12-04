using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2021.Main;

namespace AOC2021 {
	public class Day4 {
		public static int PlayBingo(string data, bool firstToWin) {
			BingoGame game = BingoGame.ParseBingoData(data);
			int score = 0;
			foreach (int calledNumber in game.NumbersToCall) {
				//loop over each board and mark off the called number
				for (int i = 0; i < game.BingoCards.Count; i++) {
					BingoCard card = game.BingoCards[i];
					if (!card.Complete && card.TryFindCompleteLine(calledNumber, out int unmarkedSum)) {
						card.Complete = true;
						score = unmarkedSum * calledNumber;
						Console.WriteLine(
							$"BingoCard {i+1} line found! Unmarked sum {unmarkedSum}, called number {calledNumber}, Score {score}");
						if (firstToWin) {
							return score;
						}

					}
				}
			}

			return score;
		}

		public class BingoGame {
			private List<BingoCard> bingoCards;
			private List<int> numbersToCall;

			public List<BingoCard> BingoCards => bingoCards;

			public List<int> NumbersToCall => numbersToCall;

			public BingoGame(List<BingoCard> bingoCards, List<int> numbersToCall) {
				this.bingoCards = bingoCards;
				this.numbersToCall = numbersToCall;
			}

			public static BingoGame ParseBingoData(string data) {
				List<string> dataSections = data.SplitByEmptyLine();
				//first section is the row numbers, the rest is the bingo cards
				List<int> numbers = dataSections[0].Split(',').ToList().StringToInts();
				dataSections.RemoveAt(0);
				//parse the dataSections into bingocards
				return new BingoGame(BingoCard.ParseBingoCardData(dataSections), numbers);
			}
		}

		public class BingoCard {
			private int[,] lines;
			private bool complete;
			public BingoCard(int[,] lines) {
				this.lines = lines;
				this.complete = false;
			}

			public bool Complete {
				get => complete;
				set => complete = value;
			}

			public int[,] Lines => lines;

			public bool CheckLine(int cellX, int cellY) {
				bool rowLine = true;
				bool colLine = true;
				for (int i = 0; i < 5; i++) {
					if (rowLine && lines[cellX, i] != -1) {
						rowLine = false;
					}

					if (colLine && lines[i, cellY] != -1) {
						colLine = false;
					}
				}

				return rowLine || colLine;
			}

			public bool TryFindCompleteLine(int numberCalled, out int unmarkedSum) {
				bool lineFound = false;
				unmarkedSum = 0;
				//loop along rows first and check if any are complete,
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						//mark off the cell if it matches
						if (!lineFound) {
							if (lines[x, y] == numberCalled) {
								lines[x, y] = -1;
								//check if this cell has a winning row/column
								lineFound = CheckLine(x, y);
							}
						}

						if (lines[x, y] != -1) {
							unmarkedSum += lines[x, y];
						}
					}
				}
				
				return lineFound;
			}

			public static List<BingoCard> ParseBingoCardData(List<string> cardData) {
				List<BingoCard> bingoCards = new List<BingoCard>();
				string[] seperators = new string[] {"  ", " "};

				//the card data is a 5x5 matrix, as by bingos rules
				foreach (string card in cardData) {
					List<string> rows = card.StringToRows();
					;
					int[,] dataGrid = new int[5, 5];

					for (int x = 0; x < rows.Count; x++) {
						int y = 0;
						foreach (var col in rows[x].Split(seperators, StringSplitOptions.None)) {
							if (int.TryParse(col, out int value)) {
								dataGrid[x, y] = value;
								y++;
							}
						}
					}

					bingoCards.Add(new BingoCard(dataGrid));
				}

				return bingoCards;
			}
		}
	}
}