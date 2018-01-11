namespace Clippy
{
	partial class ClippyForm
	{
		private System.Windows.Forms.ListBox ClipsListBox;
		private System.Windows.Forms.TextBox InputTextBox;
		private System.Windows.Forms.Button ClearButton;

		private System.ComponentModel.IContainer components = null;


		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClippyForm));
			this.ClipsListBox = new System.Windows.Forms.ListBox();
			this.InputTextBox = new System.Windows.Forms.TextBox();
			this.ClearButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ClipsListBox
			// 
			this.ClipsListBox.FormattingEnabled = true;
			this.ClipsListBox.Location = new System.Drawing.Point(12, 12);
			this.ClipsListBox.Name = "ClipsListBox";
			this.ClipsListBox.Size = new System.Drawing.Size(263, 212);
			this.ClipsListBox.TabIndex = 0;
			this.ClipsListBox.SelectedIndexChanged += new System.EventHandler(this.ClipsListBox_SelectedIndexChanged);
			// 
			// InputTextBox
			// 
			this.InputTextBox.Location = new System.Drawing.Point(13, 235);
			this.InputTextBox.Name = "InputTextBox";
			this.InputTextBox.Size = new System.Drawing.Size(177, 22);
			this.InputTextBox.TabIndex = 1;
			InputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(InputTextBox_KeyDown);
			// 
			// ClearButton
			// 
			this.ClearButton.Location = new System.Drawing.Point(201, 235);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new System.Drawing.Size(75, 23);
			this.ClearButton.TabIndex = 2;
			this.ClearButton.Text = "Clear";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// ClipboardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(288, 269);
			this.ControlBox = false;
			this.Controls.Add(this.ClearButton);
			this.Controls.Add(this.InputTextBox);
			this.Controls.Add(this.ClipsListBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ClipboardForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClipboardForm_Closing);
			this.Load += new System.EventHandler(this.ClipboardForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

