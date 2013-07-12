using System;
using NUnit.Framework;
using SzondiTest;
using System.Collections.Generic;

namespace SzondiTestUnitTests
{
	[TestFixture]
	public class TestBasicSzondiCounts
	{
		[Test]
		public void TestExperimentalComplementar()
		{
			TestSeries testSerie;
			
			// from buch 3 p.393, Fall 35
			var foregroundProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				new TestProfile("0,-", "0,!±", "+,0", "±,0"),
				new TestProfile("-,-", "0,!±", "+,0", "±,0"),
				new TestProfile(1, 1, 1, 3, 0, 1, 4, 2, 2, 1, 1, 1, 3, 2, 0, 1)
				//TODO add Buch3 p.209
			};
			var exComplementarProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				new TestProfile("-,-", "±,0", "+,+", "0,±"),
				new TestProfile("-,-", "±,0", "+,±", "0,-"),
				new TestProfile(1, 3, 0, 2, 2, 3, 0, 0, 3, 0, 3, 1, 1, 0, 2, 3)
			};
			
			testSerie = new TestSeries(Sex.Male, foregroundProfiles, exComplementarProfiles);
		}
		
		[Test]
		public void TestTestProfileConstructor()
		{
			string inputDescription = "S(h0, s+) P(e-, hy+/-) Sch(k0, p+) C(d-, m+/-)";
			TestProfile profileDetected = new TestProfile(inputDescription);
			
			Assert.IsTrue(profileDetected.S.firstFactorReaction.IsEqualTo("0"));
			Assert.IsTrue(profileDetected.S.secondFactorReaction.IsEqualTo("+"));
			Assert.IsTrue(profileDetected.P.firstFactorReaction.IsEqualTo("-"));
			Assert.IsTrue(profileDetected.P.secondFactorReaction.IsEqualTo("±"));
			Assert.IsTrue(profileDetected.Sch.firstFactorReaction.IsEqualTo("0"));
			Assert.IsTrue(profileDetected.Sch.secondFactorReaction.IsEqualTo("+"));
			Assert.IsTrue(profileDetected.C.firstFactorReaction.IsEqualTo("-"));
			Assert.IsTrue(profileDetected.C.secondFactorReaction.IsEqualTo("±"));
		}
		
		[Test]
		public void TestVectorEqualsTo()
		{
			// Sch(k0, p-)
			var vectorReaction = new VectorReaction("0", "-", Vectors.Sch);
			bool vectorsMatch = vectorReaction.EqualsTo("0,-");
			Assert.IsTrue(vectorsMatch);
		}
		
		[Test]
		public void TestFactorVectorMismatch()
		{
			try
			{
				// s is not in C
				var vectorReaction 
					= new VectorReaction("0", "-", Vectors.C);
				vectorReaction.HasFactorReaction("0", Factors.s);
				Assert.Fail();
			}
			catch(ArgumentException)
			{
				Assert.Pass();
			}
		}
		
		[Test]
		public void TestFactorContains()
		{
			TestFactorContainsHelper("±", "+", true);
			TestFactorContainsHelper("±", "-", true);
			TestFactorContainsHelper("±", "±", true);
			TestFactorContainsHelper("+", "+", true);
			TestFactorContainsHelper("-", "-", true);
			TestFactorContainsHelper("0", "0", true);
			
			TestFactorContainsHelper("±", "0", false);
			TestFactorContainsHelper("+", "0", false);
			TestFactorContainsHelper("-", "0", false);
			
			TestFactorContainsHelper("+", "-", false);
			TestFactorContainsHelper("+", "±", false);
			TestFactorContainsHelper("-", "+", false);
			TestFactorContainsHelper("-", "±", false);
			
			TestFactorContainsHelper("0", "+", false);
			TestFactorContainsHelper("0", "-", false);
			TestFactorContainsHelper("0", "±", false);
			
			//with hypertension
			
			#region with ambivalence
			TestFactorContainsHelper("!±", "+", true);
			TestFactorContainsHelper("!±", "-", true);
			TestFactorContainsHelper("±!", "+", true);
			TestFactorContainsHelper("±!", "-", true);
			
			TestFactorContainsHelper("!±", "+!", true);
			TestFactorContainsHelper("±!", "-!", true);
			TestFactorContainsHelper("!±", "!±", true);
			TestFactorContainsHelper("±!", "±!", true);
			
			TestFactorContainsHelper("!±", "+!!", false);
			TestFactorContainsHelper("±!", "-!!", false);
			TestFactorContainsHelper("!±", "-!", false);
			TestFactorContainsHelper("±!", "+!", false);
			TestFactorContainsHelper("!±", "0", false);
			TestFactorContainsHelper("±!", "0", false);
			#endregion
			
			#region Unitendenz with hypertension
			TestFactorContainsHelper("+!!!", "+", true);
			TestFactorContainsHelper("+!!!", "+!", true);
			TestFactorContainsHelper("+!!!", "+!!", true);
			TestFactorContainsHelper("+!!!", "+!!!", true);
			TestFactorContainsHelper("+!!", "+", true);
			TestFactorContainsHelper("+!!", "+!", true);
			TestFactorContainsHelper("+!!", "+!!", true);
			TestFactorContainsHelper("+!!", "+!!!", false);
			TestFactorContainsHelper("+!", "+", true);
			TestFactorContainsHelper("+!", "+!", true);
			TestFactorContainsHelper("+!", "+!!", false);
			TestFactorContainsHelper("+!", "+!!!", false);
			
			
			TestFactorContainsHelper("-!!!", "-", true);
			TestFactorContainsHelper("-!!!", "-!", true);
			TestFactorContainsHelper("-!!!", "-!!", true);
			TestFactorContainsHelper("-!!!", "-!!!", true);
			TestFactorContainsHelper("-!!", "-", true);
			TestFactorContainsHelper("-!!", "-!", true);
			TestFactorContainsHelper("-!!", "-!!", true);
			TestFactorContainsHelper("-!!", "-!!!", false);
			TestFactorContainsHelper("-!", "-", true);
			TestFactorContainsHelper("-!", "-!", true);
			TestFactorContainsHelper("-!", "-!!", false);
			TestFactorContainsHelper("-!", "-!!!", false);
			
			TestFactorContainsHelper("-!", "+!", false);
			#endregion
		}
		
		private void TestFactorContainsHelper(string supposedContainer, 
		                                      string supposedContained, 
		                                      bool expectedToContain)
		{
			var factorReaction1 = new FactorReaction(supposedContainer);
			var factorReaction2 = new FactorReaction(supposedContained);
			bool result = factorReaction1.Contains(factorReaction2);
			Assert.IsTrue(result == expectedToContain);
		}
		
		[Test]
		public void TestComputeLinnaeus()
		{
			Fälle.Fall18[1].partOf.ComputeLinnaeus();
			List<KeyValuePair<int , Triebklasse>> latezProportionen 
				= Fälle.Fall18[1].partOf.latezProportionen;
			
			Assert.IsTrue(latezProportionen[0].Key == 8);
			Assert.IsTrue(latezProportionen[0].Value == new Triebklasse("C", "m", "-"));
			Assert.IsTrue(latezProportionen[1].Key == 7);
			Assert.IsTrue(latezProportionen[1].Value == new Triebklasse("Sch", "p", "-"));
			Assert.IsTrue(latezProportionen[2].Key == 6);
			Assert.IsTrue(latezProportionen[2].Value == new Triebklasse("S", "h", "+"));
			Assert.IsTrue(latezProportionen[3].Key == 3);
			Assert.IsTrue(latezProportionen[3].Value == new Triebklasse("P", "hy", "-"));
		}
		
		[Test]//[Ignore]
		public void CustomTest()
		{	
			{	
				var profile1 = Fälle.Fall36[1];
				var profile2 = Fälle.Fall36[2];
				var profile9 = Fälle.Fall36[9];
				var profile7 = Fälle.Fall36[7];
				var profile10 = Fälle.Fall36[10];
				Syndromatic.BestimmungDerExistenzformen(profile1);
				Syndromatic.BestimmungDerExistenzformen(profile2);
				Syndromatic.BestimmungDerExistenzformen(profile9);
				Syndromatic.BestimmungDerExistenzformen(profile7);
				Syndromatic.BestimmungDerExistenzformen(profile10);
				System.Console.WriteLine("pause");
			}
			
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall34[1].partOf.Hintergrundprofile);
				
				var profile2 = profilesHinter[2];
				var profile5 = profilesHinter[5];
				Syndromatic.BestimmungDerExistenzformen(profile2);
				Syndromatic.BestimmungDerExistenzformen(profile5);
				System.Console.WriteLine("pause");
			}
		}
	}
}