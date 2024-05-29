namespace PPAI2024
{
    partial class InterfazImportadorBodega
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
            this.grillaBodegas = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grillaBodegas)).BeginInit();
            this.SuspendLayout();
            // 
            // grillaBodegas
            // 
            this.grillaBodegas.AllowUserToAddRows = false;
            this.grillaBodegas.AllowUserToDeleteRows = false;
            this.grillaBodegas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaBodegas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre});
            this.grillaBodegas.Location = new System.Drawing.Point(12, 12);
            this.grillaBodegas.Name = "grillaBodegas";
            this.grillaBodegas.ReadOnly = true;
            this.grillaBodegas.Size = new System.Drawing.Size(464, 260);
            this.grillaBodegas.TabIndex = 0;
            this.grillaBodegas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grillaBodegas_CellDoubleClick);
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // InterfazImportadorBodega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grillaBodegas);
            this.Name = "InterfazImportadorBodega";
            this.Text = "FormularioBodegas";
            ((System.ComponentModel.ISupportInitialize)(this.grillaBodegas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grillaBodegas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
    }
}