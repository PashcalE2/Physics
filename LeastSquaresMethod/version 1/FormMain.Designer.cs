namespace LeastSquaresMethod
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.Pic = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.buttonTakeScreenshot = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxDrawLine = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxSmallDash = new System.Windows.Forms.CheckBox();
            this.checkBoxBigDash = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAxisYScale = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAxisXScale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.checkBoxAxisXCross = new System.Windows.Forms.CheckBox();
            this.checkBoxAxisYCross = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pic
            // 
            this.Pic.BackColor = System.Drawing.SystemColors.Window;
            this.Pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic.Location = new System.Drawing.Point(20, 20);
            this.Pic.Name = "Pic";
            this.Pic.Size = new System.Drawing.Size(400, 400);
            this.Pic.TabIndex = 0;
            this.Pic.TabStop = false;
            this.Pic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GetObjInfo);
            this.Pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveCoordCenterStart);
            this.Pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveCoordCenter);
            this.Pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveCoordCenterLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Pic);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 485);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonSaveData);
            this.panel3.Controls.Add(this.buttonTakeScreenshot);
            this.panel3.Controls.Add(this.buttonOpenFile);
            this.panel3.Location = new System.Drawing.Point(20, 440);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 25);
            this.panel3.TabIndex = 16;
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Location = new System.Drawing.Point(160, 0);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(80, 25);
            this.buttonSaveData.TabIndex = 6;
            this.buttonSaveData.Text = "Сохранить";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // buttonTakeScreenshot
            // 
            this.buttonTakeScreenshot.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonTakeScreenshot.Location = new System.Drawing.Point(320, 0);
            this.buttonTakeScreenshot.Name = "buttonTakeScreenshot";
            this.buttonTakeScreenshot.Size = new System.Drawing.Size(80, 25);
            this.buttonTakeScreenshot.TabIndex = 5;
            this.buttonTakeScreenshot.Text = "Скриншот";
            this.buttonTakeScreenshot.UseVisualStyleBackColor = true;
            this.buttonTakeScreenshot.Click += new System.EventHandler(this.buttonTakeScreenshot_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonOpenFile.Location = new System.Drawing.Point(0, 0);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(80, 25);
            this.buttonOpenFile.TabIndex = 4;
            this.buttonOpenFile.Text = "Данные";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.checkBoxAxisYCross);
            this.panel2.Controls.Add(this.checkBoxAxisXCross);
            this.panel2.Controls.Add(this.checkBoxDrawLine);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.checkBoxSmallDash);
            this.panel2.Controls.Add(this.checkBoxBigDash);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBoxAxisYScale);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBoxAxisXScale);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(420, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 295);
            this.panel2.TabIndex = 15;
            // 
            // checkBoxDrawLine
            // 
            this.checkBoxDrawLine.AutoSize = true;
            this.checkBoxDrawLine.Checked = true;
            this.checkBoxDrawLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawLine.Location = new System.Drawing.Point(23, 168);
            this.checkBoxDrawLine.Name = "checkBoxDrawLine";
            this.checkBoxDrawLine.Size = new System.Drawing.Size(88, 17);
            this.checkBoxDrawLine.TabIndex = 24;
            this.checkBoxDrawLine.Text = "Отображать";
            this.checkBoxDrawLine.UseVisualStyleBackColor = true;
            this.checkBoxDrawLine.CheckedChanged += new System.EventHandler(this.DrawLineCheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Прямая";
            // 
            // checkBoxSmallDash
            // 
            this.checkBoxSmallDash.AutoSize = true;
            this.checkBoxSmallDash.Checked = true;
            this.checkBoxSmallDash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSmallDash.Location = new System.Drawing.Point(23, 120);
            this.checkBoxSmallDash.Name = "checkBoxSmallDash";
            this.checkBoxSmallDash.Size = new System.Drawing.Size(97, 17);
            this.checkBoxSmallDash.TabIndex = 22;
            this.checkBoxSmallDash.Text = "Мелкая сетка";
            this.checkBoxSmallDash.UseVisualStyleBackColor = true;
            this.checkBoxSmallDash.CheckedChanged += new System.EventHandler(this.SmallDashCheckedChanged);
            // 
            // checkBoxBigDash
            // 
            this.checkBoxBigDash.AutoSize = true;
            this.checkBoxBigDash.Checked = true;
            this.checkBoxBigDash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBigDash.Location = new System.Drawing.Point(23, 97);
            this.checkBoxBigDash.Name = "checkBoxBigDash";
            this.checkBoxBigDash.Size = new System.Drawing.Size(100, 17);
            this.checkBoxBigDash.TabIndex = 21;
            this.checkBoxBigDash.Text = "Крупная сетка";
            this.checkBoxBigDash.UseVisualStyleBackColor = true;
            this.checkBoxBigDash.CheckedChanged += new System.EventHandler(this.BigDashCheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Сетка";
            // 
            // textBoxAxisYScale
            // 
            this.textBoxAxisYScale.Location = new System.Drawing.Point(86, 45);
            this.textBoxAxisYScale.Name = "textBoxAxisYScale";
            this.textBoxAxisYScale.Size = new System.Drawing.Size(71, 20);
            this.textBoxAxisYScale.TabIndex = 19;
            this.textBoxAxisYScale.Text = "1";
            this.textBoxAxisYScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyEnterAxisYRescale);
            this.textBoxAxisYScale.Leave += new System.EventHandler(this.FocusAxisYRescale);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Ось Y";
            // 
            // textBoxAxisXScale
            // 
            this.textBoxAxisXScale.Location = new System.Drawing.Point(86, 19);
            this.textBoxAxisXScale.Name = "textBoxAxisXScale";
            this.textBoxAxisXScale.Size = new System.Drawing.Size(71, 20);
            this.textBoxAxisXScale.TabIndex = 17;
            this.textBoxAxisXScale.Text = "1";
            this.textBoxAxisXScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyEnterAxisXRescale);
            this.textBoxAxisXScale.Leave += new System.EventHandler(this.FocusAxisXRescale);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Ось X";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Масштаб";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // checkBoxAxisXCross
            // 
            this.checkBoxAxisXCross.AutoSize = true;
            this.checkBoxAxisXCross.Checked = true;
            this.checkBoxAxisXCross.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAxisXCross.Location = new System.Drawing.Point(23, 191);
            this.checkBoxAxisXCross.Name = "checkBoxAxisXCross";
            this.checkBoxAxisXCross.Size = new System.Drawing.Size(147, 17);
            this.checkBoxAxisXCross.TabIndex = 25;
            this.checkBoxAxisXCross.Text = "Пересечение с осью Ox";
            this.checkBoxAxisXCross.UseVisualStyleBackColor = true;
            this.checkBoxAxisXCross.CheckedChanged += new System.EventHandler(this.AxisXCrossCheckedChanged);
            // 
            // checkBoxAxisYCross
            // 
            this.checkBoxAxisYCross.AutoSize = true;
            this.checkBoxAxisYCross.Checked = true;
            this.checkBoxAxisYCross.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAxisYCross.Location = new System.Drawing.Point(23, 214);
            this.checkBoxAxisYCross.Name = "checkBoxAxisYCross";
            this.checkBoxAxisYCross.Size = new System.Drawing.Size(147, 17);
            this.checkBoxAxisYCross.TabIndex = 26;
            this.checkBoxAxisYCross.Text = "Пересечение с осью Oy";
            this.checkBoxAxisYCross.UseVisualStyleBackColor = true;
            this.checkBoxAxisYCross.CheckedChanged += new System.EventHandler(this.AxisYCrossCheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(620, 505);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Метод наименьших квадратов";
            this.Load += new System.EventHandler(this.FullLoad);
            this.SizeChanged += new System.EventHandler(this.Resizing);
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Pic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.Button buttonTakeScreenshot;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxDrawLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxSmallDash;
        private System.Windows.Forms.CheckBox checkBoxBigDash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAxisYScale;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAxisXScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAxisYCross;
        private System.Windows.Forms.CheckBox checkBoxAxisXCross;
    }
}

