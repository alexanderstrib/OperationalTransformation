using System;
using System.Collections.Generic;

namespace OperationalTransformation
{
	public class OperationalTransformation : IOperationalTransformation
	{
		private readonly Operations _operations;
		private readonly OperationsExecutor _operationsExecutor;
		private List<string> _operationsAPrime;
		private List<string> _operationsBPrime;
		private int _indexA;
		private int _indexB;

		public OperationalTransformation()
		{
			_operations = new Operations();
			_operationsExecutor = new OperationsExecutor();
		}

		public string Transform(string original, string changesA, string changesB)
		{
			_indexA = _indexB = 0;
			_operationsAPrime = new List<string>();
			_operationsBPrime = new List<string>();
			var operationsA = _operations.GetOperations(original, changesA);
			var operationsB = _operations.GetOperations(original, changesB);

			Xform(operationsA, operationsB);

			var documentA = _operationsExecutor.Execute(operationsA, original);
			var documentAFinal = _operationsExecutor.Execute(_operationsBPrime, documentA);

			var documentB = _operationsExecutor.Execute(operationsB, original);
			var documentBFinal = _operationsExecutor.Execute(_operationsAPrime, documentB);

			if (documentAFinal != documentBFinal)
				throw new Exception("Transformed documents are not the same");
			
			return documentAFinal;
		}

		public void Xform(List<string> operationsA, List<string> operationsB)
		{
			var xformTable = BuildXformTable();

			while (_indexA < operationsA.Count && _indexB < operationsB.Count)
			{
				var editA = operationsA[_indexA];
				var editB = operationsB[_indexB];
				
				var operationAType = Operations.Type(editA);
				var operationBType = Operations.Type(editB);
				Action<string, string, int, int> xformer = xformTable[operationAType][operationBType];

				if (xformer != null)
					xformer(editA, editB, _indexA, _indexB);
				else
					throw new Exception(string.Format("Unknown combination to transform: {0} {1}", operationAType, operationBType));
			}

			for (; _indexA < operationsA.Count; _indexA++)
			{
				_operationsAPrime.Add(operationsA[_indexA]);
				_operationsBPrime.Add(Operations.Retain(1));

			}
		
			for (; _indexB < operationsB.Count; _indexB++)
			{
				_operationsBPrime.Add(operationsB[_indexB]);
				_operationsAPrime.Add(Operations.Retain(1));
			}
		}

		private void AddOperation(string aPrime, string bPrime, int newIndexA, int newIndexB)
		{
			_indexA = newIndexA;
			_indexB = newIndexB;
			if (!string.IsNullOrEmpty(aPrime))
				_operationsAPrime.Add(aPrime);
			if (!string.IsNullOrEmpty(bPrime))
				_operationsBPrime.Add(bPrime);
		}

		private Dictionary<OperationType, Dictionary<OperationType, Action<string, string, int, int>>> BuildXformTable()
		{
			var xformTable = new Dictionary<OperationType, Dictionary<OperationType, Action<string, string, int, int>>>();
			xformTable[OperationType.Retain] = new Dictionary<OperationType, Action<string, string, int, int>>();
			xformTable[OperationType.Retain] = new Dictionary<OperationType, Action<string, string, int, int>>();
			xformTable[OperationType.Delete] = new Dictionary<OperationType, Action<string, string, int, int>>();
			xformTable[OperationType.Insert] = new Dictionary<OperationType, Action<string, string, int, int>>();
			xformTable[OperationType.Retain][OperationType.Retain] = RetainRetain;
			xformTable[OperationType.Delete][OperationType.Delete] = DeleteDelete;
			xformTable[OperationType.Insert][OperationType.Insert] = InsertInsert;
			xformTable[OperationType.Retain][OperationType.Delete] = RetainDelete;
			xformTable[OperationType.Delete][OperationType.Retain] = DeleteRetain;
			xformTable[OperationType.Insert][OperationType.Retain] = InsertRetain;
			xformTable[OperationType.Insert][OperationType.Retain] = InsertRetain;
			xformTable[OperationType.Retain][OperationType.Insert] = RetainInsert;
			xformTable[OperationType.Insert][OperationType.Delete] = InsertDelete;
			xformTable[OperationType.Delete][OperationType.Insert] = DeleteInsert;

			return xformTable;
		}

		private void RetainRetain(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(editA, editB, indexA + 1, indexB + 1);
		}

		private void DeleteDelete(string editA, string editB, int indexA, int indexB)
		{
			if (OperationsAreEqual(editA, editB))
			{
				AddOperation(null, null, indexA + 1, indexB + 1);
			}
			else
			{
				throw new Exception(string.Format("Mismatch state of document: delete({0}) != delete({1})", editA, editB));
			}
		}

		private static bool OperationsAreEqual(string editA, string editB)
		{
			return editA == editB;
		}

		private void InsertInsert(string editA, string editB, int indexA, int indexB)
		{
			if (OperationsAreEqual(editA, editB))
				AddOperation(Operations.Retain(1), Operations.Retain(1), indexA + 1, indexB + 1);
			else
				AddOperation(editA, Operations.Retain(1), indexA + 1, indexB);
		}

		private void RetainDelete(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(null, editB, indexA + 1, indexB + 1);
		}

		private void DeleteRetain(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(editA, null, indexA + 1, indexB + 1);
		}

		private void InsertRetain(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(editA, editB, indexA + 1, indexB);
		}

		private void RetainInsert(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(editA, editB, indexA, indexB + 1);
		}

		private void InsertDelete(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(editA, Operations.Retain(1), indexA + 1, indexB);
		}

		private void DeleteInsert(string editA, string editB, int indexA, int indexB)
		{
			AddOperation(Operations.Retain(1), editB, indexA, indexB + 1);
		}
	}
}
