﻿namespace PPAI2024
{
    partial class FormImportaciones
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnImportarActualizaciones = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportarActualizaciones
            // 
            this.btnImportarActualizaciones.Location = new System.Drawing.Point(115, 123);
            this.btnImportarActualizaciones.Name = "btnImportarActualizaciones";
            this.btnImportarActualizaciones.Size = new System.Drawing.Size(208, 48);
            this.btnImportarActualizaciones.TabIndex = 0;
            this.btnImportarActualizaciones.Text = "Importar Actualizacion de vinos Bodega";
            this.btnImportarActualizaciones.UseVisualStyleBackColor = true;
            this.btnImportarActualizaciones.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormImportaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 293);
            this.Controls.Add(this.btnImportarActualizaciones);
            this.Name = "FormImportaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario Importaciones";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportarActualizaciones;
    }
}

