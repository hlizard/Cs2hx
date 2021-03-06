﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cs2hx
{
	static class WriteLiteralExpression
	{
		public static void Go(HaxeWriter writer, LiteralExpressionSyntax expression)
		{
			var str = expression.ToString();

			if (str.StartsWith("@"))
				str = "\"" + str.RemoveFromStartOfString("@\"").RemoveFromEndOfString("\"").Replace("\\", "\\\\").Replace("\"\"", "\\\"") + "\"";
			
			if (str.StartsWith("'") && str.EndsWith("'"))
			{
				//chars just get written as integers

				str = str.Substring(1, str.Length - 2);

				if (str.StartsWith("\\"))
					str = str.Substring(1);

				if (str.Length != 1)
					throw new Exception("Unexpected char string: " + str);
				str = ((int)str[0]).ToString();
			}

			if (str.EndsWith("f") && !str.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				str = str.Substring(0, str.Length - 1);


			writer.Write(str);
		}

        public static string FromObject(object obj)
        {
            if (obj == null)
                return "null";
            else if (obj is bool)
                return obj.ToString().ToLower();
            else if (obj is string)
                return "\"" + obj.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
            else
                return obj.ToString();

        }

	}
}
