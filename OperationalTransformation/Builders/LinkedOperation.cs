namespace OperationalTransformation.Builders
{
	public class LinkedOperation
	{
		public string Operation { get; set; }
		public LinkedOperation NextOperation { get; set; }
		public int Length { get; set; }

		public new string ToString() {
			var result = Operation;
			var node = NextOperation;
			while (node != null)
			{
				if (!string.IsNullOrEmpty(node.Operation)) 
					result += node.Operation;
				node = node.NextOperation;
			}
			return result;
		}
	}
}