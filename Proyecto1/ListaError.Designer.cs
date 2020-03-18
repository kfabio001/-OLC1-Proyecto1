namespace Proyecto1
{
    partial class ListaError
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
            this.idListView = new System.Windows.Forms.ListView();
            this.idError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idToken = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idFila = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idColumna = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // idListView
            // 
            this.idListView.BackColor = System.Drawing.SystemColors.Menu;
            this.idListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idError,
            this.idToken,
            this.idDescripcion,
            this.idFila,
            this.idColumna});
            this.idListView.Font = new System.Drawing.Font("Candara", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idListView.Location = new System.Drawing.Point(12, 40);
            this.idListView.Name = "idListView";
            this.idListView.Size = new System.Drawing.Size(873, 428);
            this.idListView.TabIndex = 0;
            this.idListView.UseCompatibleStateImageBehavior = false;
            this.idListView.View = System.Windows.Forms.View.Details;
            this.idListView.SelectedIndexChanged += new System.EventHandler(this.idListView_SelectedIndexChanged);
            // 
            // idError
            // 
            this.idError.Text = "No";
            // 
            // idToken
            // 
            this.idToken.Text = "Error";
            this.idToken.Width = 272;
            // 
            // idDescripcion
            // 
            this.idDescripcion.Text = "Descipcion";
            this.idDescripcion.Width = 353;
            // 
            // idFila
            // 
            this.idFila.Text = "Fila";
            this.idFila.Width = 76;
            // 
            // idColumna
            // 
            this.idColumna.Text = "Columna";
            this.idColumna.Width = 91;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Listado de errores";
            //this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(487, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListaError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 480);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.idListView);
            this.Name = "ListaError";
            this.Text = "ListaError";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView idListView;
        private System.Windows.Forms.ColumnHeader idError;
        private System.Windows.Forms.ColumnHeader idToken;
        private System.Windows.Forms.ColumnHeader idDescripcion;
        private System.Windows.Forms.ColumnHeader idFila;
        private System.Windows.Forms.ColumnHeader idColumna;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}