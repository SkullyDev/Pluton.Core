﻿namespace Pluton.Core
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public static class StringExtensions
	{
		public static string JsonPretty(this string self, string indent = "\t")
		{
			return String.Join(Environment.NewLine, self.yieldPretty(indent).ToArray()).TrimStart('\n', '\r', ' ', '\t').Replace(":", ": ");
		}

		static IEnumerable<string> yieldPretty(this string json, string indent)
		{
			int depth = 0;
			foreach (string line in json.Replace("{", "{\n").Replace("}", "\n}").Replace(",\"", ",\n\"").Replace("[", "[\n").Replace("]", "\n]").Replace("},{", "},\n{").Replace("],[", "],\n[").Split('\n')) {
				if (line.Contains("}") || line.Contains("]"))
					depth -= 1;

				yield return indent.Multiply(depth) + line;

				if (line.Contains("{") || line.Contains("["))
					depth += 1;
			}
		}

		public static string Multiply(this string self, int multiply)
		{
			if (multiply < 0)
				throw new ArgumentOutOfRangeException("multiply", multiply, $"Can't multiply a string by x{multiply}");

			string result = String.Empty;
			for (int i = 0; i < multiply; i++)
				result += self;
			return result;
		}

		public static string QuoteSafe(this string self)
		{
			self = self.Replace("\"", "\\\"");
			self = self.TrimEnd(new char[] { '\\' });
			return "\"" + self + "\"";
		}
	}
}

