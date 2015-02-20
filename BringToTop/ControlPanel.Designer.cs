namespace BringToTop
{
    partial class ControlPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonToTop = new System.Windows.Forms.Button();
            this.listViewProcesses = new System.Windows.Forms.ListView();
            this.columnHeaderTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClassName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderWindowHandle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderZorder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.groupBoxProcesses = new System.Windows.Forms.GroupBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxForce = new System.Windows.Forms.CheckBox();
            this.groupBoxProcesses.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonToTop
            // 
            this.buttonToTop.Location = new System.Drawing.Point(389, 314);
            this.buttonToTop.Name = "buttonToTop";
            this.buttonToTop.Size = new System.Drawing.Size(75, 23);
            this.buttonToTop.TabIndex = 0;
            this.buttonToTop.Text = "To Top";
            this.buttonToTop.UseVisualStyleBackColor = true;
            this.buttonToTop.Click += new System.EventHandler(this.buttonToTop_Click);
            // 
            // listViewProcesses
            // 
            this.listViewProcesses.CheckBoxes = true;
            this.listViewProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTitle,
            this.columnHeaderClassName,
            this.columnHeaderWindowHandle,
            this.columnHeaderZorder});
            this.listViewProcesses.Location = new System.Drawing.Point(6, 18);
            this.listViewProcesses.Name = "listViewProcesses";
            this.listViewProcesses.Size = new System.Drawing.Size(458, 290);
            this.listViewProcesses.TabIndex = 1;
            this.listViewProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewProcesses.View = System.Windows.Forms.View.Details;
            this.listViewProcesses.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewProcesses_ItemChecked);
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 131;
            // 
            // columnHeaderClassName
            // 
            this.columnHeaderClassName.Text = "ClassName";
            this.columnHeaderClassName.Width = 165;
            // 
            // columnHeaderWindowHandle
            // 
            this.columnHeaderWindowHandle.Text = "WindowHandle";
            // 
            // columnHeaderZorder
            // 
            this.columnHeaderZorder.Text = "ZOrder";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(198, 314);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // groupBoxProcesses
            // 
            this.groupBoxProcesses.Controls.Add(this.listViewProcesses);
            this.groupBoxProcesses.Controls.Add(this.buttonRefresh);
            this.groupBoxProcesses.Controls.Add(this.buttonToTop);
            this.groupBoxProcesses.Location = new System.Drawing.Point(3, 3);
            this.groupBoxProcesses.Name = "groupBoxProcesses";
            this.groupBoxProcesses.Size = new System.Drawing.Size(470, 343);
            this.groupBoxProcesses.TabIndex = 2;
            this.groupBoxProcesses.TabStop = false;
            this.groupBoxProcesses.Text = "Processes";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBoxForce);
            this.groupBoxOptions.Location = new System.Drawing.Point(479, 3);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(230, 42);
            this.groupBoxOptions.TabIndex = 3;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // checkBoxForce
            // 
            this.checkBoxForce.AutoSize = true;
            this.checkBoxForce.Checked = true;
            this.checkBoxForce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxForce.Location = new System.Drawing.Point(6, 18);
            this.checkBoxForce.Name = "checkBoxForce";
            this.checkBoxForce.Size = new System.Drawing.Size(152, 16);
            this.checkBoxForce.TabIndex = 0;
            this.checkBoxForce.Text = "Forced to run at ID start.";
            this.checkBoxForce.UseVisualStyleBackColor = true;
            this.checkBoxForce.CheckedChanged += new System.EventHandler(this.checkBoxForce_CheckedChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.groupBoxProcesses);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(712, 351);
            this.groupBoxProcesses.ResumeLayout(false);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonToTop;
        private System.Windows.Forms.ListView listViewProcesses;
        private System.Windows.Forms.ColumnHeader columnHeaderTitle;
        private System.Windows.Forms.ColumnHeader columnHeaderClassName;
        private System.Windows.Forms.ColumnHeader columnHeaderWindowHandle;
        private System.Windows.Forms.ColumnHeader columnHeaderZorder;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.GroupBox groupBoxProcesses;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxForce;
    }
}
