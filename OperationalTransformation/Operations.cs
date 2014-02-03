using System;
using System.Collections.Generic;
using OperationalTransformation.Builders;

namespace OperationalTransformation
{
	public class Operations
	{
		private readonly OperationsTableBuilder _operationsTableBuilder;

		public Operations()
		{
			_operationsTableBuilder = new OperationsTableBuilder();
		}

		public List<string> GetOperations(string original, string changes)
		{
			var originalLength = original.Length;
			var changesLength = changes.Length;
			
			var editsTable = _operationsTableBuilder.Build(original, changes);

			var operations = new List<string>();
			var operationsString = editsTable[changesLength][originalLength].ToString();
			if (string.IsNullOrEmpty(operationsString)) return operations;

			for (var i = operationsString.Length - 1; i >= 0; i -= 2)
			{
				operations.Add(string.Format("{0}{1}", operationsString[i - 1], operationsString[i]));
			}

			return operations;
		}

		public static OperationType Type(string operation)
		{
			switch (operation[0])
			{
				case 'r':
					return OperationType.Retain;
				case 'd':
					return OperationType.Delete;
				case 'i':
					return OperationType.Insert;
				default:
					throw new Exception(string.Format("Invalid edit type {0}", operation));
			}
		}

		public static string Insert(char val)
		{
			return string.Format("i{0}", val);
		}

		public static string Delete(char val)
		{
			return string.Format("d{0}", val);
		}

		public static string Retain(int n)
		{
			return string.Format("r{0}", + n);
		}
	}
}