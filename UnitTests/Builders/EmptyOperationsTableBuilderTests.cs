using System.Collections.Generic;
using NUnit.Framework;
using OperationalTransformation.Builders;
using Shouldly;

namespace UnitTests.Builders
{
	public class EmptyOperationsTableBuilderTests
	{
		[TestFixture]
		public class When_building_an_empty_table_of_operations
		{
			private Dictionary<int, Dictionary<int, LinkedOperation>> _result;

			[SetUp]
			public void SetUp()
			{
				var builder = new EmptyOperationsTableBuilder();

				_result = builder.Build("AG", "XG");
			}

			[Test]
			public void It_should_return_table_with_correct_dimensions()
			{
				_result[0][0].Operation.ShouldBeEmpty();
				_result[0][1].Length.ShouldBe(1);
				_result[0][2].Length.ShouldBe(2);
				_result[1][0].Length.ShouldBe(1);
				_result[2][0].Length.ShouldBe(2);
			}

			[Test]
			public void It_should_return_table_with_correct_values()
			{
				_result[0][0].Operation.ShouldBeEmpty();
				_result[0][1].Operation.ShouldContain("dA");
				_result[0][2].Operation.ShouldBe("dG");
				_result[1][0].Operation.ShouldBe("iX");
				_result[2][0].Operation.ShouldBe("iG");
			}
		}

		[TestFixture]
		public class When_building_an_empty_table_of_operations_for_345x_to_abxx
		{
			private Dictionary<int, Dictionary<int, LinkedOperation>> _result;

			[SetUp]
			public void SetUp()
			{
				var builder = new EmptyOperationsTableBuilder();

				_result = builder.Build("345x", "abxx");
			}

			[Test]
			public void It_should_return_table_with_correct_dimensions()
			{
				_result[0][0].Operation.ShouldBeEmpty();
				_result[0][1].Length.ShouldBe(1);
				_result[0][2].Length.ShouldBe(2);
				_result[0][3].Length.ShouldBe(3);
				_result[0][4].Length.ShouldBe(4);
				_result[1][0].Length.ShouldBe(1);
				_result[2][0].Length.ShouldBe(2);
				_result[3][0].Length.ShouldBe(3);
				_result[4][0].Length.ShouldBe(4);
			}

			[Test]
			public void It_should_return_table_with_correct_values()
			{
				_result[1][0].Operation.ShouldBe("ia");

				_result[2][0].Operation.ShouldBe("ib");

				_result[3][0].Operation.ShouldBe("ix");

				_result[4][0].Operation.ShouldBe("ix");
			}
		}
	}
}