using System;
//using System.Drawing;
using System.Windows.Forms;

namespace SzondiTest
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public partial class SerieForm : Form
	{
		public SerieForm(ushort serieNumberValue, SzondiCounts szondiCalculator)
		{
			this.currentTestPhase = SzondiCounts.TestPhases.PPP;
			this.currentSerieNumber = serieNumberValue;
			this.szondiCalculator = szondiCalculator;
			InitializeComponent();
		}
		
		private void OnNextButtonClick(object sender, System.EventArgs e)
		{
			if(!GetNumbersAndSympathyOfCurrentSerieSelectedImages())
			{
				return;
			}
			
			// Last serie of a phase (PPP or PCE phase)
			if(this.currentSerieNumber == SzondiCounts.NumberOfSeries)
			{
				if(this.currentTestPhase == SzondiCounts.TestPhases.PPP)
				{
					// switch to PCE
					// passing from PPP to PCE
					this.currentTestPhase = SzondiCounts.TestPhases.PCE;
					this.currentSerieNumber = 1;	
					UpdateAllPicturesAndSympathySelections();		
					HidePPPSelectedImages();					
				}
				else
				{	// PCE done too, test completed
					// calculate results
					this.szondiCalculator.CountWholeTestSelectionTotals();
					
					WriteReportToFile(this.szondiCalculator);
					
					// end of program, manually closing by user
				}
			}
			else
			{	// more serie to be done in current phase
				// change serie, FlowLayoutPanel change
				this.currentSerieNumber++;
				UpdateAllPicturesAndSympathySelections();
				
				// set invisible images selected in PPP
				if(this.currentTestPhase == SzondiCounts.TestPhases.PCE)
				{
					HidePPPSelectedImages();
				}
			}
			
			// TODO verify bindind imagenumber/factor when passing to PCE
		}
		
		private static void WriteReportToFile(SzondiCounts szondiCalculator)
		{
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			szondiCalculator.WriteReportToStringBuilder(sb);
			
			// writing to file
			/*{
				string filename = "Szondi test (" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ").txt";
	            System.IO.TextWriter tw = null;
	            
	            // attempt to init StreamWriter
				try
				{
					// create a writer and open the file	
					tw = new System.IO.StreamWriter(filename);
	
				}
				catch(Exception)
				{
					// attempt auto alternative path, if succeeds path to messagebox
					// openfile dialog
					System.IO.Stream myStream;
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.FileName = filename;
					
					if(saveFileDialog.ShowDialog() == DialogResult.OK)
				    {
						if((myStream = saveFileDialog.OpenFile()) != null)
						{
							tw = new System.IO.StreamWriter(myStream);
	    				}
				    }
					
				}
				
				//attempt to write report/log
				try
				{
					tw.WriteLine(sb.ToString());
			
		            // close the stream
		            tw.Close();
		            			
		            MessageBox.Show("OK");
				}
			}*/
			//catch(Exception)
			{
				// messagebox for file error, with all results
				// messagebox with copyable text? Autocopy to clipboard
				try
				{
					System.Windows.Forms.Clipboard.SetText(sb.ToString());
				}
				catch(System.Runtime.InteropServices.ExternalException)
				{
					
				}				

					
				MessageBox.Show("Can't write file. Test results: " 
				                + sb.ToString());				
			}			

		}
		
		private void OnComboBoxSelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Need to count how many symphatetic selected, to keep it at <= 2
			// same for the unsymphatetic			
		}
		
		private bool GetNumbersAndSympathyOfCurrentSerieSelectedImages()
		{
			// count selections
			// for each groupbox, read its combobox
			// and update data structure accordingly
			
			// side effect: in PPP, count unpicked images in current serie
			// (so to display 4 in PCE instead of 8)
			try
			{
				for(ushort imageArrayIndex=0; 
			    imageArrayIndex < this.imageSplitContainers.Length; 
			    imageArrayIndex++)
				{
					SplitContainer currentSplitContainer = this.imageSplitContainers[imageArrayIndex];
					ComboBox currentComboBox = currentSplitContainer.Panel2.Controls[ComboSympUnsympName] as ComboBox;
					string currentComboBoxValue = currentComboBox.SelectedItem as string;
					if (!string.IsNullOrEmpty(currentComboBoxValue))
					{
						SzondiCounts.SympUnsymp sympOrUnsymp;
					
						if(currentComboBoxValue.Equals(SympatheticDisplayText))
						{
							sympOrUnsymp = SzondiCounts.SympUnsymp.Symp;
						}
						else if (currentComboBoxValue.Equals(UnsympatheticDisplayText))
						{
							sympOrUnsymp = SzondiCounts.SympUnsymp.Unsymp;
						}
						else
						{	// redundant
							throw new System.ArgumentException("Wrong combobox value: " + currentComboBoxValue);
						}
						
						szondiCalculator.SetSingleUserSelection(
								this.currentTestPhase, this.CurrentSerieNumber, 
								sympOrUnsymp,
								(ushort)(imageArrayIndex +1));
					}
					
					//TODO add more checks, count 2+2
					
					/* if(!string.IsNullOrEmpty(comboBox.SelectedItem as string))
					{	
						// selected in PPP, will not be displayed for PCE
						ushort selectedImageNumber = (ushort)(imageArrayIndex+1);
						this.imagesSelectedInPPP[this.currentSerieNumber].Add(selectedImageNumber);
					}*/
				}
				System.Collections.Generic.List<ushort> imagesSelectedInCurrentSerie 
					= szondiCalculator.GetImagesSelectedInGivenSerie(this.currentTestPhase,
						this.CurrentSerieNumber);
				if(imagesSelectedInCurrentSerie.Count != 4)
				{
					string message = "4 images should be selected. Selections for current serie " 
						+ this.CurrentSerieNumber
						+ " are " + imagesSelectedInCurrentSerie.Count;
					throw new System.ArgumentException(message);
				}
			}
			catch(System.Exception exception)
			{
				// revert changes on Current Serie Selections
				szondiCalculator.RevertSerieSelections(
								this.currentTestPhase, this.CurrentSerieNumber);
				MessageBox.Show(exception.Message);
				return false;
			}
			
			return true;
		}		
		
		private SzondiCounts.TestPhases currentTestPhase;
		private SzondiCounts szondiCalculator;
		private ushort currentSerieNumber;
		public ushort CurrentSerieNumber
		{
			get { return this.currentSerieNumber;}
		}
	}
	
	
	
}
