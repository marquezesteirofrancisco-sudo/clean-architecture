namespace Proyecto_Pruebas
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
            btnListar = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnListar
            // 
            btnListar.Location = new Point(41, 72);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(198, 79);
            btnListar.TabIndex = 0;
            btnListar.Text = "Listar Cervezas";
            btnListar.UseVisualStyleBackColor = true;
            btnListar.Click += btnListar_Click;
            // 
            // button1
            // 
            button1.Location = new Point(282, 72);
            button1.Name = "button1";
            button1.Size = new Size(208, 79);
            button1.TabIndex = 1;
            button1.Text = "Añadir Cervezas";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 372);
            Controls.Add(button1);
            Controls.Add(btnListar);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnListar;
        private Button button1;
    }
}
