using System.Collections.Generic;
using NUnit.Framework;
using OperationalTransformation.Builders;
using Shouldly;

namespace UnitTests.Builders
{
	class OperationsTableBuilderTests
	{
		[TestFixture]
		public class When_gettin_a_table_with_all_edits
		{
			private OperationsTableBuilder _operationsTableBuilder;
			private Dictionary<int, Dictionary<int, LinkedOperation>> _editsTable;

			[SetUp]
			public void SetUp()
			{
				const string original = "oXw";
				const string changes = "oXXW";
				_operationsTableBuilder = new OperationsTableBuilder();

				_editsTable = _operationsTableBuilder.Build(original, changes);
			}

			[Test]
			public void It_should_return_correct_operations_for_1_and_1()
			{
				_editsTable[1][1].Operation.ShouldBe("r1");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_and_2()
			{
				_editsTable[1][2].Operation.ShouldBe("dX");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_and_3()
			{
				_editsTable[1][3].Operation.ShouldBe("dw");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_and_1()
			{
				_editsTable[2][1].Operation.ShouldBe("iX");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_and_2()
			{
				_editsTable[2][2].Operation.ShouldBe("r1");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_and_3()
			{
				_editsTable[2][3].Operation.ShouldBe("dw");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_and_1()
			{
				_editsTable[3][1].Operation.ShouldBe("iX");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_and_2()
			{
				_editsTable[3][2].Operation.ShouldBe("iX");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_and_3()
			{
				_editsTable[3][3].Operation.ShouldBe("iX");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_and_1()
			{
				_editsTable[4][1].Operation.ShouldBe("iW");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_and_2()
			{
				_editsTable[4][2].Operation.ShouldBe("iW");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_and_3()
			{
				_editsTable[4][3].Operation.ShouldBe("iW");
			}

			[Test]
			public void It_should_convert_result_operations_to_string()
			{
				_editsTable[4][3].ToString().ShouldBe("iWdwiXr1r1");
			}
		}

		[TestFixture]
		public class When_gettin_a_table_with_all_editsfor_345x_to_abxx
		{
			private OperationsTableBuilder _operationsTableBuilder;
			private Dictionary<int, Dictionary<int, LinkedOperation>> _editsTable;

			[SetUp]
			public void SetUp()
			{
				const string original = "345x";
				const string changes = "abxx";
				_operationsTableBuilder = new OperationsTableBuilder();

				_editsTable = _operationsTableBuilder.Build(original, changes);
			}

			[Test]
			public void It_should_return_correct_operations_for_1_1_0()
			{
				_editsTable[1][1].Operation.ShouldContain("ia");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_2()
			{
				_editsTable[1][2].Operation.ShouldBe("ia");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_3()
			{
				_editsTable[1][3].Operation.ShouldBe("ia");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_4()
			{
				_editsTable[1][4].Operation.ShouldBe("ia");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_1()
			{
				_editsTable[2][1].Operation.ShouldBe("ib");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_2()
			{
				_editsTable[2][2].Operation.ShouldBe("ib");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_3()
			{
				_editsTable[2][3].Operation.ShouldBe("ib");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_4()
			{
				_editsTable[2][4].Operation.ShouldBe("ib");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_1()
			{
				_editsTable[3][1].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_2()
			{
				_editsTable[3][2].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_3()
			{
				_editsTable[3][3].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_return_correct_operations_for_3_4()
			{
				_editsTable[3][4].Operation.ShouldBe("r1");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_1()
			{
				_editsTable[4][1].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_2()
			{
				_editsTable[4][2].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_3()
			{
				_editsTable[4][3].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_return_correct_operations_for_4_4()
			{
				_editsTable[4][4].Operation.ShouldBe("ix");
			}

			[Test]
			public void It_should_convert_result_operations_to_string()
			{
				_editsTable[4][4].ToString().ShouldBe("ixr1ibd5iad4d3");
			}
		}

		[TestFixture]
		public class When_gettin_a_table_with_all_edits_for_abcd_to_a1d23
		{
			private OperationsTableBuilder _operationsTableBuilder;
			private Dictionary<int, Dictionary<int, LinkedOperation>> _editsTable;

			[SetUp]
			public void SetUp()
			{
				const string original = "abcd";
				const string changes = "a1d23";
				_operationsTableBuilder = new OperationsTableBuilder();

				_editsTable = _operationsTableBuilder.Build(original, changes);
			}

			[Test]
			public void It_should_return_correct_operations_for_1_1_0()
			{
				_editsTable[1][1].Operation.ShouldContain("r1");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_2()
			{
				_editsTable[1][2].Operation.ShouldBe("db");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_3()
			{
				_editsTable[1][3].Operation.ShouldBe("dc");
			}

			[Test]
			public void It_should_return_correct_operations_for_1_4()
			{
				_editsTable[1][4].Operation.ShouldBe("dd");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_1()
			{
				_editsTable[2][1].Operation.ShouldBe("i1");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_2()
			{
				_editsTable[2][2].Operation.ShouldBe("i1");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_3()
			{
				_editsTable[2][3].Operation.ShouldBe("i1");
			}

			[Test]
			public void It_should_return_correct_operations_for_2_4()
			{
				_editsTable[2][4].Operation.ShouldBe("i1");
			}
		}
	}
}
