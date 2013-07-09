using System.Windows.Forms;
using System.Collections.Generic;

namespace SzondiTest
{
	partial class SerieForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		private void InitializeComponent()
		{
			this.Name = "Szondi Test";
			this.resources
				= new System.ComponentModel.ComponentResourceManager(typeof(SerieForm));
			
			userImageJudgements = new string[]{string.Empty, SympatheticDisplayText, UnsympatheticDisplayText};
			
			InitImagesTableLayoutPanel();
			InitGroupBoxes(this.resources);
			InitBroadSplitContainer();
			
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(723, 530);
			this.Controls.Add(this.broadSplitContainer);
			
			this.EndInitResumeLayout();
			
			this.WindowState = FormWindowState.Maximized;
		}
		
		private void InitImagesTableLayoutPanel()
		{
			this.imagesTableLayoutPanel = new TableLayoutPanel();
			this.imagesTableLayoutPanel.SuspendLayout();
			this.imagesTableLayoutPanel.ColumnCount = 4;
			this.imagesTableLayoutPanel.RowCount = 2;
			this.imagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.imagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.imagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.imagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.imagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.imagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.imagesTableLayoutPanel.Location = new System.Drawing.Point(0, 0);//(1, 12);
			this.imagesTableLayoutPanel.Name = "imagesTableLayoutPanel";
			this.imagesTableLayoutPanel.Dock = DockStyle.Fill;//DockStyle.Fill;
			this.imagesTableLayoutPanel.Size = new System.Drawing.Size(603, 477);
			//this.imagesFlowLayoutPanel.AutoSize = true;
			//this.imagesFlowLayoutPanel.Anchor = (AnchorStyles.Top );
			//this.imagesFlowLayoutPanel.TabIndex = 2;
		}
		
		private void InitBroadSplitContainer()
		{
			this.broadSplitContainer = new SplitContainer();
			//((System.ComponentModel.ISupportInitialize)(this.broadSplitContainer)).BeginInit();
			this.broadSplitContainer.Panel1.SuspendLayout();
			this.broadSplitContainer.Panel2.SuspendLayout();
			this.broadSplitContainer.SuspendLayout();
			
			this.broadSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.broadSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.broadSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.broadSplitContainer.Name = "broadSplitContainer";
			this.broadSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			
			// 
			// broadSplitContainer.Panel1
			// 
			this.broadSplitContainer.Panel1.Controls.Add(this.imagesTableLayoutPanel);
			
			// 
			// broadSplitContainer.Panel2
			// 
			// 
			// goNextSerie button
			//
			this.goNextSerie = new System.Windows.Forms.Button();
			
			this.goNextSerie.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.goNextSerie.Location = new System.Drawing.Point(166, 4);//(354, 495);
			this.goNextSerie.Name = "goNextSerie";
			this.goNextSerie.Size = new System.Drawing.Size(75, 23);
			this.goNextSerie.TabIndex = 0;
			this.goNextSerie.Text = "-->";
			this.goNextSerie.UseVisualStyleBackColor = true;
			
			this.goNextSerie.Click +=
				new System.EventHandler(OnNextButtonClick);
			
			this.broadSplitContainer.Panel2.Controls.Add(this.goNextSerie);
			this.broadSplitContainer.Size = new System.Drawing.Size(402, 320);
			if ((this.broadSplitContainer != null)) {
			}
			this.broadSplitContainer.SplitterDistance = 286;
			this.broadSplitContainer.TabIndex = 1;
			
		}
		
		private void InitGroupBoxes(System.ComponentModel.ComponentResourceManager resources)
		{
			this.imageSplitContainers
				= new System.Windows.Forms.SplitContainer[SzondiCounts.ImagesPerSerie];
			
			for(ushort imageArrayIndex=0; imageArrayIndex < this.imageSplitContainers.Length; imageArrayIndex++)
			{
				InitPictureAndComboBox(imageArrayIndex, resources);
				this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[imageArrayIndex]);
			}
			
			/* TODO Szondi display order 1234 8765
  			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[0], 0, 0);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[1], 0, 1);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[2], 0, 2);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[3], 0, 3);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[4], 1, 7);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[5], 1, 6);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[6], 1, 5);
			this.imagesTableLayoutPanel.Controls.Add(this.imageSplitContainers[7], 1, 4);*/
			
			UpdateAllPicturesAndSympathySelections();
			//this.imagesFlowLayoutPanel.TabIndex = 99;
			this.imagesTableLayoutPanel.TabStop = true;
		}
		
		private void InitPictureAndComboBox(ushort imageArrayIndex, 
				System.ComponentModel.ComponentResourceManager resources)
		{
			// pictureBox
			PictureBox pictureBox = new PictureBox();
			pictureBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
			// 
			// pictureBox
			// 
			pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			//pictureBox.Size = new System.Drawing.Size(94, 104);//(123, 164);
			pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pictureBox.Location = new System.Drawing.Point(0, 0);//(8, 21);
			pictureBox.Name = PictureBoxName;			
			pictureBox.TabIndex = 4;//0
			pictureBox.TabStop = false;		
			
			// 
			// comboBox
			// 
			ComboBox comboBox = new ComboBox();
			//comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
			comboBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			comboBox.FormattingEnabled = true;
			comboBox.Location = new System.Drawing.Point(0, 5);//(10, 191);
			comboBox.Name = ComboSympUnsympName;
			//comboBox.Size = new System.Drawing.Size(94, 24);//(121, 24);
			comboBox.TabIndex = 1;
			
			comboBox.Items.AddRange(userImageJudgements);
			comboBox.Font = new System.Drawing.Font("Wingdings", 15);
			
			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox.SelectedIndexChanged +=
				new System.EventHandler(OnComboBoxSelectedIndexChanged);
				
			// 
			// imageSplitContainer initialization
			// 
			this.imageSplitContainers[imageArrayIndex] = new System.Windows.Forms.SplitContainer();
		
			this.imageSplitContainers[imageArrayIndex].Size = new System.Drawing.Size(94, 137);//(141, 229);
			//this.imageSplitContainers[imageArrayIndex].TabIndex = 9;
			//this.imageSplitContainer.Name = "imageSplitContainer";
			this.imageSplitContainers[imageArrayIndex].Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageSplitContainers[imageArrayIndex].FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.imageSplitContainers[imageArrayIndex].Location = new System.Drawing.Point(203, 3);
			this.imageSplitContainers[imageArrayIndex].Orientation = System.Windows.Forms.Orientation.Horizontal;
			
			this.imageSplitContainers[imageArrayIndex].Panel1.Controls.Add(pictureBox);
			this.imageSplitContainers[imageArrayIndex].Panel2.Controls.Add(comboBox);
			this.imageSplitContainers[imageArrayIndex].SplitterDistance = 100;
		}

		private void UpdateAllPicturesAndSympathySelections()
		{
			for(ushort imageArrayIndex=0; 
			    imageArrayIndex < this.imageSplitContainers.Length; 
			    imageArrayIndex++)
			{
				UpdatePictureAndSympathySelection(
					this.imageSplitContainers[imageArrayIndex],
					imageArrayIndex, 
					this.CurrentSerieNumber,
					this.resources);
			}
			
			this.Text = this.Name 
				+ " - Serie " + this.CurrentSerieNumber 
				+ " (" + this.currentTestPhase + ")";
		}
		
		// update picture
		private static void UpdatePictureAndSympathySelection(
			SplitContainer imageSplitContainer,
			ushort imageArrayIndex,
			ushort currentSerieNumber, 
			System.ComponentModel.ComponentResourceManager resources)
		{
			PictureBox pictureBox = imageSplitContainer.Panel1.Controls[PictureBoxName] as PictureBox;
			ComboBox comboBox = imageSplitContainer.Panel2.Controls[ComboSympUnsympName] as ComboBox;
			
			string imageName = ImageNameAt(imageArrayIndex, currentSerieNumber);
			pictureBox.Image = ((System.Drawing.Image)(resources.GetObject(imageName)));
			comboBox.SelectedItem = null;
		}
			
		private void HidePPPSelectedImages()
		{
			// first reset invisible set in previous series
			{
				foreach(SplitContainer groupBox in this.imageSplitContainers)
				{
					groupBox.Visible = true;
				}
			}
			
			List<ushort> imagesSelectedInPPP 
				= this.szondiCalculator.GetImagesSelectedInGivenSerie(
					SzondiCounts.TestPhases.PPP,
					this.CurrentSerieNumber);
			foreach(ushort selectedImgNum in imagesSelectedInPPP)
			{	// imgs selected in PPP will not be displayed for PCE
				ushort selectedImgIndex = (ushort)(selectedImgNum - 1);
				this.imageSplitContainers[selectedImgIndex].Visible = false;
			}
			
			/*foreach(ushort selectedImgNum
				in this.imagesSelectedInPPP[this.CurrentSerieNumber])
			{	// imgs selected in PPP will not be displayed for PCE
				ushort selectedImgIndex = (ushort)(selectedImgNum - 1);
				this.groupBoxes[selectedImgIndex].Visible = false;
			}*/
		}
		
		private static string ImageNameAt(ushort arrayIndexPosition, ushort currentSerieNumber)
		{
			//ImageIndexPosition: values from 0-7
			//CurrentDisplayPositionToFill flowLayoutPanel orders 1234 5678
			//SzondiImagePositionHere: Szondi display order, 1234 8765
			ushort CurrentDisplayPositionToFill = (ushort)(arrayIndexPosition + 1);
			ushort SzondiImagePositionHere = CurrentDisplayPositionToFill;
			/* Better using the Control.Tab property for display order,
			 * and keeping arrayIndex / image name correspondence
			if(CurrentDisplayPositionToFill >= 5)
			{
				SzondiImagePositionHere = 13 - CurrentDisplayPositionToFill;
			}*/
			
			string currentImageName = "s" + currentSerieNumber
				+ "-i" + SzondiImagePositionHere + ".Image";
			return currentImageName;
		}
		
		private void EndInitResumeLayout()
		{
			for(int imageNumber=0; imageNumber < this.imageSplitContainers.Length; imageNumber++)
			{
				Control currentPictureBox
					= this.imageSplitContainers[imageNumber].Panel1.Controls[PictureBoxName];
				((System.ComponentModel.ISupportInitialize)(currentPictureBox)).EndInit();
				this.imageSplitContainers[imageNumber].ResumeLayout(false);
				
			}
			
			this.imagesTableLayoutPanel.ResumeLayout(false);
			this.broadSplitContainer.Panel1.ResumeLayout(false);
			this.broadSplitContainer.Panel2.ResumeLayout(false);
			this.broadSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		
		private System.Windows.Forms.SplitContainer[] imageSplitContainers;
		private System.Windows.Forms.Button goNextSerie;
		private TableLayoutPanel imagesTableLayoutPanel;
		
		private SplitContainer broadSplitContainer;
		//private FlowLayoutPanel navigationFlowLayoutPanel;
		
		private const string PictureBoxName = "PictureBox";
		private const string ComboSympUnsympName = "ComboSympUnsymp";
		private string[] userImageJudgements;
		
		//In Wingdings font, J is :-), C is thumb up
		//L is :-(, D is thumb down
		private const string SympatheticDisplayText = "CJ";
		private const string UnsympatheticDisplayText = "DL";
		
		private System.ComponentModel.ComponentResourceManager resources;
		
	}
}