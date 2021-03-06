﻿using System;
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
				errorMessage = profile + " in " + profile.PartOf.Name
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
						errorMessage = profile + " in " + profile.PartOf.Name
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
			List<TestProfile> exampleProfiles, List<TestProfile> expComplementarProfiles)
		{
			TestSeries testSerie = new TestSeries(sexToSet, exampleProfiles, 
			                                      expComplementarProfiles, name);
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
			
			// from Buch2, p.430
			{	var profiles = Fälle.B2BiExi1;
				var haves = new List<int>{1};//
				var haveNots = new List<int>{};//

				TestNoteHelper(InterpretationNotes.Phobie, 
				                       Syndromatic.DetectPhobieNote,
				                       profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestTotaleDesintegrationAffektlebenNote()
		{
			{	var profiles = Fälle.Fall19;
				var haves = new List<int>() {5,6,};//Buch 3 p.274 II.7
				var haveNots = new List<int>() {1,2,3,4,7,8,9,10};
				var note = InterpretationNotes.TotalDesintegrAffekte_ApathieStupor;
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
				TestNoteHelper(note, Syndromatic.DetectKainUndAbel, profiles, haves, haveNots);	
			}
			
			// Buch 3 p.350
			{	var profiles = Fälle.Fall30;
				var haves = new List<int>() {1,4,5};
				var haveNots = new List<int>() {2,3,6,7,8,9,10};
				var note = InterpretationNotes.Kain;
				TestNoteHelper(note, Syndromatic.DetectKainUndAbel, profiles, haves, haveNots);	
			}
			
			
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall32[1].PartOf.Hintergrundprofile);
				
				//Buch 3 p.380 II.2
				var haves = new List<int>() {1,2,5,};
				var haveNots = new List<int>() {3,4,6};
				var note = InterpretationNotes.Kain;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				               profilesHinter, haves, haveNots);
			}
		}
		
		[Test]
		public void TestParoxKainSyndrom()
		{
			// Buch 3 p.398 III
			{	var profiles = Fälle.Fall36;
				var haves = new List<int>() {1,2,4,5,9,6,7,10};
				var haveNots = new List<int>() {8};//3,
				var note = InterpretationNotes.ParoxyKainSyndrom;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				               profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestGeltungsdrang()
		{
			// Buch 3 p.280 VII
			{	var profiles = Fälle.Fall21;
				var haves = new List<int>() {18};
				var haveNots = new List<int>() {11, 12,13,14,15,16,17,19,20};//?
				var note = InterpretationNotes.BesessenheitGeltungsdrangMitte;
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
				var note = InterpretationNotes.PhobischenBesessenheitMitte;
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
		public void TestSchuldUStrafangstMitte()
		{
			{	//B3 p.402 A.3
				var profiles = Fälle.Fall37;
				var haves = new List<int>() {5,3,6};
				var haveNots = new List<int>() {};
				var note = InterpretationNotes.SchuldUStrafangstMitte;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
			
			{	//p.442 I.2.a)
				var profiles = Fälle.Fall39;
				var haves = new List<int>() {2,3,6,7,9};
				var haveNots = new List<int>() {};
				var note = InterpretationNotes.SchuldUStrafangstMitte;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestPhobieExistenzform()
		{
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
			
			// from Buch2, p.430
			{	var profiles = Fälle.B2BiExi1;
				var haves = new List<int>{1};//
				var haveNots = new List<int>{};//

				TestExistenzformHelper(Existenzformen.Hysteriforme, 
				                       Syndromatic.DetectHysteriforme,
				                       profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestEpileptiforme()
		{
			{	// Test derivedspeculatively from Sonzdi intro 1972
				var profiles = Fälle.Fall34;
				var haves = new List<int>() {4,8};//unsure about profil 4, but should be two in total
				var haveNots = new List<int>() {};
				var exForm = Existenzformen.Tötende_Gesinnung_Epileptiforme;
				TestExistenzformHelper(exForm,
				                       Syndromatic.DetectEpileptiforme, profiles, haves, haveNots);
			}
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
			
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall32[1].PartOf.Hintergrundprofile);
				
				//Buch 3 p.380 II.2
				var haves = new List<int>() {1,2,5,};
				var haveNots = new List<int>() {3,4,6};
				var note = InterpretationNotes.MörderE;
				TestNoteHelper(note, Syndromatic.DetectEpileptiforme, profilesHinter, haves, haveNots);	
			}

			{	var profiles = Fälle.Fall36;
				
				//Buch 3 p.398 I.
				var haves = new List<int>() {1,4,5,9,};
				var haveNots = new List<int>() {2,3,6,7,8,10};
				var note = InterpretationNotes.MörderE;
				TestNoteHelper(note, Syndromatic.DetectEpileptiforme, profiles, haves, haveNots);	
			}
			
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall37[1].PartOf.Hintergrundprofile);
				
				//Buch 3 p.402 B.1
				var haves = new List<int>() {5,};
				var haveNots = new List<int>() {1,2,3,4,6};
				var note = InterpretationNotes.MörderE;
				TestNoteHelper(note, Syndromatic.DetectEpileptiforme, profilesHinter, haves, haveNots);	
			}
			
			{	var profiles = Fälle.Fall42;
				
				//Buch 3 p.503
				var haves = new List<int>() {11};
				var haveNots = new List<int>() {1,2,3,4,5,6,7,8,9};
				var note = InterpretationNotes.MörderE;
				TestNoteHelper(note, Syndromatic.DetectEpileptiforme, profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public void TestOtherHysterie()
		{
			//Skala row 14b
			
			// from Buch2, p.430
			{	var profiles = Fälle.B2BiExi2;
				var haves = new List<int>{1, 2};//
				var haveNots = new List<int>{};//

				TestExistenzformHelper(Existenzformen.Hysteriforme, 
				                       Syndromatic.DetectHysteriforme,
				                       profiles, haves, haveNots);
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
		#region ProjektivParanoiden
		
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
				
				// from Buch3, pp.260, 261, 263, Tab. Abb.33 Fall16, prof.X
				// example from Buch 3 p.263, Fall 16, Profil II
				{	var profiles = Fälle.Fall16;
					var haves = new List<int>() {2,10};
					var haveNots = new List<int>() {1,3,};//4,5,6,7,8,9,
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
				TestSeries serie16 = Fälle.Fall17[1].PartOf;
				TestSeries serie17 = Fälle.Fall17[1].PartOf;
				
				SeriesContainsDiagnostic(serie17, Existenzformen.ProjektivParanoide,
				                         Syndromatic.DetectProjektivParanoid);
				SeriesContainsDiagnostic(serie16, Existenzformen.ProjektivParanoide,
				                         Syndromatic.DetectProjektivParanoid);
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
		public void TestProjektivParanoidenSubtypes()
		{
			//Maniforme Paranoid
			{
				var profiles = Fälle.Fall18;
				TestSeries serie = profiles[1].PartOf;
				Existenzformen[] exForms 
					= {Existenzformen.ProjektivParanoide, Existenzformen.Hypomanische_Manische};
				ExistenzFormDetector[] detectors 
					= {Syndromatic.DetectProjektivParanoid, 
					Syndromatic.DetectHypomanische_Manische};
				SeriesContainsDiagnostics(serie, exForms, detectors);
			}
			
			{
				TestSeries serie19 = Fälle.Fall19[1].PartOf;
				
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
				TestSeries serie = profiles[1].PartOf;
				Existenzformen exForm = Existenzformen.Katatoniforme;
				ExistenzFormDetector detector = Syndromatic.DetectKatatoniforme;
				SeriesContainsDiagnostic(serie, exForm, detector);
			}
		}
		
		
		
		#endregion
		
		#region Katatoni
		
		[Test]
		public void TestTotaleKatatonifIchsperrung()
		{
			{	var profiles = Fälle.Fall22;
				//p.310
				var haves = new List<int>() {4,5,7,8,};//3,
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.TotaleKatatonifIchsperrung, 
			                       Syndromatic.DetectKatatoniforme,
			                       profiles, haves, haveNots);
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
				//p.310
				var haves = new List<int>() {4,9,};//1,2,3,6,10,5,7,8,
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.Katatoniforme, 
			                       Syndromatic.DetectKatatoniforme,
			                       profiles, haves, haveNots);
			}
			
			//Katatoniforme 
			{
				TestSeries serie22 = Fälle.Fall22[1].PartOf;
				TestSeries serie23 = Fälle.Fall22[1].PartOf;
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
		public static void TestIrrealenBlocksSyndrom()
		{
			// Buch 3 p.291 VII
			{	var profiles = Fälle.Fall23;
				var haves = new List<int>() {8};
				var haveNots = new List<int>() {1,2,3,4,5,6,7,9,10};
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
			
			// Buch 3 p.301 IV d)
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {10};
				var haveNots = new List<int>() {1,2,3,4,5,6,7,8,9,};
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
			
			// Buch 3 p.353 XI
			{	var profiles = Fälle.Fall31;
				var haves = new List<int>() {9};
				var haveNots = new List<int>() {1,2,3,4,5,6,7,8,10};
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
				TestNoteHelper(note, Syndromatic.FurtherNotes, profiles, haves, haveNots);	
			}
		}
		#endregion
		
		#region InflaivParanoiden
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
				TestSeries serie = profiles[11].PartOf;
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
		
		[Test]//TODO FIXME no test examples
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
		public void TestParanoideGrößenwahnSyndrom()
		{
			{
				var profiles = Fälle.Fall36;
				var haves = new List<int>() {1,9};//2, p.400
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.MordMitte;//FIXME overlapping mitte, add Größenwahn
				TestNoteHelper(note, Syndromatic.DetectInflativParanoideMitte, 
				                     profiles, haves, haveNots);
			}
		}
		
		#endregion
		
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
			
			{	 // Buch 3 p.350 I.
				var profiles = Fälle.Fall30;
				var haves = new List<int>() {7};////TODO revise: 1,
				var haveNots = new List<int>() {2,3,4,5,9,};////TODO revise: 6,8,10
				TestNoteHelper(note, Syndromatic.DetectParanoideSpaltungsSyndrom,
				                     profiles, haves, haveNots);
			}
			
			{	 // Buch 3 p.480
				var profiles = new List<TestProfile>();
				profiles.Add(null);
				profiles.AddRange(Fälle.Fall40[1].PartOf.Hintergrundprofile);
				var haves = new List<int>() {7,};//1,2,3,4,5,6,8,9,10
				var haveNots = new List<int>() {};
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
	
		[Test]//[Ignore]//fix and restore
		public void TestHeboide_ProjektiveHypochondrischeSynd()
		{
			{	/*Buch 3 p.298 4.a)*/
				var profiles = Fälle.Fall24;
				var haves = new List<int>() {1,3,4,5,6,7,9,10,2};//
				var haveNots = new List<int>() {};
				var note = InterpretationNotes.HebephreneSyndrom;
				TestNoteHelper(note, Syndromatic.DetectHeboide, 
				               profiles, haves, haveNots);
			}
			
			{	/*Buch 3 p.300 IV.1, IV.2.*/
				var profiles = Fälle.Fall25;
				var haves = new List<int>() {9,1,3,4,10,2,5,6,};//
				var haveNots = new List<int>() {7,8};
				var note = InterpretationNotes.HebephreneSyndrom;
				TestNoteHelper(note, Syndromatic.DetectHeboide, 
				               profiles, haves, haveNots);
			}
			
			// Buch 3 p.301 IV
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {1,2,4,3,7,8,9,10};
				var haveNots = new List<int>() {};//5,6,
				var note = InterpretationNotes.HebephreneSyndrom;
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
			
			// from Buch5, p.115
			// 9 out of ten profile have it. 
			{	var profiles = Fälle.Fall31;
				var haves = new List<int>{6,8,1,5,7,10,4,2,9,};//
				var haveNots = new List<int>{3,};//

				TestExistenzformHelper(Existenzformen.Hypomanische_Manische, 
				                       Syndromatic.DetectHypomanische_Manische,
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
				var haveNots = new List<int>{3,4,6,8,9,10};//1,

				TestExistenzformHelper(Existenzformen.Depressive_Melancholische, 
				                       Syndromatic.DetectDepressive_Melancholische,
				                       profiles, haves, haveNots);
			}
			
			// from Buch3, p.352 VII.
			{	var profiles = Fälle.Fall30;
				var haves = new List<int>{4,7,1,6,2,9,10};//
				var haveNots = new List<int>{3,5,};//8,simply C0-

				TestExistenzformHelper(Existenzformen.Depressive_Melancholische, 
				                       Syndromatic.DetectDepressive_Melancholische,
				                       profiles, haves, haveNots);
			}
			
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall31[1].PartOf.Hintergrundprofile);
				
				//Buch 5 p.115, 9 out of 10 have this exForm
				var haves = new List<int>() {1,2,3,4,5,6,7,8,9,10,};//which one should not have it?
				var haveNots = new List<int>() {};				
				TestExistenzformHelper(Existenzformen.Depressive_Melancholische,
				                       Syndromatic.DetectDepressive_Melancholische,
				                       profilesHinter, haves, haveNots);
			}
			
			// from Buch2, p.430
			{	var profiles = Fälle.B2BiExi2;
				var haves = new List<int>{1, 2};//
				var haveNots = new List<int>{};//

				TestExistenzformHelper(Existenzformen.Depressive_Melancholische, 
				                       Syndromatic.DetectDepressive_Melancholische,
				                       profiles, haves, haveNots);
			}
		}		
	
		[Test]
		public void TestSucht()
		{
			{
				var suchtProfiles = new List<TestProfile>()
				{				
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
			
			{	var profiles = Fälle.B3Tab50IV;
				var haves = new List<int>() {};//p.428 1,2,
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.CompulsiveZwang,
				                       Syndromatic.DetectKontaktPsychopathische,
				                       profiles, haves, haveNots);
				TestNoteHelper(InterpretationNotes.Sucht,
				               Syndromatic.DetectKontaktstörungen,
				               profiles, haves, haveNots);	
			}
			
			{	var profiles = Fälle.Fall01;
				//2(issues: s±, d+, m-),10,3,5
				var haves = new List<int>() {7,8,4};//2,10,3,5,1,9,
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.KontaktPsychopathische,
				                       Syndromatic.DetectKontaktPsychopathische,
				                       profiles, haves, haveNots);
				TestNoteHelper(InterpretationNotes.Sucht,
				               Syndromatic.DetectSucht,
				               profiles, haves, haveNots);	
			}
			
			{	var profiles = Fälle.Fall38;
				var haves = new List<int>() {4,6,1,5};//
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.KontaktPsychopathische,
				                       Syndromatic.DetectKontaktPsychopathische,
				                       profiles, haves, haveNots);
				TestNoteHelper(InterpretationNotes.Sucht,
				               Syndromatic.DetectSucht,
				               profiles, haves, haveNots);	
			}
		}
	
		[Test]
		public void TestSuchtMitte()
		{
			{	var profiles = Fälle.Fall01;
				//p.434 V.p.435 VI.
				var haves = new List<int>() {7,3,10};//4,8,2
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.TrunksuchtMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
			
			{	var profiles = Fälle.Fall38;//p.439
				var haves = new List<int>() {1,5,};//2,3,4,6
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.TrunksuchtMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
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
		public void TestHypomanischeOrHaltlose()
		{
			{	var profiles = Fälle.Fall25;
				var haves = new List<int>() {3,5,6,9,10};// Buch 3 p.300 V.1.
				var haveNots = new List<int>() {1,2,4,7,8,};
				
				var noteSex = InterpretationNotes.HypomanischeOrHaltlose;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall26;
				var haves = new List<int>() {2,5,6,8,9};// Buch 3 p.301 V.1.
				var haveNots = new List<int>() {1,3,4,7,10};
				
				var noteSex = InterpretationNotes.HypomanischeOrHaltlose;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall02;
				var haves = new List<int>() {4,5,6,7,8,9,10};//Buch 3 p.440 I.1
				var haveNots = new List<int>() {1,2,3};
				
				var noteSex = InterpretationNotes.HypomanischeOrHaltlose;
				TestNoteHelper(noteSex, Syndromatic.FurtherNotes, 
				                     profiles, haves, haveNots);		
			}
		}
	
		[Test]
		public void TestMelancholischeMitte()
		{		
			// Buch 3 p.350
			{	var profiles = Fälle.Fall30;
				var haves = new List<int>() {1,4,2,6};
				var haveNots = new List<int>() {3,5,7,8,9,10};
				var note = InterpretationNotes.MelancholischeMitte;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				               profiles, haves, haveNots);
			}
		}
		
		[Test]
		public void TestManischeMitte()
		{		
			// Buch 3 p.353 V.
			{	var profiles = Fälle.Fall31;
				var haves = new List<int>() {4,8,6,3,7};
				var haveNots = new List<int>() {1,2,5,9,10};
				var note = InterpretationNotes.ManischeMitte;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				               profiles, haves, haveNots);
			}
		}
	}
	
	[TestFixture]
	public class TestSexAndKontaktePsychopatische : BaseSzondiUnitTests
	{
		[Test]
		public void TestOtherPsychopatischenMitte()
		{
			{	// PositiveSchwacheMitte p.435 VI
				var profiles = Fälle.Fall01;
				var haves = new List<int>() {7,3,10,4,};//2,8 just P00, TODO fix condition
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.VerlustDerMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
					
			{	// PositiveSchwacheMitte p.439
				var profiles = Fälle.Fall38;
				var haves = new List<int>() {1,5};//,2 just Sch00 verlust des ichs, TODO fix condition
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.VerlustDerMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
			
			{	// PositiveSchwacheMitte p.440
				var profiles = Fälle.Fall02;
				var haves = new List<int>() {3,5,6,9,10};
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.VerlustDerMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
			
			{	// PositiveSchwacheMitte p.442
				var profiles = Fälle.Fall39;
				var haves = new List<int>() {4};//10 (Schwache? p.383 does not include it)
				var haveNots = new List<int>() {2,3,5,6,7,8,9};//1, (why P00 not?)
				TestNoteHelper(InterpretationNotes.VerlustDerMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
		}
		
		[Test]
		public static void TestPsychopatischerMitte()
		{
			{	var profiles = Fälle.Fall33;
				var haves = new List<int>() {8,};//p.383
				var haveNots = new List<int>() {1,2,3,5,4,6,7,9,10};//
				
				var note = InterpretationNotes.PsychopatischerMitte;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall33;
				var haves = new List<int>() {1,5,4,6,7};//p.383
				var haveNots = new List<int>() {2,3,8,9,10};//
				
				var note = InterpretationNotes.SchwacheMitte;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	// PositiveSchwacheMitte p.439 I
				var profiles = Fälle.Fall38;
				var haves = new List<int>() {3,4,6};
				var haveNots = new List<int>() {};
				TestNoteHelper(InterpretationNotes.PositiveSchwacheMitte,
				               Syndromatic.DetectIntepretationNotes,
				               profiles, haves, haveNots);	
			}
		}
	
		[Test]
		public static void TestLustprinzipSyndrom()
		{
			{	var profiles = Fälle.Fall12;
				var haves = new List<int>() {1};//p.210
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall32;
				var haves = new List<int>() {1,2,4,5,6};//p.380
				var haveNots = new List<int>() {};//TODO review 3,
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
			
			//TODO p.375: add Fall 33 (p.384) for Lustprinzip_Perversionssyndrom
			{	var profiles = Fälle.Fall33;
				var haves = new List<int>() {1,9,4};//p.383// perversen Lustsyndroms
				var haveNots = new List<int>() {2,3,5,6,7,8,10};
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall34;
				var haves = new List<int>() {9,10};//p.389// perversen Lustsyndroms
				var haveNots = new List<int>() {3,5,6,7,8,};//1,2,4,
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall35;
				var haves = new List<int>() {1,2};//p.394// polymorph perverse Lustsyndrom
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);
			}
			
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall35[1].PartOf.Hintergrundprofile);
				
				var haves = new List<int>() {1,2};//p.394// polymorph perverse
				var haveNots = new List<int>() {};
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profilesHinter, haves, haveNots);
			}
			
			{	var profilesEmpirischekomp = new List<TestProfile>();
				profilesEmpirischekomp.Add(null);//To preserve numbering
				profilesEmpirischekomp.AddRange(Fälle.Fall35[1].PartOf.empirischekomplementprofile);
				
				var haves = new List<int>() {1,};//p.394// polymorph perverse
				var haveNots = new List<int>() {2};
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectIntepretationNotes, 
				                     profilesEmpirischekomp, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall36;
				var haves = new List<int>() {3};//p.394// polymorph perverse Lustsyndrom
				var haveNots = new List<int>() {1,4,5,6,7,8,9,10};//2,FIXME d- only in special cases apparently
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectLustprinzip, 
				                     profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall38;
				var haves = new List<int>() {3,4};//p.439 I.1
				var haveNots = new List<int>() {};//
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectLustprinzip, 
				                     profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall02;
				var haves = new List<int>() {1,2,3};//p.441 III.4
				var haveNots = new List<int>() {};//
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectLustprinzip, 
				                     profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall39;
				var haves = new List<int>() {1,3,5,6,7,8,4,10,9};//p.442 I.
				var haveNots = new List<int>() {2};//
				
				var note = InterpretationNotes.Lustprinzip;
				TestNoteHelper(note, Syndromatic.DetectLustprinzip, 
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
		public void TestSadismus()
		{		
			{	var profilesHinter = new List<TestProfile>();
				profilesHinter.Add(null);//To preserve numbering
				profilesHinter.AddRange(Fälle.Fall32[1].PartOf.Hintergrundprofile);
				
				//Buch 3 p.380 II.2
				var haves = new List<int>() {1,2,5,};
				var haveNots = new List<int>() {3,4,6};
				var note = InterpretationNotes.SadistischeKain;
				TestNoteHelper(note, Syndromatic.DetectPerversionSadomasochismus, 
				               profilesHinter, haves, haveNots);
			}
		}
		
		[Test]
		public void TestTriebzielinversion()
		{
			//TODO add p.320 c) latente Triebzielinversion
			var noteSex = InterpretationNotes.Triebzielinversion;
			
			{	var profiles = Fälle.Fall16;
				var haves = new List<int>() {2,10,4,5,6,7,8,9,};// Buch 3 p.262 IX.1};
				// +± 
				var haveNots = new List<int>() {1,3,};
				TestNoteHelper(noteSex, Syndromatic.DetectTriebzielinversion, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall33;
				var haves = new List<int>() {1,2,3,4,5,6,7,8,9,10};// Buch 3 p.385
				var haveNots = new List<int>() {};
				TestNoteHelper(noteSex, Syndromatic.DetectTriebzielinversion, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall05;
				var haves = new List<int>{1,2,3,4,5,7,8,9};//Buch3, pp.413-4
				var haveNots = new List<int>{6,10};//
				TestNoteHelper(noteSex, Syndromatic.DetectTriebzielinversion,
				               profiles, haves, haveNots);
			}
			
			{	//p.434 III.1
				var profiles = Fälle.Fall01;
				var haves = new List<int>{2,3,4,5,9,10};//
				var haveNots = new List<int>{1,6,};//7,8
				TestNoteHelper(noteSex, Syndromatic.DetectTriebzielinversion,
				               profiles, haves, haveNots);
			}
			
			{	//p.444 III.2. a)
				var profiles = Fälle.Fall39;
				var haves = new List<int>{1,2,3,4,5,6,7,8,9,10};//
				var haveNots = new List<int>{};//
				TestNoteHelper(noteSex, Syndromatic.DetectTriebzielinversion,
				               profiles, haves, haveNots);
			}
		}
		
		[Test][Ignore]
		public void TestSexualstörungen()
		{
			//TODO FIXME 
			//detect notable cases of "Sexualstörungen" that differ from "Triebzielinversion"
			
			/* TODO: DetectSexualstörungen deleted. replaced by Triebzielinversion. Male only. 
			// No kontaktstörungen
			// Sexualstörungen pp.261 X., example p.262, //p.262 IX.2.
			*/
			
			{	var profiles = Fälle.Fall16;
				var haves = new List<int>() 
				{	2, 10,	// Buch 3 p.262 IX.1, profiles 2, 10 p.263
					4, 5, 6, 7, 8, 9,// p.262 IX.2
				};
				var haveNots = new List<int>() {1, 3, };
				
				var noteSex = InterpretationNotes.Sexualstörungen;
				TestNoteHelper(noteSex, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall17;
				var haves = new List<int>() {3, 4, 5, 8, 9, 10};// Buch 3 p.264 X.3,//TODO 1,2,6,7
				var haveNots = new List<int>() {};
				
				var noteSex = InterpretationNotes.Sexualstörungen;
				TestNoteHelper(noteSex, Syndromatic.DetectIntepretationNotes, 
				                     profiles, haves, haveNots);		
			}
		}
		
		[Test]
		public void TestSzondiHomosexualität()
		{
			var maleProfiles = new List<TestProfile>()
			{
				// Buch 3 p.406-
				new TestProfile("+,-", "+,-", "0,±", "+,+"),
				new TestProfile("±,-", "±,-", "0,±", "+,+"),
			};
			SetSexAndNameForProfiles(Sex.Male, "[male profiles]", maleProfiles);
			VerifyExistenzformHelper(Existenzformen.Inversion_SzondiHomo_Trans, 
			                         maleProfiles,
			                        Syndromatic.DetectInversion);
						
			// from Buch3, p.415
			{	var profiles = Fälle.B3Tab49Mann;
				var haves = new List<int>{1,2,3,4,5,6,7,8,9,};//FIXME 10,11,12,13,14
				var haveNots = new List<int>{};

				TestExistenzformHelper(Existenzformen.Inversion_SzondiHomo_Trans, 
				                       Syndromatic.DetectInversion,
				                       profiles, haves, haveNots);
			}
			
			// from Buch3, p.415
			{	var profiles = Fälle.B3Tab49Frau;
				var haves = new List<int>{1,14};//FIXME 2,3,4,5,6,7,8,9,10,11,12,13
				var haveNots = new List<int>{};

				TestExistenzformHelper(Existenzformen.Inversion_SzondiHomo_Trans, 
				                       Syndromatic.DetectInversion,
				                       profiles, haves, haveNots);
			}
			
			// from Buch3, pp.413-4 (Fall 5 p.187)
			{	var profiles = Fälle.Fall05;
				var haves = new List<int>{2,3,4,};//
				var haveNots = new List<int>{1,5,6,7,8,9,10};//

				TestExistenzformHelper(Existenzformen.Inversion_SzondiHomo_Trans, 
				                       Syndromatic.DetectInversion,
				                       profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.Fall39;
				var haves = new List<int>{4,10};//p.444 III.1
				var haveNots = new List<int>{};

				TestExistenzformHelper(Existenzformen.Inversion_SzondiHomo_Trans, 
				                       Syndromatic.DetectInversion,
				                       profiles, haves, haveNots);
			}
			
			// from Buch2, p.430
			{	var profiles = Fälle.B2BiExi1;
				var haves = new List<int>{1};//
				var haveNots = new List<int>{};//

				TestExistenzformHelper(Existenzformen.Inversion_SzondiHomo_Trans, 
				                       Syndromatic.DetectInversion,
				                       profiles, haves, haveNots);
			}
			
			{	//TODO FIXME
				var profiles = Fälle.Fall07;
				TestSeries serie = profiles[1].PartOf;
				Existenzformen exForm = Existenzformen.Inversion_SzondiHomo_Trans;
				ExistenzFormDetector detector = Syndromatic.DetectInversion;
				//SeriesContainsDiagnostic(serie, exForm, detector);
			}
		}
	
		[Test]
		public static void TestAnalmasochismus()
		{
			{	var profiles = Fälle.Fall32;
				var haves = new List<int>() {1,2,4,5,6};//p.380
				var haveNots = new List<int>() {3};//
				
				var note = InterpretationNotes.Analmasochismus;
				TestNoteHelper(note, Syndromatic.DetectPerversionSadomasochismus, 
				                     profiles, haves, haveNots);		
			}
			
			{	var profiles = Fälle.Fall36;
				var haves = new List<int>() {};//1,3//p.400
				var haveNots = new List<int>() {2,4,5,6,7,8,9,10};//
				
				var note = InterpretationNotes.Analmasochismus;
				TestNoteHelper(note, Syndromatic.DetectPerversionSadomasochismus, 
				                     profiles, haves, haveNots);		
			}
		}
		
		[Test]
		public static void TestAnalsadismus()
		{
			{	var profiles = Fälle.Fall36;
				var haves = new List<int>() {4,5,6,7,8,9};//p.400
				var haveNots = new List<int>() {1,2,3,10};//
				
				var note = InterpretationNotes.Analsadismus;
				TestNoteHelper(note, Syndromatic.DetectPerversionSadomasochismus, 
				                     profiles, haves, haveNots);		
			}
		}
		
		[Test]
		public static void TestFetischismus()
		{
			{	var profiles = Fälle.Fall32;
				var haves = new List<int>() {3};//p.380
				var haveNots = new List<int>() {};//1,2,4,5,6,
				
				var note = InterpretationNotes.Fetischismus;
				TestNoteHelper(note, Syndromatic.DetectPerversionSadomasochismus, 
				                     profiles, haves, haveNots);		
			}
		}
		
		[Test]
		public void TestPolymorphPervers()
		{
			var polymorphPerversProfiles = new List<TestProfile>()
			{
				// Buch 3 p.479 Profil VI, VIII, X
				// new TestProfile("+,-", "±,-", "±,0", "0,±"),// VI, VIII, X

				// p.502, IV (profiles 6, 8 from p.501)
				// p.394
			};
			
			SetSexForProfiles(Sex.Male, polymorphPerversProfiles);
			var note = InterpretationNotes.PolymorphPervers;
			//TestExistenzBestimmung.VerifyInterpretationNote(note, polymorphPerversProfiles);		
		}
	}
	
	[TestFixture]
	public class TestSchutzExistenzFormen : BaseSzondiUnitTests
	{
		[Test]
		public void TestCompulsiveNeurotic()
		{
			{	var profiles = Fälle.Fall40;
				var haves = new List<int>() {2,5,6/*=8,10*/,9};//Buch3 p.479 Tab. 52
				var haveNots = new List<int>() {4,};//1,3,7
				TestExistenzformHelper(Existenzformen.CompulsiveZwang, 
			                       Syndromatic.DetectCompulsiveNeurotic,
			                       profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.B3Tab50I;
				var haves = new List<int>() {1,2,};//p.428
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.CompulsiveZwang, 
			                       Syndromatic.DetectCompulsiveNeurotic,
			                       profiles, haves, haveNots);
			}
		}	
		
		[Test]
		public void TestHypochondrische()
		{
			{	var profiles = Fälle.Fall27;
				var haves = new List<int>() {1,2,};//p.317. Doubt: 4,6
				var haveNots = new List<int>() {};//3,10,5,7,8,9,
				TestExistenzformHelper(Existenzformen.Hypochondrische_Organneurose, 
			                       Syndromatic.DetectHypochondrischeExistenzformen,
			                       profiles, haves, haveNots);
			}
			
			{	var profiles = Fälle.B2Abb40;
				var haves = new List<int>() {1};
				var haveNots = new List<int>() {};
				TestExistenzformHelper(Existenzformen.Hypochondrische_Organneurose, 
			                       Syndromatic.DetectHypochondrischeExistenzformen,
			                       profiles, haves, haveNots);
			}
		}
	}
	
	[TestFixture]
	public class TestExistenzBestimmung : BaseSzondiUnitTests
	{				
		[Test]//[Ignore]
		public static void TestParseAndSyndromaticProfiles()
		{
			string inputFilename = "profiles.txt";
			
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
	            			
	            			line = sr.ReadLine();
	            		}
	            	}
	            	#endregion
	            }
	            //assume it corresponds to fall 34
	            string profilesSeriesName = "Fall 34";
	            profilesSeries 
	            	= new TestSeries(testTakerSex, foregroundProfiles, EKPprofiles, profilesSeriesName);
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
	        WriteToFile(profilesSeries);
	        
	        var serieFall31 = Fälle.Fall31[1].PartOf;
	        serieFall31.Interpret();
	        WriteToFile(serieFall31);
	        #endregion
		}

		private static void WriteToFile(TestSeries profilesSeries)
		{
			string dateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
			string outputFilename = "detectedExistenzformen " 
	        	+ " (" + dateTimeStamp + ") "
				+ profilesSeries.Name
				+ ".txt";
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
		            		sw.WriteLine(profile.ToString(true));
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
		}
	}
}
