using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EmailSpamDetection
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private readonly HttpClient _httpClient = new HttpClient();
        private  async void button1_ClickAsync(object sender, EventArgs e)
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
                            lblResult.BackColor = Color.Green;
                            lblResult.Visible = true;
                        }
                        else
                        {
                            lblResult.Text = "It is spam";
                            lblResult.BackColor = Color.Red;
                            lblResult.Visible = true;
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


        }
    }
