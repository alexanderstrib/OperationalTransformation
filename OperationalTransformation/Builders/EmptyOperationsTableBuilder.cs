using System.Collections.Generic;

namespace OperationalTransformation.Builders
{
	public class EmptyOperationsTableBuilder
	{
		public Dictionary<int, Dictionary<int, LinkedOperation>> Build(string original, string changes)
		{
			var table = CreateEmptyTable();
			AddColumnOfInserts(changes, table);
			AddRowOfDeletes(original, table);

			return table;
		}

		private static Dictionary<int, Dictionary<int, LinkedOperation>> CreateEmptyTable()
		{
			var table = new Dictionary<int, Dictionary<int, LinkedOperation>>();
			table[0] = new Dictionary<int, LinkedOperation>();
			table[0][0] = new LinkedOperation{Length = 0, Operation = string.Empty};

			return table;
		}

		private void AddRowOfDeletes(string original, IDictionary<int, Dictionary<int, LinkedOperation>> table)
		{
			for (var j = 1; j <= original.Length; j++)
			{
				table[0][j] = BuildLinkedOperation(Operations.Delete(original[j - 1]), table[0][j - 1]);
			}
		}

		private void AddColumnOfInserts(string changes, IDictionary<int, Dictionary<int, LinkedOperation>> table)
		{
			for (var i = 1; i <= changes.Length; i++)
			{
				table[i] = new Dictionary<int, LinkedOperation>();
				table[i][0] = BuildLinkedOperation(Operations.Insert(changes[i - 1]), table[i - 1][0]);
			}
		}

		private LinkedOperation BuildLinkedOperation(string operation, LinkedOperation nextOperation)
		{
			return new LinkedOperation
			{
				Operation = operation,
				NextOperation = nextOperation,
				Length = 1 + nextOperation.Length,
			};
		}
	}
}