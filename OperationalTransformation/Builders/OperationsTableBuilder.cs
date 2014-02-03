using System;
using System.Collections.Generic;

namespace OperationalTransformation.Builders
{
	public class OperationsTableBuilder
	{
		private readonly EmptyOperationsTableBuilder _emptyOperationsTableBuilder;

		public OperationsTableBuilder()
		{
			_emptyOperationsTableBuilder = new EmptyOperationsTableBuilder();
		}

		public Dictionary<int, Dictionary<int, LinkedOperation>> Build(string original, string changes)
		{
			var operationsTable = _emptyOperationsTableBuilder.Build(original, changes);

			for (var i = 1; i <= changes.Length; i++)
			{
				for (var j = 1; j <= original.Length; j++)
				{
					ChooseCell(operationsTable, i, j, original, changes);
				}
			}

			return operationsTable;
		}

		private void ChooseCell(IDictionary<int, Dictionary<int, LinkedOperation>> operationsTable, int x, int y, string original, string changes)
		{
			var prevEdits = operationsTable[x][y - 1];
			var direction = Direction.Up;

			if (operationsTable[x - 1][y].Length < prevEdits.Length)
			{
				prevEdits = operationsTable[x - 1][y];
				direction = Direction.Left;
			}

			if (operationsTable[x - 1][y - 1].Length < prevEdits.Length)
			{
				prevEdits = operationsTable[x - 1][y - 1];
				direction = Direction.Diagonal;
			}
			switch (direction)
			{
				case Direction.Left:
					operationsTable[x][y] = BuildLinkedOperation(Operations.Insert(changes[x - 1]), prevEdits);
					break;
				case Direction.Up:
					operationsTable[x][y] = BuildLinkedOperation(Operations.Delete(original[y - 1]), prevEdits);
					break;
				case Direction.Diagonal:
					if (original[y - 1] == changes[x - 1])
						operationsTable[x][y] = BuildLinkedOperation(Operations.Retain(1), prevEdits);
					else
						operationsTable[x][y] = BuildLinkedOperation(Operations.Insert(changes[x - 1]), BuildLinkedOperation(Operations.Delete(original[y - 1]), prevEdits));
					break;
				default:
					throw new Exception("Unknown direction");
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

	enum Direction
	{
		Up,
		Left,
		Diagonal
	}
}