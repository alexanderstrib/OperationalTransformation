using System.Collections.Generic;
using NUnit.Framework;
using OperationalTransformation;
using Shouldly;

namespace UnitTests
{
	public class OperationsExecutorTests
	{
		public class Given_opperations_executor
		{
			protected OperationsExecutor OperationsExecutor;
			protected string Result;
			protected string OriginalDocument;
			protected List<string> Operations;

			[SetUp]
			public void SetUp()
			{
				OperationsExecutor = new OperationsExecutor();
			}
		}

		[TestFixture]
		public class When_executing_set_of_operations_76543210_to_0123456789 : Given_opperations_executor
		{
			[SetUp]
			public new void SetUp()
			{
				OriginalDocument = "0123456789";
				Operations = new List<string>();
				Operations.AddRange("d0,d1,i7,d2,i6,d3,i5,r1,d5,d6,i3,d7,i2,d8,i1,d9,i0".Split(','));

				Result = OperationsExecutor.Execute(Operations, OriginalDocument);
			}

			[Test]
			public void It_should_return_76543210()
			{
				Result.ShouldBe("76543210");
			}
		}

		[TestFixture]
		public class When_executing_set_of_operations_hEllo__WorlD_to_Hello_world :Given_opperations_executor
		{
			[SetUp]
			public new void SetUp()
			{
				OriginalDocument = "Hello world";
				Operations = new List<string>();
				Operations.AddRange("dH,ih,de,iE,r1,r1,r1,r1,i ,dw,iW,r1,r1,r1,iD,dd,i!".Split(','));
				
				Result = OperationsExecutor.Execute(Operations, OriginalDocument);
			}

			[Test]
			public void It_should_return_hEllo__WorlD()
			{
				Result.ShouldBe("hEllo  WorlD!");
			}
		}
	}
}
