namespace Utilities.Types.Tracer
{
    partial class TraceDialog
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
            this.btn_Next = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_CompleteTracing = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_IgnoreMember = new System.Windows.Forms.Button();
            this.btn_IgnoreObject = new System.Windows.Forms.Button();
            this.btn_IgnoreLine = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Object = new System.Windows.Forms.Label();
            this.lbl_Member = new System.Windows.Forms.Label();
            this.lbl_Line = new System.Windows.Forms.Label();
            this.lbl_Commentary = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(12, 97);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(75, 81);
            this.btn_Next.TabIndex = 0;
            this.btn_Next.Text = "Далее";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Объект:";
            // 
            // btn_CompleteTracing
            // 
            this.btn_CompleteTracing.Location = new System.Drawing.Point(12, 184);
            this.btn_CompleteTracing.Name = "btn_CompleteTracing";
            this.btn_CompleteTracing.Size = new System.Drawing.Size(260, 23);
            this.btn_CompleteTracing.TabIndex = 2;
            this.btn_CompleteTracing.Text = "Завершить трассировку";
            this.btn_CompleteTracing.UseVisualStyleBackColor = true;
            this.btn_CompleteTracing.Click += new System.EventHandler(this.btn_CompleteTracing_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Член:";
            // 
            // btn_IgnoreMember
            // 
            this.btn_IgnoreMember.Location = new System.Drawing.Point(93, 126);
            this.btn_IgnoreMember.Name = "btn_IgnoreMember";
            this.btn_IgnoreMember.Size = new System.Drawing.Size(179, 23);
            this.btn_IgnoreMember.TabIndex = 4;
            this.btn_IgnoreMember.Text = "Игнорировать член";
            this.btn_IgnoreMember.UseVisualStyleBackColor = true;
            this.btn_IgnoreMember.Click += new System.EventHandler(this.btn_IgnoreMember_Click);
            // 
            // btn_IgnoreObject
            // 
            this.btn_IgnoreObject.Location = new System.Drawing.Point(93, 97);
            this.btn_IgnoreObject.Name = "btn_IgnoreObject";
            this.btn_IgnoreObject.Size = new System.Drawing.Size(179, 23);
            this.btn_IgnoreObject.TabIndex = 5;
            this.btn_IgnoreObject.Text = "Игнорировать объект";
            this.btn_IgnoreObject.UseVisualStyleBackColor = true;
            this.btn_IgnoreObject.Click += new System.EventHandler(this.btn_IgnoreObject_Click);
            // 
            // btn_IgnoreLine
            // 
            this.btn_IgnoreLine.Location = new System.Drawing.Point(93, 155);
            this.btn_IgnoreLine.Name = "btn_IgnoreLine";
            this.btn_IgnoreLine.Size = new System.Drawing.Size(179, 23);
            this.btn_IgnoreLine.TabIndex = 6;
            this.btn_IgnoreLine.Text = "Игнорировать строку";
            this.btn_IgnoreLine.UseVisualStyleBackColor = true;
            this.btn_IgnoreLine.Click += new System.EventHandler(this.btn_IgnoreLine_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Строка:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Комментарий:";
            // 
            // lbl_Object
            // 
            this.lbl_Object.AutoSize = true;
            this.lbl_Object.Location = new System.Drawing.Point(90, 13);
            this.lbl_Object.Name = "lbl_Object";
            this.lbl_Object.Size = new System.Drawing.Size(25, 13);
            this.lbl_Object.TabIndex = 9;
            this.lbl_Object.Text = "      ";
            // 
            // lbl_Member
            // 
            this.lbl_Member.AutoSize = true;
            this.lbl_Member.Location = new System.Drawing.Point(90, 26);
            this.lbl_Member.Name = "lbl_Member";
            this.lbl_Member.Size = new System.Drawing.Size(25, 13);
            this.lbl_Member.TabIndex = 10;
            this.lbl_Member.Text = "      ";
            // 
            // lbl_Line
            // 
            this.lbl_Line.AutoSize = true;
            this.lbl_Line.Location = new System.Drawing.Point(90, 39);
            this.lbl_Line.Name = "lbl_Line";
            this.lbl_Line.Size = new System.Drawing.Size(25, 13);
            this.lbl_Line.TabIndex = 11;
            this.lbl_Line.Text = "      ";
            // 
            // lbl_Commentary
            // 
            this.lbl_Commentary.AutoSize = true;
            this.lbl_Commentary.Location = new System.Drawing.Point(90, 61);
            this.lbl_Commentary.Name = "lbl_Commentary";
            this.lbl_Commentary.Size = new System.Drawing.Size(25, 13);
            this.lbl_Commentary.TabIndex = 12;
            this.lbl_Commentary.Text = "      ";
            // 
            // TraceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 218);
            this.Controls.Add(this.lbl_Commentary);
            this.Controls.Add(this.lbl_Line);
            this.Controls.Add(this.lbl_Member);
            this.Controls.Add(this.lbl_Object);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_IgnoreLine);
            this.Controls.Add(this.btn_IgnoreObject);
            this.Controls.Add(this.btn_IgnoreMember);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_CompleteTracing);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Next);
            this.Name = "TraceDialog";
            this.Text = "Трассировка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_CompleteTracing;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_IgnoreMember;
        private System.Windows.Forms.Button btn_IgnoreObject;
        private System.Windows.Forms.Button btn_IgnoreLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Object;
        private System.Windows.Forms.Label lbl_Member;
        private System.Windows.Forms.Label lbl_Line;
        private System.Windows.Forms.Label lbl_Commentary;
    }
}