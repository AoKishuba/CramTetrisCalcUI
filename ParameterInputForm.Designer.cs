namespace CramTetrisCalcUi
{
    partial class ParameterInputForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TetrisSizeLabel = new Label();
            TetrisSizeDD = new ComboBox();
            ToolTip = new ToolTip(components);
            LayerCountUD = new NumericUpDown();
            CenteredPrimeConnectorCB = new CheckBox();
            PrimeConnectorXUD = new NumericUpDown();
            PrimeConnectorYUD = new NumericUpDown();
            PrimeConnectorZUD = new NumericUpDown();
            CommaDecimalSeparatorCB = new CheckBox();
            LayerCountLabel = new Label();
            PrimeConnectorCoordPanel = new Panel();
            PrimeConnectorZLabel = new Label();
            PrimeConnectorYLabel = new Label();
            PrimeConnectorXLabel = new Label();
            PrimeConnectorCoordsLabel = new Label();
            RunButton = new Button();
            ((System.ComponentModel.ISupportInitialize)LayerCountUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PrimeConnectorXUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PrimeConnectorYUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PrimeConnectorZUD).BeginInit();
            PrimeConnectorCoordPanel.SuspendLayout();
            SuspendLayout();
            // 
            // TetrisSizeLabel
            // 
            TetrisSizeLabel.AutoSize = true;
            TetrisSizeLabel.Location = new Point(9, 9);
            TetrisSizeLabel.Name = "TetrisSizeLabel";
            TetrisSizeLabel.Size = new Size(60, 15);
            TetrisSizeLabel.TabIndex = 0;
            TetrisSizeLabel.Text = "Turret size";
            // 
            // TetrisSizeDD
            // 
            TetrisSizeDD.DropDownStyle = ComboBoxStyle.DropDownList;
            TetrisSizeDD.FormattingEnabled = true;
            TetrisSizeDD.Location = new Point(89, 5);
            TetrisSizeDD.Name = "TetrisSizeDD";
            TetrisSizeDD.Size = new Size(75, 23);
            TetrisSizeDD.TabIndex = 1;
            ToolTip.SetToolTip(TetrisSizeDD, "Horizontal turret dimensions");
            TetrisSizeDD.SelectedIndexChanged += TetrisSizeDD_SelectedIndexChanged;
            // 
            // LayerCountUD
            // 
            LayerCountUD.Location = new Point(89, 32);
            LayerCountUD.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            LayerCountUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            LayerCountUD.Name = "LayerCountUD";
            LayerCountUD.Size = new Size(40, 23);
            LayerCountUD.TabIndex = 3;
            LayerCountUD.TextAlign = HorizontalAlignment.Right;
            ToolTip.SetToolTip(LayerCountUD, "Number of vertical Tetris layers");
            LayerCountUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            LayerCountUD.ValueChanged += LayerCountUD_ValueChanged;
            // 
            // CenteredPrimeConnectorCB
            // 
            CenteredPrimeConnectorCB.AutoSize = true;
            CenteredPrimeConnectorCB.Checked = true;
            CenteredPrimeConnectorCB.CheckState = CheckState.Checked;
            CenteredPrimeConnectorCB.Location = new Point(9, 59);
            CenteredPrimeConnectorCB.Name = "CenteredPrimeConnectorCB";
            CenteredPrimeConnectorCB.Size = new Size(152, 19);
            CenteredPrimeConnectorCB.TabIndex = 5;
            CenteredPrimeConnectorCB.Text = "Center prime connector";
            ToolTip.SetToolTip(CenteredPrimeConnectorCB, "Leave checked to automatically place the prime connector at top center of the Tetris.\r\nThe prime connector is the connector to which all others in the cannon must be connected.");
            CenteredPrimeConnectorCB.UseVisualStyleBackColor = true;
            CenteredPrimeConnectorCB.CheckedChanged += CenteredPrimeConnectorCB_CheckedChanged;
            // 
            // PrimeConnectorXUD
            // 
            PrimeConnectorXUD.Location = new Point(64, 37);
            PrimeConnectorXUD.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            PrimeConnectorXUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PrimeConnectorXUD.Name = "PrimeConnectorXUD";
            PrimeConnectorXUD.Size = new Size(40, 23);
            PrimeConnectorXUD.TabIndex = 6;
            PrimeConnectorXUD.TextAlign = HorizontalAlignment.Right;
            ToolTip.SetToolTip(PrimeConnectorXUD, "Left-Right");
            PrimeConnectorXUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // PrimeConnectorYUD
            // 
            PrimeConnectorYUD.Location = new Point(64, 67);
            PrimeConnectorYUD.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            PrimeConnectorYUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PrimeConnectorYUD.Name = "PrimeConnectorYUD";
            PrimeConnectorYUD.Size = new Size(40, 23);
            PrimeConnectorYUD.TabIndex = 8;
            PrimeConnectorYUD.TextAlign = HorizontalAlignment.Right;
            ToolTip.SetToolTip(PrimeConnectorYUD, "Vertical Up/Down");
            PrimeConnectorYUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // PrimeConnectorZUD
            // 
            PrimeConnectorZUD.Location = new Point(64, 97);
            PrimeConnectorZUD.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            PrimeConnectorZUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PrimeConnectorZUD.Name = "PrimeConnectorZUD";
            PrimeConnectorZUD.Size = new Size(40, 23);
            PrimeConnectorZUD.TabIndex = 10;
            PrimeConnectorZUD.TextAlign = HorizontalAlignment.Right;
            ToolTip.SetToolTip(PrimeConnectorZUD, "Forward-Backward");
            PrimeConnectorZUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CommaDecimalSeparatorCB
            // 
            CommaDecimalSeparatorCB.AutoSize = true;
            CommaDecimalSeparatorCB.Location = new Point(9, 218);
            CommaDecimalSeparatorCB.Name = "CommaDecimalSeparatorCB";
            CommaDecimalSeparatorCB.Size = new Size(166, 19);
            CommaDecimalSeparatorCB.TabIndex = 6;
            CommaDecimalSeparatorCB.Text = "Comma decimal separator";
            ToolTip.SetToolTip(CommaDecimalSeparatorCB, "Check if your computer displays \"five and three tenths\" as 5,3 (with a comma).");
            CommaDecimalSeparatorCB.UseVisualStyleBackColor = true;
            // 
            // LayerCountLabel
            // 
            LayerCountLabel.AutoSize = true;
            LayerCountLabel.Location = new Point(9, 34);
            LayerCountLabel.Name = "LayerCountLabel";
            LayerCountLabel.Size = new Size(69, 15);
            LayerCountLabel.TabIndex = 2;
            LayerCountLabel.Text = "Layer count";
            // 
            // PrimeConnectorCoordPanel
            // 
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorZUD);
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorZLabel);
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorYUD);
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorYLabel);
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorXUD);
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorXLabel);
            PrimeConnectorCoordPanel.Controls.Add(PrimeConnectorCoordsLabel);
            PrimeConnectorCoordPanel.Location = new Point(9, 84);
            PrimeConnectorCoordPanel.Name = "PrimeConnectorCoordPanel";
            PrimeConnectorCoordPanel.Size = new Size(155, 125);
            PrimeConnectorCoordPanel.TabIndex = 4;
            // 
            // PrimeConnectorZLabel
            // 
            PrimeConnectorZLabel.AutoSize = true;
            PrimeConnectorZLabel.Location = new Point(45, 99);
            PrimeConnectorZLabel.Name = "PrimeConnectorZLabel";
            PrimeConnectorZLabel.Size = new Size(12, 15);
            PrimeConnectorZLabel.TabIndex = 9;
            PrimeConnectorZLabel.Text = "z";
            // 
            // PrimeConnectorYLabel
            // 
            PrimeConnectorYLabel.AutoSize = true;
            PrimeConnectorYLabel.Location = new Point(45, 69);
            PrimeConnectorYLabel.Name = "PrimeConnectorYLabel";
            PrimeConnectorYLabel.Size = new Size(13, 15);
            PrimeConnectorYLabel.TabIndex = 7;
            PrimeConnectorYLabel.Text = "y";
            // 
            // PrimeConnectorXLabel
            // 
            PrimeConnectorXLabel.AutoSize = true;
            PrimeConnectorXLabel.Location = new Point(45, 39);
            PrimeConnectorXLabel.Name = "PrimeConnectorXLabel";
            PrimeConnectorXLabel.Size = new Size(13, 15);
            PrimeConnectorXLabel.TabIndex = 1;
            PrimeConnectorXLabel.Text = "x";
            // 
            // PrimeConnectorCoordsLabel
            // 
            PrimeConnectorCoordsLabel.AutoSize = true;
            PrimeConnectorCoordsLabel.Location = new Point(27, 3);
            PrimeConnectorCoordsLabel.Name = "PrimeConnectorCoordsLabel";
            PrimeConnectorCoordsLabel.Size = new Size(97, 30);
            PrimeConnectorCoordsLabel.TabIndex = 0;
            PrimeConnectorCoordsLabel.Text = "Prime Connector\r\nCoördinates";
            PrimeConnectorCoordsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RunButton
            // 
            RunButton.Location = new Point(50, 247);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(75, 23);
            RunButton.TabIndex = 7;
            RunButton.Text = "Optimize!";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // ParameterInputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(174, 284);
            Controls.Add(RunButton);
            Controls.Add(CommaDecimalSeparatorCB);
            Controls.Add(CenteredPrimeConnectorCB);
            Controls.Add(PrimeConnectorCoordPanel);
            Controls.Add(LayerCountUD);
            Controls.Add(LayerCountLabel);
            Controls.Add(TetrisSizeDD);
            Controls.Add(TetrisSizeLabel);
            Name = "ParameterInputForm";
            Text = "CramCalcUI";
            ((System.ComponentModel.ISupportInitialize)LayerCountUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PrimeConnectorXUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PrimeConnectorYUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PrimeConnectorZUD).EndInit();
            PrimeConnectorCoordPanel.ResumeLayout(false);
            PrimeConnectorCoordPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TetrisSizeLabel;
        private ComboBox TetrisSizeDD;
        private ToolTip ToolTip;
        private Label LayerCountLabel;
        private NumericUpDown LayerCountUD;
        private Panel PrimeConnectorCoordPanel;
        private CheckBox CenteredPrimeConnectorCB;
        private NumericUpDown PrimeConnectorZUD;
        private Label PrimeConnectorZLabel;
        private NumericUpDown PrimeConnectorYUD;
        private Label PrimeConnectorYLabel;
        private NumericUpDown PrimeConnectorXUD;
        private Label PrimeConnectorXLabel;
        private Label PrimeConnectorCoordsLabel;
        private CheckBox CommaDecimalSeparatorCB;
        private Button RunButton;
    }
}