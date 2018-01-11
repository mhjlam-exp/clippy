using System;
using System.Drawing;
using System.Windows.Forms;


namespace Clippy
{
    public partial class ClippyForm : Form
    {
        private Point location;
        private IntPtr nextClipboardViewer;
        private ClippyContext trayApplicationContext;


        public ClippyForm(ClippyContext trayAppContext)
        {
            InitializeComponent();

            this.ShowInTaskbar = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            Rectangle workingArea = Screen.GetWorkingArea(this);
            location = new Point(workingArea.Right - Size.Width - 10, workingArea.Bottom - Size.Height - 10);
            
            this.Location = location;
            this.trayApplicationContext = trayAppContext;
        }
        
        protected override void WndProc(ref Message m)
        {
            switch ((Win32.Msgs)m.Msg)
            {
                // The WM_DRAWCLIPBOARD message is sent to the first window 
                // in the clipboard viewer chain when the content of the 
                // clipboard changes. This enables a clipboard viewer 
                // window to display the new content of the clipboard.
                case Win32.Msgs.WM_DRAWCLIPBOARD:
                {
                    try
                    {
                        IDataObject dataObject = Clipboard.GetDataObject();

                        if (dataObject.GetDataPresent(DataFormats.Text))
                        {
                            string text = (string)dataObject.GetData(DataFormats.Text);

                            if (!ClipsListBox.Items.Contains(text))
                            {
                                ClipsListBox.Items.Add(text);
                                //trayApplicationContext.ShowBalloonTip(text);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }

                    // Each window that receives the WM_DRAWCLIPBOARD message 
                    // must call the SendMessage function to pass the message 
                    // on to the next window in the clipboard viewer chain.
                    Win32.User32.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                }
                break;

                // The WM_CHANGECBCHAIN message is sent to the first window 
                // in the clipboard viewer chain when a window is being removed from the chain. 
                case Win32.Msgs.WM_CHANGECBCHAIN:
                {
                    // When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
                    // it should call the SendMessage function to pass the message to the 
                    // next window in the chain, unless the next window is the window 
                    // being removed. In this case, the clipboard viewer should save 
                    // the handle specified by the lParam parameter as the next window in the chain. 

                    // wParam is the Handle to the window being removed from the clipboard viewer chain 
                    // lParam is the Handle to the next window in the chain following the window being removed
                    if (m.WParam == nextClipboardViewer)
                    {
                        // If wParam is the next clipboard viewer then it is being removed,
                        // so update pointer to the next window in the clipboard chain
                        nextClipboardViewer = m.LParam;
                    }
                    else
                    {
                        Win32.User32.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    }
                }
                break;

                default: // Let the form process the messages that we are not interested in
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            this.Location = location;
            this.Show();
            this.Activate();

            InputTextBox.Focus();

            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            this.Hide();
            base.OnDeactivate(e);
        }

        private void ClipboardForm_Load(object sender, EventArgs e)
        {
            nextClipboardViewer = Win32.User32.SetClipboardViewer(this.Handle);
        }
        
        private void ClipboardForm_Closing(object sender, FormClosingEventArgs e)
        {
            Win32.User32.ChangeClipboardChain(this.Handle, nextClipboardViewer);
        }

        private void ClipsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string item = ClipsListBox.SelectedItem.ToString();
                Clipboard.SetText(item);
            }
            catch (Exception)
            {

            }
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClipsListBox.Items.Add(InputTextBox.Text);
                InputTextBox.Clear();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClipsListBox.Items.Clear();
        }
    }
}
