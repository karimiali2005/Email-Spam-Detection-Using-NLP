using System;
using System.Drawing;
using System.Net.Http;
using System.Security.Principal;
using System.Windows.Forms;


namespace ShopBorhanServiceConfig
{
    public partial class FrmMain : Form
    {
        private bool dragging;
        private Point pointClicked;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnInstallService_Click(object sender, EventArgs e)
        {

          
        }

        private void BtnServiceStart_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnServiceStop_Click(object sender, EventArgs e)
        {
          
        }

        private readonly HttpClient _httpClient = new HttpClient();
        private async void BtnServiceRestart_Click(object sender, EventArgs e)
        {
            try
            {
                // Set up the request URL with query parameters
                var apiUrl = "http://127.0.0.1:5000/queryparams";
                var param1 = txtName.Text;

                var url = $"{apiUrl}?name={param1}";

                // Send the GET request
                var response = await _httpClient.GetAsync(url);

                // Check if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Do something with the response content, like update a label
                    if (responseContent != null)
                    {
                        if (responseContent == "0")
                        {
                            lblResult.Text = "It is safe";
                            lblResult.ForeColor = Color.FromArgb(43, 174, 148);
                            lblResult.Visible = true;
                            pic.Visible = true;
                        }
                        else
                        {
                            lblResult.Text = "It is spam";
                            lblResult.ForeColor = Color.Red;
                            lblResult.Visible = true;
                            pic.Visible = false;
                        }

                    }
                    else
                        MessageBox.Show("Error");

                }
                else
                {
                    // Handle the error response
                    MessageBox.Show($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtName.ScrollBars = ScrollBars.Vertical;

           
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point pointMoveTo;
                // Find the current mouse position in screen coordinates.
                pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));
                // Compensate for the position the control was clicked.
                pointMoveTo.Offset(-pointClicked.X, -pointClicked.Y);
                // Move the form.
                this.Location = pointMoveTo;
            }
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Turn drag mode on and store the point clicked.
                dragging = true;
                pointClicked = new Point(e.X, e.Y);
            }
            else
            {
                dragging = false;
            }
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
