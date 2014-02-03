namespace OperationalTransformation
{
	public interface IOperationalTransformation
	{
		string Transform(string original, string changesA, string changesB);
	}
}