using CutMicro.Properties;

namespace CutMicro
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DC = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.FONT = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.FHC = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TC = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.HC = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LC = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BC = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NormalShift = new System.Windows.Forms.RadioButton();
            this.Supershift = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NormalAlt = new System.Windows.Forms.RadioButton();
            this.ReverseAlt = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearAutoSave = new System.Windows.Forms.Button();
            this.AutoSaveSize = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.autoSaveCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.autosaveinterval = new System.Windows.Forms.NumericUpDown();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKbutton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autosaveinterval)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(320, 312);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DC);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.FONT);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.FHC);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.TC);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.HC);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.LC);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.BC);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(312, 286);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cores e texto";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DC
            // 
            this.DC.Location = new System.Drawing.Point(119, 74);
            this.DC.Name = "DC";
            this.DC.Size = new System.Drawing.Size(185, 23);
            this.DC.TabIndex = 15;
            this.DC.UseVisualStyleBackColor = true;
            this.DC.Click += new System.EventHandler(this.BC_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Cor do detalhe";
            // 
            // FONT
            // 
            this.FONT.Location = new System.Drawing.Point(119, 192);
            this.FONT.Name = "FONT";
            this.FONT.Size = new System.Drawing.Size(185, 45);
            this.FONT.TabIndex = 13;
            this.FONT.Text = global::CutMicro.Properties.Resources._1__234AaZz__567x890;
            this.FONT.UseVisualStyleBackColor = true;
            this.FONT.Click += new System.EventHandler(this.FONT_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Fonte";
            // 
            // FHC
            // 
            this.FHC.Location = new System.Drawing.Point(119, 163);
            this.FHC.Name = "FHC";
            this.FHC.Size = new System.Drawing.Size(185, 23);
            this.FHC.TabIndex = 11;
            this.FHC.UseVisualStyleBackColor = true;
            this.FHC.Click += new System.EventHandler(this.BC_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Cor de traço de área livre";
            // 
            // TC
            // 
            this.TC.Location = new System.Drawing.Point(119, 134);
            this.TC.Name = "TC";
            this.TC.Size = new System.Drawing.Size(185, 23);
            this.TC.TabIndex = 7;
            this.TC.UseVisualStyleBackColor = true;
            this.TC.Click += new System.EventHandler(this.BC_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Cor do texto";
            // 
            // HC
            // 
            this.HC.Location = new System.Drawing.Point(119, 105);
            this.HC.Name = "HC";
            this.HC.Size = new System.Drawing.Size(185, 23);
            this.HC.TabIndex = 5;
            this.HC.UseVisualStyleBackColor = true;
            this.HC.Click += new System.EventHandler(this.BC_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Cor do traço";
            // 
            // LC
            // 
            this.LC.Location = new System.Drawing.Point(119, 44);
            this.LC.Name = "LC";
            this.LC.Size = new System.Drawing.Size(185, 23);
            this.LC.TabIndex = 3;
            this.LC.UseVisualStyleBackColor = true;
            this.LC.Click += new System.EventHandler(this.BC_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cor da linha";
            // 
            // BC
            // 
            this.BC.Location = new System.Drawing.Point(119, 15);
            this.BC.Name = "BC";
            this.BC.Size = new System.Drawing.Size(185, 23);
            this.BC.TabIndex = 1;
            this.BC.UseVisualStyleBackColor = true;
            this.BC.Click += new System.EventHandler(this.BC_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cor de fundo";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(312, 286);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Comportamento";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NormalShift);
            this.groupBox2.Controls.Add(this.Supershift);
            this.groupBox2.Location = new System.Drawing.Point(4, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 72);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shift";
            // 
            // NormalShift
            // 
            this.NormalShift.AutoSize = true;
            this.NormalShift.Checked = true;
            this.NormalShift.Location = new System.Drawing.Point(6, 19);
            this.NormalShift.Name = "NormalShift";
            this.NormalShift.Size = new System.Drawing.Size(193, 17);
            this.NormalShift.TabIndex = 4;
            this.NormalShift.TabStop = true;
            this.NormalShift.Text = "Shift força a igronar o corte de linha";
            this.NormalShift.UseVisualStyleBackColor = true;
            // 
            // Supershift
            // 
            this.Supershift.AutoSize = true;
            this.Supershift.Location = new System.Drawing.Point(6, 42);
            this.Supershift.Name = "Supershift";
            this.Supershift.Size = new System.Drawing.Size(188, 17);
            this.Supershift.TabIndex = 5;
            this.Supershift.Text = "Shift força a ignorar o fim do painel";
            this.Supershift.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NormalAlt);
            this.groupBox1.Controls.Add(this.ReverseAlt);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 72);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arrastar e área livre";
            // 
            // NormalAlt
            // 
            this.NormalAlt.AutoSize = true;
            this.NormalAlt.Checked = true;
            this.NormalAlt.Location = new System.Drawing.Point(6, 19);
            this.NormalAlt.Name = "NormalAlt";
            this.NormalAlt.Size = new System.Drawing.Size(332, 17);
            this.NormalAlt.TabIndex = 4;
            this.NormalAlt.TabStop = true;
            this.NormalAlt.Text = "Levar o detalhe mais próximo.  Alt força a encontrar área máxima.";
            this.NormalAlt.UseVisualStyleBackColor = true;
            // 
            // ReverseAlt
            // 
            this.ReverseAlt.AutoSize = true;
            this.ReverseAlt.Location = new System.Drawing.Point(6, 42);
            this.ReverseAlt.Name = "ReverseAlt";
            this.ReverseAlt.Size = new System.Drawing.Size(332, 17);
            this.ReverseAlt.TabIndex = 5;
            this.ReverseAlt.Text = "Encontrar área máxima.  Alt força a levar ao detalhe mais próximo";
            this.ReverseAlt.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(312, 286);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Salvar e carregar";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(0, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(309, 86);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CPXML";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(290, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = global::CutMicro.Properties.Resources.Associate;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ClearAutoSave);
            this.groupBox3.Controls.Add(this.AutoSaveSize);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.autoSaveCheckBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.autosaveinterval);
            this.groupBox3.Location = new System.Drawing.Point(4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 114);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Autosalvar";
            // 
            // ClearAutoSave
            // 
            this.ClearAutoSave.Location = new System.Drawing.Point(220, 71);
            this.ClearAutoSave.Name = "ClearAutoSave";
            this.ClearAutoSave.Size = new System.Drawing.Size(75, 23);
            this.ClearAutoSave.TabIndex = 5;
            this.ClearAutoSave.Text = "Limpar";
            this.ClearAutoSave.UseVisualStyleBackColor = true;
            this.ClearAutoSave.Click += new System.EventHandler(this.ClearAutoSave_Click);
            // 
            // AutoSaveSize
            // 
            this.AutoSaveSize.AutoSize = true;
            this.AutoSaveSize.Location = new System.Drawing.Point(6, 71);
            this.AutoSaveSize.Name = "AutoSaveSize";
            this.AutoSaveSize.Size = new System.Drawing.Size(13, 13);
            this.AutoSaveSize.TabIndex = 4;
            this.AutoSaveSize.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(221, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tamanho atual do diretório de AutoSalvação:";
            // 
            // autoSaveCheckBox
            // 
            this.autoSaveCheckBox.AutoSize = true;
            this.autoSaveCheckBox.Location = new System.Drawing.Point(6, 19);
            this.autoSaveCheckBox.Name = "autoSaveCheckBox";
            this.autoSaveCheckBox.Size = new System.Drawing.Size(112, 17);
            this.autoSaveCheckBox.TabIndex = 0;
            this.autoSaveCheckBox.Text = "Autosalva a cada ";
            this.autoSaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "minutos";
            // 
            // autosaveinterval
            // 
            this.autosaveinterval.Location = new System.Drawing.Point(150, 19);
            this.autosaveinterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.autosaveinterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.autosaveinterval.Name = "autosaveinterval";
            this.autosaveinterval.Size = new System.Drawing.Size(69, 20);
            this.autosaveinterval.TabIndex = 1;
            this.autosaveinterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(166, 320);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancelar";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKbutton
            // 
            this.OKbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKbutton.Location = new System.Drawing.Point(90, 320);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 2;
            this.OKbutton.Text = global::CutMicro.Properties.Resources.OK;
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(5, 320);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Padrão";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Location = new System.Drawing.Point(242, 320);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 1;
            this.button10.Text = "Applicar";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // fontDialog1
            // 
            this.fontDialog1.MaxSize = 14;
            this.fontDialog1.ShowEffects = false;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.OKbutton;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(321, 351);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.Text = "Opções";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autosaveinterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button BC;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button FHC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button TC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button HC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button FONT;
        private System.Windows.Forms.RadioButton ReverseAlt;
        private System.Windows.Forms.RadioButton NormalAlt;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton NormalShift;
        private System.Windows.Forms.RadioButton Supershift;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.NumericUpDown autosaveinterval;
        private System.Windows.Forms.CheckBox autoSaveCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ClearAutoSave;
        private System.Windows.Forms.Label AutoSaveSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button DC;
        private System.Windows.Forms.Label label10;
    }
}