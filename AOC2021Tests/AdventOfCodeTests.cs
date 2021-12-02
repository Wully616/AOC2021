﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AOC2021;
using AOC2021.Main;

namespace AOC2021Tests {
	[TestClass]
	public class AdventOfCodeTests {
		[TestMethod]
		public void D1P1() {
			string example = Utils.ReadResource("Resources.d1.example.dat");
			string input = Utils.ReadResource("Resources.d1.input.dat");

			Assert.AreEqual(7, Day1.GetDepthIncreases(example));

			Console.WriteLine(Day1.GetDepthIncreases(input));
		}

		[TestMethod]
		public void D1P2() {
			string example = Utils.ReadResource("Resources.d1.example.dat");
			string input = Utils.ReadResource("Resources.d1.input.dat");
			Assert.AreEqual(5, Day1.GetSlidingWindowDepthIncreases(example));

			Console.WriteLine(Day1.GetSlidingWindowDepthIncreases(input));
		}

		[TestMethod]
		public void D2P1() {
			string example = Utils.ReadResource("Resources.d2.example.dat");
			string input = Utils.ReadResource("Resources.d2.input.dat");
			Assert.AreEqual(150, Day2.CalculatePosition(example));

			Console.WriteLine(Day2.CalculatePosition(input));
		}

		[TestMethod]
		public void D2P2() {
			string example = Utils.ReadResource("Resources.d2.example.dat");
			string input = Utils.ReadResource("Resources.d2.input.dat");
			Assert.AreEqual(900, Day2.CalculateAim(example));

			Console.WriteLine(Day2.CalculateAim(input));
		}

		[TestMethod]
		public void D3P1() {
			string example = Utils.ReadResource("Resources.d3.example.dat");
			string input = Utils.ReadResource("Resources.d3.input.dat");
		}

		[TestMethod]
		public void D3P2() {
			string example = Utils.ReadResource("Resources.d3.example.dat");
			string input = Utils.ReadResource("Resources.d3.input.dat");
		}
	}
}