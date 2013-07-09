using System;
using System.Collections.Generic;
using NUnit.Framework;
using SzondiTest;

namespace SzondiTestUnitTests
{
	internal class TestProfileSkippable : TestProfile
	{
		public TestProfileSkippable(string S, string P, string Sch, string C)
			:base( S, P, Sch, C)
		{
			
		}
	}
	
	internal delegate void InterpretationNoteDetector(TestProfile profile);
	internal delegate void ExistenzFormDetector(TestProfile profile);
	
	public class BaseSzondiUnitTests
	{
		internal static void TestNoteHelper(InterpretationNotes note,
		                                  InterpretationNoteDetector detector,
		                                  List<TestProfile> profiles, 
		                                  List<int> haves,
		                                  List<int> haveNots)
		{
			foreach(var profile in profiles)
			{
				if(profile != null)
				{
					Syndromatic.DetectIntepretationNotes(profile);
					detector(profile);
				}
			}
			
			VerifyInterpretationNote(
				note, profiles, haves);
			VerifyNotInterpretationNotes(
				note, profiles, haveNots);		
		}
		
		internal static void TestExistenzformHelper(
			Existenzformen expected, ExistenzFormDetector detector, 
			List<TestProfile> profiles,
			List<int> haves, List<int> haveNots)
		{
			/*foreach(var profile in profiles)
			{
				if(profile != null)
				{
					Syndromatic.DetectIntepretationNotes(profile);
					detector(profile);
				}
			}*/
			
			VerifyExistenzformHelper(
				expected, profiles, detector, haves, true);
			VerifyExistenzformHelper(
				expected, profiles, detector, haveNots, false);
		}
		
		internal static void VerifyExistenzformHelper(
			Existenzformen expected, List<TestProfile> profiles,
			ExistenzFormDetector detector, List<int> selected, 
			bool expectedToHaveIt)
		{
			foreach(var profileNr in selected)
			{
				var profile = profiles[profileNr];
				VerifyProfileHelper(profile, expected, detector, expectedToHaveIt);
			}
		}
		
		internal static void VerifyExistenzformHelper(
			Existenzformen expected, List<TestProfile> profiles,
			ExistenzFormDetector detector)
		{
			foreach(var profile in profiles)
			{	
				if(! (profile is TestProfileSkippable))
				{
					VerifyProfileHelper(profile, expected, detector, true);
				}
			}
		}
		
		/*internal static void VerifyProfileHelper(
			TestProfile profile, Existenzformen expected,
			ExistenzFormDetector detector)
		{
			if(profile != null)
			{
				detector(profile);
			}
			
			bool expectedOk = profile.HasExistenzform(expected);
			string errorMessage = profile + " expected as " + expected;
			Assert.True(expectedOk, errorMessage);
		}*/
		
		/*internal static void VerifyProfileHelper(TestProfile profile, 
		                                         Existenzformen expected)
		{
			var detected = Syndromatic.BestimmungDerExistenzformen(profile);
			bool expectedOk = detected.Contains(expected);
			string errorMessage = profile + " expected as " + expected;
			Assert.True(expectedOk, errorMessage);
		}*/
		
		internal static void VerifyProfileHelper(
			TestProfile profile, Existenzformen exForm,
			ExistenzFormDetector detector,
			bool expectedToHaveIt)
		{
			if(profile == null)
			{
				return;
			}
			Syndromatic.DetectIntepretationNotes(profile);
			detector(profile);
			bool expectedOk = (expectedToHaveIt 
			                   == profile.HasExistenzform(exForm));
			string errorMessage = null;
			if(!expectedOk)
			{
				string not = string.Empty;
				if(!expectedToHaveIt)
				{
					not = " NOT";
				}
				errorMessage = profile + " in " + profile.partOf.Name
					+ not + " expected as " + exForm;
			}
			Assert.True(expectedOk, errorMessage);
		}
		
		internal static void VerifyInterpretationNotes(
			InterpretationNotes note,
			List<TestProfile> profiles,
			List<int> selection,
			bool expectedToHaveIt)
		{
			foreach(var profileNr in selection)
			{
				var profile = profiles[profileNr];
				if(profile != null)
				{	bool hasInterpretationNote
						= profile.HasInterpretationNote(note);
					bool expectedOk = (expectedToHaveIt == hasInterpretationNote);
					string errorMessage = string.Empty;
					if(!expectedOk)
					{	string not = string.Empty;
						if(!expectedToHaveIt)
						{	not = " NOT";
						}
						errorMessage = profile + " in " + profile.partOf.Name
							+ not + " expected with " + note;
					}
					Assert.True(expectedOk, errorMessage);
				}
			}	
		}
		
		internal static void VerifyNotInterpretationNotes(
			InterpretationNotes notExpectedNote,
			List<TestProfile> profiles,
			List<int> haveNots)
		{	VerifyInterpretationNotes(notExpectedNote, profiles, haveNots, false);
		}
		
		internal static void VerifyInterpretationNote(
			InterpretationNotes expectedNote,
			List<TestProfile> profiles,
			List<int> haves)
		{	VerifyInterpretationNotes(expectedNote, profiles, haves, true);
			/*foreach(var profileNr in haves)
			{
				var profile = profiles[profileNr];
				if(profile != null)
				{	bool hasInterpretationNote
						= profile.HasInterpretationNote(expectedNote);
					if(!hasInterpretationNote)
					{
						string errorMessage = profile + " in " + profile.partOf.Name
							+  " expected with " + expectedNote;
						Assert.Fail(errorMessage);
					}
				}
			}*/			
		}	
		
		internal static void SetSexAndNameForProfiles(Sex sexToSet, string name,
			List<TestProfile> exampleProfiles)
		{
			TestSeries testSerie = new TestSeries(sexToSet, exampleProfiles, name);
		}
		
		internal static void SetSexForProfiles(Sex sexToSet,
			List<TestProfile> exampleProfiles)
		{
			TestSeries testSerie = new TestSeries(sexToSet, exampleProfiles);
		}
		
		internal static void SetSexForProfiles(
			List<TestProfile> exampleProfiles)
		{
			SetSexForProfiles(Sex.Female, exampleProfiles);
		}	
	
		internal static void SeriesContainsDiagnostic(
			TestSeries serie, Existenzformen exForm, ExistenzFormDetector detector)
		{
			SeriesContainsDiagnostics(serie, new Existenzformen[] {exForm},
			                                 new ExistenzFormDetector[] {detector});
		}
		
		internal static void SeriesContainsDiagnostics(
			TestSeries serie, Existenzformen[] exForms, ExistenzFormDetector[] detectors)
		{
			foreach(var profile in serie.vorergrundprofile)
			{	
				Syndromatic.DetectIntepretationNotes(profile);
				
				foreach(var detector in detectors)
				{
					detector(profile);
				}
			}
			
			foreach(var exForm in exForms)
			{
				bool detected = false;
				foreach(var profile in serie.vorergrundprofile)
				{
					if(profile.HasExistenzform(exForm))
					{
						detected = true;
						break;
					}
				}
				
				if(!detected)
				{
					string error = exForm + " not detected in "	+ serie.Name;
					Assert.Fail(error);
				}
			}
		}
	}
	
	[TestFixture]
	public class TestAffektExistenzFormen : BaseSzondiUnitTests
	{
		[Test]
		public void TestPhobieNote()
		{
			{	var profiles = Fälle.Fall17;
				var haves = new List<int>() {2,6,7 /*Buch 3 p.266 XI*/};
				var haveNots = new List<int>() {1,3,4,5,8,9,10};
				var note = InterpretationNotes.Phobie;
				TestNoteHelper(note, Syndromatic.DetectPhobieNote, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestTotaleDesintegrationAffektlebenNote()
		{
			{	var profiles = Fälle.Fall19;
				var haves = new List<int>() {5,6,};//Buch 3 p.274 II.7
				var haveNots = new List<int>() {1,2,3,4,7,8,9,10};
				var note = InterpretationNotes.TotalDesintegrAffektleben;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestKain()
		{
			// Buch 3 p.279
			{	var profiles = Fälle.Fall21;
				var haves = new List<int>() {12,13};
				var haveNots = new List<int>() {};//?
				var note = InterpretationNotes.Kain;
				TestNoteHelper(note, Syndromatic.DetectKain, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestGeltungsdrang()
		{
			// Buch 3 p.280 VII
			{	var profiles = Fälle.Fall21;
				var haves = new List<int>() {18};
				var haveNots = new List<int>() {11, 12,13,14,15,16,17,19,20};//?
				var note = InterpretationNotes.Geltungsdrang;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestSensitiveBeziehungsangst()
		{
			// Buch 3 p.285 I. 2. b)
			{	var profiles = Fälle.Fall22;
				var haves = new List<int>() {1,9};
				var haveNots = new List<int>() {2,3,4,5,6,7,8,10};
				var note = InterpretationNotes.SensitiveBeziehungsangst;
				TestNoteHelper(note, Syndromatic.DetectAffektstörungenNotes, 
				               profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestPhobischenBesessenheit()
		{
			// Buch 3 p.280 VII
			{	var profiles = Fälle.Fall21;
				var haves = new List<int>() {16};
				var haveNots = new List<int>() {11, 12,13,14,15,17,18,19,20};//?
				var note = InterpretationNotes.PhobischenBesessenheit;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestLamentationNote()
		{
			{	var profiles = Fälle.Fall19;
				
				//Buch 3 p.274 II.8
				var haves = new List<int>() {1,4,9};//verify why 1 not in II.8 (factors interference?) 
				var haveNots = new List<int>() {2,3,5,6,7,8,10};
				var note = InterpretationNotes.Lamentation;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestPhobieExistenzform()
		{
			var profiles1 = new List<TestProfile>()
			{
				// p.491 Tabelle 54, nr. 1-14
				new TestProfile("-,0", "+,0", "±,±", "0,+"),
				new TestProfile("-,0", "+,±", "±,±", "0,+"),
				new TestProfile("-,0", "±,±", "±,±", "0,+!"),
				new TestProfile("-!,0", "+,+!", "±,±", "0,+!"),
				
				// nr.5-9
				new TestProfile("-,0", "+,±", "±,±", "0,0"),
				new TestProfile("-,-", "+,+", "±,±", "+,+!"),
				//new TestProfile("-!,0", "±,+", "±,+", "-,+"),
				//new TestProfile("-!,0", "±,0", "0,+", "±,+"),
				//new TestProfile("-,-!", "+,+", "±,±", "0,+!"),
				
				new TestProfile("+!,-", "0,-!", "0,0", "0,+"),
				
				//new TestProfile("+!,-", "+,-", "0,0", "±,+"),
				//new TestProfile("+!,-", "0,-", "±,+", "±,+"),
				//new TestProfile("+!,-", "+,-", "0,0", "+,-"), //Paranoide Phobie
				//new TestProfile("+!,-", "0,-", "-,0", "±,0"),
			};
			
			var profiles2 = new List<TestProfile>()
			{
				// p.508, from profiles p.507, II, V, VI, VII, VIII, IX, X
				new TestProfile("-,0", "+,0", "±,±", "-,0"),// II, V
				new TestProfile("-,0", "+,0", "±,±", "-,-"),// VI, VIII, IX, X
				new TestProfile("-,0", "+,0", "±,±", "-,±"),// VII
			};
			
			SetSexForProfiles(profiles1);
			SetSexForProfiles(profiles2);
			var exForm = Existenzformen.Hysteriforme;
			VerifyExistenzformHelper(exForm, profiles1, Syndromatic.DetectHysteriforme);
			VerifyExistenzformHelper(exForm, profiles2, Syndromatic.DetectHysteriforme);
		}
		
		[Test]
		public void TestEpileptiformeMörderE()
		{
			{	var allProbandensProfile = new List<List<TestProfile>>();
				
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.503 Fall 42, extra 11. Profil
					new TestProfile("+,+!", "-,0", "+,±", "-!,-")
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.380 Hintergänger ThKomplementar, Tab.AddInterpretationNote 41
					// I, II, V
					new TestProfile("-,+!!!", "-,0", "-,±", "±,-"),
					new TestProfile("-,+!!", "-,0", "-,±", "±,-"),
					new TestProfile("-!,+!!!", "-,0", "-,±", "±,-"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.495 Tabelle 55 I
					new TestProfile("+!,+!!", "-,-", "-,-", "0,-"),
					new TestProfile("+,+!", "0,-", "-,-", "+,-"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.495 Tabelle 55 II
					new TestProfile("+!!,+", "-,0", "0,-", "±,-"),
					new TestProfile("+!!!,+", "0,-!!", "-,0", "±,-"),
					new TestProfile("+!!!,+", "+,-", "0,-", "-,-"),
					new TestProfile("+!,+", "-,-!", "0,0", "±,0"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.495 Tabelle 55 III
					new TestProfile("+!!,0", "-,-", "-,0", "+,-"),
					new TestProfile("+!,±", "0,-", "0,-", "+,-"),
					new TestProfile("±,+", "0,0", "±,-", "+,-"),
					new TestProfile("+!,+", "-,0", "+,-", "0,-"),
					new TestProfile("+,±", "-,+", "+,-", "0,-"),
					//new TestProfile("+!,+", "-!,-", "+,0", "±,-"), ??
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.495 Tabelle 55 IV
					new TestProfile("+,+", "0,-!", "±,±", "0,-"),
					new TestProfile("+!!,0", "0,±", "±,-", "-,-"),
					new TestProfile("+,+", "-,-", "-,±", "0,-"),
				});		
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.495 Tabelle 55 V
					new TestProfile("+!,0", "+,-", "-,-", "+,-"),
					new TestProfile("+!,0", "-,±", "0,0", "+,-"),
					new TestProfile("+!,0", "0,±", "-,0", "+,-"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 VI
					//new TestProfile("±,±", "+,-", "0,+", "0,-"), ?
					new TestProfile("+!!,±", "±,0", "0,0", "+,-"),
					new TestProfile("+!!!,±", "0,-!", "-,+", "0,-!"),
					new TestProfile("+!!!,±", "-,0", "-!,-!", "+,±"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 VII
					new TestProfile("0,-", "+,-", "±,±", "-,+"),//1 //can't w/o 3
					new TestProfile("0,±", "0,-", "-,±", "-,+"),//2 //can't w/o 3
					new TestProfile("-,-", "+,+", "0,0", "-,±"),//4 //can't w/o 3
					
					// used only for e GroßeMobilität
					new TestProfileSkippable("0,-", "-,±", "+,0", "0,+"),//3 
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 VIII ??
					/*
					new TestProfile("+,+", "0,-", "-,±", "0,-"),//2
					new TestProfile("+,+", "0,-", "-,±", "-,+"),//3 ?
					new TestProfile("+,+", "0,-", "-,±", "-,0"),//1 ?
					*/
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 IX
					new TestProfile("0,+", "-,±", "-,±", "+,-"),
					new TestProfile("+!!,+!", "0,-", "0,0", "+,-!!"),
					
					new TestProfileSkippable("-,+", "±,-", "0,+", "-,±"),
					new TestProfileSkippable("-,0", "±,-", "+,+", "-,±"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 X
					new TestProfile("+!,+!", "+,-", "0,-", "-,-"),
					new TestProfile("+,0", "0,-", "0,-", "-,-"),
					new TestProfile("+!!,+", "-,+", "0,-", "+,-!"),
					new TestProfile("+!,+!", "-!,+", "0,0", "-,-"),
					//new TestProfile("+,0", "-,0", "±,-", "0,±"), ?
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 XI
					new TestProfile("-,+", "0,-", "0,0", "0,+"),
					new TestProfile("±,+!", "0,-", "0,0", "-,+"),
					new TestProfile("±,+!!", "0,-", "0,0", "-,±"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 XII
					new TestProfile("+,+!", "0,-!!", "0,0", "±,0"),//1
					new TestProfile("+,+!!", "0,-", "-,-", "0,-"),//3
					new TestProfile("+!,+!", "+,-", "-,-", "-,0"),//4
					
					new TestProfileSkippable("+,+", "-,-", "+,0", "-!,0"),//2
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 XIII
					new TestProfile("+!,+!!", "-,-!", "-,-", "0,-"),
					new TestProfile("+,+!", "0,-", "0,-", "+,-"),
				});
				allProbandensProfile.Add(new List<TestProfile>
				{
					// p.496 Tabelle 55 XIV
					new TestProfileSkippable("+,+", "+,-!", "-,+", "0,+"),
					new TestProfile("+!,+", "0,-", "-,0", "0,+!"),
					new TestProfile("+!,0", "-,-!", "-,0", "0,+!"),
					new TestProfile("+!!,0", "0,-", "-,0", "-!,+!!"),
				});
				
				foreach(var epiProfiles in allProbandensProfile)
				{
					SetSexForProfiles(Sex.Male, epiProfiles);
				}
				
				var epiEx = Existenzformen.Tötende_Gesinnung_Epileptiforme;
				foreach(var epiProfiles in allProbandensProfile)
				{
					VerifyExistenzformHelper(epiEx, epiProfiles, Syndromatic.DetectEpileptiforme);
				}
			}
			
			{	var profiles = Fälle.Fall23;
				
				//Buch 3 p.290 II.2
				var haves = new List<int>() {1,2,5,7,8,9,10};
				var haveNots = new List<int>() {3,4,6,};
				var note = InterpretationNotes.MörderE;
				TestNoteHelper(note, Syndromatic.DetectEpileptiforme, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestKonversionshysterie()
		{
			{
				var frauExamples1to3 = new System.Collections.Generic.List<TestProfile>()
				{
					// p.487 Tabelle 53, nr. 1-3
					// TODO detect Sukzession
					new TestProfile("-,+", "+,+", "-,+", "±,0"), 
					new TestProfile("-,0", "0,0", "-,+", "±,+"), 
					new TestProfile("-,0", "0,0", "-,±", "±,+!"),
					
					new TestProfile("±,-", "+,+", "-,0", "+,-"),
					//new TestProfile("±,-", "+,0", "0,±", "+,±"),
					new TestProfile("±,-", "+,+", "-,±", "+,-"),
					//new TestProfile("+,-", "+,-", "-,±", "+,+"),
					//new TestProfile("0,-!", "+,-", "-,±", "±,0"),
					//new TestProfile("+,-", "+!,-", "-,+", "±,0"),
				};
				
				var mannExamples4to13 = new List<TestProfile>()
				{
					// nr. 4-ff
					//new TestProfile("±,-", "+,+", "-,0", "+,-"), 
					//new TestProfile("±,-", "+,0", "0,±", "+,±"),
					new TestProfile("±,-", "+,+", "-,±", "+,-")
				};
				
				// set sex for profiles
				SetSexForProfiles(Sex.Female, frauExamples1to3);
				SetSexForProfiles(Sex.Male, mannExamples4to13);
				
				VerifyExistenzformHelper(Existenzformen.Hysteriforme, 
				                         frauExamples1to3, 
				                         Syndromatic.DetectHysteriforme
				                        );
				VerifyExistenzformHelper(Existenzformen.Hysteriforme, 
				                         mannExamples4to13,
				                         Syndromatic.DetectHysteriforme);
			}
			
		}
		
		[Test]
		public void TestGewissenangs()
		{
			{	var profiles = Fälle.Fall17;
				
				var haves = new List<int>() 
				{
					// Buch 3 p.266 XI
					3,4,5,8,9,10,
				};
				var haveNots = new List<int>() 
				{
					 1,2,6,7
				};
				
				var note = InterpretationNotes.Gewissensangst;
				TestNoteHelper(note, Syndromatic.DetectAffektstörungenNotes, 
				                     profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestAffektPolarität()
		{
			{	var profiles = Fälle.Fall17;
				
				var haves = new List<int>() 
				{
					// Buch 3 p.266 XI
					1
				};
				var haveNots = new List<int>() 
				{
					 2,3,4,5,6,7,8,9,10
				};
				
				var note = InterpretationNotes.AffektPolarität;
				TestNoteHelper(note, Syndromatic.DetectAffektstörungenNotes, 
				                     profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestAffektstörungen()
		{
			{	var profiles = Fälle.Fall16;
				
				var haves = new List<int>() 
				{	// Buch 3 p.262 X.1., profiles 2, 4, 5, 7, 9 p.263
					2, 4, 5, 7, 9,
					// X.3.
					6, 8, 10,
					// X.5
					1,
				};
				var haveNots = new List<int>() {3,};
				
				var note = InterpretationNotes.Affektstörungen;
				TestNoteHelper(note, Syndromatic.DetectAffektstörungenNotes, 
				                     profiles, haves, haveNots);	
			}
			
			{	var profiles = Fälle.Fall21;
				//TODO
				var haves = new List<int>() {/*11,12,13,14,15,16,17,18,19,20*/};//p.280 XI
				var haveNots = new List<int>() {};
				var note = InterpretationNotes.Affektstörungen;
				TestNoteHelper(note, Syndromatic.DetectAffektstörungenNotes, 
				                     profiles, haves, haveNots);	
			}
		}
	}
	
	[TestFixture]
	public class TestIchExistenzFormen : BaseSzondiUnitTests
	{
		[Test]
		public void TestProjektivParanoiden()
		{
			/*
			  * p.166
			  * Das projektine Paranoid hat eine maniforme, depressive 
			  * und steife zwanghafte Variation.
			  * 
			  * 
			  * */
			
			{
				var exampleProfiles = new List<TestProfile>()
				{
					// from Buch3, p.242, Tabelle 29
					new TestProfile("+,-", "+,-", "+,-", "+,-"), // paradigma p.241
					new TestProfile("+,-", "+,-", "0,-", "+,-"), // paradigma p.241
					new TestProfile("+!!!,-!!", "+,0", "0,-", "+,-"),
					new TestProfile("+!!!,-", "0,0", "0,-", "+,-"),
					new TestProfile("+,-", "+,-", "0,-", "+,+"),
					new TestProfile("+,-", "+,-", "0,-", "+,±"),
					new TestProfile("+,-", "+,-", "0,-", "+,0"),
					new TestProfile("+,±", "+,-", "0,-", "+,-"),
					new TestProfile("+!!,-", "+,-", "0,-", "0,-"),
					new TestProfile("+!!,0", "+,-", "0,-", "0,-"),
					
					// from Buch3, p.242, Tabelle 30
					// Katatoniformen und zwanghaften Paranoiden
					new TestProfile("+!,-", "+,-", "±,-", "+,-"),
					new TestProfile("+,-", "+,-", "±,-", "0,-"),
					new TestProfile("+,-", "+,-", "±,-", "-,0"),
					new TestProfile("+,-", "+,±", "±,-", "+,-"),
					
					// from Buch3, pp.260, 261, 263, Tab. Abb.33 Fall16, prof.X
					Fälle.Fall16[10],
				};
				
				// set sex for profiles
				SetSexForProfiles(exampleProfiles);
				
				VerifyExistenzformHelper(Existenzformen.ProjektivParanoide, 
				                         exampleProfiles, 
				                         Syndromatic.DetectProjektivParanoid);
			}
			
			//Buch 3, pp.241-3, Tabelle 32
			{
			
				var exampleProfiles = new List<TestProfile>()
				{
					new TestProfile("+,+", "0,-!", "0,-!", "+,-"),
					new TestProfile("+,±", "0,-", "0,-", "+,-"),
					new TestProfile("+,0", "0,-", "0,-", "±,±"),
					new TestProfile("+!!,+!", "0,-!!", "0,-", "0,0"),
					new TestProfile("0,-", "+,-", "0,-!!", "0,-"),
				};
				
				SetSexForProfiles(exampleProfiles);
				VerifyExistenzformHelper(Existenzformen.ProjektivParanoide, 
				                         exampleProfiles, 
				                         Syndromatic.DetectProjektivParanoid);
			}
			
			//Buch 2, p.430
			// Skala row 2a
			{
				{
					var profiles = Fälle.Fall19;
					var haves = new List<int>() 
					{
						// example from Buch 3 p.272, Fall 19, Profil 4
						4,
					};
					var haveNots = new List<int>() {1,2,5,6,7,8,9,10};
					TestExistenzformHelper(Existenzformen.ProjektivParanoide, 
				                       Syndromatic.DetectProjektivParanoid,
				                       profiles, haves, haveNots);
				}
				
				{
					var profiles = Fälle.Fall18;
					var haves = new List<int>() 
					{
						// example from Buch 3 p.269, Fall 18, Profil X
						10,
					};
					var haveNots = new List<int>() {2,3};
					TestExistenzformHelper(Existenzformen.ProjektivParanoide, 
				                       Syndromatic.DetectProjektivParanoid,
				                       profiles, haves, haveNots);
				}
				
				{
					var profiles = Fälle.Fall16;
					var haves = new List<int>() 
					{
						// example from Buch 3 p.263, Fall 16, Profil II
						2,
					};
					var haveNots = new List<int>() {1,3};
					TestExistenzformHelper(Existenzformen.ProjektivParanoide, 
				                       Syndromatic.DetectProjektivParanoid,
				                       profiles, haves, haveNots);
				}			
			}
			
			{
				var profiles = Fälle.Fall17;
				var haves = new List<int>() 
				{
					// Buch 3 p.264 I., profiles p.265
					4, 5, 8, 3, 9,
				};
				var haveNots = new List<int>() {1,2,7,10};
				
				TestExistenzformHelper(Existenzformen.ProjektivParanoide, 
				                       Syndromatic.DetectProjektivParanoid,
				                       profiles, haves, haveNots);
			}
			
			//Projective Paranoid
			{
				TestSeries serie16 = Fälle.Fall17[1].partOf;
				TestSeries serie17 = Fälle.Fall17[1].partOf;
				
				SeriesContainsDiagnostic(serie17, Existenzformen.ProjektivParanoide,
				                         Syndromatic.DetectProjektivParanoid);
				SeriesContainsDiagnostic(serie16, Existenzformen.ProjektivParanoide,
				                         Syndromatic.DetectProjektivParanoid);
			}
		}
		
		[Test]
		public void TestProjektivParanoidenSubtypes()
		{
			//Maniforme Paranoid
			{
				var profiles = Fälle.Fall18;
				TestSeries serie = profiles[1].partOf;
				Existenzformen[] exForms 
					= {Existenzformen.ProjektivParanoide, Existenzformen.Hypomanische_Manische};
				ExistenzFormDetector[] detectors 
					= {Syndromatic.DetectProjektivParanoid, 
					Syndromatic.DetectHypomanische_Manische};
				SeriesContainsDiagnostics(serie, exForms, detectors);
			}
			
			{
				TestSeries serie19 = Fälle.Fall19[1].partOf;
				
				Existenzformen[] exForms 
					= {Existenzformen.ProjektivParanoide, Existenzformen.Depressive_Melancholische};
				ExistenzFormDetector[] detectors 
					= {Syndromatic.DetectProjektivParanoid, 
					Syndromatic.DetectDepressive_Melancholische};
				SeriesContainsDiagnostics(serie19, exForms, detectors);
			}
			
			//Katatoniforme (zwanghafte) Paranoid
			{
				var profiles = Fälle.Fall20;
				TestSeries serie = profiles[1].partOf;
				Existenzformen exForm = Existenzformen.Katatoniforme;
				ExistenzFormDetector detector = Syndromatic.DetectKatatoniforme;
				SeriesContainsDiagnostic(serie, exForm, detector);
			}
		}
		
		[Test]
		public void TestKatatoniforme()
		{	{
				// from Buch3, p.243, Tabelle 33
				// Skala row 5a
				var exampleProfiles = new List<TestProfile>()
				{	new TestProfile("±,+", "-,0", "-,0", "-,-"),
					new TestProfile("+!,+!!!", "0,-", "-,0", "-,-"),
					new TestProfile("0,+!!!", "0,-", "-,+", "-,-"),
					new TestProfile("+!,+!!", "0,-", "-,+", "-,-"),
				};
				
				SetSexForProfiles(exampleProfiles);
				
				VerifyExistenzformHelper(Existenzformen.Katatoniforme, 
				                         exampleProfiles, Syndromatic.DetectKatatoniforme);
			}
					
			{	var profiles = Fälle.Fall22;
				var haves = new List<int>() {4,9,};//1,2,3,5,6,7,8,10,
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.Katatoniforme, 
			                       Syndromatic.DetectKatatoniforme,
			                       profiles, haves, haveNots);
			}
			
			//Katatoniforme 
			{
				TestSeries serie22 = Fälle.Fall22[1].partOf;
				TestSeries serie23 = Fälle.Fall22[1].partOf;
				Existenzformen exForm = Existenzformen.Katatoniforme;
				ExistenzFormDetector detector = Syndromatic.DetectKatatoniforme;
				SeriesContainsDiagnostic(serie22, exForm, detector);
				SeriesContainsDiagnostic(serie23, exForm, detector);
			}
		}

		[Test]
		public void TestKatatoniformeMitte()
		{
			{	var profiles = Fälle.Fall22;
				var haves = new List<int>() {1,4,9,7,8,};
				var haveNots = new List<int>() {2,3,5,6,10};
				TestNoteHelper(InterpretationNotes.KatatonifMitte, 
			                       Syndromatic.FurtherNotes,
			                       profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestKatatoniformeSperrungssyndrom()
		{
			{	var profiles = Fälle.Fall22;
				var haves = new List<int>() {4,7,8,5};//p.286 VII.
				var haveNots = new List<int>() {1,2,3,6,9,10};
				TestNoteHelper(InterpretationNotes.KatatonifSperrungsSynd, 
			                       Syndromatic.FurtherNotes,
			                       profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestInflaivParanoiden()
		{
			// from Buch3, p.242, Tabelle 31
			// Skala row 3b
			{	var exampleProfiles = new List<TestProfile>()
				{
					new TestProfile("-,+", "-,±", "0,+", "-,+"),
					new TestProfile("-,+", "-,±", "0,+!", "-,+"),
					new TestProfile("-,±", "-,-", "0,+", "+,0"),
					// new TestProfile("+,±", "-,0", "0,+", "0,-"),// ?
				};
				
				// set sex for profiles
				SetSexForProfiles(exampleProfiles);		
				VerifyExistenzformHelper(Existenzformen.InflativParanoide, 
				                         exampleProfiles, Syndromatic.DetectInflativParanoid);
			}
			
			// from Buch2, p.230
			// Skala row 3a
			{	var exampleProfiles = new System.Collections.Generic.List<TestProfile>()
				{
					// examples from Buch 3 p.387, Fall 34
					new TestProfile("±,-", "+,-", "+,+!", "0,±"),
					new TestProfile("±,±", "-,0", "0,+!", "0,+"),
					new TestProfile("±!,+", "-,-", "0,+!", "0,-"),
					new TestProfile("-,-!!!", "-,+", "0,+!!", "0,+")
				};
				
				// set sex for profiles
				SetSexForProfiles(exampleProfiles);
				
				VerifyExistenzformHelper(Existenzformen.InflativParanoide, 
				                         exampleProfiles, Syndromatic.DetectInflativParanoid);
			}
			
			{	var profiles = Fälle.Fall21;
				var haves = new List<int>() {11,12,17};// Buch 3 p.280 V.Faktorenverband
				var haveNots = new List<int>() {};
				
				TestExistenzformHelper(Existenzformen.InflativParanoide, 
				                       Syndromatic.DetectInflativParanoid,
				                       profiles, haves, haveNots);
			}
			
			//InflaivParanoiden 
			{	var profiles = Fälle.Fall21;
				TestSeries serie = profiles[11].partOf;
				SeriesContainsDiagnostic(serie, Existenzformen.InflativParanoide, 
				                         Syndromatic.DetectInflativParanoid);
			}
		}
	
		[Test]
		public void TestInflativParanoideMitte()
		{
			{
				var profiles = Fälle.Fall21;
				var haves = new List<int>() {13,14,15,17,18,19};
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.InflativParanoMitte;
				TestNoteHelper(note, Syndromatic.DetectInflativParanoideMitte, 
				                     profiles, haves, haveNots);
			}
		}
		
		[Test][Ignore]
		public void TestInflativParanoideSyndrom()
		{
			{
				var profiles = Fälle.Fall21;
				var haves = new List<int>() {};
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.InflativParanoSyndrom;
				TestNoteHelper(note, Syndromatic.DetectInflativParanoidSyndrom, 
				                     profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestProjektivParanoideMitte()
		{
			var notePM = InterpretationNotes.ProjektivParanoMitte;
			
			{	var profiles = Fälle.Fall16;
				var haves = new List<int>() 
				{
					// Buch 3 p.262 V. 1., profiles p.263 2, 4-5, 7, 9
					2, 4, 5, 7, 9,
					
					// Buch 3 p.262 V. 1., profiles p.263 1
					1,
					
					// Buch 3 p.262 V. 2., profiles 6, 8, 10 p.263 
					6, 8, 10
				};
				var haveNots = new List<int>() {3};
				
				TestNoteHelper(notePM, Syndromatic.DetectProjektivParanoideMitte, 
				                     profiles, haves, haveNots);	
			}
			{
				var profiles = Fälle.Fall17;
				var haves = new List<int>() 
				{
					// Buch 3 p.264 VI., profiles p.265
					3, 4, 5, 8, 9,
				};
				var haveNots = new List<int>() {1,2,6,7,10};
				
				TestNoteHelper(notePM, Syndromatic.DetectProjektivParanoideMitte, 
				                     profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestParanoideSpaltungsSyndrom()
		{
			var note = InterpretationNotes.ParanoSpaltungsSynd;
			
			{	var profiles = Fälle.Fall18;
				var haves = new List<int>() {1, 6, 4, 7, 8, 9, 5}; // Buch 3 p.268 I.2,
				var haveNots = new List<int>() {2,3};// check: 10 (incomplete)
				
				TestNoteHelper(note, Syndromatic.DetectParanoideSpaltungsSyndrom, 
				                     profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall19;
				var haves = new List<int>() {4,}; // Buch 3 p.272 I.4,
				var haveNots = new List<int>() {1,2,3,7,8,9,10};//check: 5,6,
				
				TestNoteHelper(note, Syndromatic.DetectParanoideSpaltungsSyndrom, 
				                     profiles, haves, haveNots);
			}
			
			{	 // Buch 3 p.275 V.
				var profiles = Fälle.Fall20;
				var haves = new List<int>() {1,5};//Why not 4 in p.275 V. (mistake?)
				var haveNots = new List<int>() {2,3,6,8,9,10};//7, ±!
				
				TestNoteHelper(note, Syndromatic.DetectParanoideSpaltungsSyndrom, 
				                     profiles, haves, haveNots);
			}
			
			{	 // Buch 3 p.278 I.
				var profiles = Fälle.Fall21;
				var haves = new List<int>() {11,12,17};
				var haveNots = new List<int>() {19,};//TODO revise: 13,14,15(=16),18,20
				
				TestNoteHelper(note, Syndromatic.DetectParanoideSpaltungsSyndrom, 
				                     profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestParanoideKammSyndromUnpureForm()
		{
			var noteUnpureKamm = InterpretationNotes.ParanoKammSyndU;
			{
				var profiles = Fälle.Fall16;
				var havesUnpureKamm = new List<int>()
				{
					// Buch 3 p.262 VI., profiles 2, 4-9 p.263, unpure form 
					2, 4, 5, 6, 7, 8, 9,				
				};
				
				var haveNots = new List<int>() 
				{
					// Buch 3 p.262 VI., profiles 1, 3 p.263
					1, 3
				};
				TestNoteHelper(noteUnpureKamm, 
                     Syndromatic.DetectParanoideKammSyndrom,
                     profiles,
                     havesUnpureKamm, haveNots);
			}
			
			{
				var profiles = Fälle.Fall17;
				var haves = new List<int>() 
				{
					// Buch 3 p.264 VII., profiles p.265
					8, 10, //NB. m± contains m-, or s±, unpure form (p.262)
					7,//per p.272 I.6
				};
				var haveNots = new List<int>() {1,2,3,6};
				
				TestNoteHelper(
					noteUnpureKamm,
					Syndromatic.DetectParanoideKammSyndrom,
				    profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall19;
					
				// Buch 3 p.272 II.4 				
				var havesUnPureKamm = new List<int>()	{4,5,6,};
				var haveNots = new List<int>() {1,2,3,7,8,9,10};
				
				TestNoteHelper(noteUnpureKamm, Syndromatic.DetectParanoideKammSyndrom,
				               profiles, havesUnPureKamm, haveNots);								
			}
		}
		
		[Test]
		public void TestParanoideKammSyndrom()
		{			
			var notePureKamm = InterpretationNotes.ParanoKammSynd;			
			{
				var profiles = Fälle.Fall16;
					
				// Buch 3 p.262 VI., profile 10 p.263, pure form 				
				var havesPureKamm = new List<int>()	{10};
				
				// Buch 3 p.262 VI., profiles 1, 3 p.263
				var haveNots = new List<int>() {1, 3};
				
				TestNoteHelper(notePureKamm, 
				                     Syndromatic.DetectParanoideKammSyndrom,
				                     profiles,
				                     havesPureKamm, haveNots);								
			}
			
			{
				var profiles = Fälle.Fall17;
				var haves = new List<int>() 
				{
					// Buch 3 p.264 VII., profiles p.265
					4, 5,
				};
				var haveNots = new List<int>() {1,2,6,7,3};
				
				TestNoteHelper(
					notePureKamm,
					Syndromatic.DetectParanoideKammSyndrom,
				    profiles, haves, haveNots);
			}
		}		
	
		[Test]
		public void TestHeboide_ProjektiveHypochondrischeSynd()
		{
			{	/*Buch 3 p.298 4.a)*/
				var profiles = Fälle.Fall24;
				var haves = new List<int>() {1,3,4,5,6,7,9,10,2};//
				var haveNots = new List<int>() {};
				var note = InterpretationNotes.ProjektiveHypochondrSynd;
				TestNoteHelper(note, Syndromatic.DetectHeboide, 
				               profiles, haves, haveNots);
			}
			
			{	/*Buch 3 p.300 IV.1, IV.2.*/
				var profiles = Fälle.Fall25;
				var haves = new List<int>() {9,1,3,4,10,2,5,6,};//
				var haveNots = new List<int>() {7,8};
				var note = InterpretationNotes.ProjektiveHypochondrSynd;
				TestNoteHelper(note, Syndromatic.DetectHeboide, 
				               profiles, haves, haveNots);
			}
			
			// Buch 3 p.301 IV
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {1,2,4,3,7,8,9,10};
				var haveNots = new List<int>() {5,6,};
				var note = InterpretationNotes.ProjektiveHypochondrSynd;
				TestNoteHelper(note, Syndromatic.DetectHeboide, 
				               profiles, haves, haveNots);
			}
		}
	}
	
	[TestFixture]
	public class TestKontaktExistenzFormen : BaseSzondiUnitTests
	{
		[Test]
		public void TestHypomanische_Manische()
		{
			{
				// from Buch3, p.244, Tabelle 35
				var exampleProfiles = new List<TestProfile>()
				{
					new TestProfile("+!,+!", "+,-", "-!,-", "0,-"),
					new TestProfile("+!,+!!!", "0,0", "-!,-", "0,-"),
					new TestProfile("+!,+!", "0,-", "-,-", "0,-!"),
					new TestProfile("+,+!", "+,-", "-,-", "0,-")
				};
				
				// set sex for profiles
				SetSexForProfiles(exampleProfiles);
				
				VerifyExistenzformHelper(Existenzformen.Hypomanische_Manische, 
				                         exampleProfiles, Syndromatic.DetectHypomanische_Manische);
			}
			{	// Buch 3 p.280 IX. 4.
				var profiles = Fälle.Fall21;
				var haves = new List<int>() {18};
				var haveNots = new List<int>() {11, 12,13,14,15,17,16,19,20};//?
				var exForm = Existenzformen.Hypomanische_Manische;
				TestExistenzformHelper(exForm, Syndromatic.DetectHypomanische_Manische,
				                       profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestDepressive_Melancholische()
		{			
			//TODO :
			/*
			  * p.266 (distinguish circular depressive from other types
				* Auf Grund der Syndromanalyse können wir 
				* die Diagnose auf das Paranoid stellen.
				* Probandin zeitigt aber im Kontaktgebiet 
				* die zweitgrößte Triebgefahr in Form von Cd+/5. 
				* Nur ist die Frage, ob sie neben dem paranoiden 
				* nicht auch ein melancholisches Syndrom aufweist. 
				* Gegen eine wirklich zirkuläre Depression sprechen 
				* die Reaktionen k=0 und k=-. Bei der Melancholie finden 
				* wir die Reaktion k=+. (Sie später das melancholische Syndrom). 
				* Wir müssen demnach die Triebgefahr d=+! mit p=- als 
				* ständiges Suchen nach dem Verfolger, nach Beschuldigung
				*  und Streit deuten. Diagnose: Depressive Paranoid.
			  * */
			 
			// from Buch3, p.243-4, Tabelle 34
			{
				var exampleProfiles = new List<TestProfile>()
				{
					new TestProfile("0,0", "-!,+", "+,-", "+,-"),
					new TestProfile("+,0", "0,0", "+,-", "+!,-!!"),
					new TestProfile("+,-", "0,-", "+!,-", "+,±"),
					new TestProfile("+,-", "-,+", "+,0", "+,±"),
					new TestProfile("0,-!", "+,-", "+,-", "+!,±")
				};
				
				// set sex for profiles
				SetSexForProfiles(exampleProfiles);
				
				VerifyExistenzformHelper(Existenzformen.Depressive_Melancholische, 
				                         exampleProfiles, Syndromatic.DetectDepressive_Melancholische);
			}
			
			// from Buch3, p.274 II.6
			{	var profiles = Fälle.Fall19;
				var haves = new List<int>{2,5,7};
				var haveNots = new List<int>{1,3,4,6,8,9,10};

				TestExistenzformHelper(Existenzformen.Depressive_Melancholische, 
				                       Syndromatic.DetectDepressive_Melancholische,
				                       profiles, haves, haveNots);
			
			}
		}		
	
		[Test]
		public void TestSucht()
		{
			
			var suchtProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				// from Buch3, p.435, Fall 1 (p.176)
				// profile VII
				new TestProfile("+,-!!", "0,0", "0,0", "+!,±"),//TODO remove? only Mitte?
				// profiles 2, 8, 4, 10, 3, 5		
				//new TestProfile("+,±", "0,0", "-,+", "+,-"),//2 s±, d+, m-
				new TestProfile("-,-", "0,0", "-,+", "+!!,-"),//8 m-
				new TestProfile("+,±", "0,0", "-,0", "+!,±"),//4
				//new TestProfile("+,±", "0,-", "0,0", "+!!,-"),//10, 3
				//new TestProfile("+,±", "0,±", "-,0", "+,-"),//5
				
				//pp.438-9, Fall 38, IV, VI, I, V
				new TestProfile("+!,-!", "+,0", "0,-", "-,±"),//1, 5
				new TestProfile("±,-!", "0,+", "+,±", "+,0"),//4 //k+, d+
				new TestProfile("±,-!", "+,+", "0,±", "0,-"),//6 //e+, m-
			};
			
			// set sex for profiles
			SetSexForProfiles(Sex.Male, suchtProfiles);
			
			VerifyExistenzformHelper(Existenzformen.KontaktPsychopathische, 
			                         suchtProfiles, 
			                         Syndromatic.DetectKontaktPsychopathische);
			//TestExistenzBestimmung.VerifyInterpretationNote(InterpretationNotes.Sucht, suchtProfiles);
		}
	
		[Test]
		public void TestKontaktstörungen()
		{
			{
				var profiles = Fälle.Fall16;
				
				var haves = new List<int>() 
				{
					// Buch 3 p.262 VIII., profiles p.263
					4, 5, 6, 7, 8, 9, 10,
				};
				var haveNots = new List<int>() {1,2,3};
				
				var note = InterpretationNotes.Kontaktstörungen;
				TestNoteHelper(note, 
				                     Syndromatic.DetectKontaktstörungen,
				                     profiles, haves, haveNots);	
			}
			
			{	var profiles = Fälle.Fall17;
				var haves = new List<int>() {4, 5, 7,};// Buch 3 p.264 IX, profiles p.265
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.Kontaktstörungen;
				TestNoteHelper(note,
				               Syndromatic.DetectKontaktstörungen,
				               profiles, haves, haveNots);
				
				// TODO 1,2,6,8,10 have ± which includes m-. Verify in more examples
				// 3,9 should have SuchenNachVerfolger
				var havesSuchenNachVerfolger = new List<int>() 
				{
					// Buch 3 p.264 IX, profiles p.265
					1,2,6,8,10,
					3,9,
				};
				TestNoteHelper(InterpretationNotes.SuchenNachVerfolger, 
				                     Syndromatic.DetectKontaktstörungen,
				                     profiles, havesSuchenNachVerfolger, haveNots);
			}
			{	var profiles = Fälle.Fall21;
				var haves = new List<int>() {13,14};// Buch 3 p.280 IX.3
				var haveNots = new List<int>() {11,12,15,16,17,18,19,20};
				
				var note = InterpretationNotes.Akzeptationsdrang;
				TestNoteHelper(note,
				               Syndromatic.DetectKontaktstörungen,
				               profiles, haves, haveNots);
			}
		}
	
		[Test]
		public void TestKontaktsperre()
		{
			{	var profiles = Fälle.Fall22;
				var haves = new List<int>() {2,4,5,6,7,8,9,10};// Buch 3 p.286 I.3
				var haveNots = new List<int>() {1,3,};
				
				var noteSex = InterpretationNotes.Kontaktsperre;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
			
			
			{	var profiles = Fälle.Fall25;
				var haves = new List<int>() {2,4,8,};// Buch 3 p.300 V.2
				var haveNots = new List<int>() {1,3,5,6,7,9,10};
				
				var noteSex = InterpretationNotes.Kontaktsperre;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {10};// Buch 3 p.301 V.2
				var haveNots = new List<int>() {1,2,4,8,3,5,6,7,9,};
				
				var noteSex = InterpretationNotes.Kontaktsperre;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
		}
		
		[Test]
		public void TestHypomanischeBenehmen()
		{
			{	var profiles = Fälle.Fall25;
				var haves = new List<int>() {3,5,6,9,10};// Buch 3 p.300 V.1.
				var haveNots = new List<int>() {1,2,4,7,8,};
				
				var noteSex = InterpretationNotes.HypomanischeBenehmen;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {2,5,6,8,9};// Buch 3 p.301 V.1.
				var haveNots = new List<int>() {1,3,4,7,10};
				
				var noteSex = InterpretationNotes.HypomanischeBenehmen;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
		}
	}
	
	[TestFixture]
	public class TestSexuelleExistenzFormen : BaseSzondiUnitTests
	{
		[Test]
		public void TestPerversionSadomasochismus()
		{
			var masochistProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				// Buch 3 p.389 Profil IX, X (from p.387)
				// perversen Lustsyndroms
				new TestProfile("±,-", "+,-", "+,+!", "0,±"),
				
				// Buch 3 p.383, I. Profil I, IV, IX (from p.384)
				// masochistische, perverse Lustsyndrom
				new TestProfile("+,-!", "-,-", "+,+", "+,±"),
				//new TestProfile("+,-", "-,-", "±,+", "+,±"),
				new TestProfile("+,-!!", "+,-", "+,±", "+,0")
			};
			
			var sadomasochistProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				// Buch 3 p.389 Profil IX, X (from p.387)
				// perversen Lustsyndroms
				new TestProfile("±,±", "-,0", "0,+!", "0,+")
			};
			
			// set sex for profiles
			SetSexForProfiles(masochistProfiles);
			SetSexForProfiles(sadomasochistProfiles);
			
			VerifyExistenzformHelper(Existenzformen.Perversion_Sadismus_Masochismus, 
			                         masochistProfiles,
			                         Syndromatic.DetectPerversionSadomasochismus);
			VerifyExistenzformHelper(Existenzformen.Perversion_Sadismus_Masochismus, 
			                         sadomasochistProfiles,
			                         Syndromatic.DetectPerversionSadomasochismus);
			
			//TestExistenzBestimmung.VerifyInterpretationNote(InterpretationNotes.Masochismus, sadomasochistProfiles);
			//TestExistenzBestimmung.VerifyInterpretationNote(InterpretationNotes.Sadomasochismus, masochistProfiles);						
		}	

		[Test]
		public void TestMasochismus()
		{		
			var profiles = new List<TestProfile>()
			{
				// Buch 3 p.379, Profil I, II, IV, V, VI
				// p.380
				// perversen Lustsyndroms
				new TestProfile("+,-!!!", "+,±", "+,0", "0,+"),
				new TestProfile("+,-!!", "+,±", "+,0", "0,+"),
				new TestProfile("+,-!", "±,±", "+,0", "0,+"),
				new TestProfile("+!,-!!!", "+,±", "+,0", "0,+"),
				new TestProfile("+!,-!!", "±,±", "+,0", "0,+"),
					
				// profiles pp.383-4, I, IV, IX
				new TestProfile("+,-!", "-,-", "+,+", "+,±"),
				// new TestProfile("+,-", "-,-", "±,+", "+,±"),
				new TestProfile("+,-!!", "+,-", "+,±", "+,0")
			};
			SetSexForProfiles(Sex.Male, profiles);
			VerifyExistenzformHelper(Existenzformen.Perversion_Sadismus_Masochismus, 
			                         profiles,
			                         Syndromatic.DetectPerversionSadomasochismus
			                        );
		}
	
		[Test]
		public void TestPolymorphPervers()
		{
			var polymorphPerversProfiles = new List<TestProfile>()
			{
				// Buch 3 p.479 Profil VI, VIII, X
				// new TestProfile("+,-", "±,-", "±,0", "0,±"),// VI, VIII, X

				// p.502, IV (profiles 6, 8 from p.501)				
			};
			
			SetSexForProfiles(Sex.Male, polymorphPerversProfiles);
			var note = InterpretationNotes.PolymorphPervers;
			//TestExistenzBestimmung.VerifyInterpretationNote(note, polymorphPerversProfiles);						
		
		}
		
		[Test]
		public void TestSexualstörungen()
		{
			{	var profiles = Fälle.Fall16;
				var haves = new List<int>() 
				{	2, 10,	// Buch 3 p.262 IX.1, profiles 2, 10 p.263
					4, 5, 6, 7, 8, 9,// p.262 IX.2
				};
				var haveNots = new List<int>() {1, 3, };
				
				var noteSex = InterpretationNotes.Sexualstörungen;
				TestNoteHelper(noteSex, Syndromatic.DetectSexualstörungen, 
				                     profiles, haves, haveNots);		
			}
			
			{
				var profiles = Fälle.Fall17;
				
				var haves = new List<int>() 
				{	// Buch 3 p.264 X.3,
					3, 4, 5, 8, 9, 10
				};//TODO 1,2,6,7
				var haveNots = new List<int>() 
				{};
				
				var noteSex = InterpretationNotes.Sexualstörungen;
				TestNoteHelper(noteSex, Syndromatic.DetectSexualstörungen, 
				                     profiles, haves, haveNots);		
			}
		}
		
		[Test]
		public void TestSzondiHomo()
		{
			var maleProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				// Buch 3 p.406-
				new TestProfile("+,-", "+,-", "0,±", "+,+"),
				new TestProfile("±,-", "±,-", "0,±", "+,+"),
					
				// Buch 3, p.187, p.413, II, III
				new TestProfile("+,-", "0,-", "0,±", "+!,+"),
				new TestProfile("0,-!", "+,-", "0,±", "+,0"),
				// new TestProfile("+,-", "0,-", "-,±", "+,±") // IV
					
				// Buch 3, p.415, Tabelle 49
				new TestProfile("+,-", "+,-", "0,±", "+,+"),
				new TestProfile("+,-!", "0,-", "0,±", "+!,+"),
				//new TestProfile("+,-", "0,-", "-,±", "+,±"),
				//new TestProfile("+,-", "0,-", "-,±", "+,+"),// Sch-±
				//new TestProfile("0,±", "0,-", "-,±", "+,+"), // 5
				new TestProfile("0,-!!", "0,±", "0,±", "+,+"),
				new TestProfile("±,-!!", "0,±", "0,±", "+,0"),
				new TestProfile("+,-", "0,±", "0,±", "+,+"),
				new TestProfile("-,-!!", "0,-", "0,±", "+!,+"),
				//new TestProfile("±,-!", "+,0", "0,±", "0,+!"),//10
				//new TestProfile("±,±", "0,-", "0,±", "0,0"),
				//new TestProfile("±,±", "0,0", "0,+!", "0,0"),
				//new TestProfile("±,-!", "+,-", "0,+!", "0,0"),
				//new TestProfile("+,±", "-,-", "0,+!", "0,+"),//14
			};
			SetSexForProfiles(Sex.Male, maleProfiles);
			VerifyExistenzformHelper(Existenzformen.Inversion_Homo_Trans, 
			                         maleProfiles,
			                        Syndromatic.DetectInversion);
			
			var femaleProfiles = new List<TestProfile>()
			{
				// Buch 3, p.415, Tabelle 49
				new TestProfile("-,+", "-,+", "±,0", "+,+"),
				//new TestProfile("-,+", "-,0", "±,0", "0,0"),
				//new TestProfile("±,+", "±,0", "±,0", "+,+"),
				//new TestProfile("±,+", "+,±", "0,0", "-,0"),
				//new TestProfile("±,+", "0,-", "0,0", "+,±"),//5
				//new TestProfile("±,+", "-,+", "+!,0", "±,±"),
				//new TestProfile("-!,-!", "-,+", "+,-", "+,±"),
				//new TestProfile("-!,-!", "±,+", "+!,+", "0,0"),
				//new TestProfile("±,±", "-,+", "0,+", "0,+"),
				//new TestProfile("-,±", "±,+", "+!,0", "-,0"),//10
				//new TestProfile("+,0", "+,±", "±,-!", "0,0"),
				//new TestProfile("-,0", "-,+", "+,-", "+,+"),
				//new TestProfile("-,-!", "±,-", "0,0", "+,+"),
				//new TestProfile("±,-!", "0,±", "±,0", "+,0"),//14
			};
			SetSexForProfiles(Sex.Female, femaleProfiles);
			VerifyExistenzformHelper(Existenzformen.Inversion_Homo_Trans, 
			                         femaleProfiles, 
			                         Syndromatic.DetectInversion);
		}
	}
	
	[TestFixture]
	public class TestSchutzExistenzFormen : BaseSzondiUnitTests
	{
		[Test]
		public void TestCompulsiveNeurotic()
		{
			// from Buch3, p.479, Tabelle 52
			var exampleProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				new TestProfile("±,-", "±,0", "±,0", "0,+"),
				new TestProfile("±,-", "±,+", "±,0", "0,±"),
				new TestProfile("+,-", "±,0", "±,0", "0,±"),
				new TestProfile("+,-", "±,+", "±,0", "0,±")
			};
			
			SetSexForProfiles(exampleProfiles);
			
			VerifyExistenzformHelper(Existenzformen.CompulsiveZwang, 
			                         exampleProfiles,
			                         Syndromatic.DetectCompulsiveNeurotic);
		}	
		
		[Test]
		public void TestHypochondrische()
		{
			
		}
	}
	
	[TestFixture]
	public class TestExistenzBestimmung : BaseSzondiUnitTests
	{				
		[Test]//[Ignore]
		public static void TestParseAndSyndromaticProfiles()
		{
			string inputFilename = "profiles.txt";
			string outputFilename = "detectedExistenzformen (" 
				+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ").txt";;
			
			
			TestSeries profilesSeries;
			string EKPprofilesHeadingMarker = "PCE";
			
			#region read file
			try
	        {
				var foregroundProfiles = new System.Collections.Generic.List<TestProfile>();
				var EKPprofiles = new System.Collections.Generic.List<TestProfile>();
				Sex testTakerSex;
				
	            using (var sr = new System.IO.StreamReader(inputFilename))
	            {
	            	
	            	#region read sex
	            	string line = sr.ReadLine();
	            	if(string.Equals(line, Sex.Male.ToString(), StringComparison.CurrentCultureIgnoreCase))
	            	{
	            		testTakerSex = Sex.Male;
	            	}
	            	else if(string.Equals(line, Sex.Female.ToString(), StringComparison.CurrentCultureIgnoreCase))
	            	{
	            		testTakerSex = Sex.Female;
	            	}
	            	else
	            	{
	            		// error
	            		throw new ArgumentException("Unable to read sex of profile taker.");
	            	}
	            	#endregion
	            	
	            	#region read foreground profile
	            	line = sr.ReadLine();
	            	while(line != null && !line.Equals(EKPprofilesHeadingMarker))
	            	{
	                	TestProfile profile = new TestProfile(line);
	                	profile.dimension = DimensionenUndFormenDerPsyche.VGP;
	                	foregroundProfiles.Add(profile);
	                	
	                	line = sr.ReadLine();
	            	}
	            	#endregion
	            	
	            	#region read Experimental profile
	            	if(line != null && line.Equals(EKPprofilesHeadingMarker))
	            	{
	            		line = sr.ReadLine();
	            		for(int i=0;  line != null; i++)
	            		{
	            			TestProfile EKPprofile = new TestProfile(line);
	            			EKPprofile.dimension = DimensionenUndFormenDerPsyche.EKP;
	            			EKPprofiles.Add(EKPprofile);
	            			foregroundProfiles[i].ExperimentalComplementar = EKPprofile;
	            		}
	            	}
	            	#endregion
	            }
	            
	            profilesSeries = new TestSeries(testTakerSex, foregroundProfiles, EKPprofiles);
	        }
	        catch (Exception e)
	        {
	            Console.WriteLine("The file could not be read:");
	            Console.WriteLine(e.Message);
	            Assert.Fail(e.Message);
	            return;
	        }
	        #endregion
	        
	        #region Interpret
	        profilesSeries.Interpret();
	        #endregion
	        
	        #region write to file
	        try
	        {
	            using (var sw = new System.IO.StreamWriter(outputFilename))
	            {
	            	
	            	string line = string.Empty;
	            	foreach(var profile in profilesSeries.vorergrundprofile)
	            	{
	            		sw.WriteLine(profile.ToString(true));
	            	}
	            	
	            	// Theoric complementar profiles
	            	sw.WriteLine("\r\n" + "PCT:");
					foreach(var profile in profilesSeries.vorergrundprofile)
	            	{
	            		sw.WriteLine(profile.TheoricComplementar.ToString(true));
	            	}
					
					// Experimental complementar profiles
					if(profilesSeries.empirischekomplementprofile != null)
					{
						sw.WriteLine("\r\n" + "PCE:");
						
						foreach(var profile in profilesSeries.empirischekomplementprofile)
		            	{
		            		sw.WriteLine(profile.TheoricComplementar.ToString(true));
		            	}
					}
					
					// write interpretation report
					sw.Write(profilesSeries.GetInterpretationReport());
	            }
	        }
	        catch (Exception e)
	        {
	            Console.WriteLine("The could not write file.");
	            Console.WriteLine(e.Message);
	            Assert.Fail(e.Message);
	        }
	        #endregion
		}		
								
		[Test]
		public static void TestHasLustprinzipSyndrom()
		{
			var exampleProfiles = new System.Collections.Generic.List<TestProfile>()
			{
				// pp.379-80, Fall 32
				new TestProfile("+,-!!!", "+,±", "+,0", "0,+"),
				new TestProfile("+,-!!", "+,±", "+,0", "0,+"),//II
				new TestProfile("+,-!", "±,±", "+,0", "0,+"),//IV
				new TestProfile("+!,-!!!", "+,±", "+,0", "0,+"),
				new TestProfile("+!,-!!", "±,±", "+,0", "0,+"),
					
				// Buch 3 p.389 Profil IX, X (from p.387) 
				// perversen Lustsyndroms
				new TestProfile("±,±", "-,0", "0,+!", "0,+"),
				new TestProfile("±,-", "+,-", "+,+!", "0,±"),
					
				//p.210
				new TestProfile("-,-!", "+,0", "+,+", "±,0"),
			};
			
			foreach(var profile in exampleProfiles)
			{
				Assert.IsTrue(Syndromatic.HasLustprinzipSyndrom(profile));
			}
		}
	
		[Test]
		public static void TestIrrealenBlocksSyndrom()
		{
			// Buch 3 p.291 VII
			{	var profiles = Fälle.Fall23;
				var haves = new List<int>() {8};
				var haveNots = new List<int>() {1,2,3,4,5,6,7,9,10};
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
			
			// Buch 3 p.301 VII
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {10};
				var haveNots = new List<int>() {1,2,3,4,5,6,7,8,9,};
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
	}
}
