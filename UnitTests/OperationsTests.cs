using System.Collections.Generic;
using NUnit.Framework;
using OperationalTransformation;
using Shouldly;

namespace UnitTests
{
	public class OperationsTests
	{
		public class Given_operations
		{
			protected List<string> Result;
			protected Operations Operations;

			[SetUp]
			public void SetUp()
			{
				Operations = new Operations();
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_AG_to_XG : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("AG", "XG");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("dA,iX,r1");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_AG_to_XG1 : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("AG", "B");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("dA,dG,iB");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_GA_to_GZ : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("GA", "GZ");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("r1,dA,iZ");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_GUMBO_to_GAMBOL : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("GUMBO", "GAMBOL");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("r1,dU,iA,r1,r1,r1,iL");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_0123456789_to_76543210 : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("0123456789", "76543210");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("d0,d1,i7,d2,i6,d3,i5,r1,d5,d6,i3,d7,i2,d8,i1,d9,i0");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_Hello_world_to_hEllo__WorlD : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("Hello world", "hEllo  WorlD!");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("dH,ih,de,iE,r1,r1,r1,r1,i ,dw,iW,r1,r1,r1,iD,dd,i!");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_given_string : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("sdfsdf 234 swejsdf a2kj342  kajsdf", "sfsdf a;klj2234 sdf s");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("r1,dd,r1,r1,r1,r1,r1,ia,i;,ik,il,ij,r1,i2,r1,r1,r1,r1,dw,de,dj,ds,r1,r1,r1,da,d2,dk,dj,d3,d4,d2,d ,d ,dk,da,dj,r1,dd,df");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_given_string2 : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("oXw sdfasdf 234234  sdfa 24 234 asdfas  23423  asdfasd 234234 sdfsdf 2342 ", "oXw sdfasdd sdg sdfg s4564sfgdsfg34 456  dsfg 45645   dfgsdgsdgf 234234  sdfa 24 234 asdfas  2342");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("r1,r1,r1,r1,r1,r1,r1,r1,r1,r1,id,i ,is,id,ig,i ,is,id,r1,ig,r1,d2,d3,is,r1,i5,i6,i4,is,if,ig,id,is,if,d2,ig,r1,r1,r1,i4,i5,i6,r1,i ,id,r1,dd,r1,da,ig,r1,d2,r1,d ,d2,i5,d3,i6,r1,i5,r1,i ,i ,id,if,da,ig,r1,r1,ig,is,id,ig,r1,da,ds,r1,d ,r1,r1,r1,r1,r1,i4,r1,r1,da,r1,r1,r1,r1,ds,dd,r1,r1,d3,r1,i ,r1,r1,r1,r1,ia,r1,r1,r1,ia,r1,dd,df,r1,i ,r1,r1,r1,r1,d ");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_Hello_world_to_abocde : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("Hello world", "abocde");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("dH,de,dl,ia,dl,ib,r1,d ,dw,do,dr,dl,ic,r1,ie");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_Hello_world_to_12345 : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("Hello world", "12345");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("dH,de,dl,dl,do,d ,dw,i1,do,i2,dr,i3,dl,i4,dd,i5");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_345x_to_abxx : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("345x", "abxx");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("d3,d4,ia,d5,ib,r1,ix");
			}
		}

		[TestFixture]
		public class When_getting_edit_opperations_for_abcd_to_a1d23 : Given_operations
		{
			[SetUp]
			public new void SetUp()
			{
				Result = Operations.GetOperations("abcd", "a1d23");
			}

			[Test]
			public void It_should_return_correct_edits_opperations()
			{
				string.Join(",", Result).ShouldBe("r1,db,dc,i1,r1,i2,i3");
			}
		}
	}
}