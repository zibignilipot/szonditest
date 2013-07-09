using System;
using System.Collections.Generic;

namespace SzondiTest
{	
	/// <summary>
	/// Description of SzondiCounts.
	/// </summary>
	public class SzondiCounts
	{			
		public SzondiCounts()
		{
			//structures used to keeep track of user selections (only image numbers, no decoding)
			this.availableUnselectedImages = new Dictionary<ushort, List<ushort>>(NumberOfSeries);
			for(ushort serieNum=1; serieNum<=NumberOfSeries; serieNum++)
			{
				this.availableUnselectedImages[serieNum]
					= new List<ushort> {1, 2, 3, 4, 5, 6, 7, 8};
			}
			
			this.userSelectionNumbers = new Dictionary<TestPhases,
			Dictionary<ushort, Dictionary<SympUnsymp, List<ushort>>>>(2);
			foreach(TestPhases testPhase
			        in (TestPhases[])Enum.GetValues(typeof(TestPhases)))
			{
				this.userSelectionNumbers[testPhase]
					= new Dictionary<ushort, Dictionary<SympUnsymp, List<ushort>>>(NumberOfSeries);
				
				for(ushort serieNum=1; serieNum<=NumberOfSeries; serieNum++)
				{
					this.userSelectionNumbers[testPhase][serieNum]
						= new Dictionary<SympUnsymp, List<ushort>>(2);
					
					foreach(SympUnsymp sympUnsymp
					        in (SympUnsymp[])Enum.GetValues(typeof(SympUnsymp)))
					{
						this.userSelectionNumbers[testPhase][serieNum][sympUnsymp]
							= new List<ushort>();
					}
				}
			}
			
			// Structures used for selection decodings after test ends
			InitTestKeys();
			this.DecodedSelectionsBySerie
				= new Dictionary<TestPhases, Dictionary<ushort, List<SzondiCounts.FactorsSelections>>>(2);
			foreach(TestPhases testPhase
			        in (TestPhases[])Enum.GetValues(typeof(TestPhases)))
			{
				this.DecodedSelectionsBySerie[testPhase] = new Dictionary<ushort, List<FactorsSelections>>
					(NumberOfSeries);
			}
		}
		
		public void SetSingleUserSelection(
			TestPhases testPhase, ushort serieNum,
			SympUnsymp sympUnsymp, ushort imageNum)
		{
			if(!this.availableUnselectedImages[serieNum].Remove(imageNum))
			{
				throw new ArgumentException("image " + imageNum + " in serie " + serieNum
				                            + " has already been selected.");
			}
			
			this.userSelectionNumbers[testPhase][serieNum][sympUnsymp].Add(imageNum);
			if(this.userSelectionNumbers[testPhase][serieNum][sympUnsymp].Count > 2)
			{
				throw new ArgumentOutOfRangeException(
					"Too many images selected: " + testPhase + " Serie " + serieNum
					+ ", " + sympUnsymp + " count >= "
					+ this.userSelectionNumbers[testPhase][serieNum][sympUnsymp].Count);
			}
		}
		
		public void RevertSerieSelections(TestPhases testPhase, ushort serieNum)
		{
			// to revert:
			//this.availableUnselectedImages[serieNum].Remove(imageNum);
			//this.userSelectionNumbers[testPhase][serieNum][sympUnsymp]..Add(imageNum);
			
			foreach(SympUnsymp sympUnsymp
			        in (SympUnsymp[])Enum.GetValues(typeof(SympUnsymp)))
			{
				foreach(var imageNum
				        in this.userSelectionNumbers[testPhase][serieNum][sympUnsymp])
				{
					this.availableUnselectedImages[serieNum].Add(imageNum);
				}
				this.userSelectionNumbers[testPhase][serieNum][sympUnsymp].Clear();
			}
		}
		
		/// <summary>
		/// If selected in PPP, should not be displayed in PCE
		/// </summary>
		public List<ushort> GetImagesSelectedInGivenSerie(TestPhases testPhase, ushort serieNum)
		{
			List<ushort> imagesSelectedInGivenSerie = new List<ushort>();
			imagesSelectedInGivenSerie.AddRange(this.userSelectionNumbers[testPhase][serieNum][SympUnsymp.Symp]);
			imagesSelectedInGivenSerie.AddRange(this.userSelectionNumbers[testPhase][serieNum][SympUnsymp.Unsymp]);
			
			return imagesSelectedInGivenSerie;
		}
		
		private void InitTestKeys()
		{
			if(SerieImagesKeys != null)
			{	return;
			}
			
			{
				int factorsLenght = Enum.GetNames(typeof(Factors)).Length;
				if(factorsLenght != ImagesPerSerie)
				{
					string message = "#factors: " + factorsLenght
						+ "; ImagesPerSerie: " + ImagesPerSerie;
					throw new ArgumentException(message);
				}
			}
			
			SerieImagesKeys
				= new Dictionary<ushort, Dictionary<ushort, Factors>>(NumberOfSeries);
			for(ushort serieIndex = 1; serieIndex<=NumberOfSeries; serieIndex++)
			{
				Dictionary<ushort, Factors> currentSerieDictionary
					= new Dictionary<ushort, Factors>(ImagesPerSerie);
				SerieImagesKeys.Add(serieIndex, currentSerieDictionary);
			}
			
			// Taken from Szondi, L Shicksalsanalyse Band 3, cap.25
			
			//Serie 1
			SerieImagesKeys[1][1] = Factors.k;
			SerieImagesKeys[1][2] = Factors.s;
			SerieImagesKeys[1][3] = Factors.p;
			SerieImagesKeys[1][4] = Factors.d;
			SerieImagesKeys[1][5] = Factors.h;
			SerieImagesKeys[1][6] = Factors.e;
			SerieImagesKeys[1][7] = Factors.m;
			SerieImagesKeys[1][8] = Factors.hy;
			
			//Serie 2
			SerieImagesKeys[2][1] = Factors.hy;
			SerieImagesKeys[2][2] = Factors.m;
			SerieImagesKeys[2][3] = Factors.e;
			SerieImagesKeys[2][4] = Factors.h;
			SerieImagesKeys[2][5] = Factors.d;
			SerieImagesKeys[2][6] = Factors.p;
			SerieImagesKeys[2][7] = Factors.s;
			SerieImagesKeys[2][8] = Factors.k;
			
			
			//Serie 3
			SerieImagesKeys[3][1] = Factors.h;
			SerieImagesKeys[3][2] = Factors.e;
			SerieImagesKeys[3][3] = Factors.s;
			SerieImagesKeys[3][4] = Factors.m;
			SerieImagesKeys[3][5] = Factors.k;
			SerieImagesKeys[3][6] = Factors.d;
			SerieImagesKeys[3][7] = Factors.hy;
			SerieImagesKeys[3][8] = Factors.p;
			
			//Serie 4
			SerieImagesKeys[4][1] = Factors.p;
			SerieImagesKeys[4][2] = Factors.hy;
			SerieImagesKeys[4][3] = Factors.d;
			SerieImagesKeys[4][4] = Factors.k;
			SerieImagesKeys[4][5] = Factors.m;
			SerieImagesKeys[4][6] = Factors.s;
			SerieImagesKeys[4][7] = Factors.e;
			SerieImagesKeys[4][8] = Factors.h;
			
			//Serie 5
			SerieImagesKeys[5][1] = Factors.e;
			SerieImagesKeys[5][2] = Factors.d;
			SerieImagesKeys[5][3] = Factors.hy;
			SerieImagesKeys[5][4] = Factors.p;
			SerieImagesKeys[5][5] = Factors.s;
			SerieImagesKeys[5][6] = Factors.k;
			SerieImagesKeys[5][7] = Factors.h;
			SerieImagesKeys[5][8] = Factors.m;
			
			//Serie 6
			SerieImagesKeys[6][1] = Factors.m;
			SerieImagesKeys[6][2] = Factors.h;
			SerieImagesKeys[6][3] = Factors.k;
			SerieImagesKeys[6][4] = Factors.s;
			SerieImagesKeys[6][5] = Factors.p;
			SerieImagesKeys[6][6] = Factors.hy;
			SerieImagesKeys[6][7] = Factors.d;
			SerieImagesKeys[6][8] = Factors.e;
		}
		
		public void CountWholeTestSelectionTotals()
		{
			// Decode step
			{
				/*ushort symp1 = 0;
				ushort symp2 = 0;
				ushort unsymp1 = 0;
				ushort unsymp2 = 0;
				if(!GetIndexesOfSelectedImages(ref symp1, ref symp2, ref unsymp1, ref unsymp2))
				{
					// failed selection
					return;
				}*/
				
				// Decode step: from "serie -> selected img#" to  "serie -> factor +symp"
				foreach(TestPhases testPhase
				        in (TestPhases[])Enum.GetValues(typeof(TestPhases)))
				{
					for(ushort serieNum=1; serieNum<=NumberOfSeries; serieNum++)
					{
						//
						ushort symp1  = this.userSelectionNumbers[testPhase][serieNum][SympUnsymp.Symp][0];
						ushort symp2 = this.userSelectionNumbers[testPhase][serieNum][SympUnsymp.Symp][1];
						ushort unsymp1 = this.userSelectionNumbers[testPhase][serieNum][SympUnsymp.Unsymp][0];
						ushort unsymp2 = this.userSelectionNumbers[testPhase][serieNum][SympUnsymp.Unsymp][1];
						
						SzondiCounts.CountSerieSelection(
							serieNum, symp1,
							symp2, unsymp1, unsymp2,
							this.DecodedSelectionsBySerie[testPhase]);
					}
				}
				/*{
					Dictionary<ushort, HashSet<SzondiCounts.FactorsSelections>> planeSelectionValues;
					if(this.currentTestPhase == SzondiCounts.TestPhases.PPP)
					{	planeSelectionValues = this.szondiCalculator.PPPSelectionValuesBySerie;
					}
					else
					{	planeSelectionValues = this.szondiCalculator.PCESelectionValuesBySerie;
					}
					
					SzondiCounts.CountSerieSelection(this.currentSerieNumber, symp1,
					                                 symp2, unsymp1, unsymp2, planeSelectionValues);
				}*/
			}
			
			//Decode, CountPlaneSelectionTotals
			this.decodedAndCountedSelections
				= new Dictionary<TestPhases, Dictionary<FactorsSelections, ushort>>(2);
			foreach(TestPhases testPhase
			        in (TestPhases[])Enum.GetValues(typeof(TestPhases)))
			{
				decodedAndCountedSelections[testPhase]
					= CountPlaneSelectionTotals(this.DecodedSelectionsBySerie[testPhase]);
			}
			
			// redundant: verify that each factor has been selected 6 times
			{
				
			}
			
			// TODO: veryfy no duplicates, 24 symp, 48 total, each fator 6 selections
		}
		
		public void WriteReportToStringBuilder(System.Text.StringBuilder sb)
		{
			//TODO compact, less newlines
			
			sb.AppendLine("PPP");
			AppendWriteFactorsSelections(sb, this.decodedAndCountedSelections[TestPhases.PPP]);
			sb.AppendLine("\r\nPCE");
			AppendWriteFactorsSelections(sb, this.decodedAndCountedSelections[TestPhases.PCE]);
			
			sb.AppendLine("\r\nPPP");
			AppendWriteVectorialImages(sb, this.decodedAndCountedSelections[TestPhases.PPP]);
			sb.AppendLine("\r\nPCE");
			AppendWriteVectorialImages(sb, this.decodedAndCountedSelections[TestPhases.PCE]);
		}
		
		public void LoadProfilesFromTextFile()
		{
			
		}
		
		
		private static void AppendWriteVectorialImages(
			System.Text.StringBuilder sb,
			Dictionary<FactorsSelections, ushort> countedPlaneSelectionValues)
		{
			// single VectorialImage
			// S(h+, s±)

			AppendWriteSingleVectorialImage(sb, "S",
			                                Factors.h,
			                                countedPlaneSelectionValues[FactorsSelections.h_symp],
			                                countedPlaneSelectionValues[FactorsSelections.h_unsymp],
			                                Factors.s,
			                                countedPlaneSelectionValues[FactorsSelections.s_symp],
			                                countedPlaneSelectionValues[FactorsSelections.s_unsymp]);
			AppendWriteSingleVectorialImage(sb, "P",
			                                Factors.e,
			                                countedPlaneSelectionValues[FactorsSelections.e_symp],
			                                countedPlaneSelectionValues[FactorsSelections.e_unsymp],
			                                Factors.hy,
			                                countedPlaneSelectionValues[FactorsSelections.hy_symp],
			                                countedPlaneSelectionValues[FactorsSelections.hy_unsymp]);
			AppendWriteSingleVectorialImage(sb, "Sch",
			                                Factors.k,
			                                countedPlaneSelectionValues[FactorsSelections.k_symp],
			                                countedPlaneSelectionValues[FactorsSelections.k_unsymp],
			                                Factors.p,
			                                countedPlaneSelectionValues[FactorsSelections.p_symp],
			                                countedPlaneSelectionValues[FactorsSelections.p_unsymp]);
			AppendWriteSingleVectorialImage(sb, "C",
			                                Factors.d,
			                                countedPlaneSelectionValues[FactorsSelections.d_symp],
			                                countedPlaneSelectionValues[FactorsSelections.d_unsymp],
			                                Factors.m,
			                                countedPlaneSelectionValues[FactorsSelections.m_symp],
			                                countedPlaneSelectionValues[FactorsSelections.m_unsymp]);
		}
		
		internal static void  AppendWriteSingleVectorialImage(
			System.Text.StringBuilder sb,
			string vectorName,
			Factors firstFactor,
			ushort firstFactorPositiveDirection,
			ushort firstFactorNegativeDirection,
			Factors secondFactor,
			ushort secondFactorPositiveDirection,
			ushort secondFactorNegativeDirection)
		{
			// single VectorialImage
			// S(h+, s±)
			
			string firstFactorReaction
				= SingleChoiceReactionToString(
					firstFactorPositiveDirection,
					firstFactorNegativeDirection);
			string secondFactorReaction
				= SingleChoiceReactionToString(
					secondFactorPositiveDirection,
					secondFactorNegativeDirection);
			
			sb.Append(vectorName + "("
			          + firstFactor + firstFactorReaction + ", "
			          + secondFactor + secondFactorReaction + ") ");
		}
		
		internal static string SingleChoiceReactionToString(
			ushort positiveDirection, ushort negativeDirection)
		{
			var factorReaction
				= new FactorReaction(positiveDirection, negativeDirection);
			return factorReaction.ToString();
		}
		
		private static void AppendWriteFactorsSelections(
			System.Text.StringBuilder sb,
			Dictionary<FactorsSelections, ushort> countedPlaneSelectionValues)
		{
			foreach(var factorSelection
			        in countedPlaneSelectionValues)
			{
				string tendencyConfiguration = string.Empty;;
				switch(factorSelection.Value)
				{
					case 0:
						case 1: tendencyConfiguration = "0"; break;
					case 2:
						case 3: tendencyConfiguration = "?"; break;
						case 4: tendencyConfiguration = "!"; break;
						case 5: tendencyConfiguration = "!!"; break;
						case 6: tendencyConfiguration = "!!!"; break;
						//default: tendencyConfiguration = string.Empty;
				}
				
				string line = tendencyConfiguration + " ("
					+ factorSelection.Value + ") " + factorSelection.Key;
				sb.AppendLine(line);
			}
		}
		
		private Dictionary<FactorsSelections, ushort> CountPlaneSelectionTotals(
			Dictionary<ushort, List<FactorsSelections>> planeSelections)
		{
			Dictionary<FactorsSelections, ushort> totalPlaneSelections;
			
			// Init
			{
				totalPlaneSelections = new Dictionary<FactorsSelections, ushort>
					(NumberOfFactors*2);
				
				foreach(FactorsSelections factorSelection
				        in (FactorsSelections[])Enum.GetValues(typeof(FactorsSelections)))
				{
					totalPlaneSelections.Add(factorSelection, 0);
				}
			}
			
			
			{
				for(ushort serieIndex = 1; serieIndex<= NumberOfSeries; serieIndex++)
				{
					foreach(FactorsSelections singleSelection
					        in planeSelections[serieIndex])
					{
						totalPlaneSelections[singleSelection]++;
					}
				}
			}
			return totalPlaneSelections;
		}
		
		/// <summary>
		/// Given curren serie, and four user made choices (for each, image number
		/// and sympathetic/unsympathetic), assigns/updates the factor values (h, s, e...)
		/// selected for this serie.
		/// </summary>
		/// <param name="currentSerie"></param>
		/// <param name="sympathetic1"></param>
		/// <param name="sympathetic2"></param>
		/// <param name="unsympathetic1"></param>
		/// <param name="unsympathetic2"></param>
		/// <param name="PlaneSelectionValues">Can be either PPP or PCE</param>
		public static void CountSerieSelection(ushort currentSerie,
		                                       ushort sympathetic1, ushort sympathetic2,
		                                       ushort unsympathetic1, ushort unsympathetic2,
		                                       Dictionary<ushort, List<FactorsSelections>> PlaneSelectionValues)
		{
			// caricare corrispondenza numeri img-fattori pulsionali
			// method: dati serie, num, simp/antip
			// restituisce fattore, + o -
			List<FactorsSelections> serieSelectionValues
				= new List<FactorsSelections>();
			
			CountSingleSelection(currentSerie, sympathetic1, false, serieSelectionValues);
			CountSingleSelection(currentSerie, sympathetic2, false, serieSelectionValues);
			CountSingleSelection(currentSerie, unsympathetic1, true, serieSelectionValues);
			CountSingleSelection(currentSerie, unsympathetic2, true, serieSelectionValues);
			
			// Assign choice values for current serie
			// If there was a choice already for this serie, it gets updated
			PlaneSelectionValues[currentSerie] = serieSelectionValues;
			
			// TODO verifica totale per serie: 2+, 2-(in other method? a GUI one?)
			// ricordare img scelte/non scelte
			// ricordare tot valori pulsionali
		}
		
		/// <summary>
		/// Works for both selections for PPP (from 8 images) and PCE (from 4 images)
		/// </summary>
		/// <param name="currentSerie"></param>
		/// <param name="selectedImgNum"></param>
		/// <param name="unsymp"></param>
		/// <param name="serieSelectionValues"></param>
		private static void CountSingleSelection(ushort currentSerie, ushort selectedImgNum, bool unsymp,
		                                         List<FactorsSelections> serieSelectionValues)
		{
			string sympSuffix = "symp";
			string unsympExtraSuffix = "";
			if(unsymp)
			{	unsympExtraSuffix =	"un";
			}
			
			Factors sympFactorSelected
				= SerieImagesKeys[currentSerie][selectedImgNum];
			// i.e. hy_unsymp
			string selectionValueString
				= sympFactorSelected.ToString() + "_" + unsympExtraSuffix + sympSuffix;
			FactorsSelections selectionValue
				= (FactorsSelections)FactorsSelections.Parse(typeof(FactorsSelections), selectionValueString);
			
			if(serieSelectionValues.Contains(selectionValue))
			{
				throw new ArgumentException(selectionValueString + ", " + selectionValue);
			}
			else
			{
				serieSelectionValues.Add(selectionValue);
			}
		}
		
		public const ushort NumberOfSeries = 6;
		public const ushort ImagesPerSerie = 8;
		public static ushort NumberOfFactors
		{
			/// <summary>
			/// Each serie has an image (8) for each factor
			/// </summary>
			get { return ImagesPerSerie; }
		}
		
		// phases or stages
		public enum TestPhases {PPP, PCE};
		
		
		/*{
			private enum PulsionalVectors {S, P, Sch, C};
			//directions Factors x (positiveNegative,
			public enum ChoiceReaction { positive, negative, }
		}*/
		
		// change to smthing like <Factors x SympUnsymp> Factorchoice;
		public enum FactorsSelections {h_symp, h_unsymp, s_symp, s_unsymp,
			e_symp, e_unsymp, hy_symp, hy_unsymp, k_symp, k_unsymp,
			p_symp, p_unsymp, d_symp, d_unsymp, m_symp,	m_unsymp};
		
		
		public enum SympUnsymp {Symp, Unsymp};

		
		private static Dictionary<ushort, Dictionary<ushort, Factors>> SerieImagesKeys;
		
		/// <summary>
		/// ushort: serie number (1-6)
		/// HashSet<FactorsSelections>: a set of four selections for a single serie,
		/// i.e. { h_symp, s_unsymp,  e_unsymp, p_symp}
		/// </summary>
		//private Dictionary<ushort, HashSet<FactorsSelections>> PPPSelectionValuesBySerie;
		//private Dictionary<ushort, HashSet<FactorsSelections>> PCESelectionValuesBySerie;
		private Dictionary<TestPhases, Dictionary<ushort, List<FactorsSelections>>> DecodedSelectionsBySerie;
		
		/// <summary>
		/// Data structure with all selections.
		/// PPP/PCE, serie, symp/unsymp, and image numbers (2 for each entry)
		/// After a serie has been done by the user, it must have 2 symp and 2 unsymp filled
		/// </summary>
		private Dictionary<TestPhases, Dictionary<ushort, Dictionary<SympUnsymp, List<ushort>>>> userSelectionNumbers;
		
		/// <summary>
		/// Data structure with all image numbers that have not yet been selected by the user.
		/// Ushort serie, ushort image num (8 for each serie initially).
		/// After a serie has been done by the user, four get removed
		/// (to be inserted into userSelectionNumbers)
		/// After PPP, each serie must have 4 left (out of initial 8).
		/// </summary>
		private Dictionary<ushort, List<ushort>> availableUnselectedImages;
		
		public Dictionary<TestPhases, Dictionary<FactorsSelections, ushort>> decodedAndCountedSelections;
		
	}
}
