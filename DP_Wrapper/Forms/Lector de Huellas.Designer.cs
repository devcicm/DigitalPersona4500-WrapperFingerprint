namespace DP_Wrapper
{
    partial class Form1
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
            bttn_CapturarHuella = new Button();
            bttn_VerificarHuella = new Button();
            SuspendLayout();
            // 
            // bttn_CapturarHuella
            // 
            bttn_CapturarHuella.Location = new Point(12, 12);
            bttn_CapturarHuella.Name = "bttn_CapturarHuella";
            bttn_CapturarHuella.Size = new Size(106, 23);
            bttn_CapturarHuella.TabIndex = 0;
            bttn_CapturarHuella.Text = "Capturar Huella";
            bttn_CapturarHuella.UseVisualStyleBackColor = true;
            bttn_CapturarHuella.Click += bttn_CapturarHuella_Click;
            // 
            // bttn_VerificarHuella
            // 
            bttn_VerificarHuella.Location = new Point(124, 12);
            bttn_VerificarHuella.Name = "bttn_VerificarHuella";
            bttn_VerificarHuella.Size = new Size(115, 23);
            bttn_VerificarHuella.TabIndex = 1;
            bttn_VerificarHuella.Text = "Verificar Huella";
            bttn_VerificarHuella.UseVisualStyleBackColor = true;
            bttn_VerificarHuella.Click += bttn_VerificarHuella_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(252, 45);
            Controls.Add(bttn_VerificarHuella);
            Controls.Add(bttn_CapturarHuella);
            Name = "Form1";
            Text = "Capturadora DigitalPersona4500";
            ResumeLayout(false);
        }

        #endregion

        private Button bttn_CapturarHuella;
        private Button bttn_VerificarHuella;
    }
}
