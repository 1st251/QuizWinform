namespace QuizWinform
{
    partial class Quiz
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
            cbA = new CheckBox();
            cbB = new CheckBox();
            cbD = new CheckBox();
            cbC = new CheckBox();
            groupBox1 = new GroupBox();
            btnFinish = new Button();
            label1 = new Label();
            cbWarning = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            lbTime = new Label();
            lbExamCode = new Label();
            lbStudent = new Label();
            lbChoice = new Label();
            btnNext = new Button();
            btnQuest = new Button();
            SuspendLayout();
            // 
            // cbA
            // 
            cbA.AutoSize = true;
            cbA.Location = new Point(29, 175);
            cbA.Name = "cbA";
            cbA.Size = new Size(34, 19);
            cbA.TabIndex = 0;
            cbA.Text = "A";
            cbA.UseVisualStyleBackColor = true;
            // 
            // cbB
            // 
            cbB.AutoSize = true;
            cbB.Location = new Point(29, 224);
            cbB.Name = "cbB";
            cbB.Size = new Size(33, 19);
            cbB.TabIndex = 1;
            cbB.Text = "B";
            cbB.UseVisualStyleBackColor = true;
            // 
            // cbD
            // 
            cbD.AutoSize = true;
            cbD.Location = new Point(29, 323);
            cbD.Name = "cbD";
            cbD.Size = new Size(34, 19);
            cbD.TabIndex = 3;
            cbD.Text = "D";
            cbD.UseVisualStyleBackColor = true;
            // 
            // cbC
            // 
            cbC.AutoSize = true;
            cbC.Location = new Point(29, 274);
            cbC.Name = "cbC";
            cbC.Size = new Size(34, 19);
            cbC.TabIndex = 2;
            cbC.Text = "C";
            cbC.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(152, 97);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1363, 547);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(1371, 687);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(97, 33);
            btnFinish.TabIndex = 5;
            btnFinish.Text = "Finish";
            btnFinish.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 106);
            label1.Name = "label1";
            label1.Size = new Size(86, 30);
            label1.TabIndex = 6;
            label1.Text = "Answer";
            // 
            // cbWarning
            // 
            cbWarning.AutoSize = true;
            cbWarning.ForeColor = Color.FromArgb(0, 0, 192);
            cbWarning.Location = new Point(1371, 662);
            cbWarning.Name = "cbWarning";
            cbWarning.Size = new Size(144, 19);
            cbWarning.TabIndex = 7;
            cbWarning.Text = "I want finish this exam";
            cbWarning.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(154, 7);
            label2.Name = "label2";
            label2.Size = new Size(94, 21);
            label2.TabIndex = 8;
            label2.Text = "Exam Code: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(154, 47);
            label3.Name = "label3";
            label3.Size = new Size(66, 21);
            label3.TabIndex = 9;
            label3.Text = "Student:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(553, 47);
            label4.Name = "label4";
            label4.Size = new Size(73, 21);
            label4.TabIndex = 10;
            label4.Text = "Time left:";
            // 
            // lbTime
            // 
            lbTime.AutoSize = true;
            lbTime.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTime.ForeColor = Color.Red;
            lbTime.Location = new Point(642, 38);
            lbTime.Name = "lbTime";
            lbTime.Size = new Size(100, 40);
            lbTime.TabIndex = 11;
            lbTime.Text = "label5";
            lbTime.Click += label5_Click;
            // 
            // lbExamCode
            // 
            lbExamCode.AutoSize = true;
            lbExamCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbExamCode.Location = new Point(254, 7);
            lbExamCode.Name = "lbExamCode";
            lbExamCode.Size = new Size(0, 21);
            lbExamCode.TabIndex = 12;
            // 
            // lbStudent
            // 
            lbStudent.AutoSize = true;
            lbStudent.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbStudent.Location = new Point(226, 47);
            lbStudent.Name = "lbStudent";
            lbStudent.Size = new Size(0, 21);
            lbStudent.TabIndex = 13;
            // 
            // lbChoice
            // 
            lbChoice.AutoSize = true;
            lbChoice.Location = new Point(154, 77);
            lbChoice.Name = "lbChoice";
            lbChoice.Size = new Size(0, 15);
            lbChoice.TabIndex = 14;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(12, 363);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(86, 28);
            btnNext.TabIndex = 15;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnQuest
            // 
            btnQuest.Location = new Point(152, 660);
            btnQuest.Name = "btnQuest";
            btnQuest.Size = new Size(28, 27);
            btnQuest.TabIndex = 16;
            btnQuest.Text = "1";
            btnQuest.UseVisualStyleBackColor = true;
            // 
            // Quiz
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1581, 812);
            Controls.Add(btnQuest);
            Controls.Add(btnNext);
            Controls.Add(lbChoice);
            Controls.Add(lbStudent);
            Controls.Add(lbExamCode);
            Controls.Add(lbTime);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cbWarning);
            Controls.Add(label1);
            Controls.Add(btnFinish);
            Controls.Add(groupBox1);
            Controls.Add(cbD);
            Controls.Add(cbC);
            Controls.Add(cbB);
            Controls.Add(cbA);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Quiz";
            Text = "Quiz";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            Load += Quiz_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cbA;
        private CheckBox cbB;
        private CheckBox cbD;
        private CheckBox cbC;
        private GroupBox groupBox1;
        private Button btnFinish;
        private Label label1;
        private CheckBox cbWarning;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lbTime;
        private Label lbExamCode;
        private Label lbStudent;
        private Label lbChoice;
        private Button btnNext;
        private Button btnQuest;
    }
}