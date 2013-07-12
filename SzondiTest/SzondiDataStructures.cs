using System;
using System.Collections.Generic;

namespace SzondiTest
{
	
	// pulsional factors / pulsional needs
	public enum Factors {h, s, e, hy, k, p, d, m, undefined};
	//
	public enum Vectors {Undefined, S, P, Sch, C};
	
	// Buch 2, pp.232-3
	public enum DimensionenUndFormenDerPsyche {
		VGP, // Vorergrundprofile, Vorderbühne, Vordergang
		// Hinterbühne des Hintergängers:
		EKP, // Form 1: das empirische (oder expetimentelle) Komplementprofil
		ThKP // Form 2: das theoretische Komplementprofil
	};
	
	public enum Triebklassdirection
	{
		[System.ComponentModel.Description("+")]
		plus,
		
		[System.ComponentModel.Description("-")]
		minus,
		
		[System.ComponentModel.Description("")]
		equal
	};
	
	public struct Triebklasse
	{
		public Vectors vector;
		public Factors factor;
		public Triebklassdirection direction;
		
		public Triebklasse(Vectors vector, Factors factor, Triebklassdirection direction)
		{
			this.vector = vector;
			this.factor = factor;
			this.direction = direction;
		}
		
		public Triebklasse(string vectorName, 
		                                   string factorName,
		                                   string directionDecription)
		{
			this.vector = (Vectors) Enum.Parse(typeof(Vectors), vectorName);
			this.factor = (Factors) Enum.Parse(typeof(Factors), factorName);
			
			if(directionDecription.Equals("+"))
			{
				this.direction = Triebklassdirection.plus;
			}
			else if(directionDecription.Equals("-"))
			{
				this.direction = Triebklassdirection.minus;
			}
			else
			{
				this.direction = Triebklassdirection.equal;
			}		
		}
		
		public override string ToString()
		{
			string factorDescr = string.Empty;
			if(factor != Factors.undefined)
			{
				factorDescr = factor.ToString();
			}
			
			return vector.ToString() + factorDescr
				+ GetDescription(direction);
		}
		
		public static bool operator ==(Triebklasse x, Triebklasse y) 
		{
		  return x.vector == y.vector 
		  	&& x.factor == y.factor 
		  	&& x.direction == y.direction;
		}
		
		public static bool operator !=(Triebklasse x, Triebklasse y) 
		{
			return !(x == y);
		}
		
		public override bool Equals(Object obj) 
		{
		  return obj is Triebklasse && this == (Triebklasse)obj;
		}
		
		public override int GetHashCode() 
		{
			// with bitwise operator (^)
			return vector.GetHashCode() ^ factor.GetHashCode() ^ direction.GetHashCode();
		}
		
		public static string GetDescription(object enumValue)
		{
			string desc = string.Empty;
			System.Reflection.FieldInfo fi 
				= enumValue.GetType().GetField(enumValue.ToString());
	        
	        if (null != fi)
	        {
	            object[] attrs 
	            	= fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute),
	            	                         true);
	            if (attrs != null && attrs.Length > 0)
	            {
	            	desc = ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
	            }
	        }
	
	        return desc;
	}
	}
	
	public enum Sex {Female, Male};
	
	public enum InterpretationNotes
	{
		#region Ich
		ParanoKammSynd, // -s -hy -p -m"
		ParanoKammSyndU, //-s -hy -p -m
		//Simple bilds
		FuguesAusreißen,//Sch±-(!) p.390 (p-! p.?)
		Dissimulation,//--!
		DestruktiveVerneinung,//Sch-!,0
		Alienation,//Sch 0,-(!!!) p.?
		Entfremdung,// Sch -± p.330
		ParanoTotalProjektion,//Sch 0,-(!) p.310
		Ichsperrung,//k- p0
		KatatonifIchsperrung,//k-! p0
		HebeKonbProjUndSperrung,//-!-(!)
		TotaleInflation,//0+!
		Introinflation,//++!
		//Projektiv
		ProjektivParanoMitte,//ProjektivMitte, same as ProjektivParanoideMitte
		ParanoSpaltungsSynd,
		//Katatoni
		TotaleKatatonifIchsperrung,//Sch-0, C--
		KatatonifSperrungsSynd,//p.283
		KatatonifMitte,
		IrrealenBlocksSyndrom,//e,hy,k,p,d,m -- -- --
		//Hebefr
		HebephreneSyndrom,//p.298 in der projektiven Hypochondrie
		//Inflativ
		InflativParanoMitte,
		InflativParanoSyndrom,
		GrößenwahnParano,
		#endregion
		
		#region Affekt
		Kain,//-+ (-0)
		Abel,//+-
		Gewissensangst,//+-(!)
		AffektPolarität,//"±0, ±-
		SensitiveBeziehungsangst,//Was würden die Menschen sagen? 0-, ±-
		Phobie,//+0
		TotalDesintegrAffekte,//p.274 II.7 TotalDesintegrAffektleben, P00 
		Lamentation,//p.274 II.8 P0± 
		Religionswahn,//p.390 P+±
		KainHideWechsel,//0--0
		ParoxKainSyndrom,
		KonvHyst,
		KonvHystSukzessionAffekt,
		KonvHystSukzessionIch,
		Affektstörungen,
		AffektLabilitätUndPolarität,
		HysteriformeMitte,
		PhobischenBesessenheitMitte,//p.280 
		BesessenheitGeltungsdrangMitte,//need for admiration, exhibition obsession
		PhobischeMitte,
		ReineKainMitte,
		// Affekt - Epi
		MörderE,// p.381 Affektmörder Raubmördersyndrom,// (psychop.?) p.398
		MörderE_mitVentil,
		KainAbelWechsel,//p.349 XI.
		#endregion
		
		#region Sexuelle
		SzondiHomo,
		Bisexualität,//p.390 S±±
		Sublimation,//p.394 S--
		Sexualstörungen,
		Triebzielinversion,
		Exhibitionismus,//p.375
		// Sexuelle perversions
		Asketismus,// S00
		Fetischismus,
		Masochismus,
		Sadomasochismus,
		Sadismus,
		SadistischeKain,
		Analsadismus,
		Analmasochismus,
		PolymorphPervers,
		TierischeBrutalität,//unmenschliche Brutalität p.391
		// Sexuelle - Lustprinzip
		Lustprinzip,//p.358 ist oft gleichzeitig auch das Syndrom der Polymorphen Perversion
		Lustprinzip_Kleptomanie,
		Lustprinzip_AcquisitionUrge,
		Lustprinzip_ConstRivalWPartner,
		Lustprinzip_IdealsNegation,
		Lustprinzip_Oralsadismus,
		PerverseLustprinzip,
		GrößenwahnLustprinzip, //
		// 
		FetischMitte,
		MasochistMitte,
		SadistMitte,
		NegativeMitte,
		ExhibitionistMitte,
		PsychopatischerMitte,//p.383, p.394
		SchwacheMitte,//p.383
		#endregion
					
		#region Schutz
		HebephreneMitte,
		Schuldangst,
		HypochondrischeMitte,//"das Syndrom der Hypochondrie" p.317
		PsychotischeHypochondrischeSynd,//p.325
		// Schutz - neurotischeMitte
		InhibitedMitte, //gehemmte
		ObsessiveCompulsiveMitte,// anankastische
		KlassischeZwangsneurose,
		Zwangsimpulse,
		Zwangsperson,//p.388
		#endregion
		
		#region Kontakt
		TrunksuchtMitte,
		MelancholischeMitte,
		ManischeMitte,
		Sucht,
		Kontaktstörungen, // -p +d -m
		SuchenNachVerfolger,
		DepressionHinweis,
		Akzeptationsdrang,//C0+
		Kontaktsperre,// C--
		HypomanischeBenehmen, //C0-
		InzestuösesAnhangen,//C-+, p.374
		#endregion
		
		Ödipussyndrom,
		
		// move among Affektmörder ?
		MordUndRapeMitte,
		MordMitte,
		LustmordMitte,
			
		//doubles
		ManiformeParanoide,
		
		//
		DepersonalisationMitte,
	};
	
	public enum Existenzformen
	{
		//A.Gefahr-Existenzformen
		//I. Sexuelle Existenzformen
		Inversion_SzondiHomo_Trans=10,
		Perversion_Sadismus_Masochismus=9,
		
		//II. Affektanfällige Existenzformen
		Tötende_Gesinnung_Epileptiforme=13,
		Hysteriforme=14,
		
		//III. Ich-Existenzformen
		Praepsychotische=1,
		ProjektivParanoide=2,
		InflativParanoide=3,
		Heboide=4,//Und Hebephrene
		Katatoniforme=5,
		
		//IV. Stimmungs-Kontakt-Existenzformen
		Depressive_Melancholische=6,
		Hypomanische_Manische=7,
		KontaktPsychopathische=8,
		
		//B. Schutz-Existenzen
		Hypochondrische_Organneurose=11,//also 15
		CompulsiveZwang=12,
		SozialisierendeAlltagsmensche=16,
		GeistigHumanisierendeMenschen=17
			
	};
	public enum FactorsComparisonOptions
	{
		Hypertension_insensitive, HypertensionOnOff, Hypertension_sensitive,
		HypertensionEqualORGreater, HypertensionEqualORLesser
	};
	
	public class FactorReaction
	{
		public ushort positivTendenz;
		public ushort negativTendenz;
		public bool isExperimentalComplementar;
		
		private FactorReaction thComplementar;
		private FactorReaction experimentalComplementar;
		
		public  FactorReaction(ushort positivTendenz, ushort negativTendenz)
		{
			this.positivTendenz=positivTendenz;
			this.negativTendenz=negativTendenz;
		}
		
		public FactorReaction(string description)
		{
			// standardize ±
			description = description.Replace("+/-", "±");
			
			positivTendenz = 0;
			negativTendenz = 0;
			switch(description)
			{
				case "0":
				case "Ø":
				{
					positivTendenz = 0;
					negativTendenz = 0;
				} break;
				case "+": positivTendenz = 2; break;
				case "-": negativTendenz = 2; break;
				case "±":
				{
					positivTendenz = 2;
					negativTendenz = 2;
				} break;
				case "+!": positivTendenz = 4; break;
				case "+!!": positivTendenz = 5; break;
				case "+!!!": positivTendenz = 6; break;
				case "-!": negativTendenz = 4; break;
				case "-!!": negativTendenz = 5; break;
				case "-!!!": negativTendenz = 6; break;
				case "!±" :
				{
					// convention: ! at the left refers to positivTendenz +
					positivTendenz = 4;
					negativTendenz = 2;
				} break;
				case "±!":
				{
					positivTendenz = 2;
					negativTendenz = 4;
				} break;
				default:
				{
					string errorMessage = "Invalid factor reaction description: " + description;
					throw new ArgumentException(errorMessage);
				}
			}
		}
		
		// by default case sensitive
		public bool Contains(string compareToDescription)
		{
			var compareTo = new FactorReaction(compareToDescription);
			return this.Contains(compareTo);
		}
		
		public bool Contains(string compareToDescription,
		                     FactorsComparisonOptions comparereOptions)
		{
			var compareTo = new FactorReaction(compareToDescription);
			return this.Contains(compareTo, comparereOptions);
		}
		
		// by default case sensitive
		public bool Contains(FactorReaction compareTo)
		{
			return this.Contains(compareTo,
			                     FactorsComparisonOptions.HypertensionEqualORGreater);
		}
		
		public bool Contains(FactorReaction compareTo,
		                     FactorsComparisonOptions comparereOptions)
		{
			if(this.IsEqualTo(compareTo, comparereOptions))
			{
				return true;
			}
			
			bool containsBareReaction = false;
			if(this.IsAmbivalentReaction && !compareTo.IsNullReaction)
			{
				containsBareReaction = true;
			}
			
			bool hypertensionGreaterOrEqual = false;
			if(comparereOptions == FactorsComparisonOptions.Hypertension_insensitive)
			{
				hypertensionGreaterOrEqual = true;
			}
			else
			{	// TODO verify is correct for ambivalence: !± contains - ?
				hypertensionGreaterOrEqual
					= (this.positivTendenz >= compareTo.positivTendenz)
					&& (this.negativTendenz >= compareTo.negativTendenz);
			}
			
			return containsBareReaction && hypertensionGreaterOrEqual;
		}
		
		// Hypertension_sensitive by default
		public bool IsEqualTo(string compareToDescription)
		{
			return this.IsEqualTo(compareToDescription, 
			                      FactorsComparisonOptions.Hypertension_sensitive);
		}
		
		public bool IsAny(string compare1, string compare2)
		{
			return IsEqualTo(compare1) || IsEqualTo(compare2);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3)
		{
			return IsEqualTo(compare1) || IsAny(compare2, compare3);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3, 
		                  string compare4)
		{
			return IsEqualTo(compare1) || IsAny(compare2, compare3, compare4);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3, 
		                  string compare4, string compare5)
		{
			return IsEqualTo(compare1) 
				|| IsAny(compare2, compare3, compare4, compare5);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3, 
		                  string compare4, string compare5, string compare6)
		{
			return IsEqualTo(compare1) 
				|| IsAny(compare2, compare3, compare4, compare5, compare6);
		}
		
		public bool IsEqualTo(string compareToDescription, 
		                      FactorsComparisonOptions compareOptions)
		{
			var compareTo = new FactorReaction(compareToDescription);
			return this.IsEqualTo(compareTo, compareOptions);
		}
		
		public bool IsEqualTo(FactorReaction compareTo,
		                      FactorsComparisonOptions compareOptions)
		{
			if(this.IsNullReaction && compareTo.IsNullReaction)
			{
				// reagrdless if its forced null reaction
				return true;
			}
			
			bool bareReactionsMatch = false;
			{
				if(this.IsPositiveUnitendenz && compareTo.IsPositiveUnitendenz)
				{
					bareReactionsMatch = true;
				}
				
				if(this.IsNegativeUnitendenz && compareTo.IsNegativeUnitendenz)
				{
					bareReactionsMatch = true;
				}
				
				if(this.IsAmbivalentReaction && compareTo.IsAmbivalentReaction)
				{
					bareReactionsMatch = true;
				}
			}
			
			bool hypertensionsMatch = false;
			{
				if(this.NoHypertension && compareTo.NoHypertension)
				{
					hypertensionsMatch = true;
				}
				else if(this.IsAmbivalentReaction && compareTo.IsAmbivalentReaction)
				{
					// considering equal !±, ±! and ±
					hypertensionsMatch = true;
				}
				else
				{
					if(compareOptions == FactorsComparisonOptions.Hypertension_insensitive)
					{
						hypertensionsMatch = true;
					}
					if(compareOptions == FactorsComparisonOptions.HypertensionOnOff)
					{
						hypertensionsMatch = this.HasHypertension == compareTo.HasHypertension;
					}
					if(((this.positivTendenz >= 5) && (compareTo.positivTendenz >= 5))
					   || ((this.negativTendenz >= 5) && (compareTo.negativTendenz >= 5)))
					{
						// considering equal !! and !!!
						hypertensionsMatch = true;
					}
					if(compareOptions == FactorsComparisonOptions.HypertensionEqualORGreater)
					{
						hypertensionsMatch = (this.positivTendenz >= compareTo.positivTendenz)
							   && (this.negativTendenz >= compareTo.negativTendenz);
					}
					if(compareOptions == FactorsComparisonOptions.Hypertension_sensitive)
					{
						if(this.positivTendenz >3)
						{
							hypertensionsMatch = (this.positivTendenz == compareTo.positivTendenz);
						}
						if(this.negativTendenz >3)
						{
							hypertensionsMatch = (this.negativTendenz == compareTo.negativTendenz);
						}
					}
				}
			}
			//TODO complete for other hypertension sensitive scenarios
			
			return bareReactionsMatch && hypertensionsMatch;
		}
		
		public bool IsPositiveUnitendenz
		{
			get
			{
				return (this.positivTendenz >= 2
				        && this.negativTendenz <= 1);
			}
		}
		
		public bool IsNegativeUnitendenz
		{
			get
			{
				return (this.negativTendenz >= 2
				        && this.positivTendenz <= 1);
			}
		}
		
		public bool IsNullReaction
		{
			get
			{
				return (this.negativTendenz <= 1
				        && this.positivTendenz <= 1);
			}
		}
		
		public bool IsForcedNullReaction
		{
			get
			{
				if(this.isExperimentalComplementar 
				   && (this.thComplementar.negativTendenz 
				        + this.thComplementar.positivTendenz
					   >= 5))
				{
					return true;
				}
				
				return false;
			}
		}
		
		public bool IsAmbivalentReaction
		{
			get
			{
				return (this.negativTendenz >= 2
				        && this.positivTendenz >= 2);
			}
		}
		
		public bool HasHypertension
		{
			get
			{
				return (this.negativTendenz >= 4
				        || this.positivTendenz >= 4);
			}
		}
		
		public bool NoHypertension
		{
			get
			{
				return !this.HasHypertension;
			}
		}
		
		public FactorReaction ExperimentalComplementar
		{
			get
			{
				return this.experimentalComplementar;
			}
			set
			{
				int totalChoices = this.positivTendenz 
					+ this.negativTendenz 
					+ value.positivTendenz 
					+ value.negativTendenz;
				
				if(totalChoices != 6)
				{
					//when initialized by numbers must be exactly 6
					//but when init by description if may not
					//"+", "-" may either be 2 or 3
					//"0" may either be 0 or 1
					//"±" may either be 4 or 5
					
					if(totalChoices < 2 || totalChoices > 6)
					{
						string message 
							= "The sum of this FactorReaction and its complementar is out of range";
						throw new ArgumentException(message);
					}					
				}
				this.experimentalComplementar = value;
				value.experimentalComplementar = this;
			}
		}
		
		public FactorReaction ThComplementar
		{
			get
			{
				if(this.thComplementar == null)
				{
					if(this.IsNullReaction)
					{
						this.thComplementar = new FactorReaction("±");
					}
					if(this.IsAmbivalentReaction)
					{
						this.thComplementar = new FactorReaction("0");
					}
					if(this.IsPositiveUnitendenz)
					{
						this.thComplementar = new FactorReaction(0, this.positivTendenz);
					}
					if(this.IsNegativeUnitendenz)
					{
						this.thComplementar = new FactorReaction(this.negativTendenz, 0);
					}
					
					this.thComplementar.thComplementar = this;
				}
				
				return this.thComplementar;
			}
		}
		
		public override string ToString()
		{
			// single choiceReaction
			// 0, +, -, +!(!!), -!(!!), !±, ±!
			string choiceReaction = string.Empty;
			
			if(this.positivTendenz <= 1 && this.negativTendenz <= 1)
			{
				// empty / null reaction
				if(this.IsForcedNullReaction)
				{
					choiceReaction = "Ø";
				}
				else
				{
					choiceReaction = "0";
				}
				
			}
			else if(this.positivTendenz >=2 && this.negativTendenz >= 2)
			{
				// ambivalent reaction
				if(this.positivTendenz > 3)
				{
					// hipertension +
					choiceReaction += "!";
				}
				
				choiceReaction += "±";
				
				if(this.negativTendenz > 3)
				{
					// hipertension -
					choiceReaction += "!";
				}
			}
			else
			{
				if(this.positivTendenz >=2)
				{
					choiceReaction += "+" + HypertensionToString(this.positivTendenz);
				}
				else if (this.negativTendenz >=2)
				{
					choiceReaction += "-" + HypertensionToString(this.negativTendenz);
				}
			}
			
			return choiceReaction;
		}
		
		private static string HypertensionToString(ushort directionValue)
		{
			string hypertension = string.Empty;
			switch(directionValue)
			{
					case 4: hypertension = "!"; break;
					case 5: hypertension = "!!"; break;
					case 6: hypertension = "!!!"; break;
			}
			
			return hypertension;
		}	
	}
	
	public class VectorReaction
	{
		private Vectors vectorName;
		public Factors firstFactorName;
		public Factors secondFactorName;
		
		public FactorReaction firstFactorReaction;
		public FactorReaction secondFactorReaction;
		
		private VectorReaction complementar;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="description">Expected as (0,-)</param>
		/// <param name="vectorName"></param>
		public VectorReaction(string description, Vectors vectorName)
			:this(vectorName)
		{
			char[] separators = {'(', ')', ',', ' '};
			string[] factors = description.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			this.firstFactorReaction = new FactorReaction(factors[0]);
			this.secondFactorReaction = new FactorReaction( factors[1]);
		}
		
		public VectorReaction(FactorReaction firstFactorReaction,
		                      FactorReaction secondFactorReaction,
		                      Vectors vectorName)
			:this(vectorName)
		{
			this.firstFactorReaction = firstFactorReaction;
			this.secondFactorReaction = secondFactorReaction;
		}
		
		public VectorReaction(string first_factor, string second_factor,
		                      Vectors vectorName)
			:this(vectorName)
		{
			this.firstFactorReaction = new FactorReaction(first_factor);
			this.secondFactorReaction = new FactorReaction(second_factor);
		}
		
		public VectorReaction(ushort firstFactorPositivTendenz,
		                      ushort firstFactorNegativTendenz,
		                      ushort secondFactorPositivTendenz,
		                      ushort secondFactorNegativTendenz,
		                      Vectors vectorName)
			:this(vectorName)
		{
			this.firstFactorReaction
				= new FactorReaction(firstFactorPositivTendenz, firstFactorNegativTendenz);
			this.secondFactorReaction
				= new FactorReaction(secondFactorPositivTendenz, secondFactorNegativTendenz);
			
		}
		
		private VectorReaction(Vectors vectorName)
		{
			this.vectorName = vectorName;
			switch(vectorName)
			{
				case Vectors.S:
					{
						this.firstFactorName = Factors.h;
						this.secondFactorName = Factors.s;
					} break;
				case Vectors.P:
					{
						this.firstFactorName = Factors.e;
						this.secondFactorName = Factors.hy;
					} break;
				case Vectors.Sch:
					{
						this.firstFactorName = Factors.k;
						this.secondFactorName = Factors.p;
					} break;
				case Vectors.C:
					{
						this.firstFactorName = Factors.d;
						this.secondFactorName = Factors.m;
					} break;
			}
		}
		
		public bool HasFactorReaction(string compareMe, 
		                              Factors factorName,
		                              FactorsComparisonOptions comparereOptions)
		{
			if(this.firstFactorName == factorName)
			{
				return this.firstFactorReaction.IsEqualTo(compareMe, comparereOptions);
			}
			else if(this.secondFactorName == factorName)
			{
				return this.secondFactorReaction.IsEqualTo(compareMe, comparereOptions);
			}
			else
			{
				throw new ArgumentException(factorName + " is not a factor of " 
				                            + this.Name);
			}
		}
		
		public bool HasAnyFactorReaction(string compare1, string compare2, 
		                              Factors factorName)
		{
			return this.HasFactorReaction(compare1, factorName) 
				|| this.HasFactorReaction(compare2, factorName);
		}
		
		public bool HasAnyFactorReaction(string compare1, string compare2, 
		                              string compare3, Factors factorName)
		{
			return this.HasAnyFactorReaction(compare1, compare2, factorName) 
				|| this.HasFactorReaction(compare3, factorName);
		}
		
		public bool HasAnyFactorReaction(string compare1, string compare2, 
		                              string compare3, string compare4,
		                              Factors factorName)
		{
			return this.HasAnyFactorReaction(compare1, compare2, factorName) 
				|| this.HasAnyFactorReaction(compare3, compare4, factorName);
		}
		
		public bool HasAnyFactorReaction(string compare1, string compare2, 
		                              string compare3, string compare4,
		                              string compare5, Factors factorName)
		{
			return this.HasAnyFactorReaction(compare1, compare2, factorName) 
				|| this.HasAnyFactorReaction(compare3, compare4, compare5, factorName);
		}
		
		public bool HasAnyFactorReaction(string compare1, string compare2, 
		                              string compare3, string compare4,
		                              string compare5, string compare6, 
		                              Factors factorName)
		{
			return this.HasAnyFactorReaction(compare1, compare2, compare3, 
			                                 compare4, compare5, factorName)
				|| this.HasFactorReaction(compare6, factorName);
		}
			
		// Hypertension_sensitive by default
		public bool HasFactorReaction(string compareMe, Factors factorName)
		{
			if(compareMe.Equals("±"))
			{
				// assumption, for ± is ok to consider equivalent also !± and ±!
				return HasFactorReaction(compareMe, factorName, FactorsComparisonOptions.Hypertension_insensitive);
			}
			else
			{
				return HasFactorReaction(compareMe, factorName, FactorsComparisonOptions.Hypertension_sensitive);
			}
		}
		
		/// <summary>
		/// Compare only one of the two factor reactions
		/// </summary>
		/// <param name="compareMe"></param>
		/// <param name="factorName"></param>
		/// <returns></returns>
		public bool ContainsFactorReaction(
			string compareMe, Factors factorName, FactorsComparisonOptions compareOptions)
		{
			if(this.firstFactorName == factorName)
			{
				return this.firstFactorReaction.Contains(compareMe, compareOptions);
			}
			else if(this.secondFactorName == factorName)
			{
				return this.secondFactorReaction.Contains(compareMe, compareOptions);
			}
			else
			{
				return false;
			}
		}
		
		public bool ContainsFactorReaction(string compareMe, Factors factorName)
		{
			return ContainsFactorReaction(compareMe, factorName, 
			                              FactorsComparisonOptions.HypertensionEqualORGreater);
		}
		
		/// <summary>
		/// Compares with another vector (both factor reactions)
		/// </summary>
		/// <param name="compareMe"></param>
		/// <returns></returns>
		public bool Contains(string compareMe)
		{
			VectorReaction vectorToCompate
				= new VectorReaction(compareMe, Vectors.Undefined);
			
			bool firstFactorContained = this.firstFactorReaction.Contains(vectorToCompate.firstFactorReaction);
			bool secondFactorContained= this.secondFactorReaction.Contains(vectorToCompate.secondFactorReaction);
			return firstFactorContained && secondFactorContained;
		}
		
		public bool IsAny(string[] compareReactions)
		{
			foreach(var reaction in compareReactions)
			{
				if(this.EqualsTo(reaction))
				{
					return true;
				}
			}
			
			return false;
		}
		
		public bool IsAny(string compare1, string compare2)
		{
			return this.EqualsTo(compare1) || this.EqualsTo(compare2);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3)
		{
			return this.EqualsTo(compare1) 
				|| this.IsAny(compare2, compare3);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3,
		                 string compare4)
		{
			return this.EqualsTo(compare1) 
				|| this.IsAny(compare2, compare3, compare4);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3,
		                 string compare4, string compare5)
		{
			return this.EqualsTo(compare1) 
				|| this.IsAny(compare2, compare3, compare4, compare5);
		}
		
		public bool IsAny(string compare1, string compare2, string compare3,
		                 string compare4, string compare5, string compare6)
		{
			return this.EqualsTo(compare1) 
				|| this.IsAny(compare2, compare3, compare4, compare5, compare6);
		}
		
		public bool EqualsTo (string compareMe)
		{
			return this.EqualsTo(compareMe,
			                     FactorsComparisonOptions.Hypertension_sensitive);
		}
		
		public bool EqualsTo (string compareMe,
		                      FactorsComparisonOptions compareOptions)
		{
			VectorReaction vectorToCompate
				= new VectorReaction(compareMe, Vectors.Undefined);
			return this.EqualsTo(vectorToCompate, compareOptions);
		}
		
		// regardless of hypertension
		public bool EqualsTo(VectorReaction vectorToCompate)
		{
			return this.EqualsTo(vectorToCompate,
			                     FactorsComparisonOptions.Hypertension_insensitive);
		}
		
		//checks hypertension
		public bool EqualsTo(VectorReaction vectorToCompate,
		                     FactorsComparisonOptions compareOptions)
		{
			bool firstFactorMatches = this.firstFactorReaction.IsEqualTo(vectorToCompate.firstFactorReaction,
			                                                             compareOptions);
			bool secondFactorMatches = this.secondFactorReaction.IsEqualTo(vectorToCompate.secondFactorReaction,
			                                                               compareOptions);
			return firstFactorMatches && secondFactorMatches;
		}
		
		public string Name
		{
			get
			{
				if(this.vectorName == Vectors.Undefined)
				{	return string.Empty;
				}
				return vectorName.ToString();
			}
		}
		
		public string FirstFactorName
		{
			get
			{
				if(this.vectorName == Vectors.Undefined)
				{	return string.Empty;
				}
				return this.firstFactorName.ToString();
			}
		}
		
		public static Factors GetFirstFactorName(Vectors vectorName)
		{
			switch(vectorName)
			{
				case Vectors.S: return Factors.h;
				case Vectors.P: return  Factors.e;
				case Vectors.Sch: return  Factors.k;
				case Vectors.C: return  Factors.d;
				default: throw new ArgumentException("No first factor in undefined vector");
			};
		}
		
		public static Factors GetSecondFactorName(Vectors vectorName)
		{
			switch(vectorName)
			{
				case Vectors.S: return Factors.s;
				case Vectors.P: return  Factors.hy;
				case Vectors.Sch: return  Factors.p;
				case Vectors.C: return  Factors.m;
				default: throw new ArgumentException("No first factor in undefined vector");
			}
		}
		/*
		public static Factors GetSecondFactorName(Vectors vectorName)
		{
			switch(vectorName)
			{
				case Vectors.S: return Factors.s.ToString();
				case Vectors.P: return Factors.hy.ToString();
				case Vectors.Sch:return Factors.p.ToString();
				case Vectors.C: return Factors.m.ToString();
				default: return null;
			}
		}
		
		public static Factors GetFirstFactorName(Vectors vectorName)
		{
			switch(vectorName)
			{
				case Vectors.S: return Factors.h.ToString();
				case Vectors.P: return Factors.e.ToString();
				case Vectors.Sch:return Factors.k.ToString();
				case Vectors.C: return Factors.d.ToString();
				default: return null;
			}
		}*/
		
		public string SecondFactorName
		{
			get
			{
				if(this.vectorName == Vectors.Undefined)
				{	return string.Empty;
				}
				return this.secondFactorName.ToString();
			}
		}
		
		private VectorReaction experimentalComplementar;
		public VectorReaction ExperimentalComplementar
		{
			get
			{
				return this.experimentalComplementar;
			}
			set
			{
				this.experimentalComplementar = value;
				value.experimentalComplementar = this;
				this.secondFactorReaction.ExperimentalComplementar = value.secondFactorReaction;
				this.firstFactorReaction.ExperimentalComplementar = value.firstFactorReaction;
			}
		}
		
		public VectorReaction ThComplementar
		{
			get
			{
				if(this.complementar == null)
				{
					this.complementar
						= new VectorReaction(this.firstFactorReaction.ThComplementar,
						                     this.secondFactorReaction.ThComplementar,
						                     this.vectorName);
					this.complementar.complementar = this;
				}
				
				return this.complementar;
			}
		}
		
		public bool NoHypertension
		{
			get
			{
				return this.firstFactorReaction.NoHypertension 
					&& this.secondFactorReaction.NoHypertension;
			}
		}
		
		
		public override string ToString()
		{
			return this.ToString(false);
		}
		
		public string ToString(bool verbose)
		{
			string firstName = string.Empty;
			string secondName = string.Empty;
			if(verbose)
			{
				// Verbose: Sch(k-!, p0)
				// Not verbose: Sch(-!, 0)
				firstName = this.FirstFactorName;
				secondName = this.SecondFactorName;
			}
			
			string myDescription = this.Name + "("
				+ firstName + this.firstFactorReaction + ", "
				+ secondName + this.secondFactorReaction + ")";
			
			return myDescription;
		}
		
	}
	
	public class TestSeries
	{
		public Sex testTakerSex;
		private string name;
			
		// Calculated with Linnaeus method
		internal List<KeyValuePair<int , Triebklasse>> latezProportionen;
		
		public List<TestProfile> vorergrundprofile;
		public List<TestProfile> empirischekomplementprofile;
		private List<TestProfile> hintergrundprofile;
		
		public List<TestProfile> Hintergrundprofile
		{
			get
			{
				if(hintergrundprofile == null)
				{
					hintergrundprofile = new List<TestProfile>();
					foreach(var profile in vorergrundprofile)
					{
						hintergrundprofile.Add(profile.TheoricComplementar);
					}
				}
				return hintergrundprofile;
			}
		}
		
		public string Name
		{
			get
			{	if(this.name != null)
				{
					return this.name;
				}
				else
				{	return "[profiles]";
				}
			}
		}
		
		#region constructors
		public TestSeries(Sex sex, List<TestProfile> vorergrundprofile)
		{
			this.testTakerSex = sex;
			this.vorergrundprofile = new List<TestProfile>();
			
			foreach(var profile in vorergrundprofile)
			{
				if(profile != null)
				{
					this.vorergrundprofile.Add(profile);
					profile.partOf = this;
				}
			}
		}
		
		public TestSeries(Sex sex,
		                  List<TestProfile> vorergrundprofile,
		                  List<TestProfile> empirischekomplementprofile)
			:this(sex, vorergrundprofile)
		{
			if(empirischekomplementprofile == null
			   || empirischekomplementprofile.Count == 0)
			{
				return;
			}
			
			if(vorergrundprofile.Count
			   != empirischekomplementprofile.Count)
			{
				throw new ArgumentException("Empirischekomplementprofile.Count differs.");
			}
			
			this.empirischekomplementprofile = new List<TestProfile>();
			foreach(var profile in empirischekomplementprofile)
			{
				if(profile != null)
				{	
					this.empirischekomplementprofile.Add(profile);
					profile.partOf = this;
				}
			}
			
			for(int i=0; i<vorergrundprofile.Count; i++)
			{
				if(vorergrundprofile[i] != null)
				{
					vorergrundprofile[i].ExperimentalComplementar
						= empirischekomplementprofile[i];
				}
					
			}
		}
		
		public TestSeries(Sex sex,
		                  List<TestProfile> vorergrundprofile,
		                  string name)
			:this(sex, vorergrundprofile, null, name)
		{
			
		}
		
		public TestSeries(Sex sex,
		                  List<TestProfile> vorergrundprofile,
		                  List<TestProfile> empirischekomplementprofile,
		                  string name)
			:this(sex, vorergrundprofile, empirischekomplementprofile)
		{
			this.name = name;
		}
		#endregion
		
		public void Interpret()
		{
			this.ComputeLinnaeus();
			
			InterpretDimensionProfiles(DimensionenUndFormenDerPsyche.VGP);
			InterpretDimensionProfiles(DimensionenUndFormenDerPsyche.ThKP);
			
			if(this.empirischekomplementprofile != null)
			{
				InterpretDimensionProfiles(DimensionenUndFormenDerPsyche.EKP);
			}
		}
		
		public void InterpretDimensionProfiles(DimensionenUndFormenDerPsyche dimension)
		{
			var profiles = this.GetProfilesByDimension(dimension);
			foreach(var profile in profiles)
			{
				if(profile != null)
				{
					Syndromatic.BestimmungDerExistenzformen(profile);
				}
			}
		}
		
		public System.Text.StringBuilder GetInterpretationReport()
		{
			var interpretationReport = new System.Text.StringBuilder();
			interpretationReport.AppendLine("\nInterpretation Report:");
			
			// existenzformen
			// correspondences between VGP,EKP, ThKP
			// ...
			
			if(this.latezProportionen == null)
			{
				this.ComputeLinnaeus();
			}
			AppendLinnaeusReport(interpretationReport);
				
			// Bio info
			interpretationReport.AppendLine("\nBio info: sex: " + this.testTakerSex);
			
			return interpretationReport;
		}

		#region Suksession und Mobilität 
		
		// TODO add strict order? reaction2 immediately after reaction1
		
		public bool Sukzession(string reaction1, string reaction2,
		                       Vectors vector, 
		                       DimensionenUndFormenDerPsyche dimension)
		{
			return this.HasVectorReaction(reaction1, vector, dimension) 
				&& this.HasVectorReaction(reaction2, vector, dimension);
		}
		
		public bool Sukzession(string reaction1, string reaction2, 
		                       string reaction3, Vectors vector,
		                       DimensionenUndFormenDerPsyche dimension)
		{
			string[] reactions = {reaction1,reaction2,reaction3,};
			return Sukzession(reactions, vector, dimension);
		}
		
		public bool Sukzession(string[] reactions, Vectors vector,
		                      DimensionenUndFormenDerPsyche dimension)
		{
			ushort countMin = 2;
			return Sukzession(reactions, vector, countMin, dimension);
		}
		
		public bool Sukzession(string[] reactions, 
		                       Vectors vector, 
		                       ushort countMin,
		                       DimensionenUndFormenDerPsyche dimension)
		{
			ushort matched = 0;
			foreach(var reaction in reactions)
			{
				if(this.HasVectorReaction(reaction, vector, dimension))
				{
					matched++;
					if(matched >= countMin)
					{
						return true;
					}
				}
			}
			
			return false;
		}
		public bool IsAlways(string reaction, Factors faktor,
		                           DimensionenUndFormenDerPsyche dimension)
		{		
			List<TestProfile> profiles = GetProfilesByDimension(dimension);
			
			foreach(var profile in profiles)
			{
				if(profile != null 
				   && 
				   ! (profile.GetFactorByName(faktor).IsEqualTo(reaction)))
				{
					return false;
				}
			}
			
			return true;
		}
		
		public bool IsAlways(string reaction, Vectors vectorName,
		                           DimensionenUndFormenDerPsyche dimension)
		{
			List<TestProfile> profiles = GetProfilesByDimension(dimension);
			
			foreach(var profile in profiles)
			{
				if(profile != null 
				   && 
				   ! (profile.GetVectorByName(vectorName).EqualsTo(reaction)))
				{
					return false;
				}
			}
			
			return true;
		}
		
		public bool GroßeMobilität(string reaction1, string reaction2,
		                           string reaction3, string reaction4, 
		                           Factors factor,
		                           DimensionenUndFormenDerPsyche dimension)
		{
			string[] reactions = {reaction1,reaction2,reaction3,reaction4,};
			return GroßeMobilität(reactions, factor, dimension);
		}
		
		public bool GroßeMobilität(string[] reactions, 
		                           Factors factor,
		                           DimensionenUndFormenDerPsyche dimension)
		{
			const ushort threshold = 3;
			ushort matches = 0;
			
			foreach(var reaction in reactions)
			{
				if(this.HasFactorReaction(reaction, factor, dimension))
				{
					matches++;
					if(matches >= threshold)
					{
						return true;
					}
				}			
			}
			
			return false;
		}
				
		public bool HasFactorReaction(string reaction, Factors faktor, 
		                             DimensionenUndFormenDerPsyche dimension)
		{
			List<TestProfile> profiles = GetProfilesByDimension(dimension);
			
			foreach(var profile in profiles)
			{
				if(profile != null 
				   && profile.GetFactorByName(faktor).IsEqualTo(reaction))
				{
					return true;
				}
			}
			
			return false;
		}
									
		public bool HasVectorReaction(string reaction, Vectors vector, 
		                             DimensionenUndFormenDerPsyche dimension)
		{
			List<TestProfile> profiles = GetProfilesByDimension(dimension);
			
			foreach(var profile in profiles)
			{
				if(profile != null 
				   && profile.GetVectorByName(vector).EqualsTo(reaction))
				{
					return true;
				}
			}
			
			return false;
		}
		#endregion
		
		private List<TestProfile> GetProfilesByDimension(DimensionenUndFormenDerPsyche dimension)
		{
			List<TestProfile> profiles = null;
			switch(dimension)
			{
				case DimensionenUndFormenDerPsyche.VGP: 
				{
					profiles = this.vorergrundprofile;
				} break;
				case DimensionenUndFormenDerPsyche.EKP: 
				{
					profiles = this.empirischekomplementprofile;
				} break;
				case DimensionenUndFormenDerPsyche.ThKP: 
				{
					profiles = this.Hintergrundprofile;
				} break;
			}
			
			return profiles;
		}
		
		#region Linnaeus
		internal void ComputeLinnaeus()
		{
			// count latencies for each vector
			// count Triebklasse
			// count root and syntomatic factors
			
			//Latenzproportionen
			
			{
				int TspD_VektorS; //Tendenzsoannungsdifferenz
				int TspD_VektorP;
				int TspD_VektorSch;
				int TspD_VektorC;
				
				Triebklasse subClassVektorS;
				Triebklasse subClassVektorP;
				Triebklasse subClassVektorSch;
				Triebklasse subClassVektorC;
				
				CountSubclass(this.vorergrundprofile, Vectors.S, out TspD_VektorS, out subClassVektorS);
				CountSubclass(this.vorergrundprofile, Vectors.P, out TspD_VektorP, out subClassVektorP);
				CountSubclass(this.vorergrundprofile, Vectors.Sch, out TspD_VektorSch, out subClassVektorSch);
				CountSubclass(this.vorergrundprofile, Vectors.C, out TspD_VektorC, out subClassVektorC);
				
				this.latezProportionen 
					= new List<KeyValuePair<int , Triebklasse>>();
				latezProportionen.Add(new KeyValuePair<int , Triebklasse>(TspD_VektorS, subClassVektorS));
				latezProportionen.Add(new KeyValuePair<int , Triebklasse>(TspD_VektorP, subClassVektorP));
				latezProportionen.Add(new KeyValuePair<int , Triebklasse>(TspD_VektorSch, subClassVektorSch));
				latezProportionen.Add(new KeyValuePair<int , Triebklasse>(TspD_VektorC, subClassVektorC));
				latezProportionen.Sort(CompareLatenzproportionen);
				latezProportionen.Reverse();		
			}
		}
		
		private void AppendLinnaeusReport(System.Text.StringBuilder interpretationReport)
		{
			foreach(var item in latezProportionen)
			{
				interpretationReport.Append(item.Value + "/" + item.Key + " ");
			}
		}
			
		private static int CompareLatenzproportionen(KeyValuePair<int, Triebklasse> a, 
		                                             KeyValuePair<int, Triebklasse> b)
	    {
			return a.Key.CompareTo(b.Key);
	    }
		
		private void CountSubclass(List<TestProfile> profiles, Vectors vectorName,
			out int TspD, out Triebklasse subClass)
		{
			Factors subClassFactor;
			Triebklassdirection direction;
			
			// Faktorielle tendenzspannungsgrad
			ushort firstFactorTspG = 0;
			ushort secondFactorTspG = 0;
			
			ushort firstFactorPositiveCount = 0;
			ushort firstFactorNegativeCount = 0;
			ushort secondFactorPositiveCount = 0;
			ushort secondFactorNegativeCount = 0;
			
			foreach(var profile in this.vorergrundprofile)
			{	
				AddFactorSintomaticAndRootReactions(
					ref firstFactorTspG, 
					ref firstFactorPositiveCount, 
					ref firstFactorNegativeCount,
					profile.GetVectorByName(vectorName).firstFactorReaction);
				
				AddFactorSintomaticAndRootReactions(
					ref secondFactorTspG, 
					ref secondFactorPositiveCount, 
					ref secondFactorNegativeCount,
					profile.GetVectorByName(vectorName).secondFactorReaction);
			}
			// TspD : Tendenzsoannungsdifferenz
			TspD = Math.Abs(firstFactorTspG - secondFactorTspG);
			
			if(firstFactorTspG > secondFactorTspG)
			{
				// second factor determines subclass
				subClassFactor = VectorReaction.GetSecondFactorName(vectorName);
				
				direction = CountSubClassDirection(
					secondFactorPositiveCount,
					secondFactorNegativeCount);
			}
			else if (firstFactorTspG < secondFactorTspG)
			{
				subClassFactor = VectorReaction.GetFirstFactorName(vectorName);
				direction = CountSubClassDirection(
					firstFactorPositiveCount,
					firstFactorNegativeCount);
			}
			else
			{
				subClassFactor = Factors.undefined;
				direction = Triebklassdirection.equal;
			}
			
			subClass = new Triebklasse(vectorName, subClassFactor, direction);
		}
		
		private Triebklassdirection CountSubClassDirection(
			ushort factorPositiveCount,
			ushort factorNegativeCount)
		{
			Triebklassdirection subClass;
			
			if(factorPositiveCount > factorNegativeCount)
			{
				subClass = Triebklassdirection.plus;
			}
			else if(factorPositiveCount < factorNegativeCount)
			{
				subClass = Triebklassdirection.minus;
			}
			else
			{
				// factorPositiveCount == factorNegativeCount
				subClass = Triebklassdirection.equal;
			}
			
			return subClass;
		}
		
		private void AddFactorSintomaticAndRootReactions(
			ref ushort factorTspG, 
			ref ushort factorPositiveCount, 
			ref ushort factorNegativeCount, 
			FactorReaction factorReaction)
		{
			if (factorReaction.IsAmbivalentReaction 
			    || factorReaction.IsNullReaction) 
			{
				factorTspG++;
			} 
			else if (factorReaction.IsPositiveUnitendenz) 
			{
				factorPositiveCount++;
			} 
			else if (factorReaction.IsNegativeUnitendenz) 
			{
				factorNegativeCount++;
			}
		}
		
		internal Triebklasse GetTriebklasse(Vectors vectorName)
		{
			foreach(var item in this.latezProportionen)
			{
				if(item.Value.vector == vectorName)
				{
					return item.Value;
				}
			}
			
			throw new ArgumentException("Can't get Triebklasse for " + vectorName);
		}
				
		internal int GetLatezProportion(Triebklasse triebklasse)
		{
			foreach(var item in this.latezProportionen)
			{
				if(item.Value == triebklasse)
				{
					return item.Key;
				}
			}
			
			throw new ArgumentException("There is no Triebklasse as " + triebklasse);
		}
		
		internal bool HasGefahrTriebklasse(string vektor, string faktor, string direction)
		{
			return this.HasGefahrTriebklasse(new Triebklasse(vektor, faktor, direction));
		}
		
		internal bool HasGefahrTriebklasse(Triebklasse triebklasse)
		{
			
			if(! (triebklasse == this.GetTriebklasse(triebklasse.vector)))
			{
				// verify that triebklasse is at least present
				return false;
			}
			
			//verify that triebklasse is dangerous 
			if(this.GetLatezProportion(triebklasse) >= 5)
			{
				// TODO implement HashauptTriebklasse w/ more refined checks, bi-, tri-, quadriequal
				return true;
			}
			
			return false;
		}
		
		#endregion
	}
	
	public class TestProfile
	{
		public DimensionenUndFormenDerPsyche dimension;
		private List<Existenzformen> existenzFormen;
		private List<InterpretationNotes> interpretationNotes;
		
		public VectorReaction S;
		public VectorReaction P;
		public VectorReaction Sch;
		public VectorReaction C;
		
		// TODO disintguish VGP, EKP, ThKP
		private TestProfile theoricComplementar;
		private TestProfile experimentalComplementar;
		public TestSeries partOf;
		
		#region Constructors
		public TestProfile(string description)
		{
			string[] vectorReactionsDescriptions
				= PreprocessProfileDescription(description);
			
			this.ConstructorsHelper(new VectorReaction(vectorReactionsDescriptions[0], Vectors.S),
			                        new VectorReaction(vectorReactionsDescriptions[1], Vectors.P),
			                        new VectorReaction(vectorReactionsDescriptions[2], Vectors.Sch),
			                        new VectorReaction(vectorReactionsDescriptions[3], Vectors.C));
		}
		
		private void ConstructorsHelper(
			VectorReaction S, VectorReaction P, VectorReaction Sch, VectorReaction C)
		{
			if(!S.Name.Equals(Vectors.S.ToString())
			   || !P.Name.Equals(Vectors.P.ToString())
			   || !Sch.Name.Equals(Vectors.Sch.ToString()) 
			   || !C.Name.Equals(Vectors.C.ToString()))
			{
				throw new ArgumentException("Wrong vector name");
			}
			
			this.S = S;
			this.P = P;
			this.Sch = Sch;
			this.C = C;
			
			this.interpretationNotes = new List<InterpretationNotes>();
		}
		
		private string[] PreprocessProfileDescription(string description)
		{
			//TODO regex implementation
			
			//remove spaces like ", " -> ","
			description = description.Replace(", ", ",");
			
			//remove vector names, ie Sch(+,-) -> (+,-)
			string[] vectorNames = {"Sch", "S", "P", "C"};
			foreach(string vectorname in vectorNames)
			{
				description = description.Replace(vectorname, string.Empty);
			}
			
			//remove factor letters, ie. "h0" -> "0"
			string[] factorNames = {"hy", "h", "s", "e", "k", "p", "d", "m", "y", "c"};
			foreach(string factorname in factorNames)
			{
				description = description.Replace(factorname, string.Empty);
			}
			
			//split by spaces, getting four vectors like Sch(+,-)
			char[] whiteSpaceSeparator = {' '};
			string[] vectorReactionsDescriptions
				= description.Split(whiteSpaceSeparator, StringSplitOptions.RemoveEmptyEntries);
			return vectorReactionsDescriptions;
			
			
			//S(h0, s+) P(e-, hy+/-) Sch(k0, p+) C(d-, m+/-)
			// (0,+) (-,±) (0,+) (-,±)
		}
		
		public TestProfile(string h, string s, string e, string hy,
		                   string k, string p, string d, string m)
			:this( new VectorReaction(h, s, Vectors.S),
			      new VectorReaction(e, hy, Vectors.P),
			      new VectorReaction(k, p, Vectors.Sch),
			      new VectorReaction(d, m, Vectors.C))
		{
			
		}
		
		public TestProfile(string S, string P, string Sch, string C)
			:this( new VectorReaction(S, Vectors.S),
			      new VectorReaction(P, Vectors.P),
			      new VectorReaction(Sch, Vectors.Sch),
			      new VectorReaction(C, Vectors.C))
		{
			
		}
		
		public TestProfile(ushort ShPositivTendenz,
		                   ushort ShNegativTendenz,
		                   ushort SSPositivTendenz,
		                   ushort SSNegativTendenz,
		                   ushort PePositivTendenz,
		                   ushort PeNegativTendenz,
		                   ushort PhyPositivTendenz,
		                   ushort PhyNegativTendenz,
		                   ushort SchkPositivTendenz,
		                   ushort SchkNegativTendenz,
		                   ushort SchpPositivTendenz,
		                   ushort SchpNegativTendenz,
		                   ushort CdPositivTendenz,
		                   ushort CdNegativTendenz,
		                   ushort CmPositivTendenz,
		                   ushort CmNegativTendenz)
		{
			#region Check Sum of choices is 12+12
			int sumNegative = ShNegativTendenz+SSNegativTendenz
				+PeNegativTendenz+PhyNegativTendenz+SchkNegativTendenz
				+SchpNegativTendenz+CdNegativTendenz+CmNegativTendenz;
			int sumPositive =ShPositivTendenz +SSPositivTendenz
				+PePositivTendenz+PhyPositivTendenz+SchkPositivTendenz
				+SchpPositivTendenz+CdPositivTendenz+CmPositivTendenz;

			if(sumNegative != 12 || sumPositive != 12)
			{
				throw new ArgumentException("Sum of choices is not 12+12");
			}
			#endregion
			
			VectorReaction S = new VectorReaction(
				ShPositivTendenz, ShNegativTendenz, 
				SSPositivTendenz, SSNegativTendenz, Vectors.S);
			VectorReaction P = new VectorReaction(
				PePositivTendenz, PeNegativTendenz,
				PhyPositivTendenz, PhyNegativTendenz, Vectors.P);
			VectorReaction Sch = new VectorReaction(
				SchkPositivTendenz, SchkNegativTendenz,
				SchpPositivTendenz, SchpNegativTendenz, Vectors.Sch);
			VectorReaction C = new VectorReaction(
				CdPositivTendenz, CdNegativTendenz,
				CmPositivTendenz, CmNegativTendenz, Vectors.C);
			
			ConstructorsHelper(S, P, Sch, C);
		}
		
		public TestProfile(VectorReaction S, VectorReaction P,
		                   VectorReaction Sch, VectorReaction C)
		{
			ConstructorsHelper(S, P, Sch, C);
		}
		#endregion
		
		#region methods
		public override string ToString()
		{
			return this.ToString(false);
		}
		
		public string ToString(bool fullReport)
		{
			string line = this.S
				+ " " + this.P
				+ " " + this.Sch
				+ " " + this.C;
			
			if(fullReport)
			{
				line += " " + this.ExistenzformenToString 
					+ " " + this.InterpretationNotesToString;
			}
	        return line;			
		}
		
		public VectorReaction GetVectorByName(Vectors vectorName)
		{
			switch(vectorName)
			{
				case Vectors.S: return this.S;
				case Vectors.P: return this.P;
				case Vectors.Sch: return this.Sch;
				case Vectors.C: return this.C;
				default: return null;
			}
		}
		
		public FactorReaction GetFactorByName(Factors faktorName)
		{
			// with reflection
			System.Reflection.PropertyInfo faktorProperty 
				= this.GetType().GetProperty(faktorName.ToString());
			
			return (FactorReaction)faktorProperty.GetValue(this, null);
			/*
			switch(faktorName)
			{
				case Factors.h: return this.h;
				default: return null;
			}*/
		}
		
		public void AddHasExistenzform(Existenzformen exForm)
		{
			if(!this.Existenzformen.Contains(exForm))
			{
				this.Existenzformen.Add(exForm);
			}
		}
		
		public void AddInterpretationNote(InterpretationNotes note)
		{
			if(!this.InterpretationNotes.Contains(note))
			{
				this.InterpretationNotes.Add(note);
			}
		}
		
		public bool HasExistenzform(Existenzformen exForm)
		{
			return this.Existenzformen.Contains(exForm);
		}
		
		public bool HasInterpretationNote(InterpretationNotes note)
		{
			return this.InterpretationNotes.Contains(note);
		}
		
		public bool HasMitte(string P_Descr, string Sch_Descr)
		{
			return this.HasMitte(P_Descr, Sch_Descr, FactorsComparisonOptions.Hypertension_sensitive);
		}
		
		public bool HasMitte(string P_Descr, string Sch_Descr, FactorsComparisonOptions comparisonOpts)
		{
			return this.P.EqualsTo(P_Descr, comparisonOpts) 
				&& this.Sch.EqualsTo(Sch_Descr, comparisonOpts);
		}
		
		public bool InSukzession(string reaction1, string reaction2, 
		                       Vectors vector)
		{
			string[] reactions = {reaction1, reaction2};
			return InSukzession(reactions, vector);
		}
		
		public bool InSukzession(string reaction1, string reaction2, 
		                       string reaction3, Vectors vector)
		{
			string[] reactions = {reaction1, reaction2, reaction3};
			return InSukzession(reactions, vector);
		}
		
		public bool InSukzession(string[] reactions, Vectors vector)
		{
			if(this.GetVectorByName(vector).IsAny(reactions))
			{
				if(this.partOf.Sukzession(reactions, vector,
			                             this.dimension))
				{
					return true;
				}
			}
			
			return false;
		}
		#endregion
		
		public bool SerieHasVectorReaction(string reaction, Vectors vectorName)
		{
			return this.partOf.HasVectorReaction(reaction, vectorName, 
			                                     this.dimension);
		}
		
		public bool GroßeMobilität(string reaction1, string reaction2, 
		                           string reaction3, string reaction4, 
		                           Factors factor)
		{
			return this.partOf.GroßeMobilität(reaction1, reaction2, reaction3, 
			                           reaction4, factor, this.dimension);
		}
		
		public bool IsAlways(string reaction, Factors factor)
		{
			return this.partOf.IsAlways(reaction, factor, this.dimension);
		}
		
		public bool IsAlways(string reaction, Vectors vectorName)
		{
			return this.partOf.IsAlways(reaction, vectorName, this.dimension);
		}
		
		#region properties
		public string ExistenzformenToString
		{
			get
			{
				string line = string.Empty;
				
				foreach(var ef in this.Existenzformen)
				{
					line += " " + (int)ef + ",";
				}
				return line;
			}
		}
		
		public string InterpretationNotesToString
		{
			get
			{
				string line = string.Empty;
				if(this.interpretationNotes != null 
				   && this.interpretationNotes.Count > 0)
				{
					foreach(var note in this.interpretationNotes)
					{
						line += " " + note + ",";
					}
				}
				return line;
			}
		}
		
		private List<Existenzformen> Existenzformen
		{
			get
			{
				if(this.existenzFormen == null 
				   //|| this.existenzFormen.Count == 0
				  )
				{
					this.existenzFormen 
						= new List<Existenzformen>();
				}
				return this.existenzFormen;
			}
		}
		private List<InterpretationNotes> InterpretationNotes
		{
			get
			{
				return this.interpretationNotes;
			}
		}
		
		public TestProfile ExperimentalComplementar
		{
			get
			{
				return this.experimentalComplementar;
			}
			set
			{
				// assume
				if(this.dimension == DimensionenUndFormenDerPsyche.VGP)
				{
					this.experimentalComplementar = value;
					this.experimentalComplementar.experimentalComplementar = this;
					
					// this already expected to be set:
					// this.experimentalComplementar.dimension = DimensionenUndFormenDerPsyche.EKP;
					
					// set each vector complementar
					// each Factor reaction checks that sum of it and compl. is == 6
					this.S.ExperimentalComplementar = this.experimentalComplementar.S;
					this.P.ExperimentalComplementar = this.experimentalComplementar.P;
					this.Sch.ExperimentalComplementar = this.experimentalComplementar.Sch;
					this.C.ExperimentalComplementar = this.experimentalComplementar.C;			
				}
				else
				{
					// error
				}
			}
		}
		
		public TestProfile TheoricComplementar
		{
			get
			{
				if(this.theoricComplementar == null)
				{
					this.theoricComplementar = new TestProfile(this.S.ThComplementar,
					                                           this.P.ThComplementar,
					                                           this.Sch.ThComplementar,
					                                           this.C.ThComplementar);
					this.theoricComplementar.theoricComplementar = this;
					
					if(this.dimension == DimensionenUndFormenDerPsyche.VGP)
					{
						this.theoricComplementar.dimension = DimensionenUndFormenDerPsyche.ThKP;
					}
					else if(this.dimension == DimensionenUndFormenDerPsyche.ThKP)
					{
						this.theoricComplementar.dimension = DimensionenUndFormenDerPsyche.VGP;
					}
					
					this.theoricComplementar.partOf = this.partOf;
				}
				
				return this.theoricComplementar;
			}
		}
		
		public bool NoHypertension
		{
			get
			{
				return this.S.NoHypertension && this.P.NoHypertension 
					&& this.Sch.NoHypertension && this.C.NoHypertension;
			}
		}
		
		#region Factors
		public FactorReaction h
		{
			get
			{
				return this.S.firstFactorReaction;
			}
		}
		
		public FactorReaction s
		{
			get
			{
				return this.S.secondFactorReaction;
			}
		}
		
		public FactorReaction e
		{
			get
			{
				return this.P.firstFactorReaction;
			}
		}
		
		public FactorReaction hy
		{
			get
			{
				return this.P.secondFactorReaction;
			}
		}
		
		public FactorReaction k
		{
			get
			{
				return this.Sch.firstFactorReaction;
			}
		}
		
		public FactorReaction p
		{
			get
			{
				return this.Sch.secondFactorReaction;
			}
		}
		
		public FactorReaction d
		{
			get
			{
				return this.C.firstFactorReaction;
			}
		}
		
		public FactorReaction m
		{
			get
			{
				return this.C.secondFactorReaction;
			}
		}
		#endregion
		#endregion
	}		
}

