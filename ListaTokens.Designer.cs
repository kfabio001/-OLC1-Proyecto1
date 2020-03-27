namespace Proyecto1
{
    partial class ListaTokens
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
            this.idNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idToken = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idLexema = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.idNo,
            this.idToken,
            this.idLexema,
            this.idTipo,
            this.idFila,
            this.idColumna});
            this.idListView.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::Proyecto1.Properties.Settings.Default, "i", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.idListView.Font = global::Proyecto1.Properties.Settings.Default.i;
            this.idListView.Location = new System.Drawing.Point(12, 48);
            this.idListView.Name = "idListView";
            this.idListView.Size = new System.Drawing.Size(951, 526);
            this.idListView.TabIndex = 0;
            this.idListView.UseCompatibleStateImageBehavior = false;
            this.idListView.View = System.Windows.Forms.View.Details;
            this.idListView.SelectedIndexChanged += new System.EventHandler(this.idListView_SelectedIndexChanged);
            // 
            // idNo
            // 
            this.idNo.Text = "No. Token";
            this.idNo.Width = 107;
            // 
            // idToken
            // 
            this.idToken.Text = "ID Token";
            this.idToken.Width = 149;
            // 
            // idLexema
            // 
            this.idLexema.Text = "Lexema";
            this.idLexema.Width = 193;
            // 
            // idTipo
            // 
            this.idTipo.Text = "Tipo";
            this.idTipo.Width = 278;
            // 
            // idFila
            // 
            this.idFila.Text = "Fila";
            this.idFila.Width = 98;
            // 
            // idColumna
            // 
            this.idColumna.Text = "Columna";
            this.idColumna.Width = 116;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(92, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Listado de token";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListaTokens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 586);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.idListView);
            this.Name = "ListaTokens";
            this.Text = "ListaTokens";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView idListView;
        private System.Windows.Forms.ColumnHeader idNo;
        private System.Windows.Forms.ColumnHeader idToken;
        private System.Windows.Forms.ColumnHeader idLexema;
        private System.Windows.Forms.ColumnHeader idTipo;
        private System.Windows.Forms.ColumnHeader idFila;
        private System.Windows.Forms.ColumnHeader idColumna;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}