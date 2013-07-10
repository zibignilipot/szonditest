using System;
using System.Collections.Generic;

namespace SzondiTest
{
	/// <summary>
	/// 
	/// </summary>
	public class Syndromatic
	{
		public static List<Existenzformen> BestimmungDerExistenzformen(TestProfile profile)
		{
			#region Interpretation notes			
			DetectIntepretationNotes(profile);
			#endregion
			
			var detectedExistenzFormen = new List<Existenzformen>();
			
			// Ich
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
				
			// Sexuelle
			DetectPerversionSadomasochismus(profile);
			DetectInversion(profile);
			
			// Kontakt
			DetectHypomanische_Manische(profile);		
			DetectDepressive_Melancholische(profile);					
			DetectKontaktPsychopathische(profile);
			
			// doubles
			DetectManiformeParanoide(profile);
						
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
			// p.261 n. X
			DetectSexualstörungen(profile);
			
			// Buch 3, p.484 IV b), p.266 XI.2, Skala row 14c			
			DetectPhobieNote(profile);
			
			// Further notes
			FurtherNotes(profile);
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
				if((profile.k.IsEqualTo ("-") //Überdruck?
				    || profile.Sch.ContainsFactorReaction("-", Factors.p)//Überdruck?
				   )
				   &&
				   profile.m.IsAny("-", "-!") //±ungültig
				  )
				{
					if(profile.e.IsEqualTo ("-")) //Überdruck?
					{
						// Mörder-E
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
			
			#region Existenzskala row 13a (reine Kain)
			// p.495, Tabelle 55, III 4-5
			if(profile.HasMitte("-,0", "+,-") 
			   || profile.HasMitte("-,+", "+,-") )
			{
				detected = true;
				var note = InterpretationNotes.ReineKain;
				profile.AddInterpretationNote(note);
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
			
			#endregion
			
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
			// Buch 3, p.483 intro, p.484 IV b)
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( profile.P.EqualsTo("+,0")
				   &&
				   (profile.Sch.EqualsTo("±,+") || profile.Sch.EqualsTo("±,±"))
				  )
				{
					// phobie
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
			#endregion
			
			#region Existenzskala row 14b (Hysteriforme)
			// Buch 3, p.484 IVa,
			// und Buch 2, Existenzskala Tabelle 25
			{
				if( (profile.P.EqualsTo("+,+") || profile.P.EqualsTo("0,0"))
				   &&
				   (profile.Sch.HasFactorReaction("-", Factors.k, FactorsComparisonOptions.Hypertension_insensitive)
				    && profile.Sch.HasFactorReaction("±", Factors.p)))
				{
					// Konversionshysterie p.484 IVa and row 14b
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
		
		internal static void DetectKain(TestProfile profile)
		{
			// Buch 3, p.279 I.1, p.280 I.
			if(profile.P.IsAny("-,+", "-,+!", "-,0"))//-0 pp.305 XI., 306 VII
			//  doubt -±, -0 p.305 XI.
			{
				var note = InterpretationNotes.Kain;
				profile.AddInterpretationNote(note);
			}
		}
		#endregion
		
		#region KontankExistenzFormen (6-8)
		
		internal static void DetectKontaktPsychopathische(TestProfile profile)
		{
			bool detected = false;
			// Buch 3 p.430, V (Trunksucht)
			// Skala row 8d
			if(profile.C.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.m)
			   //&& profile.Sch.HasFactorReaction("-!", Factors.k, FactorsComparisonOptions.HypertensionEqualORGreater)
			  )
			{
			   	detected = true;
			   	profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			
			// Buch 3 p.430, VI Variation 1.
			// Skala row 8c
			// pp.434-5, V. e) Lustprinzip 
			if(profile.S.HasAnyFactorReaction("-!", "-!!", "-!!!",Factors.s)
			   && profile.P.HasFactorReaction("0", Factors.hy)
			   && profile.Sch.HasFactorReaction("-!", Factors.k, FactorsComparisonOptions.HypertensionEqualORGreater)
			   && HasLustprinzipSyndrom(profile)// TODO fix special LustS. for row 8
			  )
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			
			//TODO add p.440 Lustprinzip
			
			// Buch 3 p.430, VI Variation 1., und VII Süchtige Mitte
			// Skala row 8a
			if(profile.HasMitte("0,0", "0,0") 
			   && HasLustprinzipSyndrom(profile))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			
			// Buch 3 p.430, VI Variation 1. (full Faktorenverban)
			if(Faktorenverband(new bool[] {
			        profile.s.IsAny("-", "-!", "-!!", "-!!!"),
					profile.e.IsEqualTo("0"),
					profile.hy.IsAny("0", "+", "+!", "+!!", "+!!!"),
					profile.k.IsAny("0", "-", "-!", "-!!", "-!!!"),
					profile.p.IsAny("0", "±", "+", "+!", "+!!", "+!!!"),
					profile.d.IsAny("0", "+!", "+!!", "+!!!"),
					profile.m.IsAny("0", "+!", "+!!", "+!!!")}))
			{
				detected = true;
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
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sucht);
			}
			
			// Buch 3, p.362, Tabelle 39 Variationen der Mitter bei Psychopathen
			if(profile.HasMitte("0,0", "0,0")
			   || profile.HasMitte("0,0", "0,+") // Paranoide Trunksucht; Kleptomanie
			   || profile.HasMitte("+,0", "0,0") // Trunksucht
			   || profile.HasMitte("0,-", "0,0") // Trunksucht; Sadomasochismus
			   || profile.HasMitte("+,0", "0,±") // Trunksucht
			   || profile.HasMitte("+,0", "0,-") // Exhibitionismus; Trunksucht
			  )
			{
				profile.AddInterpretationNote(InterpretationNotes.TrunksuchtMitte);
			}
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.KontaktPsychopathische);
			}
		}
		
		internal static void DetectDepressive_Melancholische(TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala rows 6ab (Depressive_Melancholische)
			//Buch 3, pp.243-4, Tabelle 34
			//Buch 3, p.272 I.4, p.274 II.6 (depressive Faktorenverband
			{
				if( profile.k.IsAny("+", "+!")
				   && profile.d.IsAny("+", "+!", "+!!")
				   && profile.s.IsAny("-", "-!", "0"))
				{
					detected = true;
				}
			}
			#endregion
			
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
			
			if(detected)
			{
				var exForm = Existenzformen.Depressive_Melancholische;
				profile.AddHasExistenzform(exForm);
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
		
		internal static void DetectHypomanische_Manische(TestProfile profile)
		{
			bool detected = false;
			
			#region Existenzskala rows 7abc (Hypomanische_Manische)
			//Buch 3, pp.243-4, Tabelle 35
			{
				if( profile.C.Contains("0,-")
				   && profile.Sch.Contains("-,-")
				   && profile.S.Contains("+,+!"))
				{
					detected = true;
				}
			}
			#endregion
			
			#region Existenzskala rows 7a
			{
				if(profile.C.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!"))
				{
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
			}
			#endregion
			
			#region Existenzskala rows 7c
			{
				if(profile.S.IsAny("+,+!!", "+,+!!!")
				  && profile.P.IsAny("0,0", "-,+", "+,-", "0,-")
				  && profile.Sch.IsAny("-!,-", "-!,0")
				  && profile.C.IsAny("0,-", "-,-"))
				{
					detected = true;
				}
			}
			#endregion
			
			if(detected)
			{
				var exForm = Existenzformen.Hypomanische_Manische;
				profile.AddHasExistenzform(exForm);
			}
		}
		#endregion
		
		#region SchutzExistenzformen (11-12, 15-17)
		internal static void DetectCompulsiveNeurotic(TestProfile profile)
		{
			bool detected = false;
			
			// TODO more examples (for unittesting and checking) from Buch 4 Ich-A
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
			
			#region Zwangsimpulse (row 12a) less strict version
			//row 12a un d p.474 VI. b)
			{
				// 3 Vektoren mit ± Reaktionen
				ushort countAmbivalentVectors = 0;
				
				if(profile.Sch.HasFactorReaction("±", Factors.k) 
				   || profile.Sch.HasFactorReaction("±", Factors.p))
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
		
		internal static void DetectHypochondrischeExistenzformen(TestProfile profile)
			
		{
			//p.309 diff btw Hypocondriac (11u15) and Hebephrene (4):
			// Hypocondriac: inhibits (Sch=-+) or removes (Sch=-0) the obsession
			// Hebephrene: projects the obsession to a body organ (Sc-!-!)
			
			bool detected = false;
			
			#region Skala row 11a
			// p.292, p.460
			if(profile.e.IsAny("0", "±", "+", "+!", "+!!")
			   && profile.hy.IsEqualTo("-")
			   // NB.CompareTo k-! only when p0, distinguish from Heboide
			   && profile.Sch.IsAny("-,+", "-,±", "-,0", "-!,0"))
			{
				detected = true;
			}
			
			//"das Syndrom der Hypochondrie" p.317 (and Skala 11a)
			if(profile.HasInterpretationNote(InterpretationNotes.HypochondrischeMitte))
			{	
				detected = true;
			}
			
			#endregion
			
			#region Skala rows 11b, 11c, 11d
			// Skala row 11b
			if(profile.P.IsAny("+,-", "+,-!", "±,-", "0,-")
			   && profile.Sch.IsAny("-,-", "-,-!"))
			{
				detected = true;
			}
			
			// Skala row 11c (Organneurose (15)
			if(profile.P.EqualsTo("-,-")
			   && profile.Sch.EqualsTo("-,-!"))
			{
				detected = true;
			}
			
			// Skala row 11d
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
		
		#endregion
		
		#region SexuelleExistenzformen
		internal static void DetectPerversionSadomasochismus(TestProfile profile)
		{
			bool detected = false;
			
			#region sadismus (row 9a)
			
			// p.375 III. Syndrom des Sadismus
			if(profile.S.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.s)
				&& profile.C.HasFactorReaction("+", Factors.d, FactorsComparisonOptions.Hypertension_insensitive))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Analsadismus);
			}
			
			// p.375 III. Syndrom des Sadismus, mit Lustsyndrom
			if(profile.S.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.s)
			   && HasLustprinzipSyndrom(profile)) 
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sadismus);
			}
			
			// row 9a colums S only
			// p.375 III. Syndrom des Sadismus s+!, k+!
			// p.375 II. Destruktive (entwertende Perversionene)
			if(profile.S.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.s)
				&& profile.Sch.HasAnyFactorReaction("-!", "-!!", "-!!!", Factors.k))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Sadismus);
			}
			
			#endregion
			
			#region Masochismus, Fetischismus, polymorph perversion (row 9b)
			
			// Skala row 9b
			if(profile.S.HasAnyFactorReaction("-!!", "-!!!", Factors.s))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.374 I. primären Masochismus (=Todestriebes)
			if(profile.S.HasAnyFactorReaction("-", "-!", "-!!", "-!!!", Factors.s)
			   && profile.C.EqualsTo("-,+")
			   && (profile.Sch.EqualsTo("-,+") || profile.Sch.EqualsTo("-,0")))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.375 I I. sekundären Masochismus 
			if(profile.S.HasAnyFactorReaction("-", "-!", "-!!", "-!!!", Factors.s)
			   && (profile.C.HasFactorReaction("-", Factors.d) 
			       || profile.C.HasFactorReaction("0", Factors.d))
			   && (profile.C.HasFactorReaction("±", Factors.m) 
			       || profile.C.HasFactorReaction("0", Factors.m))
			   && (profile.Sch.EqualsTo("+,-")))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Masochismus);
			}
			
			// p.372, Fetischismus, I.
			// p.376 Tabelle 40, IV., V.
			if((profile.Sch.HasAnyFactorReaction("+!", "+!!", "+!!!", Factors.k)
			    || profile.Sch.EqualsTo("+,+!", // row 9b
			                            FactorsComparisonOptions.HypertensionEqualORGreater))
			   && profile.S.HasAnyFactorReaction("-!", "-!!", "-!!!", Factors.s))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.Fetischismus);
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
			
			#region polymorph perversion (row 9c)
			// p.380 
			if(profile.S.HasFactorReaction("±", Factors.s)
				&& HasLustprinzipSyndrom(profile))
			{
				detected = true;
			}
			#endregion
			
			#region Perverses Lustsyndrom (rows 9abc)
			// p.377 (Column 3, section VII.C.), 389 
			// TODO delete? dubious, weak sources
			// p.502, IV (profiles 6, 8 from p.501)	
			if(HasLustprinzipSyndrom(profile)
			   && profile.Sch.EqualsTo("+,+!")
			   && profile.S.HasFactorReaction("-", Factors.s))
			{
				detected = true;
				profile.AddInterpretationNote(InterpretationNotes.PolymorphPervers);
			}
			#endregion
			
			if(detected)
			{
				profile.AddHasExistenzform(Existenzformen.Perversion_Sadismus_Masochismus);
			}
		}
		
		internal static void DetectInversion(TestProfile profile)
		{
			#region Existenzskala row 10a
			// Apha version: with no checks to Syndromatic
			{
				if( profile.partOf.testTakerSex == Sex.Male)
				{
					if((profile.S.ContainsFactorReaction("+", Factors.h) 
					   || profile.S.ContainsFactorReaction("0", Factors.h))
						&& profile.S.ContainsFactorReaction("-", Factors.s) // includes s-!!
						//TODO add check for "{" symbol : sequence 
					)
					{
						// detectedExistenzFormen.Add(Existenzformen.Inversion_Homo_Trans);
					}
				}
			}
			#endregion
			
			#region Existenzskala row 10c
			// Buch 3, p.406, p.409 6. a)b)c)d), (p.411 reprise)
			if( profile.partOf.testTakerSex == Sex.Male)
			{
				if(profile.S.ContainsFactorReaction("-", Factors.s)
				  && profile.P.ContainsFactorReaction("-", Factors.hy) 
				  && profile.Sch.EqualsTo("0,±") 
				  && profile.C.EqualsTo("+,+")
				 )
				{
					profile.AddHasExistenzform(Existenzformen.Inversion_Homo_Trans);
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomo);
				}
			}
			
			// Buch 3, p.411
			if( profile.partOf.testTakerSex == Sex.Female)
			{
				if(profile.S.HasFactorReaction("+", Factors.s)
				   && profile.P.ContainsFactorReaction("+", Factors.hy)
				   && (profile.Sch.EqualsTo("±,0") || profile.Sch.EqualsTo("+,0"))
				   && profile.C.EqualsTo("+,+")
				 )
				{
					profile.AddHasExistenzform(Existenzformen.Inversion_Homo_Trans);
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomo);
				}
			}
			#endregion
			
			#region Existenzskala row 10a
			// Buch 3, p.409
			if( profile.partOf.testTakerSex == Sex.Male)
			{
				if(profile.S.HasAnyFactorReaction("-", "-!", "-!!", "-!!!", Factors.s)
				   && profile.P.ContainsFactorReaction("-", Factors.hy)
				   && (profile.Sch.EqualsTo("0,±") || profile.Sch.EqualsTo("+,±")
				      || profile.Sch.EqualsTo("0,+") || profile.Sch.EqualsTo("-,+"))
				   && (profile.C.EqualsTo("±,+") || profile.C.EqualsTo("0,+")
				      || profile.C.EqualsTo("+,±") || profile.C.EqualsTo("+,0")))
				{
					profile.AddHasExistenzform(Existenzformen.Inversion_Homo_Trans);
					profile.AddInterpretationNote(InterpretationNotes.SzondiHomo);
				}
			}
						
			#endregion
		}
				
		internal static void DetectSexualstörungen(TestProfile profile)
		{
			/* TODO: delete. replaced by Triebzielinversion. Male only. No kontaktstörungen
			// Sexualstörungen pp.261 X., example p.262
			if((profile.S.IsAny("+,-", "+!,-", "+!!,-", "+!!!,-") 
			    || profile.S.IsAny("+,±", "+!,±"))//p.262 IX.2.
			   && profile.hy.IsAny("-", "-!")//-! p.262 IX.2.
			   && profile.p.IsAny("-", "-!", "+")
			   && profile.d.IsAny("+", "+!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Sexualstörungen);
			}*/
		}
		
		internal static void DetectTriebzielinversion(TestProfile profile)
		{
			// p.305, X.
			var note = InterpretationNotes.Triebzielinversion;
			
			if( profile.partOf.testTakerSex == Sex.Male)
			{
				if(profile.S.IsAny("+,-","+!,-","+!!,-", "+!!!,-"))// p.262 IX.1 (h+!)
				// doubt: +± p.262 IX.2
				{
					profile.AddInterpretationNote(note);
				}
			}
			
			if(profile.partOf.testTakerSex == Sex.Female)
			{
				if(profile.S.IsAny("-,+", "-,±"))//p.305
				{
					profile.AddInterpretationNote(note);
				}
			}
		}
		
		#endregion
		
		#region IchExistenzformen
		
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
			if((profile.P.IsAny("-,+", "-,0", "±,0", "0,+", "-,-", "0,+")
			    //|| profile.P.IsAny("-,+!",)//doubt, p.281
			   )
			   && profile.Sch.IsAny("0,+", "0,+!")
			  )
			{
				var note = InterpretationNotes.InflativParanoMitte;
				profile.AddInterpretationNote(note);
			}
		}
		
		internal static void DetectParanoideSpaltungsSyndrom(TestProfile profile)
		{
			if(DetectParanoideSpaltungsSyndromHelper(profile, true))
			{
				// Form a) +-
				// Buch 3 p.260 I.
				profile.AddInterpretationNote(InterpretationNotes.ParanoSpaltungsSynd);
			}
			else if(DetectParanoideSpaltungsSyndromHelper(profile, false))
			{
				// Form b) -+
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
			if(profile.k.IsAny("-!", "-!!", "-!!!") 
			   && profile.P.EqualsTo("-,0"))
			{
				detected = true;
			}
			
			if(profile.k.IsAny("-!", "-!!", "-!!!") 
			   && profile.P.IsAny("-,0", "-,±","0,-","0,±") 
			   && profile.s.IsAny("-!!", "-!!!", "+!!", "+!!!")
			   && profile.p.IsEqualTo("0"))
			{
				detected = true;
			}
			#endregion
			
			#region Existenzskala row 5c
			if(profile.k.IsAny("-!!", "-!!!") 
			   && profile.p.IsEqualTo("-")
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
				  && (profile.partOf.HasGefahrTriebklasse("C", "m", "-") 
				      || profile.partOf.HasGefahrTriebklasse("Sch", "p", "-")))	
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
			
			// Skala 4a
			if(profile.hy.IsAny("-!", "-!!", "-!!!")
			   && profile.k.IsAny("-!", "-!!", "-!!!"))
			{
				detected = true;
			}
			
			// Skala 4b
			// Skala 4c
			// TODO
			
			// Skala 4d, Heb.Mitte
			// p.293
			if(profile.e.IsEqualTo("0")
			   && profile.hy.IsAny("-!", "-!!")
			   && profile.k.IsEqualTo("-!")
			   && profile.hy.IsAny("-", "-!")
			  )
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
			
			if(detected)
			{
				var exForm = Existenzformen.Heboide;
				profile.AddHasExistenzform(exForm);
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
			// Buch 3 p.359
			if((// also p+ per p.389
				profile.Sch.ContainsFactorReaction("+", Factors.p) // +, ±
			   || profile.Sch.HasFactorReaction("0", Factors.p)) // seltener
			   &&
			   (profile.C.ContainsFactorReaction("+", Factors.d) // +, ±
			   || profile.C.HasFactorReaction("0", Factors.d)) // seltener
			   &&
			   (profile.C.ContainsFactorReaction("+", Factors.m) // +, ±
			   || profile.C.HasFactorReaction("0", Factors.m))) // seltener
			{
				// detected
				profile.AddInterpretationNote(InterpretationNotes.Lustprinzip);
				
				// more detailed notes:
				
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
				 	profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_ConstRivaligWPartner);
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
				
				if(profile.Sch.HasFactorReaction("-!", Factors.k))
				{
				   	// Buch 3, p.359, 4.
					// Negation of the ideals, or depreciation of all ideals
					// self-devaluation, ...
					profile.AddInterpretationNote(InterpretationNotes.Lustprinzip_NegationOfTIdeals);
				}
				#endregion
				
				// p.367 Syndrom des (perversen) Lustprinzip
				if(profile.Sch.HasAnyFactorReaction("+!", "+!!", "+!!!", "0", "±", Factors.p)
				   && profile.C.HasAnyFactorReaction("+", "0", "±", Factors.d)
				   && profile.C.HasAnyFactorReaction("+", "0", "±", Factors.m)
				   && profile.S.HasAnyFactorReaction("+", "0", "±", Factors.h)
				   && profile.S.HasAnyFactorReaction("+", "0", "-", Factors.s)
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
				
				return true;
			}
			
			return false;
		}
			
		internal static void FurtherIchSimpleNotes(TestProfile profile)
		{
			DetectParanoideSpaltungsSyndrom(profile);
			
			if(profile.Sch.EqualsTo("0,+!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.TotaleInflation);
			}
					
			if(profile.Sch.EqualsTo("+,+!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Introinflation);
			}
			
			if(profile.Sch.EqualsTo("±,-!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.FuguesAusreißen);
			}
			
			if(profile.Sch.EqualsTo("-,-!"))
			{
				profile.AddInterpretationNote(InterpretationNotes.Dissimulation);
			}
			
			if(profile.Sch.EqualsTo("-!,0"))
			{
				profile.AddInterpretationNote(InterpretationNotes.DestruktiveVerneinung);
			}
			
			if(profile.Sch.IsAny("0,-", "0,-!", "0,-!!", "0,-!!!"))
			{
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
			DetectKain(profile);
			DetectTriebzielinversion(profile);
			
			#region Katatoni notes
			// p.283 VII Ichsperre 
			if(profile.Sch.IsAny("-,0", "-!,0", "-!!,0", "-!!!,0", "-!!,±"))
			{	
				// doubt: also -!!,± ? (Skala 5a)
				profile.AddInterpretationNote(InterpretationNotes.Ichsperrung);
				
				if(profile.k.IsAny("-!", "-!!", "-!!!"))
				{	// if ! (!!) -> katatoniforme Ichsperrung, p.310
					var note = InterpretationNotes.KatatonifIchsperrung;
					profile.AddInterpretationNote(note);
				}
			}
			#endregion
			
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
			
			#region Mord
			// Buch 3, p.361, Tabelle 39 Variationen der Mitter bei Psychopathen
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
			
			// p.316, p.460, Skala 11a
			if(profile.HasMitte("+,-", "-,+")
			   || profile.HasMitte("0,-", "-,0") 
			   || profile.HasMitte("+,-", "-,0") 
			   || profile.HasMitte("+!,-", "-,0")//p.317, Skala 11a
			   || profile.HasMitte("+!!,-", "-,0")//Skala 11a
			   || profile.HasMitte("0,-", "-,+")
			   || profile.HasMitte("±,-", "-,±")
			   || profile.HasMitte("0,-", "-,±")
			   || profile.HasMitte("±,-", "-,0")
			   || profile.HasMitte("0,±", "-,0")//also psychotische hypochondrische Syndrom
			   || profile.HasMitte("+,-", "0,+!")
			   || profile.HasMitte("+,-", "+!,-")
			   || profile.HasMitte("+,-", "±,+")
			   || profile.HasMitte("+,-", "+,+")
			  )
			{
				var note = InterpretationNotes.HypochondrischeMitte;
				profile.AddInterpretationNote(note);
			}
			
			// p.325
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
				
			// p.274 II.7
			if(profile.P.EqualsTo("0,0"))
			{
				var note = InterpretationNotes.TotalDesintegrAffektleben;
				profile.AddInterpretationNote(note);
			}
			
			// p.274 II.8
			if(profile.P.EqualsTo("0,±"))
			{
				var note = InterpretationNotes.Lamentation;
				profile.AddInterpretationNote(note);
			}
			
			// p.280 VII.
			if(profile.HasMitte("+,0", "0,+", FactorsComparisonOptions.Hypertension_insensitive))
			{
				var note = InterpretationNotes.PhobischenBesessenheit;
				profile.AddInterpretationNote(note);
			}
			
			// p.280 VII.
			if(profile.HasMitte("0,+", "0,+", FactorsComparisonOptions.Hypertension_insensitive))
			{
				var note = InterpretationNotes.Geltungsdrang;
				profile.AddInterpretationNote(note);
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
			
			// p.300 V.1.
			if(profile.C.IsAny("0,-", "0,-!", "0,-!!"))
			{
				var note = InterpretationNotes.HypomanischeBenehmen;
				profile.AddInterpretationNote(note);
			}
			
			// p.283 VII Kontaktsperre
			if(profile.C.IsAny("-,-", "-,-!"))//-,-! p.286 I.3
			{
				var note = InterpretationNotes.Kontaktsperre;
				profile.AddInterpretationNote(note);
			}
				
			// p.291 IrrealenBlocksSyndrom
			if(profile.P.IsAny("-,-", "-!,-", "-,-!", "-!,-!", "0,-")//P0- p.301
			   && profile.Sch.IsAny("-,-", "-!,-", "-,-!", "-!,-!")
			   && profile.C.IsAny("-,-", "-!,-", "-,-!", "-!,-!")
			  )
			{
				var note = InterpretationNotes.IrrealenBlocksSyndrom;
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
			
			FurtherCompositeNotes(profile);
		}
		
		internal static void FurtherCompositeNotes(TestProfile profile)
		{
			if((profile.HasInterpretationNote(InterpretationNotes.Ichsperrung) 
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
			// p..312
			if(profile.HasInterpretationNote(InterpretationNotes.TotaleKatatonifIchsperrung)
			   && (profile.HasInterpretationNote(InterpretationNotes.Kain) 
			       || profile.e.IsEqualTo("-"))// hy (0,±,-) optional; hy ±,- p.286 VII.
			   && profile.s.IsAny("+!", "+!!", "+!!!") 
			  )
			{
				var note = InterpretationNotes.KatatonifSperrungsSynd;
				profile.AddInterpretationNote(note);
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
