using System;
using System.Collections.Generic;

namespace OperationalTransformation
{
	public class OperationsExecutor
	{
		public string Execute(List<string> operations, string document)
		{
			var newDocument = string.Empty;
			foreach (var operation in operations)
			{
				switch (Operations.Type(operation))
				{
					case OperationType.Retain:
						newDocument += document[0];
						document = document.Substring(1);
						break;
					case OperationType.Insert:
						newDocument += operation[1];
						break;
					case OperationType.Delete:
						if (document[0] != operation[1])
						{
							throw new Exception("Cannot find something to delete.");
						}
						
						document = document.Substring(1);
						break;
					default:
						throw new Exception("Unknown operation: " + operation);
				}
			}

			return newDocument;
		}
	}
}