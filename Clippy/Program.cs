using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clippy
{
	public class ClippyContext : ApplicationContext
	{
		public NotifyIcon NotifyIcon { get; set; }
		private ClippyForm clipboardForm;
		private IContainer components;  // a list of components to dispose when the context is disposed

		public ClippyContext()
		{
			components = new Container();

			// Create NotifyIcon
			NotifyIcon = new NotifyIcon(components)
			{
				ContextMenuStrip = new ContextMenuStrip(),
				Icon = Properties.Resources.clipboard,
				Text = "Clippy",
				Visible = true
			};

			// Add EventHanders
			NotifyIcon.Click += new EventHandler(ShowClipboard);

			// Add ContextMenu
			MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));
			NotifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { exitMenuItem });

			// Create ClipboardForm
			clipboardForm = new ClippyForm(this);
		}

		public void ShowBalloonTip(string text)
		{
			NotifyIcon.BalloonTipText = "Copied " + text;
			//NotifyIcon.BalloonTipTitle = "Title";
			NotifyIcon.Visible = true;
			NotifyIcon.ShowBalloonTip(3000);
		}

		private void ShowClipboard(object sender, EventArgs args)
		{
			clipboardForm.ShowDialog();
		}

		private void Exit(object sender, EventArgs e)
		{
			NotifyIcon.Visible = false;
			Application.Exit();
		}
	}

	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ClippyContext());
		}
	}
}
