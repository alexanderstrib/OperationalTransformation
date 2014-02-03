using NUnit.Framework;
using Shouldly;

namespace UnitTests
{
	public class OperationalTransformationTests
	{
		[TestFixture]
		public class When_getting_merge_result_after_two_independent_changes
		{
			[Test]
			[TestCase("xy", "y", "zxy", "zy", TestName = "It should return xy as a result of merge y and zxy documents ot top of zy")]
			[TestCase("", "qw", "1", "qw1", TestName = "It should return qw1 as a result of merge qw and 1 documents ot top of empty document")]
			[TestCase("test", "Test", "test test test", "Test test test", TestName = "It should return Test test test as a result of merge Test and test test test documents ot top of test")]
			[TestCase("Hel@o", "Hello", "Helo", "Hello", TestName = "It should return Hello as a result of merge Hello and Helo documents ot top of Hel@o")]
			[TestCase("@TEST@", "@TEST", "TEST@", "TEST", TestName = "It should return TEST as a result of merge @TEST and TEST@ documents ot top of @TEST@")]
			[TestCase("TEST", "TESTA", "ATEST", "ATESTA", TestName = "It should return ATESTA as a result of merge TESTA and ATEST documents")]
			[TestCase("TEST", "ATEST", "BTEST", "ABTEST", TestName = "It should return ABTEST as a result of merge ATEST and BTEST documents")]
			[TestCase("", "", "ABCDE", "ABCDE", TestName = "It should return ABCDE as a result of merge empty and ABCDE documents")]
			[TestCase("", "ABCDE", "", "ABCDE", TestName = "It should return ABCDE as a result of merge ABCDE and empty documents")]
			[TestCase("", "ABCDE", "12345", "ABCDE12345", TestName = "It should return ABCDE12345 as a result of merge ")]
			[TestCase("Hello world", "ABCDE", "12345", "A1B2C3D4E5", TestName = "It should return A1B2C3D4E5 as a result of merge ABCDE and 12345 on top of Hello world")]
			[TestCase("Hello world", "abcde", "12345", "abcd12345", TestName = "It should return abcd12345 as a result of merge ABCDE and 12345 on top of Hello world")]
			[TestCase("Hello world", "abocde", "12345", "ab123c4e5", TestName = "It should return ab123c4e5 as a result of merge ABCDE and 12345 on top of Hello world")]
			[TestCase("Apples are a fruit", "Apples are a fruit", "Bananas are also fruit", "Bananas are also fruit", TestName = "It should return Banans are also fruit as a result of merge")]
			public void It_should_return_correct_merged_document(string originalDocument, string changesA, string changesB, string expectedDocument)
			{
				var subject = new OperationalTransformation.OperationalTransformation();

				var actualDocument = subject.Transform(originalDocument, changesA, changesB);

				actualDocument.ShouldBe(expectedDocument);
			}
		}
	}
}

