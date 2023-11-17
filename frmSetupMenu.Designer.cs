namespace ProgramManageMenu
{
    partial class frmSetupMenu
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

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			treView = new System.Windows.Forms.TreeView();
			butCancel = new System.Windows.Forms.Button();
			butSave = new System.Windows.Forms.Button();
			chkChild = new System.Windows.Forms.CheckBox();
			butShow = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			SuspendLayout();
			treView.Location = new System.Drawing.Point(21, 22);
			treView.Name = "treView";
			treView.Size = new System.Drawing.Size(335, 429);
			treView.TabIndex = 0;
			treView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(treView_AfterCheck);
			treView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treView_AfterSelect);
			treView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(treView_NodeMouseDoubleClick);
			treView.DoubleClick += new System.EventHandler(treView_DoubleClick);
			butCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			butCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			butCancel.Location = new System.Drawing.Point(263, 492);
			butCancel.Name = "butCancel";
			butCancel.Size = new System.Drawing.Size(78, 31);
			butCancel.TabIndex = 3;
			butCancel.Text = "Отмена";
			toolTip1.SetToolTip(butCancel, "Отказ от сделанных изменений видимости элементов меню");
			butCancel.UseVisualStyleBackColor = false;
			butCancel.Click += new System.EventHandler(butCancel_Click);
			butSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
			butSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			butSave.Location = new System.Drawing.Point(32, 492);
			butSave.Name = "butSave";
			butSave.Size = new System.Drawing.Size(98, 31);
			butSave.TabIndex = 4;
			butSave.Text = "Сохранить";
			toolTip1.SetToolTip(butSave, "Сохраняет настройки меню для последующих запусков Revit");
			butSave.UseVisualStyleBackColor = false;
			butSave.Click += new System.EventHandler(butSave_Click);
			chkChild.AutoSize = true;
			chkChild.Checked = true;
			chkChild.CheckState = System.Windows.Forms.CheckState.Checked;
			chkChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			chkChild.Location = new System.Drawing.Point(32, 457);
			chkChild.Name = "chkChild";
			chkChild.Size = new System.Drawing.Size(264, 19);
			chkChild.TabIndex = 1;
			chkChild.Text = "применять видимость к дочерним узлам";
			toolTip1.SetToolTip(chkChild, "При изменении видимости элемента применить ее ко всем дочерним узлам");
			chkChild.UseVisualStyleBackColor = true;
			butShow.BackColor = System.Drawing.SystemColors.ControlLightLight;
			butShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			butShow.Location = new System.Drawing.Point(176, 492);
			butShow.Name = "butShow";
			butShow.Size = new System.Drawing.Size(74, 31);
			butShow.TabIndex = 2;
			butShow.Text = "Готово";
			toolTip1.SetToolTip(butShow, "Сохраняет настройки меню для текущего сеанса работы с Revit");
			butShow.UseVisualStyleBackColor = false;
			butShow.Click += new System.EventHandler(butShow_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(380, 535);
			base.Controls.Add(butShow);
			base.Controls.Add(chkChild);
			base.Controls.Add(butSave);
			base.Controls.Add(butCancel);
			base.Controls.Add(treView);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmSetupMenu";
			Text = "Настройка видимости элементов меню";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmSetupMenu_FormClosing);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
	}
}