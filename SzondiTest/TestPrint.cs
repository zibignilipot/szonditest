using System;
using NUnit.Framework;
using SzondiTest;

namespace SzondiTestUnitTests
{
	[TestFixture]
	public class TestPrint
	{
		[Test]
		public void TestVectorToString()
		{
			var sb = new System.Text.StringBuilder();
			var vectorReaction = new VectorReaction("±", "-", Vectors.Sch);
			
			SzondiCounts.AppendWriteSingleVectorialImage(sb, Vectors.Sch.ToString(),
			                                             Factors.k, 2, 2, Factors.p, 0, 2);
			
			string expected = "Sch(k±, p-) ";
			string result = sb.ToString();
			Assert.IsTrue(result.Equals(expected));
		}
		
		[Test]
		public void TestSingleChoiceReactionToString()
		{
			string result;
			string expected = "±";
			var factorReaction = new FactorReaction("±");
			
			result =SzondiCounts.SingleChoiceReactionToString(
				factorReaction.PositivTendenz, factorReaction.NegativTendenz);
			Assert.IsTrue(result.Equals(expected));
		}
		
		[Test]
		public void TestFactorReactionToString()
		{
			string[] expectedValues = {"0", "+", "-", "±",
									"!±", "±!", 
									"+!", "+!!", "+!!!", 
									"-!", "-!!", "-!!!"
								};
			foreach(string expected in expectedValues)
			{
				var factorReaction = new FactorReaction(expected);
				string result = factorReaction.ToString();
				Assert.IsTrue(result.Equals(expected));
			}		
		}
	}
}
