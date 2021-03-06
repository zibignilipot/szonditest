﻿using System;
using System.Collections.Generic;

namespace SzondiTest
{
	/// <summary>
	/// 
	/// </summary>
	public class Syndromatic
	{
		/*
		  * TODO
		  * Review "Triebüberdruck" sections to update conditions on which factors can have !
		  * */
		
		public static List<Existenzformen> BestimmungDerExistenzformen(TestProfile profile)
		{
			#region Interpretation notes			
			DetectIntepretationNotes(profile);
			#endregion
			
			var detectedExistenzFormen = new List<Existenzformen>();
			
			#region Existenzformen
			// Ich
			DetectPraepsychotischeEvSuicidale(profile);
			DetectProjektivParanoid(profile);
			DetectInflativParanoid(profile);
			DetectKatatoniforme(profile);
			DetectHeboide(profile);
				
			// Affekt
			DetectEpileptiforme(profile);
			DetectHysteriforme(profile);	
			
			// Schutz
			DetectCompulsiveNeurotic(profile);
			DetectHypochondrischeExistenzformen(profile);
			DetectSozialisierendeAlltagsmenschen(profile);
			DetectGeistigHumanisierendeMenschen(profile);
			
			// Sexuelle
			DetectPerversionSadomasochismus(profile);
			DetectInversion(profile);
			
			// Kontakt
			DetectHypomanische_Manische(profile);		
			DetectDepressive_Melancholische(profile);					
			DetectKontaktPsychopathische(profile);
			
			// doubles
			DetectManiformeParanoide(profile);

			#endregion
			
			#region	Gegen Synmptome, GegenInterpretationNotes
			DetectGegenInterpretationNotes(profile);
			#endregion
			
			return detectedExistenzFormen;			
		}
		
		internal static void DetectIntepretationNotes(TestProfile profile)
		{
			DetectProjektivParanoideMitte(profile);
			DetectInflativParanoideMitte(profile);
			DetectInflativParanoidSyndrom(profile);
			
			// p.260 n. VII
			DetectParanoideKammSyndrom(profile);			
			// p.261 n. IX
			DetectKontaktstörungen(profile);			
			// pp.358-9
			DetectLustprinzip(profile);
			
			// Buch 3, p.484 IV b), p.266 XI.2, Skala row 14c			
			DetectPhobieNote(profile);
			
			// Further notes
			FurtherNotes(profile);
		}
		
		internal static void DetectGegenInterpretationNotes(TestProfile profile)
		{
			if(profile.HasExistenzform(Existenzformen.Depressive_Melancholische) 
			   || NotesGroups.HasAnyMelancholischeNote(profile))
			{
				// p.444
				if(!profile.PartOf.HasFactorReaction("+", Factors.k, profile.dimension)
				   || profile.Sch.IsAny("-,+", "0,±")) //instead of +-
				{
					profile.AddInterpretationNote(InterpretationNotes.GegenZirkuläreDepression);
				}
			}
			
			if(profile.HasExistenzform(Existenzformen.CompulsiveZwang)
			   || NotesGroups.HasAnyZwangsNote(profile))
			{	
				if(!profile.NoHypertension)
				{	// p.478 and p.?: Zwang usually have no hypertension
					profile.AddInterpretationNote(InterpretationNotes.GegenZwangs);
				}
			}
		}
		
		#region AffektExistenzFormen (13-14)	
		internal static void DetectEpileptiforme(
			TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala row 13e (Mörder-E)
			// Buch 3, p.493
			// und Buch 2, Existenzskala Tabelle 25
			{
				if((profile.k.IsAny("-", "-!", "-!!", "-!!!") //Überdruck p.398 k-!!
				    || profile.Sch.ContainsFactorReaction("-", Factors.p)//Überdruck?
				   )
				   &&
				   profile.m.IsAny("-", "-!") //±ungültig
				  )
				{
					if(profile.e.IsEqualTo ("-")) //Überdruck?
					{
						// Mörder-E (Raubmördersyndrom p.398)
						detected = true;
						profile.AddInterpretationNote(InterpretationNotes.MörderE);
					}
					else if (profile.e.IsEqualTo("0"))
					{
						// p.493 3. b)
						var note = InterpretationNotes.MörderE_mitVentil;
						profile.AddInterpretationNote(note);
					}
				}
			}
			#endregion
			
			#region Existenzskala row 13c (Epi-Mitte)
			// Buch 3, p.494, V
			// Buch 3, p.499, VI, Column C
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( (profile.P.EqualsTo("-,-") || profile.P.EqualsTo("-,-!"))
				   &&
				   (profile.Sch.EqualsTo("-,-") 
				    || profile.Sch.EqualsTo("-!,-")
				    || profile.Sch.EqualsTo("-,-!")
				   ))
				{
					//Epi-Mitte
					detected = true;
				}
			}
			#endregion
			
			string[] paroximalSukzession = //rows ab
			{"-,+", "-,0", "-,-", "-,-!", "0,-", "0,-!",
				"-,+!", "-,±", "±,±", "±,+", "0,±",
				"-,+", "-,+!", "-,0", "-,±", "0,0", "0,+", "±,+", "±,-"
			};
			
			#region Existenzskala row 13a (reine Kain)
			// p.495, Tabelle 55, III 4-5
			if(profile.HasInterpretationNote(InterpretationNotes.ReineKainMitte))
			{
				detected = true;
			}
			
			#endregion
			
			#region Existenzskala row 13a 
			// p.495 tab.55, V: e high mobility, Sch00 in serie, m-, h+!
			// p.494 IV. 1. e high mobility (0, +, -, ±)
			{
				if(profile.m.IsEqualTo("-")
				   && profile.h.IsAny("+!", "+!!", "+!!!")
				   && profile.SerieHasVectorReaction("0,0", Vectors.Sch))
				{
					if(profile.GroßeMobilität("0", "±", "-", "+", Factors.e))
					{
						detected = true;
					}
				}
			}
			
			// p.495 tab.55, II: Sch00, e high mobility
			{
				if((profile.Sch.EqualsTo("0,0") 
				    || (profile.k.IsAny("-", "-!") && profile.p.IsAny("-", "-!"))
				    || profile.InSukzession("0,0", "±,±", Vectors.Sch))
				   && profile.GroßeMobilität("0", "±", "-", "+", Factors.e))
				{
				   detected = true;
				}
			}
			
			{
				if(profile.IsAlways("0", Factors.e) 
				   && profile.IsAlways("0,0", Vectors.Sch))
				{
					 detected = true;
				}
			}
			
			if(profile.Sch.EqualsTo("+,-") 
			   && profile.InSukzession(paroximalSukzession, Vectors.P)
			   && profile.m.IsAny("-", "-!", "-!!", "0", "+", "+!", "+!!"))
			{
				detected = true;
			}
			#endregion
			
			#region Existenzskala row 13b
			if(profile.HasInterpretationNote(InterpretationNotes.MörderE)
				   || profile.HasInterpretationNote(InterpretationNotes.MörderE_mitVentil))
			{
				if(profile.Sch.IsAny("-,±", "±,-")
				   // && profile.P.EqualsTo("0,0")
				  )
				{
					detected = true;
				}
			}
			
			if(profile.s.IsAny("-", "-!", "-!!", "0", "±")
			  && profile.GroßeMobilität("0", "±", "-", "+", Factors.e)
			  && profile.Sch.IsAny("-,±", "±,-", "-,+")
			  && profile.m.IsAny("+", "0"))
			{
				detected = true;
			}			
			
			//only from skala 13b, to detect Buch3 Fall34, profil 4
			if(//profile.GroßeMobilität("0", "±", "-", "+", Factors.e) &&
			   profile.P.IsAny("-,+", "-,+!", "-,0", "-,±", "0,0", "0,+", "±,+", "±,-")
			  && profile.Sch.IsAny("-,+", "-,±", "±,-", "0,+", "0,±", "+,+!", "±,+")
			  && profile.m.IsAny("+", "0"))
			{
				detected = true;
			}
			
			#endregion
			
			#region Skala 13d
			{
				if((profile.e.IsAny("-!", "-!!", "+!", "+!!") 
				   || profile.P.IsAny("-,-!", "+!,-", "-!,+"))
				   &&
				   profile.Sch.IsAny("0,0", "±,±", "±,-", "-,±", "-,+", "+,-", "0,+", "+,0")
				   // && profile.m.IsEqualTo("-") optional per Buch2 p.430
				   && !profile.m.IsEqualTo("±") //only for 13e?
				  )
				{
					detected = true;
				}
			}
			#endregion
			
			#region Syndromatic pp.494-6
			// p.494, IV. Faktorenverband
			{
				bool[] EpileptiformeFaktorenverband = new bool[]{
				                	profile.e.IsAny("-", "0"),
				                	profile.hy.IsAny("-!", "-!!"),
				                	profile.k.IsAny("0", "±", "-"),
				                	profile.p.IsAny("0", "±", "-"),
				                	profile.d.IsEqualTo("0"),
				                	profile.m.IsEqualTo("-"),
				                   };
				
				if(Faktorenverband(EpileptiformeFaktorenverband))
				{
					detected = true;
				}
			}
			
			// p.495-6, Tabelle 55 
			{
				ushort matchedConditions = 0;
				
				if(profile.HasInterpretationNote(InterpretationNotes.MörderE)
				   || profile.HasInterpretationNote(InterpretationNotes.MörderE_mitVentil))
				{
					matchedConditions++;
				}
					
				// I 1-2, XIV 2-4
				//row 13a (olnly for Sukzession)
				if(profile.InSukzession("-,-", "0,-", Vectors.P) 
				   || profile.InSukzession("-,-!", "0,-", Vectors.P)
				   || profile.InSukzession("-,-", "0,-!", Vectors.P)
				   || profile.InSukzession("-,-!", "0,-!", Vectors.P)
				  )
				{
					matchedConditions++;
				}
				
				// VI 3
				if(profile.GroßeMobilität("0", "±", "-", "+", Factors.e))
				{
					matchedConditions++;
				}
				
				// if any 2 conditions match
				if(matchedConditions >= 2)
				{
					detected = true;
				}
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Tötende_Gesinnung_Epileptiforme);
			}
		}
		
		internal static void DetectPhobieNote(TestProfile profile)
		{
			// Buch 3, p.484 IV b), p.266 XI.2
			// und Buch 2, Existenzskala Tabelle 25 row 14c (Hysteriforme)
			
			if(profile.P.EqualsTo("+,0"))
			{
				var note = InterpretationNotes.Phobie;
				profile.AddInterpretationNote(note);
			}
		}
		
		internal static void DetectHysteriforme(TestProfile profile)
		{

			bool detected = false;
			
			#region Existenzskala row 14c (Hysteriforme)
			{
				if( profile.P.EqualsTo("-,-")
				   &&
				   (profile.Sch.IsAny("±,+", "±,±", "0,0"))
				  )
				{
					detected = true;
				}
			}
			
			{
				if( profile.P.EqualsTo("-,-")
				   &&
				   (profile.Sch.IsAny("+,±", "+,-", "±,-"))
				  )
				{
					profile.AddInterpretationNote(InterpretationNotes.PossiblyHysteriforme);
				}
			}
			
			// Buch 3, p.483 intro, p.484 IV b)
			// und Buch 2, Existenzskala Tabelle 25
			{	if(profile.HasInterpretationNote(InterpretationNotes.Phobie))
				{
					// phobie
					//Buch 2 p.430
					detected = true;
				}
			}
			
			// Buch 3, p.490, V phobischer Faktorenverban
			{
				if(Faktorenverband(new bool[] {
				                   	profile.k.IsEqualTo("±"),
				                   	profile.p.IsAny("±", "+"),
				                   	profile.e.IsEqualTo("+"),
				                   	profile.hy.IsAny("0", "±", "+!"),
				                   	profile.s.IsAny("-", "0"),
				                   	profile.m.IsAny("0", "+!")}))
				{
					detected = true;
				}
			}
			
			//TODO add more from Skala
			#endregion
			
			#region Existenzskala row 14b (Hysteriforme)
			// Buch 3, p.484 IVa,
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( (profile.P.IsAny("+,+", "0,0", "-,-", "-,-!"))//p.484 II.1
				   && 
				   (profile.k.IsAny("-", "-!", "-!!", "-!!!")//p.484 III.a
				   && profile.p.IsAny("±", "0", "+")) 
				   || (profile.Sch.EqualsTo("0,0"))
				  )
				{
					// Konversionshysterie p.484 IVa and row 14b
				   	detected = true;
				}
			}
			
			{
				if( profile.P.IsAny("+,+", "0,0", "-,-!", "+,±", "±,+", "0,+") 
				   && (profile.Sch.IsAny("-,0", "-,+", "-,±", "-!,±", "±,-", "0,-", "0,0") 
				       || profile.Sch.IsAny("±,±", "±,+", "0,0", "+,±", "+,-", "±,-"))
				  )   
				{
				  	detected = true;
				}
			}
			
			{
				if( profile.HasInterpretationNote(InterpretationNotes.Affektflut)
				   || profile.HasInterpretationNote(InterpretationNotes.Affektebbe))
				{
				   	//Buch2 p.430
				  	detected = true;
				}
			}
			#endregion
			
			#region Existenzskala row 14a (Hysteriforme)
			{
				if(profile.hy.IsAny("+!", "+!!", "+!!!")
				   && profile.Sch.EqualsTo("-,±"))
				{
					// Konversionshysterie p.484 IVa and row 14a
				   	detected = true;
				   	var note = InterpretationNotes.KonvHyst;
				   	profile.AddInterpretationNote(note);
				}
				
				// from Skala only
				if(profile.hy.IsAny("+!", "+!!", "+!!!")
				   && profile.Sch.IsAny("-,0", "-,+", "-!,±", "0,0", "+,+")
				   && profile.m.IsAny("+!", "-!")
				  )
				{	detected = true;
				}
			}
			
			// p.486 V
			{
				if(Faktorenverband(new bool[] {
	               	profile.k.IsEqualTo("-") 
	               		&& profile.p.IsAny("±", "0", "+") 
	               		&& profile.e.IsAny("0", "+", "+!", "+!!", "+!!!")
	               		&& profile.hy.IsAny("0", "+")}))
				{
					detected = true;
					var note = InterpretationNotes.KonvHyst;
				   	profile.AddInterpretationNote(note);
				}
			}
			#endregion
			
			#region Existenzskala rows 14abc (Hysteriforme)
			// Buch 3, p.484 IVc,
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( profile.Sch.EqualsTo("0,0")
				   &&
				   (profile.P.HasFactorReaction("-", Factors.hy, FactorsComparisonOptions.Hypertension_insensitive)
				   	&& (profile.P.HasFactorReaction("0", Factors.e) 
				        || profile.P.HasFactorReaction("-", Factors.e))))
				{
					// p.484 IVc and rows 14abc
				   	detected = true;
				}
			}
			#endregion
				
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Hysteriforme);
			}
		}			
		
		/// <summary>
		/// Per p.261 XI. (und p.260 VI.)
		/// </summary>
		/// <param name="profile"></param>
		internal static void DetectAffektstörungenNotes(TestProfile profile)
		{
			InterpretationNotes mainNote = InterpretationNotes.Affektstörungen;
			
			if(profile.P.IsAny("0,-!", "±,-", "0,-"))//0- p.285 I.2.a)
			{
				InterpretationNotes note = InterpretationNotes.SensitiveBeziehungsangst;
				profile.AddInterpretationNote(note);
				profile.AddInterpretationNote(mainNote);
			}
			
			if(profile.P.IsAny("+,-", "+,-!"))
			{
				InterpretationNotes note = InterpretationNotes.Gewissensangst;
				profile.AddInterpretationNote(note);
				profile.AddInterpretationNote(mainNote);
			}
			
			/*if(profile.e.IsEqualTo("0") || profile.hy.IsEqualTo("-!"))
			{
				// TODO
				// condition: if (almost) all the same in profile?
			
				profile.AddInterpretationNote(mainNote);
			}*/
			
			// p.261 XI
			if(profile.P.IsAny("±,-", "0,-") 
			   && profile.InSukzession("±,-", "0,-", Vectors.P))
			{
				InterpretationNotes note = InterpretationNotes.AffektLabilitätUndPolarität;
				profile.AddInterpretationNote(note);
				profile.AddInterpretationNote(mainNote);
			}
			
			//p.266 XI.1, p.305 Xi.5.
			if(profile.P.IsAny("±,0", "±,-"))
			{
				profile.AddInterpretationNote(InterpretationNotes.AffektPolarität);
			}
		}
		
		internal static void DetectKainUndAbel(TestProfile profile)
		{
			// Buch 3, p.279 I.1, p.280 I.
			if(profile.P.IsAny("-,+", "-,+!", "-!,+", "-,0", "-,±"))
			//-0 pp.305 XI., 306 VII
			// -!+ p.350 IV. 4.
			//  doubt -±, -0 p.305 XI.
			{
				var note = InterpretationNotes.Kain;
				profile.AddInterpretationNote(note);
				
				if(profile.P.EqualsTo("-,±"))//auch -0 ?
				{
					profile.AddInterpretationNote(InterpretationNotes.KainMitDilemma);
				}
			}
			
			// B3 p.345 5.
			if(profile.P.EqualsTo("+,-"))//doubt 0- from Skala 7c, specular of kain
			{
				var note = InterpretationNotes.Abel;
				profile.AddInterpretationNote(note);
			}
			
			if((profile.HasInterpretationNote(InterpretationNotes.Kain)
			   || profile.HasInterpretationNote(InterpretationNotes.Abel))
			   && 
			   profile.InSukzession("+,-", "-,+", Vectors.P))
			{
				//p.349 X.I. Syndromatik der Manie
				var note = InterpretationNotes.KainAbelWechsel;
				profile.AddInterpretationNote(note);
			}
		}
		#endregion
		
		#region KontankExistenzFormen (6-8)
		
		//TODO pp.350 Mischformen: 1. paranoide Melancholie, 2. depressive Paranoid,
		// 3. stuporöse Manie, 4. agitierende Katatonie.
		internal static void DetectKSexuellenHaltlosigkeit(TestProfile profile)
		{
			// p.433
			if(profile.PartOf.testTakerSex == Sex.Female
			   && profile.d.IsEqualTo("0")
			   && profile.e.IsEqualTo("0")
			   && profile.s.IsAny("-!", "-!!", "±", "+!!")
			   && profile.k.IsEqualTo("-")
			   && profile.p.IsEqualTo("±")
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.SexuellenHaltlosigkeit);
			}
			
			if(profile.PartOf.testTakerSex == Sex.Male
			   && profile.e.IsAny("0", "±")
			   && profile.k.IsAny("-", "0")
			   && HasLustprinzipSyndrom(profile)
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.SexuellenHaltlosigkeit);
			}
		}
		
		internal static void DetectSucht(TestProfile profile)
		{
			#region Skala row 8a
			// Buch 3 p.430, VI Variation 1., und VII Süchtige Mitte
			if(profile.HasInterpretationNote(InterpretationNotes.VerlustDerMitte)
			   && HasLustprinzipSyndrom(profile))
			{
				// doubt if it is Sucht or just general Psychopatisch
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			#endregion
			
			#region Skala row 8b
			if(profile.hy.IsAny("-!", "-!!") // Buch 3 p.430, XI.1
			   && profile.k.IsEqualTo("0")
			   && HasLustprinzipSyndrom(profile))
			{
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			#endregion
			
			#region Skala row 8c
			// Buch 3 p.430, VI Variation 1.
			// pp.434-5, V. e) Lustprinzip 
			if((profile.s.IsAny("-!", "-!!", "-!!!")
			    || profile.s.IsAny("+!", "+!!", "+!!!")//not found in syndromatik
			    || profile.h.IsAny("+!", "+!!", "+!!!"))//not found in syndromatik
			   && profile.hy.IsEqualTo("0")
			   && profile.k.IsAny("-!", "-!!", "-!!!")
			   && HasLustprinzipSyndrom(profile)// TODO fix special LustS. for row 8
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.Sucht);//Sucht? Not in Skala
			}
			#endregion
			
			#region Skala row 8d
			// Buch 3 p.430, V (Trunksucht)
			if(profile.m.IsAny("+!", "+!!", "+!!!")
			   && (profile.k.IsAny("-!", "-!!", "-!!!") 
			       || profile.Sch.EqualsTo("-,+"))//not found in syndromatik
			   //&& profile.P.IsAny("0,0", "-,+")
			  )
			{
			   	profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}		
			#endregion
			
			//TODO add p.440 Lustprinzip
			
			// Buch 3 p.430, VI Variation 1. (full Faktorenverband)
			if(Faktorenverband(new bool[] {
			        profile.s.IsAny("-", "-!", "-!!", "-!!!"),
					profile.e.IsEqualTo("0"),
					profile.hy.IsAny("0", "+", "+!", "+!!", "+!!!"),
					profile.k.IsAny("0", "-", "-!", "-!!", "-!!!"),
					profile.p.IsAny("0", "±", "+", "+!", "+!!", "+!!!"),
					profile.d.IsAny("0", "+!", "+!!", "+!!!"),
					profile.m.IsAny("0", "+!", "+!!", "+!!!")}))
			{
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			
			// Buch 3 p.430, VI Variation 2. (full Faktorenverban)
			if(Faktorenverband(new bool[] {
				   profile.s.IsAny("-", "-!", "-!!", "-!!!"),
				   profile.e.IsAny("0", "+", "+!", "+!!", "+!!!"),
				   profile.hy.IsAny("0", "+", "+!", "+!!", "+!!!"),
				   profile.k.IsEqualTo("0"),
				   profile.p.IsAny("±", "-", "-!", "-!!", "-!!!"),
				   profile.d.IsAny("-", "-!", "-!!", "-!!!"),
				   profile.m.IsAny("0", "±", "+", "+!", "+!!", "+!!!")}))
			{
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
		}
		
		internal static void DetectKontaktPsychopathische(TestProfile profile)
		{
			bool detected = false;
			
			// covers most of Syndromatic and 8abcd
			if(profile.HasInterpretationNote(InterpretationNotes.Sucht) 
			   || profile.HasInterpretationNote(InterpretationNotes.SexuellenHaltlosigkeit))
			{
				detected =true;
			}
			
			// Skala 8a and Syndromatic VerlustDerMitte Symptom pp.439-40
			if(profile.HasInterpretationNote(InterpretationNotes.VerlustDerMitte)
			   && HasLustprinzipSyndrom(profile))
			{
				detected =true;
			}
			
			// Skala 8c
			//TODO "auch ohne lustsyndrom, per skala. Seems to easily met, find gegen conditions"
			if((profile.h.IsEqualTo("+!") || profile.s.IsAny("-!", "+!")) 
			   && profile.hy.IsEqualTo("0")
			   && profile.k.IsEqualTo("-!")
			  )
			{
				detected =true;
			}
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.KontaktPsychopathische);
			}
		}
		
		internal static void DetectKontaktstörungen(TestProfile profile)
		{
			// Kontaktstörungen pp.261-2
			if(profile.p.IsAny("-", "-!", "-!!", "-!!!")
			   && profile.d.IsAny("+", "+!", "+!!", "+!!!"))
				// Suchen nach dem Vervolger 
			{
				if(profile.m.IsAny("-", "-!", "-!!", "-!!!"))
					// Abtrennung von der realen Welt
				{
					profile.AddInterpretationNote(InterpretationNotes.Kontaktstörungen);
				}
				else
				{
					// p.264 IX
					profile.AddInterpretationNote(InterpretationNotes.SuchenNachVerfolger);
				}
			}
		
			// p.280 IX.3
			if(profile.C.IsAny("0,+", "0,+!", "0,+!!", "0,+!!!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Akzeptationsdrang);
			}
		}	
		
		internal static void DetectDepressive_Melancholische(TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala rows 6ab (Depressive_Melancholische)
			//Buch 3, pp.243-4, Tabelle 34
			//Buch 3, p.272 I.4, p.274 II.6 (depressive Faktorenverband
			{
				//Skala conditions in bold square
				if( profile.k.IsAny("+", "+!", "+!!", "±")//k± p.350 VII
				   && profile.d.IsAny("+", "+!", "+!!", "±")//± from skala only
				   && profile.s.IsAny("-", "-!", "-!!", "0"))//-!! from Buch 5 p.115, Buch3 Fall31, Hinter profil 4
				// p.341 m±, h+ optional (omitted) in Skala
				{
					detected = true;
				}
				
				//Skala 6aconditions not in bold squares
				if(profile.s.IsEqualTo("±")
				   && profile.P.IsAny("-,+", "0,+", "0,0")
				   && profile.k.IsAny("+", "+!", "+!!")
				   && profile.d.IsAny("+", "+!", "+!!", "±"))
				{
					detected = true;
				}
			}
			#endregion
			
			#region Existenzskala row 6c
			if(profile.d.IsAny("+!!", "+!!!"))
			{
				detected = true;
			}
			#endregion
			
			#region Existenzskala row 6d
			if(profile.s.IsAny("+!", "0")
			   && profile.Sch.IsAny("-!,-", "-!,+", "-!,±")
			   && profile.C.IsAny("+,-", "0,0"))
			{
				detected = true;
			}
			#endregion
			
			#region p.243
			
			//Buch 3, p.243, II. 1. a)
			{
				if(profile.Sch.EqualsTo("+,-") 
				   && (profile.S.EqualsTo("+,-") 
				       || profile.C.EqualsTo("+,-")))
				{
					detected = true;
				}
			}
			
			//Buch 3, p.243, II. 1. b)
			{
				if(profile.Sch.EqualsTo("+,-") 
				   && profile.C.EqualsTo("+,±"))
				{
					detected = true;
				}
			}
			
			//Buch 3, p.243, II. 1. c)
			{
				if((profile.Sch.EqualsTo("+,0")
				   && profile.S.EqualsTo("+,-")) 
					||
					(profile.S.EqualsTo("+,0")
					 && profile.Sch.EqualsTo("+,-")))
				{
					detected = true;
				}
			}
			#endregion
			
			if(detected)
			{
				var exForm = Existenzformen.Depressive_Melancholische;
				profile.AddHasExistenzform(exForm);
			}
		}
				
		internal static void DetectHypomanische_Manische(TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala rows 7abc (Hypomanische_Manische)
			//Buch 3, pp.243-4, Tabelle 35
			{
				if( profile.C.IsAny("0,-", "0,-!", "0,-!!")
				   && profile.Sch.Contains("-,-")
				   && profile.S.Contains("+,+!"))
				{
					detected = true;
				}
			}
			#endregion
			
			#region Existenzskala rows 7a
			{
				//TODO for 0- (too easily met), add i-notes that speak gegen this diagnosis
				if(profile.C.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!"))
				{
					// doubt: more stringent condition for 0- ? note only?
					detected = true;
				}
				
				if(profile.C.IsAny("-,-", "-,-!", "-,-!!", "-,-!!!"))
				{
					var note = InterpretationNotes.DepressionHinweis;
					profile.AddInterpretationNote(note);
				}
			}
			#endregion
			
			#region Existenzskala rows 7b
			{
				if((profile.S.IsAny("+,+!", "+,+!!", "+,+!!!", "+,0")
				   || profile.S.IsAny("+!,+!!", "+!!,+!!", "+!!!,+!!")
				   || profile.S.IsAny("+!,+!!!", "+!!,+!!!", "+!!!,+!!!"))				 
				  && profile.k.IsAny("-!", "-!!", "-!!!")
				  && profile.C.IsAny("0,-", "-,-"))
				{
					detected = true;
				}
				
				// doubt, per p.345 and Skala 7b, just k-! and m- enough?
				if(profile.k.IsAny("-!", "-!!", "-!!!")
				   && profile.m.IsAny("-", "-!", "-!!", "-!!!"))//doubt: hypertension?
				{
					detected = true;
				}
				
			}
			#endregion
			
			#region Existenzskala rows 7c
			{
				if(profile.h.IsAny("+", "+!", "+!!")//+! Buch5 p.115, Fall 31 Buch3, profil 2
				   && profile.s.IsAny("+", "+!", "+!!")// + Buch5 p.115, Fall 31 Buch3, profil 2
				  && profile.P.IsAny("0,0", "-,+", "+,-", "0,-")
				  && profile.Sch.IsAny("-!,-", "-!,0", "-,±", "-,-")//k-, p± Buch5 p.115, Fall 31 Buch3, profile 2,9
				  && profile.C.IsAny("0,-", "-,-", "-,-!"))//m-! Buch5 p.115, Fall 31 Buch3, profil 9
				{
					detected = true;
				}
			}
			#endregion
			
			// p.344 Faktorenverband
			if(profile.C.EqualsTo("0,-") 
			   && profile.k.IsAny("-!", "-!!")
			   && profile.s.IsAny("+!", "+!!"))
			// e+, hy- seem optional
			// p-, h+, seem very optional (not even mentioned at p.345)
			{
				detected = true;
			}
			
			if(detected)
			{
				var exForm = Existenzformen.Hypomanische_Manische;
				profile.AddHasExistenzform(exForm);
			}
		}
		#endregion
		
		#region SchutzExistenzformen (11(15), 12, 16, 17)
		internal static void DetectHypochondrischeExistenzformen(TestProfile profile)
			
		{
			//p.309 diff btw Hypocondriac (11u15) and Hebephrene (4):
			// Hypocondriac: inhibits (Sch=-+) or removes (Sch=-0) the obsession
			// Hebephrene: projects the obsession to a body organ (Sc-!-!)
			
			bool detected = false;
			
			#region Skala row 11a
			// p.292, p.460
			if(profile.P.IsAny("0,-", "±,-", "+,-", "+!,-", "+!!,-")
			   // NB.CompareTo k-! only when p0, distinguish from Heboide
			   && profile.Sch.IsAny("-,+", "-,±", "-,0"))
			{
				detected = true;
			}
			
			//"das Syndrom der Hypochondrie" p.317 (and Skala 11a)
			if(profile.HasInterpretationNote(InterpretationNotes.HypochondrischeMitte))
			{	
				detected = true;
			}
			
			#endregion
			
			#region Skala row 11b
			if(profile.P.IsAny("+,-", "+,-!", "±,-", "0,-")
			   && profile.Sch.IsAny("-,-", "-,-!"))
			{
				detected = true;
			}
			#endregion
			
			#region Skala row 11c (Organneurose (15)
			if(profile.P.EqualsTo("-,-")
			   && profile.Sch.EqualsTo("-,-!"))
			{
				detected = true;
			}
			#endregion
			
			#region Skala row 11d
			if(profile.s.IsEqualTo("-")
			   && profile.P.IsAny("0,0", "0,+")
			   && profile.Sch.IsAny("-,±", "-,+", "-!,±", "-!,+", "0,0")
			   && profile.m.IsAny("+!", "+!!"))
			{
				detected = true;
			}
			#endregion
			
			if(profile.HasInterpretationNote(InterpretationNotes.Schuldangst) 
			   && profile.C.EqualsTo("-,+"))
			{
				detected = true;//Erste Phase, p.315
			}
			
			if(profile.P.EqualsTo("+,-!")
			   && profile.Sch.IsAny("+,±", "±,+")
			   && profile.C.IsAny("-,+", "0,±"))
			{
				detected = true;//Zweite Phase, p.315
			}
			
			if(detected)
			{
				var exForm = Existenzformen.Hypochondrische_Organneurose;
				profile.AddHasExistenzform(exForm);
			}
		}
		
		internal static void DetectCompulsiveNeurotic(TestProfile profile)
		{
			bool detected = false;
			
			// TODO more examples (for unit testing and checking) from Buch 4 Ich-A
			// NB. Überdruck (!) is rare
			
			#region Zwangsimpulse (row 12a)
			//p.474 VI. b)
			if(profile.HasMitte("0, ±", "0, ±")
			   || profile.HasMitte("-, ±", "0, ±"))
			{
				detected = true;
				var note = InterpretationNotes.Zwangsimpulse;
				profile.AddInterpretationNote(note);
			}
			#endregion
			
			#region Zwangsimpulse (row 12a und 12c) less strict version
			//row 12a un d p.474 VI. b)
			{
				// 3 Vektoren mit ± Reaktionen
				ushort countAmbivalentVectors = 0;
				
				if(profile.k.IsEqualTo("±") //12a
				   || profile.p.IsEqualTo("±")//12a
				   || profile.Sch.EqualsTo("+,0")) //12c
				{
					countAmbivalentVectors++;
				}
				
				if(profile.P.HasFactorReaction("±", Factors.hy) 
				   || profile.P.HasFactorReaction("±", Factors.e))
				{
					countAmbivalentVectors++;
				}
				
				if(profile.C.HasFactorReaction("±", Factors.d) 
				   || profile.C.HasFactorReaction("±", Factors.m))
				{
					countAmbivalentVectors++;
				}
				
				if(profile.S.HasFactorReaction("±", Factors.h))
				{
					countAmbivalentVectors++;
				}
				
				if(countAmbivalentVectors >= 3)
				{
					detected = true;
				}
			}
			#endregion
			
			#region Zwangshandlungen (row 12b)
			//p.474 VI. a) klassische Zwangsneurose
			if(profile.HasMitte("±,0", "±,0") 
			   || profile.HasMitte("±,-", "±,0"))
			{
				detected = true;
				var note = InterpretationNotes.KlassischeZwangsneurose;
				profile.AddInterpretationNote(note);
			}
			
			// Buch 3 p.474 V Zwangsfaktorenverband
			// Skala row 12b (k±)
			if(Faktorenverband(new bool[] {
			        profile.k.IsEqualTo("±"),
					profile.e.IsEqualTo("±"),
					profile.m.IsEqualTo("±"),
					profile.h.IsEqualTo("±"),
					profile.hy.IsEqualTo("0"),
					profile.p.IsAny("0", "-"),
					profile.d.IsEqualTo("0"),
					profile.s.IsAny("0", "-", "+")}))
			{
				detected = true;
			}
			
			#endregion
			
			#region Zwangsneurose
			// profile.473 I.
			if(profile.Sch.HasFactorReaction("±", Factors.k)
			   && profile.P.HasFactorReaction("±", Factors.e)
			   && profile.C.HasFactorReaction("±", Factors.m)
			   && profile.S.HasFactorReaction("±", Factors.h)
			  )
			{
				detected = true;
			}
			#endregion
			
			#region Zwangsneurose
			// profile.473 IV.
			if(profile.Sch.HasFactorReaction("±", Factors.k)
			   && profile.P.HasFactorReaction("±", Factors.e)
			   && (profile.C.HasFactorReaction("±", Factors.m) 
			       || profile.C.HasFactorReaction("±", Factors.d)))
			{
				detected = true;
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.CompulsiveZwang);
			}
		}
		
		internal static void DetectSozialisierendeAlltagsmenschen(TestProfile profile)
		{
			//purely from Skala, not found in Syndromatic (because is not a disease)
			bool detected = false;
			
			#region Skala 16a
			if(profile.S.EqualsTo("+,+") 
			   && profile.Sch.EqualsTo("-,-") 
			  )
			{
				detected = true;
			}
			
			
			if(profile.S.IsAny("+,0", "+!,+", "+,+!", "+,-") 
			   && profile.Sch.IsAny("-,+", "-,0", "-!,-", "-!,+", "-!,0") 
			   && profile.C.EqualsTo("0,+"))
			{
				detected = true;
			}
			
			#endregion
			
			#region Skala 16bcd
			if(profile.S.IsAny("±,+", "+,±", "-,+", "-!,+", "-,+!", "-,-", "±,-", "-,±") 
			   && profile.Sch.IsAny("-,+", "-,0", "-!,-", "-!,+", "-!,0"))
			{
				detected = true;
			}
			#endregion
			
			#region Skala 16e
			if(profile.S.IsAny("+,+", "±,+", "+,0", "+,±") 
			   && profile.Sch.EqualsTo("±,+"))
			{
				detected = true;
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.SozialisierendeAlltagsmensche);
			}
		}
		
		internal static void DetectGeistigHumanisierendeMenschen(TestProfile profile)
		{
			//purely from Skala, not found in Syndromatic (because is not a disease)
			bool detected = false;
			
			#region Skala 17abc
			if(profile.S.IsAny("-,-", "-!,-", "-,-!", "-,0", "-!,0", "±,-", "-,±")
			   && profile.Sch.IsAny("±,±", "±,+", "+,+")
			  )
			{
				detected = true;
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.GeistigHumanisierendeMenschen);
			}
		}
		#endregion
		
		#region SexuelleExistenzformen
		internal static void DetectPerversionSadomasochismus(TestProfile profile)
		{
			bool detected = false;
			
			#region sadismus (row 9a) (Destruktive Perversion)
			
			// p.375 III. Syndrom des Sadismus s+!, d+!
			if(profile.s.IsAny("0", "+", "+!", "+!!", "+!!!")//s0(d0), s+ p.400
				&& profile.d.IsAny("0", "+", "+!", "+!!", "+!!!", "±")// d± p.389 doubt: needs s!!!?
				&& !(profile.s.IsEqualTo("0") && profile.d.IsEqualTo("0")))//p.400 s0d+ or s+d0
			{
				detected = true;// doubt: conditions enough?
				profile.AddInterpretationNote(InterpretationNotes.Analsadismus);
			}
			
			// multiple places take s ipertension as enough for sadismus note
			if(profile.s.IsAny("+!", "+!!", "+!!!")) // doubt +! too?
			{
				profile.AddInterpretationNote(InterpretationNotes.Sadismus);
			}
			
			
			// p.375 III. Syndrom des Sadismus, mit Lustsyndrom
			if(profile.s.IsAny("+!", "+!!", "+!!!")
			   && HasLustprinzipSyndrom(profile)) 
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sadismus);
			}
			
			// row 9a colums S only			
			// p.375 II. Destruktive (entwertende Perversionene)
			if(profile.s.IsAny("+!", "+!!", "+!!!")
				&& profile.k.IsAny("-!", "-!!", "-!!!"))//k-! Exhibitionismus
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sadismus);
				profile.AddInterpretationNote(InterpretationNotes.Exhibitionismus);
			}
			
			// p.380
			if(profile.s.IsAny("+!", "+!!", "+!!!")
			   && profile.HasInterpretationNote(InterpretationNotes.Kain))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.SadistischeKain);
			}
			
			#endregion
			
			#region Masochismus (row 9b) (Introjektive Perversion)
			
			// Skala row 9b
			if(profile.S.HasAnyFactorReaction("-!!", "-!!!", Factors.s))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.374 I. primären Masochismus (=Todestriebes)
			if(profile.s.IsAny("-", "-!", "-!!", "-!!!")
			   && profile.HasInterpretationNote(InterpretationNotes.InzestuösesAnhangen)
			   && (profile.Sch.EqualsTo("-,+") || profile.Sch.EqualsTo("-,0")))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.375 II. sekundären Masochismus 
			if(profile.s.IsAny("-", "-!", "-!!", "-!!!")
			   && profile.d.IsAny("-", "0")
			   && profile.m.IsAny("±", "0")
			   && (profile.Sch.EqualsTo("+,-")))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.376 Tabelle 40, Masochismus, V.
			if(profile.S.HasAnyFactorReaction("-!", "-!!", "-!!!", Factors.s)
			   && (profile.Sch.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.k)
			       || profile.Sch.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.p)))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// example p.380, Vordegängers, 1.
			// profiles pp.383-4, I, IV, IX
			if(profile.S.HasAnyFactorReaction("-!", "-!!", "-!!!", Factors.s)
				&& HasLustprinzipSyndrom(profile))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.479, just a note, anamasochismus
			if(profile.s.IsAny("-", "-!", "-!!", "-!!!") 
			   && profile.d.IsAny("0", "±"))
			{
			   	var note = InterpretationNotes.Analmasochismus;
			   	profile.AddInterpretationNote(note);
			}
			
			#endregion
			
			#region Fetischismus (row 9b) (Introjektive Perversion)
			// p.369
			if(profile.HasInterpretationNote(InterpretationNotes.Lustprinzip)
			   && profile.k.IsAny("+!", "+!!", "+!!!", "±")
			   && profile.p.IsAny("+", "±", "0"))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Fetischismus);
			}
			
			// p.372, Fetischismus, I. 
			// p.375
			// p.376 Tabelle 40, IV., V.
			if(/*(*/profile.k.IsAny("+!", "+!!", "+!!!")
			    //|| profile.Sch.EqualsTo("+,+!", // row 9b
			    //                        FactorsComparisonOptions.HypertensionEqualORGreater))
			   && profile.s.IsAny("-!", "-!!", "-!!!"))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Fetischismus);
			}
			
			// p.372 I.
			if(profile.k.IsAny("+!", "+!!", "+!!!")
			   && profile.p.IsAny("+", "±", "0")
			   && profile.s.IsAny("-!", "-!!", "-!!!")
			   && profile.hy.IsAny("-!", "-!!", "-!!!", "0", "+", "±"))
			{	detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Fetischismus);
			}
			
			// p.372 II.
			if(profile.HasInterpretationNote(InterpretationNotes.Lustprinzip))
			{	detected = true;
			}
			
			#endregion
			
			#region polymorph perversion (row 9c)
			if(profile.S.HasFactorReaction("±", Factors.s)
				&& HasLustprinzipSyndrom(profile))
			{
				detected = true;
			}
					
			if(profile.HasInterpretationNote(InterpretationNotes.PolymorphPervers))
			{
				detected = true;
			}
						
			#endregion
			
			#region row 9d 
			//from Skala only TODO check syndromatic
			{
				if(profile.S.IsAny("-,+!", "-,+!!")
				   && profile.P.IsAny("-,+", "0,+", "0,±", "0,-")
				   && profile.Sch.IsAny("+,0", "0,0", "+,-")
				   && profile.d.IsAny("-!", "-!!"))
				{
					detected = true;
				}
				
				// TODO check in syndromatic if only conditions s and m are required
				if(profile.s.IsAny("-!!", "+!!")
				   && profile.m.IsAny("+", "+!", "+!!"))//TODO confirm m with hypertension
				{
					detected = true;
				}
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Perversion_Sadismus_Masochismus);
			}
		}
		
		internal static void DetectInversion(TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala row 10a
			// p.406
			if(profile.PartOf.testTakerSex == Sex.Male)
			{	
				// p.406, 409
				if((profile.IsVorOrExperimental
				   && profile.HasInterpretationNote(InterpretationNotes.Triebzielinversion)
				   && profile.P.IsAny("+,-", "±,-", "0,-")//P0- p.409 10.a)
				   && profile.HasInterpretationNote(InterpretationNotes.Lustprinzip))//p.409
				   ||
				   (profile.IsThKP
				   && profile.HasInterpretationNote(InterpretationNotes.Triebzielinversion)
				   && profile.P.IsAny("-,+", "0,+")
				   && profile.Sch.EqualsTo("±,0")
				   && profile.C.EqualsTo("-,-")))
				{
					detected = true;
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomosexualität);
				}
				
				//p.406 and Skala 10a, lax version
				if((profile.IsVorOrExperimental
				    && profile.HasInterpretationNote(InterpretationNotes.Triebzielinversion)
				    && profile.Sch.IsAny("0,±", "0,+", "-,+", "+,±"))
				   ||
				   (profile.IsThKP
				   && profile.HasInterpretationNote(InterpretationNotes.Triebzielinversion)
				   && profile.Sch.IsAny("±,0", "±,-", "+,-", "-,0")))
				{
					detected = true;
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomosexualität);
				}
			}
			
			if(profile.PartOf.testTakerSex == Sex.Female)
			{
				// p.412 //TODO add/verify Faktorenverband
				// p.412 //TODO add more Vektor reactions
				if((profile.IsVorOrExperimental
				    && (profile.HasInterpretationNote(InterpretationNotes.Triebzielinversion)
				        || profile.S.IsAny("-,-!!", "+,-!!", "0,-!!")//p.412 9.b)
				        || profile.S.IsAny("-,-!", "-!,-!")//p.415
				        || profile.S.IsAny("±,+", "±,-", "±,-!")//p.412 9.c)
				        || profile.S.IsAny("±,0", "-,0")//p.412 9.d)
				       )
				    && profile.P.IsAny("-,+", "±,+", "0,+", "0,±")// 0± p.412
				    && profile.Sch.IsAny("±,0", "+,0", "±,+", "0,0")//Sch00 p.412
				    && profile.C.IsAny("+,+", "±,+", "+,±", "+,0", "+!,0")
				  ))
				{
					detected = true;
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomosexualität);
				}
			}
			
			// Apha version: with no checks to Syndromatic
			{
				if( profile.PartOf.testTakerSex == Sex.Male)
				{
					if((profile.S.ContainsFactorReaction("+", Factors.h) 
					   || profile.S.ContainsFactorReaction("0", Factors.h))
						&& profile.S.ContainsFactorReaction("-", Factors.s) // includes s-!!
						//TODO add check for "{" symbol : sequence 
					)
					{
						// detected = true;
					}
				}
			}
			#endregion
			
			#region Existenzskala row 10c
			// Buch 3, p.406, p.409 6. a)b)c)d), (p.411 reprise)
			if( profile.PartOf.testTakerSex == Sex.Male)
			{
				if(profile.S.ContainsFactorReaction("-", Factors.s)
				  && profile.P.ContainsFactorReaction("-", Factors.hy) 
				  && profile.p.IsAny("±", "+")//p.406-7
				  && profile.C.IsAny("+,+", "+!,+")// C+!+ B3 p.408 (4d), p.415 (Tab.49)
				 )
				{
					detected = true;
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomosexualität);
				}
			}
			
			// Buch 3, p.411
			if( profile.PartOf.testTakerSex == Sex.Female)
			{
				if(profile.S.HasFactorReaction("+", Factors.s)
				   && profile.P.ContainsFactorReaction("+", Factors.hy)
				   && (profile.Sch.EqualsTo("±,0") || profile.Sch.EqualsTo("+,0"))
				   && profile.C.EqualsTo("+,+")
				 )
				{
					detected = true;
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomosexualität);
				}
			}
			#endregion
			
			#region Existenzskala row 10a
			// Buch 3, p.409
			if( profile.PartOf.testTakerSex == Sex.Male)
			{
				if(profile.S.HasAnyFactorReaction("-", "-!", "-!!", "-!!!", Factors.s)
				   && profile.P.ContainsFactorReaction("-", Factors.hy)
				   && (profile.Sch.EqualsTo("0,±") || profile.Sch.EqualsTo("+,±")
				      || profile.Sch.EqualsTo("0,+") || profile.Sch.EqualsTo("-,+"))
				   && (profile.C.EqualsTo("±,+") || profile.C.EqualsTo("0,+")
				      || profile.C.EqualsTo("+,±") || profile.C.EqualsTo("+,0")))
				{
					detected = true;
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomosexualität);
				}
			}
						
			#endregion
			
			#region Existenzskala row 10b
			// Buch 3, p.390
			if(profile.S.EqualsTo("±,±"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Bisexualität);
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Inversion_SzondiHomo_Trans);
			}
		}
				
		internal static void DetectLustprinzip(TestProfile profile)
		{
			//p.358: Lustprinzip ist oft gleichzeitig auch das Syndrom der Polymorphen Perversion
			
			// Buch 3 p.359, p.367
			if(// also p+ per p.389
				profile.p.IsAny("+", "+!", "+!!", "+!!!", "±", "0") // +, ±. p0 seltener
				&& (profile.d.IsAny("+", "+!", "+!!", "+!!!", "±", "0")// +, ± // d0 seltener
				    || profile.d.IsAny("-", "-!", "-!!", "-!!!"))//d- Skala 8
				&& profile.m.IsAny("+", "+!", "+!!", "+!!!", "±", "0")) // +, ± // m0 seltener
			{
				profile.AddInterpretationNote(InterpretationNotes.Lustprinzip);
				
				// more detailed notes:
				
				// Perverses Lustsyndrom, PolymorphPervers (rows 9abc)
				// p.377 (Column 3, section VII.C.), 389 "Perverses Lustsyndrom"
				// TODO delete? move to note only (no exForm)? dubious, weak sources
				// p.502, IV (profiles 6, 8 from p.501)	Sch0±, -±
				//p.358: Lustprinzip ist oft gleichzeitig auch das Syndrom der Polymorphen Perversion
				// p.380
				if(profile.Sch.EqualsTo("+,+!") // ??
				   && profile.s.IsEqualTo("-"))
				{
					profile.AddInterpretationNote(InterpretationNotes.PolymorphPervers);
				}
				
				#region Buch 3, p.359
				if(profile.P.HasFactorReaction("0", Factors.e)
				  && profile.Sch.HasFactorReaction("-", Factors.k)
				  && profile.Sch.HasFactorReaction("+", Factors.p))
				{
					// TODO improve/verify if hypertension or ambivalence allowed
					// Kleptomanie, Buch 3, p.359, 2. b)
					profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_Kleptomanie);
				}
				
				if(profile.C.HasFactorReaction("0", Factors.d))
				{
					// Buch 3, p.359, 2. c) or d)
					// c) inability of accumulating the acquisition urge
					// therefore, the immediate satisfaction d=0
					// d) lack of a value scale in the acquisition of things of the world
					profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_AcquisitionUrge);
				}
				
				if(profile.Sch.HasFactorReaction("+", Factors.p)
				   && profile.C.HasFactorReaction("+", Factors.d))
				{
				 	// Buch 3, p.359, 2. e)
				 	profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_ConstRivalWPartner);
				}
				
				if((profile.C.HasFactorReaction("+", Factors.m)
				    && profile.S.HasFactorReaction("+", Factors.s))
				    ||
				    (profile.C.HasFactorReaction("0", Factors.m)
				     && profile.S.HasFactorReaction("0", Factors.s)))
				{
				   	// Buch 3, p.359, 3. c)
					// Oralsadismus (m+, s+, oder m0, s0)
					profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_Oralsadismus);
				}
				
				if(profile.k.IsAny("-!", "-!!", "-!!!"))
				{
				   	// Buch 3, p.359, 4.
					// Negation of the ideals, or depreciation of all ideals
					// self-devaluation, ...
					// p.375 Lustsyndrom oder Perversionssyndrom
					profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_IdealsNegation);
				}
				#endregion
				
				// p.367 Syndrom des (perversen) Lustprinzip
				if(profile.p.IsAny("+!", "+!!", "+!!!", "0", "±")
				   && profile.d.IsAny("+", "0", "±")
				   && profile.m.IsAny("+", "0", "±")
				   && profile.h.IsAny("+", "0", "±")
				   && profile.s.IsAny("+", "0", "-")
				  )
				{
					profile.AddInterpretationNote(InterpretationNotes.PerverseLustprinzip);
				}
				
				// p.433
				if(profile.Sch.HasFactorReaction("+", Factors.p)
					&& profile.C.HasFactorReaction("0", Factors.d)
					&& profile.C.HasFactorReaction("0", Factors.m))
				{
					profile.AddInterpretationNote(InterpretationNotes.GrößenwahnLustprinzip);
				}
			}
		}
		
		internal static void DetectTriebzielinversion(TestProfile profile)
		{
			// p.305, X.
			var note = InterpretationNotes.Triebzielinversion;
			
			if(profile.PartOf.testTakerSex == Sex.Male 
			   && profile.IsVorOrExperimental)
			{
				if(profile.S.IsAny("+,-", "+!,-", "+!!,-", "+!!!,-")
				   || profile.S.IsAny("+!,-!", "+,-!", "+,-!!", "+,-!!!")
				   || profile.S.IsAny("±,-", "0,-!", "0,-!!")//Skala 10a, p.409 9.b)c)
				   || profile.S.IsAny("+,±", "+!,±")// Triebzielinversion mit Ambivalenz, +±: p.262 IX.2, p.434 III.1
				  )
				// p.408 
				// p.262 IX.1 (h+!)
				// p.385 +-!
				{
					profile.AddInterpretationNote(note);
				}
			}
			
			if(profile.PartOf.testTakerSex == Sex.Male
			   && profile.IsThKP)
			{
				if(profile.S.IsAny("-,+", "-,+!", "-,+!!", "-,+!!!")
				   || profile.S.EqualsTo("0,+"))
				{
					profile.AddInterpretationNote(note);
				}
			}
			
			if(profile.PartOf.testTakerSex == Sex.Female
			   && profile.IsVorOrExperimental)
			{
				if((profile.S.IsAny("-,+", "-!,+")
				   || profile.S.EqualsTo("0,+")//pp.410-1
				   || profile.S.EqualsTo("±,+")//p.411
				   || profile.S.EqualsTo("-,±"))//pp.305, 412, 415
				   &&
				   (profile.Sch.IsAny("±,0", "+,0")//p.410 Needed for female
				    //dout: also Sch±- p.411 but not in skala?
				   ))
				{
					profile.AddInterpretationNote(note);
				}
			}
		}
		
		#endregion
		
		#region IchExistenzformen
		
		internal static void DetectPraepsychotischeEvSuicidale(TestProfile profile)
		{
			bool detected = false;
			
			#region Skala abcd
			//Skala only, not found in Syndromatics
			//"{" symbol: must check for a sukzession?
			if(profile.k.IsAny("-", "+", "0", "±")
			   && profile.p.IsEqualTo("-")
			   && profile.C.EqualsTo("-,-")
			  )
			{
				detected = true;
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Praepsychotische);
			}
		}
		
		#region ProjektivParanoid
		internal static void DetectProjektivParanoid(TestProfile profile)
		{
			FactorsComparisonOptions noHypertension = FactorsComparisonOptions.HypertensionOnOff;
			bool detected = false;
			
			#region Existenzskala row 2a (ProjektivParanoide)
			// Buch 2, p.230,
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( profile.Sch.EqualsTo("0,-!")
				   ||
				   profile.Sch.EqualsTo("-,-!")
				  )
				{
					//TODO "unter Umst¨anden" (Buch 2, p.430): verify/dig Syndromatik
					detected = true;
				}
			}
			
			// row 2a only
			{
				if( profile.p.IsAny("-!!", "-!!!"))
				{
					detected = true;
				}
			}
			#endregion
			
			#region Existenzskala row 2b (ProjektivParanoide)
			//Buch 3, pp.241-2, Tabelle 29, Tabelle 30
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( (profile.Sch.IsAny("0,-", "+,-", "±,-"))
				   &&
				   ((profile.S.EqualsTo("+,-"))
				    || (profile.P.EqualsTo("+,-"))
				    || (profile.C.EqualsTo("+,-")))
				  )
				{
					detected = true;
					
				}
			}
			#endregion
			
			#region Existenzskala row 2c (ProjektivParanoide)
			//Buch 3, pp.241-2, Tabelle 32
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( profile.Sch.EqualsTo("0,-", noHypertension)
				   &&
				   profile.P.Contains("0,-")
				  )
				{
					detected = true;
				}
			}
			
			// //Buch 3, pp.241-2, Tabelle 32 only
			{
				if(profile.Sch.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!")
				   && (profile.S.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!")
				      || profile.P.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!")
				      || profile.C.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!")))
				{
					detected = true;
				}
			}
			
			// full row 2c only
			// Only add InterpretationNotes.ProjektivMitte, code in notes section
			/*
			}*/
			#endregion
			
			#region Existenzskala row 2d (ProjektivParanoide)
			//Buch 3, p.260, I. Zerspaltung
			{
				if( profile.Sch.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!")
				   && profile.S.EqualsTo("+,-")
				   && profile.P.EqualsTo("+,-")
				   && profile.C.EqualsTo("+,-")
				  )
				{
					detected = true;
				}
			}
			
			// full row 2d only
			{
				if( profile.Sch.IsAny("0,-", "+,-")
				   && profile.S.IsAny("0,-", "+,-")
				   && profile.P.IsAny("0,-", "+,-")
				   && profile.C.IsAny("0,-", "+,-")
				  )
				{
					detected = true;
				}
			}
			#endregion
				
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.ProjektivParanoide);
			}
		}
		
		internal static void DetectProjektivParanoideMitte(TestProfile profile)
		{
			//Buch 3, p.260, VI. Paranoide Mitte
			{
				if(//VI. 1. sensitive + P
				   // p.262 (hy-, p-)
					(profile.P.IsAny("0, -", "0, -!", "±, -")
				   && profile.Sch.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!"))
				   ||
				   //VI. 2. Gewissenangst
				   // p.262 (hy-, p-)
				   (profile.P.IsAny("+, -", "+, -!")
				   && profile.Sch.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!"))
				   || 
				   //VI. 3. Sensitive + zw Un der P
				   profile.HasMitte("+,-", "±,-"))
				{
					var note = InterpretationNotes.ProjektivParanoMitte;
					profile.AddInterpretationNote(note);
				}
			}
			
			//TODO merge: current ParanoideMitte and ProjektivMitte as ProjektivParanoideMitte
			// add InflativParanoideMitte
			{
				if((profile.P.IsAny("0,-", "+,-", "+,±") 
				   && profile.Sch.IsAny("0,-", "+!,-", "+!!,-", "+!!!,-"))
				   || //p.305
				   profile.HasMitte("0,-!", "0,-!") 
				   || profile.HasMitte("0,-!", "±,-!")
				   || profile.HasMitte("±,-", "0,-!")
				   || profile.HasMitte("+,-!", "0,-!"))
				{
					var note = InterpretationNotes.ProjektivParanoMitte;
					profile.AddInterpretationNote(note);
				}
			}
		}	
			
		internal static void DetectParanoideKammSyndrom(TestProfile profile)
		{
			// p.262
			if(profile.p.IsAny("-", "-!", "-!!", "-!!!")
			   && profile.S.HasFactorReaction("-", Factors.s)
			   && profile.P.HasFactorReaction("-", Factors.hy)
			   && profile.C.HasFactorReaction("-", Factors.m))
			{
				profile.AddInterpretationNote(InterpretationNotes.ParanoKammSynd);
			}
			else if(profile.p.Contains("-")
			        && profile.hy.IsAny("-", "-!", "0", "±") // hy can be 0, ±: p.272 I.6
			        && !profile.m.IsAny("+", "+!", "+!!", "+!!!")
			        && !profile.s.IsAny("+", "+!", "+!!", "+!!!"))
				//TODO verify with more examples if hy can't be +
				// (see p.264 VII, where profile 7 is not listed
				//TODO veryfy w/ more examples that s, m can't be "+"
		   	{
				//NB. m± contains m-, or s± -> unpure form (p.262)
				// also admitted m0 and s0
				
		   		ushort detectionPoints = 0;
		   		
		   		detectionPoints += GradeCorrespondence(profile.S, "-", Factors.s);
		   		detectionPoints += GradeCorrespondence(profile.P, "-", Factors.hy);
		   		detectionPoints += GradeCorrespondence(profile.C, "-", Factors.m);
		   		
		   		//TODO reconsidere the whole GradeCorrespondence idea
		   		if(detectionPoints >= 6)
		   		{
		   			// impure form ("unreiner" p.262)
		   			profile.AddInterpretationNote(InterpretationNotes.ParanoKammSyndU);
		   		}
		   	}
		}		
						
		internal static void DetectManiformeParanoide(TestProfile profile)
		{
			bool detected = false;
			
			// Buch 3 p.267 I.
			if(profile.HasInterpretationNote(InterpretationNotes.ProjektivParanoMitte) 
				   || profile.HasInterpretationNote(InterpretationNotes.ParanoKammSynd) 
				   || profile.HasInterpretationNote(InterpretationNotes.ParanoKammSyndU))
			{	
				if(profile.C.EqualsTo("0,-")
				  // && profile.k.IsEqualTo("-")
				  && profile.s.IsAny("+", "0", "-")//s- in less aggressive, lesser Hypomanic
				  && (profile.PartOf.HasGefahrTriebklasse("C", "m", "-") 
				      || profile.PartOf.HasGefahrTriebklasse("Sch", "p", "-")))	
				{
					// Symptome 3, 4 p.267
					detected = true;
				}
				else if (profile.h.IsAny("+!!", "+!!!")
				         && profile.m.IsAny("-!", "-!!", "-!!!"))
				{
					// Symptom 6 p.267
					detected = true;
				}
			}
			
			if(detected)
			{
				var note = InterpretationNotes.ManiformeParanoide;
				profile.AddInterpretationNote(note);
			}
		}		
		#endregion
		
		#region InflativParanoid
		internal static void DetectInflativParanoid(TestProfile profile)
		{
			FactorsComparisonOptions noHypertension = FactorsComparisonOptions.HypertensionOnOff;
			bool detected = false;
			
			#region Existenzskala row 3a (InflativParanoide)
			// Buch 2, p.430,
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( profile.Sch.IsAny("0,+!", "0,+!!", "0,+!!!")
				   ||
				   profile.Sch.IsAny("+,+!", "+,+!!", "+,+!!!")
				  )
				{
					//TODO "unter Umständen" (Buch 2, p.430): verify/dig Syndromatik
					detected = true;
				}
				
				// row 3a only, remaining scenarios
				if(profile.p.IsAny("+!!", "+!!!") 
				   || profile.Sch.EqualsTo("±,+!"))
				{
					detected = true;
				}
			}
			#endregion
						
			#region Existenzskala row 3b (InflativParanoide)
			//Buch 3, pp.241-2, Tabelle 31
			{
				if( (profile.Sch.EqualsTo("0,+", noHypertension)
				     //|| profile.Sch.EqualsTo("+,+", noHypertension)//TODO verify this in syndromatic
				    )
				   &&
				   ((profile.S.Contains("-,+"))
				    || (profile.P.Contains("-,+"))
				    || (profile.C.Contains("-,+")))
				  )
				{
					detected = true;
					var note = InterpretationNotes.GrößenwahnParano;
					profile.AddInterpretationNote(note);
				}
			}
			#endregion
			
			#region Existenzskala row 3c
			if(profile.S.EqualsTo("-,+") 
			   && profile.P.EqualsTo("-,+") 
			   && profile.Sch.IsAny("-,+!", "0,+") 
			   && profile.C.IsAny("-,+", "0,-") 
			  )
			{
				detected = true;
			}
			#endregion
			
			#region Existenzskala row 3d
			if(profile.HasInterpretationNote(InterpretationNotes.InflativParanoMitte))
			{
				detected = true;
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.InflativParanoide);
			}
		}
		
		internal static void DetectInflativParanoidSyndrom(TestProfile profile)
		{
			// p.278 VII., p.305 Table
			if(profile.s.IsAny("+", "±")
			   && profile.P.IsAny("-,+", "±,±", "0,0")//doubt: P-±, ±0 (p.280 V.)
			   && profile.Sch.IsAny("0,+", "0,+!"))
			{
				var note = InterpretationNotes.InflativParanoSyndrom;
				profile.AddInterpretationNote(note);
			}
		}
		
		internal static void DetectInflativParanoideMitte(TestProfile profile)
		{
			// p.?
			if((profile.P.IsAny("-,+", "-,0", "±,0", "0,+", "-,-", "0,+")//FIXME duplicate 0+
			    //|| profile.P.IsAny("-,+!",)//doubt, p.281
			    || profile.P.IsAny("0,0","+,+")//Skala only
			   )
			   && profile.Sch.IsAny("0,+", "0,+!")
			  )
			{
				var note = InterpretationNotes.InflativParanoMitte;
				profile.AddInterpretationNote(note);
			}
		}
		#endregion
		
		#region Accessory: SpaltungsSyndrom
		
		internal static void DetectParanoideSpaltungsSyndrom(TestProfile profile)
		{
			if(DetectParanoideSpaltungsSyndromHelper(profile, true))
			{
				// Form a) +- +- 0- +-
				// Buch 3 p.260 I.
				profile.AddInterpretationNote(InterpretationNotes.ParanoSpaltungsSynd);
			}
			else if(DetectParanoideSpaltungsSyndromHelper(profile, false))
			{
				// Form b) -+ -+ 0+ -+
				// Buch 3 p.278 I., p.279 I.
				profile.AddInterpretationNote(InterpretationNotes.ParanoSpaltungsSynd);
			}
		}
		
		private static bool DetectParanoideSpaltungsSyndromHelper(TestProfile profile, bool formA)
		{
			//TODO revise Form b with p. 279 I., profiles: 13,14,15(=16),18,20
			
			bool formB = !formA;
			float diagolaneSpaltungen = 0;
			var vectors = new List<VectorReaction>{profile.S, profile.P, profile.Sch, profile.C};
			
			foreach(var vector in vectors)
			{
				if((formA 
				   && vector.secondFactorReaction.IsAny("+", "+!", "+!!", "+!!!")
				   && vector.firstFactorReaction.IsAny("-", "-!", "-!!", "-!!!", "±")) 
				   || (formB 
				   && vector.firstFactorName != Factors.k
				   && vector.firstFactorReaction.IsAny("+", "+!", "+!!", "+!!!"/*, "±"*/)
				   && vector.secondFactorReaction.IsAny("-", "-!", "-!!", "-!!!")) 
				   || (formB
				   && vector.firstFactorName == Factors.k
				   && vector.firstFactorReaction.IsAny("+", "+!", "+!!", "+!!!"/*, "±"*/)
				   && vector.secondFactorReaction.IsAny("-", "-!", "-!!", "-!!!")) )
				{
					// invalidated by opposide -+ (in Form A)
					return false;
				}
				
				//first factor ± also ok (in Form A), per p.275 V.
				if((formA 
				   && vector.firstFactorReaction.IsAny("0", "+", "+!", "+!!", "+!!!", "±")
				   && vector.secondFactorReaction.IsAny("-", "-!", "-!!", "-!!!")) 
				   || (formB 
				   && vector.firstFactorName != Factors.k
				   && vector.secondFactorReaction.IsAny("0", "+", "+!", "+!!", "+!!!", "±")
				   && vector.firstFactorReaction.IsAny("-", "-!", "-!!", "-!!!"))
				   || (formB
				   && vector.firstFactorName == Factors.k
				   && vector.secondFactorReaction.IsAny("+", "+!", "+!!", "+!!!"/**/)
				   && vector.firstFactorReaction.IsAny("-", "-!", "-!!", "-!!!", "0", "±")))
				{
					float prize = 1F;					
					if((formA && vector.firstFactorReaction.IsEqualTo("±")) 
					   /*|| (formB && vector.secondFactorReaction.IsEqualTo("±"))*/)
					{
						//(in form A) proper Sch(0,-) or (+-) counts more, ± counts less
						prize = 0.5F;
					}
					
					diagolaneSpaltungen += prize;
					
					if(vector.firstFactorName == Factors.k)
					{	//Sch is the most important
						// ie (in form A) ("+!,-", "0,±", "0,-!", "+,±") also counts						
						diagolaneSpaltungen += prize;
					}
				}	
			}
			
			if(diagolaneSpaltungen>=3)
			{
				return true;
			}
			
			return false;
		}
		#endregion
				
		internal static void DetectHeboide(TestProfile profile)
		{
			//1. Theatralische, Pathetische, Ekstatische Heboide
			//		(unstillbaren Geltungs- und Exhibitionsdrang) (4c ?)
			//2. Sprunghaftigkeit
			//		(Paroxysmalität der Hebeohrenen zusammen, P e0 hy-) (4d?)
			//3.irreale Phantasiewelt, Lügenhaftigkeit, Pseudologia phantastica (Mythomanie)
			//		(hy-!) (4ab?)
			//4.Leib- oder Organhalluzinationen
			// eine Form des hypocondrischen Syndroms: projektive Form des 
			// hypochondrischen Organs. (11 und 15 Skala)
			
			//p.309 main diff btw Parano and heboide is, 
			//hebephrenen use also k-! (egosystole), while paranoiden k0
			// so -!-(!) heboide, 0-! paranoide
			
			//p309 diff btw Hypocondriac (11u15) and Hebephrene (4):
			// Hypocondriac: inhibits (Sch=-+) or removes (Sch=-0) the obsession
			// Hebephrene: projects the obsession to a body organ (Sc-!-!)
			
			bool detected = false;
			
			#region Skala 4a
			if(profile.hy.IsAny("-!", "-!!", "-!!!")
			   && profile.k.IsAny("-!", "-!!", "-!!!"))
			{
				detected = true;
			}
			#endregion
			
			#region Skala 4b
			// Skala only. Not in syndromatic?
			{
				if(profile.hy.IsAny("-!", "-!!", "-!!!")
				   && profile.C.IsAny("0,0", "-,-"))
				{
					detected = true;
				}
				
				if(profile.h.IsAny("+!", "+!!") 
				   && profile.hy.IsAny("-!", "-!!", "-!!!")
				   && profile.C.EqualsTo("0,-"))
				{
					detected = true;
				}
			}
			#endregion
			
			#region Skala 4c
			// Skala only. Not in syndromatic?
			{
				if(profile.hy.IsAny("+!", "+!!", "+!!!")
				   && profile.k.IsAny("-!", "-!!", "-!!!")
				   && profile.C.IsAny("0,0", "-,-"))
				{
					detected = true;
				}
				
				if(profile.hy.IsAny("+!", "+!!", "+!!!")
				   && profile.k.IsAny("-!", "-!!", "-!!!")
				   && profile.p.IsAny("-!", "-!!", "-!!!")
				   && profile.C.EqualsTo("0,-"))
				{
					detected = true;
				}
			}
			#endregion
			
			#region Skala 4d, Heb.Mitte
			// can be symplyfied? Skala has few conditions
			// Skala only 4d
			if(profile.P.IsAny("0,-!", "0,-!!")
			   && profile.Sch.IsAny("-!,-", "-!,-!"))
			{
				detected = true;
			}
	
			// p.292 Leibhalluzinationen
			if(profile.P.EqualsTo("0,-!") 
			   && profile.Sch.IsAny("-!,-", "-!,-!"))//p.293
			{
				detected = true;
			}
			
			//p.298 4.a)
			if(profile.HasInterpretationNote(InterpretationNotes.HebephreneMitte)
			   || profile.HasMitte("0,-!", "-,-") 
			   || profile.HasMitte("0,-", "-!,-")
			   //|| profile.HasMitte("0,-", "-,-")
			   //|| profile.HasMitte("0,-", "-!!,0")//p.298 4.b)
			   || profile.HasMitte("0,-!!", "0,-")//p.298 4.c)
			   //|| profile.HasMitte("0,-", "-,±")//p.300 IV.1
			   || profile.HasMitte("0,-!", "0,±")//p.300 IV.2.
			   || profile.HasMitte("0,-", "0,±")//p.300 IV.2.
			   || profile.HasMitte("0,±", "0,±")//p.300 IV.2.
			   || profile.HasMitte("-,-!", "-,±")//p.301 IV.b)
			   || profile.HasMitte("-,-", "-,-")//p.301 IV.b)
			   || profile.HasMitte("-,-", "-,±")//p.301 IV.c)
			   || profile.HasMitte("0,-!", "-!,-!")//p.306 VII
			   || profile.HasMitte("0,-", "-!!,-!")//p.309
			   || profile.HasMitte("0,-!", "-!!,-!")//p.309
			)
			{
				if(profile.s.IsAny("+!","+!!","+!!!"))
				{
					detected = true;
					var note = InterpretationNotes.HebephreneSyndrom;
					profile.AddInterpretationNote(note);
				}
				else if(profile.C.IsAny("0,0", "0,-", "-,-"))
					//p.295 VII. 3. Faktorenverband, p.306 VII hebephrene Syndrom
				{
					detected = true;
				}
			}
			
			//Faktorenverband verband p.296			
			{
				bool[] HebephreneSyndromFaktorenverband = new bool[]{
					profile.s.IsAny("+!!", "+!!!")
						|| (profile.s.IsEqualTo("+!")
						    && profile.PartOf.HasFactorReaction("+!!", Factors.s, profile.dimension)),
					profile.e.IsEqualTo("0"),
				    profile.hy.IsAny("-!", "±", "-"),
				    profile.k.IsEqualTo("-!")
				    	|| (profile.k.IsEqualTo("-")
				        	&& profile.PartOf.HasFactorReaction("-!", Factors.k, profile.dimension)),
                	profile.p.IsEqualTo("-!"),
                	profile.d.IsAny("-", "0"),
                	profile.m.IsAny("-", "0"),
				};
				
				if(Faktorenverband(HebephreneSyndromFaktorenverband))
				{
					detected = true;
					var note = InterpretationNotes.HebephreneSyndrom;
					profile.AddInterpretationNote(note);
				}

			}
			#endregion
			

			if(detected)
			{
				var exForm = Existenzformen.Heboide;
				profile.AddHasExistenzform(exForm);
			}
		}
		
		internal static void DetectKatatoniforme(TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala rows 5a (Katatoniforme)
			//Buch 3, pp.243, Tabelle 33
			//B3 p.284
			//B3 p.310 "totale katatoniforme Sperrung"
			{
				if(profile.HasInterpretationNote(InterpretationNotes.TotaleKatatonifIchsperrung))
				{
					if(profile.k.HasHypertension)
					{
						detected = true;
					}
				}
			}
			
			//B3p.312 
			if(profile.HasInterpretationNote(InterpretationNotes.KatatonifSperrungsSynd))
			{
			   detected = true;
			}
		
			//Buch 3, pp.243, 6. a) b)
			{
				// 6. a)
				if((profile.HasMitte("-,0", "-,0")
				    || profile.HasMitte("0,-", "-,0"))
				    && 
				    profile.C.EqualsTo("-,-"))
				{
					detected = true;
				}
				
				// 6. b)
				if(profile.P.IsAny("-,0", "0,-")
				    && profile.Sch.EqualsTo("-,+")
				    && profile.C.EqualsTo("-,-"))
				{
					detected = true;
				}			
			}
			#endregion
			
			#region Existenzskala row 5b
			//pure skala 5b (bold square conditions)
			if(profile.k.IsAny("-!", "-!!", "-!!!")
			   && profile.P.EqualsTo("-,0"))
			{
				detected = true;
			}
			
			//pure skala 5b (remaining conditions)
			if(profile.k.IsAny("-!", "-!!", "-!!!") 
			   && profile.P.IsAny("-,0", "-,±","0,-","0,±") 
			   && profile.s.IsAny("-!!", "-!!!", "+!!", "+!!!")
			   && profile.p.IsEqualTo("0"))
			{
				detected = true;
			}
			
			#endregion
			
			#region Existenzskala row 5c
			if(profile.Sch.IsAny("-!!,-", "-!!!,-")
			   && profile.C.EqualsTo("-,-"))
			{
				detected = true;
			}
			#endregion
			
			#region Existenzskala row 5d
			if(profile.k.IsAny("-!", "-!!", "-!!!") 
			   && profile.p.IsAny("0", "±")
			   && profile.C.IsAny("-,-", "0,0")
			   && profile.P.IsAny("-!,0", "-!!,0", "0,-", "-,-")
			   && (profile.S.IsAny("+!,+!!", "+!,+!!!" , "+!!,+!!" , "+!!,+!!!","+!!!,+!!!")
			       || profile.S.IsAny("0,+!", "0,+!!", "0,+!!!")))
			{
				detected = true;
			}
			#endregion
			
			#region Existenzskala rows 5d and 5b mix
			//B3 p.282 I.
			if(profile.HasInterpretationNote(InterpretationNotes.KatatonifMitte) 
			   && profile.C.IsAny("-,-", "0,0") 
			   && profile.h.IsAny("0", "+", "+!", "+!!", "+!!!") 
			   && profile.s.IsAny("+!", "+!!", "+!!!"))
			{
				detected = true;
			}
			
			//pure Skala 5d
			if(profile.S.IsAny("+!,+!!", "0,+!")
			   && profile.P.IsAny("-!,0", "0,-", "-,-") 
			   && profile.Sch.IsAny("-!,0", "-!,±") 
			   && profile.C.IsAny("-,-", "0,0")
			  )
			{
			
			}
			#endregion
			
			//Buch 3, p.275 II., p.276
			{
				if( profile.k.IsAny("-!", "-!!", "-!!!")
				   && profile.m.IsAny("-!", "-!!", "-!!!"))
				{
					detected = true;
				}
			}
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Katatoniforme);		
			}
		}
		
		#endregion
		
		/// <summary>
		/// "strict" requirements for Lustsyndrom?
		/// </summary>
		/// <param name="profile"></param>
		/// <returns></returns>
		internal static bool HasLustsndromOderPerversionssyndrom(TestProfile profile)
		{
			// Buch 3 p.375
			if(profile.Sch.HasFactorReaction("-!", Factors.k)
			   && profile.Sch.HasFactorReaction("+", Factors.p)
			   && profile.C.HasFactorReaction("+", Factors.d)
			   && profile.C.HasFactorReaction("+", Factors.m)
			  )
			{
				return true;
			}
			
			return false;
		}
		
		internal static bool HasLustprinzipSyndrom(TestProfile profile)
		{	
			InterpretationNotes[] LustprinzipNotes = 
			{
				InterpretationNotes.Lustprinzip,
				InterpretationNotes.Lustprinzip_Kleptomanie,
				InterpretationNotes.Lustprinzip_AcquisitionUrge,
				InterpretationNotes.Lustprinzip_ConstRivalWPartner,
				InterpretationNotes.Lustprinzip_IdealsNegation,
				InterpretationNotes.Lustprinzip_Oralsadismus,
			};
			
			foreach(var note in LustprinzipNotes)
			{
				if(profile.HasInterpretationNote(note))
				{
					return true;
				}
			}
			
			return false;
		}
			
		internal static void FurtherIchSimpleNotes(TestProfile profile)
		{
			DetectParanoideSpaltungsSyndrom(profile);
			
			if(profile.Sch.IsAny("0,+", "0,+!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.TotaleInflation);
			}
			else if(profile.Sch.IsAny("0,+!", "0,+!!", "0,+!!!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.DeliriodeInflation);
			}
			
			if(profile.Sch.IsAny("+,+", "+,+!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Introinflation);
			}
			
			// Sch±-(!) p.390 (p-! p.?)
			if(profile.Sch.IsAny("±,-", "±,-!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.FuguesAusreißen);
			}
			
			// p.388
			if(profile.Sch.IsAny("±,-!!", "±,-!!!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Zwangsperson);
			}
			
			if(profile.Sch.EqualsTo("-,-!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Dissimulation);
			}
			
			if(profile.Sch.IsAny("-,-", "-!,-", "-!!,-", "-!!!,-"))
			{
				//Buch2 Sch6 section
				profile.AddInterpretationNote(InterpretationNotes.Adaptation);
			}
			
			if(profile.Sch.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!"))
			{
				// p.? TODO veryfy is actually alienation for 0-
				profile.AddInterpretationNote(InterpretationNotes.Alienation);
				profile.AddInterpretationNote(InterpretationNotes.ParanoTotalProjektion);
			}
			
			if(profile.Sch.EqualsTo("-,±"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Entfremdung);
			}
			
		}
		
		internal static void FurtherNotes(TestProfile profile)
		{
			FurtherIchSimpleNotes(profile);
			DetectAffektstörungenNotes(profile);
			DetectKainUndAbel(profile);
			DetectTriebzielinversion(profile);
			
			#region Katatoni notes
			// p.283 VII Ichsperre 
			if(profile.Sch.IsAny("-,0", "-!,0", "-!!,0", "-!!!,0", "-!!,±"))
			{	
				// doubt: also -!!,± ? (Skala 5a)
				profile.AddInterpretationNote(InterpretationNotes.RepresionIchsperrung);
				
				if(profile.k.IsAny("-!", "-!!", "-!!!"))
				{	// if ! (!!) -> katatoniforme Ichsperrung, p.310 Buch3?, Buch2 Sch3 section 
					var note = InterpretationNotes.KatatonifNegativism;
					profile.AddInterpretationNote(note);
				}
				
				if(profile.Sch.EqualsTo("-!,0"))
				{
					//p.?
					profile.AddInterpretationNote(InterpretationNotes.DestruktiveVerneinung);
				}
			}
			
			// p.282 I.1
			// Skata row 5d and 5b
			if((profile.P.IsAny("-,0", "-!,0", "0,-", "-,±", "0,±"/*or 0+? p.283 VI.*/)
			    && profile.Sch.IsAny("-,0", "-!,0",  "-!!,0", "-!,±")) 
			    || 
			    //p.286 VI.1. hypochondrisch katatonische Mitte
			    profile.HasMitte("0,-","-,+")) 
			{
				//Nb hy±: präkatatonische Mitte (p.286 VI.2.)
				
				var note = InterpretationNotes.KatatonifMitte;
				profile.AddInterpretationNote(note);
			}
			
			// p.291 IrrealenBlocksSyndrom
			if(profile.hy.IsAny("-", "-!")
			   && profile.Sch.IsAny("-,-", "-!,-", "-,-!", "-!,-!")
			   && profile.C.IsAny("-,-", "-!,-", "-,-!", "-!,-!")
			  )
			// e ("-", "-!", "0") optional per p.301, p.353 
			{
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
				profile.AddInterpretationNote(note);
			}
			#endregion
	
			#region Mord
			// Buch 3, p.361, Tabelle 39 Variationen der Mitter bei Psychopathen
			//p.377 VI
			if(profile.HasMitte("0,-", "-,±")
			   || profile.HasMitte("0,-", "0,-") 
			   || profile.HasMitte("0,-", "0,+") 
			   || profile.HasMitte("-,0", "0,+")
			   || profile.HasMitte("-,0", "-,±")
			   || profile.HasMitte("-,-", "-,-") 
			   || profile.HasMitte("-,-", "0,-") 
			   || profile.HasMitte("±,-", "-,-") 
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.LustmordMitte);
			}
			
			// Buch 3, p.361, Tabelle 39 Variationen der Mitter bei Psychopathen
			if(profile.HasMitte("-!,+", "-,±")
			   || profile.HasMitte("-,+", "-,-") 
			   || profile.HasMitte("-,+", "-,-") 
			   || profile.HasMitte("-,-", "±,-") 
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.MordUndRapeMitte);
			}
			
			// Buch 3, p.361, Tabelle 39 Variationen der Mitter bei Psychopathen
			if(profile.HasMitte("-,-", "0,0")
			   || profile.HasMitte("-!,+", "0,0") 
			   || profile.HasMitte("-,0", "-!!,+") 
			   || profile.HasMitte("-,+", "-,+")
			   || profile.HasMitte("-,+", "-,0")
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.MordMitte);
			}
			#endregion
			
			#region neurotische Mitte, Buch 3, p.460
			
			#region Hypochondrische Mitte
			
			// p.316, p.460, Skala 11a
			if(profile.HasMitte("+,-", "-,+")
			   || profile.HasMitte("0,-", "-,0") 
			   || profile.HasMitte("+,-", "-,0") 
			   || profile.HasMitte("+!,-", "-,0")//p.317, Skala 11a
			   || profile.HasMitte("+!!,-", "-,0")//Skala 11a
			   || profile.HasMitte("0,-", "-,+")
			   || profile.HasMitte("±,-", "-,±") //Also B2 p.216
			   || profile.HasMitte("0,-", "-,±")
			   || profile.HasMitte("±,-", "-,0")
			   || profile.HasMitte("0,±", "-,0")//also psychotische hypochondrische Syndrom
			   || profile.HasMitte("+,-", "0,+!")
			   || profile.HasMitte("+,-", "+!,-")
			   || profile.HasMitte("+,-", "±,+")
			   || profile.HasMitte("+,-", "+,+")
			   || profile.HasMitte("+,-", "-,+")//B2 p.216 
			  )
			{
				var note = InterpretationNotes.HypochondrischeMitte;
				profile.AddInterpretationNote(note);
			}
			
			//Schuld- und Strangfast Syndrom B2 pp.216, 421 (Abb.40)
			if(profile.P.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!") 
			   && profile.Sch.IsAny("-,0", "-!,0", "-!!,0", "-!!!,0"))
			{
				var note = InterpretationNotes.HypochondrischeMitte;
				profile.AddInterpretationNote(note);
			}
						   
			// B3 p.325
			if(profile.HasMitte("0,-!", "-,-")
			   || profile.HasMitte("+,-!", "-,-")
			   || profile.HasMitte("0,-!", "±,-")
			   || profile.HasMitte("0,-!", "-,±")
			   || profile.HasMitte("0,±", "-,0")//also HypochondrischeMitte
			   || profile.HasMitte("+,±!", "-,-"))
			{
				var note = InterpretationNotes.PsychotischeHypochondrischeSynd;
				profile.AddInterpretationNote(note);
			}
			
			#endregion
			
			if(profile.HasMitte("-,+", "-,+")
			   || profile.HasMitte("-,0", "-,+")
			  )
			{
				var note = InterpretationNotes.InhibitedMitte;
				profile.AddInterpretationNote(note);
			}
			
			if( // Klassische Zwangsneurose
				profile.HasMitte("±,0", "±,0")
			   || profile.HasMitte("±,-", "±,0")
			   
			   // Zwangsimpulse
			   || profile.HasMitte("0,±", "0,±")
			   || profile.HasMitte("-,±", "0,±")
			  )
			{
				var note = InterpretationNotes.ObsessiveCompulsiveMitte;
				profile.AddInterpretationNote(note);
			}
			
			if(profile.HasMitte("+,+", "-,0")
			   || profile.HasMitte("+,+", "-,+")
			   || profile.HasMitte("+,+", "-,±")
			  )
			{
				var note = InterpretationNotes.HysteriformeMitte;
				profile.AddInterpretationNote(note);
			}
			
			// B3 p.460
			if(profile.HasMitte("+,0", "±,±")
			   || profile.HasMitte("+,0", "±,+")
			   || profile.HasMitte("+,±", "±,±")
			  )
			{
				var note = InterpretationNotes.PhobischeMitte;
				profile.AddInterpretationNote(note);
			}
			#endregion
			
			#region Affekt notes
			
			// p.274 II.7
			if(profile.P.EqualsTo("0,0"))
			{
				var note = InterpretationNotes.TotalDesintegrAffekte_ApathieStupor;
				profile.AddInterpretationNote(note);
			}
			
			// p.274 II.8
			if(profile.P.EqualsTo("0,±"))
			{
				var note = InterpretationNotes.Lamentation;
				profile.AddInterpretationNote(note);
			}
			
			// p.390 
			if(profile.P.EqualsTo("+,±"))
			{
				var note = InterpretationNotes.Religionswahn;
				profile.AddInterpretationNote(note);
			}
			
			// Buch2 p.430
			if(profile.P.EqualsTo("+,+"))
			{
				var note = InterpretationNotes.Affektflut;
				profile.AddInterpretationNote(note);
			}
			
			// Buch2 p.430
			if(profile.P.EqualsTo("0,0"))
			{
				var note = InterpretationNotes.Affektebbe;
				profile.AddInterpretationNote(note);
			}
			
			// Buch2 p.112
			if(profile.P.EqualsTo("±,±"))
			{
				var note = InterpretationNotes.EthMoralDilemma;
				profile.AddInterpretationNote(note);
			}
			
			if(profile.HasMitte("-,0", "+,-") 
			   || profile.HasMitte("-,+", "+,-") )
			{	// skala row 13a (reine Kain)
				// p.495, Tabelle 55, III 4-5
				var note = InterpretationNotes.ReineKainMitte;
				profile.AddInterpretationNote(note);
			}
			
			// p.487
			{
				if(profile.InSukzession("+,+", "0,0", Vectors.P))
				{
					var note = InterpretationNotes.KonvHystSukzessionAffekt;
					profile.AddInterpretationNote(note);
				}
				
				if(profile.InSukzession("-,0", "-,+", "-,±", Vectors.Sch))
				{
					var note = InterpretationNotes.KonvHystSukzessionIch;
					profile.AddInterpretationNote(note);
				}
			}
			
			//p.398
			if(profile.InSukzession("0,-", "-,0", Vectors.P))
			{
				var note = InterpretationNotes.KainHideWechsel;
				profile.AddInterpretationNote(note);
			}
			
			//B3 p.402 A.3
			if(profile.HasMitte("+,-", "-!,+")
			   || profile.HasMitte("+,-", "-,±")
			   || profile.HasMitte("0,-", "-,±")
			   || profile.HasMitte("0,-", "-,+"))//p.442 I.2.a)
			{
				profile.AddInterpretationNote(InterpretationNotes.SchuldUStrafangstMitte);
			}

			//B3 p.402 B.2 a)
			if(profile.HasMitte("-,+", "+,0")
			   || profile.HasMitte("-,+", "+!,-"))
			{
				profile.AddInterpretationNote(InterpretationNotes.AutistischenKainMitte);
			}
			
			//B3 p.402 B.2 b)
			if(profile.HasMitte("0,+", "+,+"))
			{
				profile.AddInterpretationNote(InterpretationNotes.PositivMitte);
			}
			
			//B3 p.402 B.2 b)
			if(profile.HasMitte("0,+", "+,+"))
			{
				profile.AddInterpretationNote(InterpretationNotes.TeilsPositivKainsMitte);
			}
			
			if(profile.P.EqualsTo("±,±") 
			   && profile.S.EqualsTo("-,-")
			   && profile.Sch.IsAny("+,+", "±,+", "±,±")
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.BesonderEthMoralDilemma);
			}
			
			#endregion
			
			#region Kontakt notes
			// p.300 V.1.
			if(profile.C.IsAny("0,-", "0,-!", "0,-!!"))
			{
				var note = InterpretationNotes.HypomanischeOrHaltlose;
				profile.AddInterpretationNote(note);
			}
			
			// p.283 VII Kontaktsperre
			if(profile.C.IsAny("-,-", "-,-!"))//-,-! p.286 I.3
			{
				var note = InterpretationNotes.Kontaktsperre;
				profile.AddInterpretationNote(note);
			}

			if(profile.C.EqualsTo("-,+"))//p.374
			{
				var note = InterpretationNotes.InzestuösesAnhangen;
				profile.AddInterpretationNote(note);			
			}

			#region Melancholische und manische Mitte
			//B3 p.349
			if(profile.HasMitte("-,+", "+!,-")
			   || profile.HasMitte("0,0", "+!,-")
			   || profile.HasMitte("0,+", "+,-")
			   || profile.HasMitte("0,+", "+,0")
			   || profile.HasMitte("-!,+", "+,-")//p.350 VI.
			   || profile.HasMitte("-,+", "+,0")//p.350 VI.
			   || profile.HasMitte("-!,0", "+!!,0")//p.350 VI.
			   || profile.HasMitte("0,0", "+,-")//p.350 VI.
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.MelancholischeMitte);
			}
			
			//B3 p.349
			if(profile.HasMitte("0,0", "-!,-")
			   || profile.HasMitte("0,+", "-!,-")
			   || profile.HasMitte("+,-", "-!,-")
			   || profile.HasMitte("±,0", "-!,-")
			   || profile.HasMitte("0,0", "-!,±")//.353 V. 1.
			   || profile.HasMitte("0,+", "-,-")//.353 V. 4.
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.ManischeMitte);
			}
			#endregion
							
			#endregion
			
			// p.?
			// doubt: move to DetectPsychopatischeMitte or DetectSexualMitte?
			if(profile.HasMitte("±,-", "-,-")
			   || profile.HasMitte("+,-", "-,-")
			   || profile.HasMitte("+,±", "-!,-")
			   || profile.HasMitte("-,-", "-,-"))
			{
				profile.AddInterpretationNote(InterpretationNotes.NegativeMitte);
			}
			
			if((profile.S.EqualsTo("0,+") || profile.S.EqualsTo("-,+"))
			   && profile.P.EqualsTo("-,±")
			   && profile.Sch.EqualsTo("-,±")
			   && profile.C.EqualsTo("-,±")
			  )
			{
				// Buch 3, p.359
				// and Buch 3, cap.III
				profile.AddInterpretationNote(InterpretationNotes.Ödipussyndrom);
			}
			
			// p.280 VII.
			if(profile.HasMitte("+,0", "0,+", FactorsComparisonOptions.Hypertension_insensitive))
			{
				var note = InterpretationNotes.PhobischenBesessenheitMitte;
				profile.AddInterpretationNote(note);
			}
			
			// p.280 VII.
			if(profile.HasMitte("0,+", "0,+", FactorsComparisonOptions.Hypertension_insensitive))
			{
				var note = InterpretationNotes.BesessenheitGeltungsdrangMitte;
				profile.AddInterpretationNote(note);
			}
			
			// p.293
			if(profile.HasMitte("0,-!", "-!,-")
			   || profile.HasMitte("0,-", "-!,-!")
			   || profile.HasMitte("0,-!", "-!,0")
			   || profile.HasMitte("0,-!", "-,±")
			   || profile.HasMitte("-,-", "-,-!")// doubt: overlap w/ hypochondrische
			   //p.295
			   || profile.HasMitte("0,-", "-,-!!")// doubt: overlap w/ hypochondrische
			   || profile.HasMitte("0,-!", "-!,±")
			   || profile.HasMitte("+,-!", "-!,-")
			   || profile.HasMitte("0,-!!", "-!,0")
			   || profile.HasMitte("-,-!", "-!,-")//Antisoziale Mite
			  )
			{
				var note = InterpretationNotes.HebephreneMitte;
				profile.AddInterpretationNote(note);
			}
			
			if(profile.HasMitte("0,-", "-,+")
			   || profile.HasMitte("+,-", "-,+"))
			{	//p.315
				var note = InterpretationNotes.Schuldangst;
				profile.AddInterpretationNote(note);
			}
	
			if(profile.HasMitte("+,-", "-,±")
			   || profile.HasMitte("+,-!", "-,±")
			   || profile.HasMitte("0,-", "-,±")
			   || profile.HasMitte("-,-", "-,±")
			   || profile.HasMitte("-,-", "-,0")//p.336 V.
			  )
			{	//p.329
				var note = InterpretationNotes.DepersonalisationMitte;
				profile.AddInterpretationNote(note);
			}
			
			if(profile.s.IsAny("-", "-!", "-!!", "-!!!")
			   && profile.k.IsAny("-", "-!", "-!!", "-!!!"))
			{
				//p.442 II.4
				var note = InterpretationNotes.EntwertungSelb;
				profile.AddInterpretationNote(note);
			}
			
			DetectSexualMitte(profile);
			DetectPsychopatischeMitte(profile);
			
			FurtherCompositeNotes(profile);
		}
		
		internal static void DetectSexualMitte(TestProfile profile)
		{
			// pp.376-7
			if(profile.HasMitte("+,-", "+,0")
			   || profile.HasMitte("+,±", "+,0")
			   || profile.HasMitte("+,0", "+,0")
			   || profile.HasMitte("+,0", "+,±")
			   || profile.HasMitte("+,+", "+,0")
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.FetischMitte);
			}
			
			if(profile.HasMitte("+,-", "+,-")
			   || profile.HasMitte("+,-", "0,+")
			   || profile.HasMitte("+,-", "+,+")
			   || profile.HasMitte("0,-", "+,-")
			   || profile.HasMitte("0,-", "0,+")
			   || profile.HasMitte("0,-", "+,+")
			   || profile.HasMitte("+,-", "0,0"))
			{
				profile.AddInterpretationNote(InterpretationNotes.MasochistMitte);
			}
			
			if((profile.P.IsAny("-,+", "-,±") && profile.Sch.IsAny("-,+", "-,±"))
			   || (profile.P.EqualsTo("-,0") && profile.Sch.IsAny("-!,+", "-!,±", "0,-!"))
			   || (profile.P.EqualsTo("0,-") && profile.Sch.IsAny("-,+", "-,±", "-,0"))
			   || (profile.P.EqualsTo("0,0") && profile.Sch.IsAny("0,±", "0,+", "-,-"))
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.SadistMitte);
			}
			
			if(profile.HasMitte("0,-", "-,±")
			   || profile.HasMitte("0,0", "-,±")
			   || profile.HasMitte("-,-", "-,±")
			   || profile.HasMitte("0,-", "-,0"))
			{
				profile.AddInterpretationNote(InterpretationNotes.ExhibitionistMitte);
			}
			
			if(profile.HasMitte("+,-", "0,±")
			   || profile.HasMitte("0,-", "0,±")
			   || profile.HasMitte("0,-", "-,+")
			   || profile.HasMitte("0,±", "0,±")
			   || profile.HasMitte("0,±", "0,+")
			   || profile.HasMitte("0,0", "0,+"))
			{	//p.408
				profile.AddInterpretationNote(InterpretationNotes.SzondiInversionsMitte);
			}
			
			// Buch 3, p.390
			if(profile.S.EqualsTo("0,0"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Asketismus);
			}
			
			// Buch 3, p.394
			if(profile.S.EqualsTo("-,-"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Sublimation);
			}
			
			// Buch 3, p.391
			if(profile.S.EqualsTo("+!,+!!!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.TierischeBrutalität);
			}
		}
		
		internal static void DetectPsychopatischeMitte(TestProfile profile)
		{
			if(profile.HasMitte("0,±", "0,0")//p.383
			   || profile.HasMitte("0,±", "+,0")//p.394
			  )
			{	
				profile.AddInterpretationNote(InterpretationNotes.PsychopatischerMitte);
			}
			
			if(profile.HasMitte("-,-", "+,+")
			   || profile.HasMitte("-,-", "±,+")
			   || profile.HasMitte("0,-", "+,+")
			   || profile.HasMitte("0,-", "+,0"))
			{	//p.383
				profile.AddInterpretationNote(InterpretationNotes.SchwacheMitte);
			}
			
			//p.439
			if(profile.HasMitte("0,+", "+,±")
			   || profile.HasMitte("+,+", "0,±"))
			{
				profile.AddInterpretationNote(InterpretationNotes.PositiveSchwacheMitte);
			}
			
			// p.433 haltlose Mitte 
			if(profile.HasMitte("-,-", "-,+")// also "asoziale Mitte" p.442
			   || profile.HasMitte("-,-", "-,±")
			   || profile.HasMitte("+,+", "+,+")
			   || profile.HasMitte("+,+", "+,±")
			   || profile.HasMitte("+,-", "-,+")
			   || profile.HasMitte("0,-", "-,+")
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.HaltloseMitte);
			}
			
			// Buch 3, p.362, Tabelle 39 Variationen der Mitter bei Psychopathen
			// p.430 Süchtige Mitte
			// p.433 haltlose Mitte (some coincide)
			if(profile.HasMitte("0,0", "0,0")
			   || profile.HasMitte("0,0", "0,+") // Paranoide Trunksucht; Kleptomanie
			   || profile.HasMitte("+,0", "0,0") // Trunksucht
			   || profile.HasMitte("0,-", "0,0") // Trunksucht; Sadomasochismus
			   || profile.HasMitte("+,0", "0,±") // Trunksucht
			   || profile.HasMitte("+,0", "0,-") // Exhibitionismus; Trunksucht
			   || profile.HasMitte("0,0", "0,-") // p.430 Süchtige Mitte
			   || profile.HasMitte("0,0", "0,±")
			   || profile.HasMitte("±,0", "0,0")
			   || profile.HasMitte("-,0", "0,0")
			  )
			{
				// overlap with VerlustDerMitte
				profile.AddInterpretationNote(InterpretationNotes.TrunksuchtMitte);
			}
			
			//p.439-40 x00x, 00xx, xx00
			if((profile.P.EqualsTo("0,0") && (profile.k.IsEqualTo("0") || profile.p.IsEqualTo("0")))
			   || ( profile.Sch.EqualsTo("0,0") && (profile.e.IsEqualTo("0") || profile.hy.IsEqualTo("0")))
			   || (profile.hy.IsEqualTo("0")
			       && profile.k.IsEqualTo("0")))
			{
				// overlap with TrunksuchtMitte
				profile.AddInterpretationNote(InterpretationNotes.VerlustDerMitte);
			}
		}
		
		internal static void FurtherCompositeNotes(TestProfile profile)
		{
			DetectSucht(profile);
			DetectKSexuellenHaltlosigkeit(profile);
			
			if((profile.HasInterpretationNote(InterpretationNotes.RepresionIchsperrung) 
			   || profile.Sch.EqualsTo("-!!,±"))//doubt, from Skala 5a
			   &&
			   profile.HasInterpretationNote(InterpretationNotes.Kontaktsperre))
			{	
				//Existenzskala rows 5a (Katatoniforme)
				//Buch 3, pp.243, Tabelle 33
				//B3 p.284
				//B3 p.310 "totale katatoniforme Sperrung"
			
				// more doubt: add C0- (with Sukzession condition?)
				var note = InterpretationNotes.TotaleKatatonifIchsperrung;
				profile.AddInterpretationNote(note);		
			}
			
			// p.283 VII katatoniforme Sperrungssyndrom
			// p.306 VII. (s+! + kain + Ichsperre + kontaktsperre)
			// p.312
			if(profile.HasInterpretationNote(InterpretationNotes.TotaleKatatonifIchsperrung)
			   && (profile.HasInterpretationNote(InterpretationNotes.Kain) 
			       || profile.e.IsEqualTo("-"))// hy (0,±,-) optional; hy ±,- p.286 VII.
			   && profile.s.IsAny("+!", "+!!", "+!!!") 
			  )
			{
				var note = InterpretationNotes.KatatonifSperrungsSynd;
				profile.AddInterpretationNote(note);
			}
			
			//p.398 III. paroxymale (epileptiforme) «Kain»-Syndrom
			if(profile.HasInterpretationNote(InterpretationNotes.Kain) 
			   || profile.P.EqualsTo("0,-")// sich verstecken
			   || profile.HasInterpretationNote(InterpretationNotes.TotalDesintegrAffekte_ApathieStupor)
			   || profile.HasInterpretationNote(InterpretationNotes.KainHideWechsel)
			   || profile.HasInterpretationNote(InterpretationNotes.MörderE)
			   || profile.HasInterpretationNote(InterpretationNotes.MörderE_mitVentil))
			{
				ushort symptoms = 0;
				
				if(profile.GroßeMobilität("-", "0", "+", "±", Factors.e))
				{
					symptoms++;
				}
				
				if(profile.HasInterpretationNote(InterpretationNotes.Kontaktsperre))
				{
					symptoms++;
				}
				
				if(profile.InSukzession("0,-", "-,0", Vectors.P))
				{
					symptoms++;
				}
				
				if(profile.HasInterpretationNote(InterpretationNotes.MörderE)
				   || profile.HasInterpretationNote(InterpretationNotes.MörderE_mitVentil))
				{
					symptoms++;
				}
				
				if(profile.HasInterpretationNote(InterpretationNotes.TotalDesintegrAffekte_ApathieStupor)
				   || profile.HasInterpretationNote(InterpretationNotes.Kain)
				   || profile.P.EqualsTo("0,-"))
				{
					symptoms++;
				}
				
				
				if(profile.HasInterpretationNote(InterpretationNotes.KainHideWechsel))
				{
					symptoms++;
				}
					
				if(symptoms >= 2)
				{
					profile.AddInterpretationNote(InterpretationNotes.ParoxyKainSyndrom);
				}
			}	
		}
		
		#region utils
		private static bool Faktorenverband(bool[] conditions)
		{
			ushort defaultToleranceFalseConditions = 0;
			
			if(conditions.Length >= 7)
			{
				defaultToleranceFalseConditions = 2;
			}
			else if(conditions.Length >= 4 && conditions.Length < 7)
			{
				defaultToleranceFalseConditions = 1;
			}
			return Faktorenverband(conditions, defaultToleranceFalseConditions);
		}
		
		private static bool Faktorenverband(bool[] conditions, ushort tolerance)
		{
			ushort trueCount = 0;
			
			foreach(var condition in conditions)
			{
				if(condition)
				{
					trueCount++;
				}
			}
			
			bool enouchConditionsTrue 
				= (trueCount + tolerance) >= conditions.Length;
			return enouchConditionsTrue;
		}
		
		private static ushort GradeCorrespondence(
			VectorReaction vectorReaction, string factorReaction, Factors factorName)
		{
			if(vectorReaction.HasFactorReaction(factorReaction, factorName))
			{
				return 3;   
			}
			if(vectorReaction.HasFactorReaction(factorReaction, 
			                                    factorName, 
			                                    FactorsComparisonOptions.Hypertension_insensitive))
			{
				return 3;   
			}
			if(vectorReaction.ContainsFactorReaction(factorReaction, factorName))
			{
				return 2;   
			}
			
			//TODO fix whole method w/ proper general distance funktion bwt reactions
			if(vectorReaction.HasFactorReaction("0", factorName))
			{
				return 0;
			}
			
			return 0;
		}
		#endregion
	}
}
