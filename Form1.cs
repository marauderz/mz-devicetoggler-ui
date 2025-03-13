using System.Management;
using System.Security.Principal;



namespace DeviceTogglerUI
{
    public partial class Form1 : Form
    {
        private List<ManagementObject> devicelist = new List<ManagementObject>();
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void PopulateDevices(string findname, bool manualinvoke)
        {
            string query = "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%" + findname + "%'";
            
            devicelist.Clear();
            cboDevices.Items.Clear();

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                var deviceresults = searcher.Get();
                if (deviceresults.Count == 0)
                {
                    btnToggle.BackColor = Color.Yellow;
                    if (manualinvoke)
                    {
                        MessageBox.Show("No devices found matching pattern!", "Darn", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                    return;
                }

                foreach (ManagementObject device in deviceresults)
                {
                    //Add the names in
                    cboDevices.Items.Add(device["Name"]);
                    devicelist.Add(device);
                }

                cboDevices.SelectedIndex = 0;
            }
        }

        private void btnFindDevices_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDevices.Text))
            {
                MessageBox.Show("Enter a name to search for or all the devices will be listed!",
                    "Come on!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PopulateDevices(cboDevices.Text, true);
        }

        private void cboDevices_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (cboDevices.SelectedIndex != -1)
            {
                //get status of the corresponding device
                var targetdevice = devicelist[cboDevices.SelectedIndex];
                if (targetdevice["Status"].ToString() == "OK")
                {
                    btnToggle.BackColor = Color.Lime;
                }
                else
                {
                    btnToggle.BackColor = Color.Salmon;
                }
            }
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (cboDevices.SelectedIndex == -1)
            {
                MessageBox.Show("No device is selected, or device not found!", "Darn", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                return;
            }

            if (!IsRunningAsAdmin())
            {
                MessageBox.Show("Can only toggle if running as admin!", "Darn", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Exclamation);
                return;
            }

            var targetdevice = devicelist[cboDevices.SelectedIndex];
            try
            {

                if (targetdevice["Status"].ToString() == "OK")
                {

                    try
                    {
                        targetdevice.InvokeMethod("Disable", null);
                    }
                    catch (System.NullReferenceException nullex)
                    {
                        //There's an error that occurs internally
                        //with the system.management classes
                        //that causes an error trying to set the output of the invocation
                    }
                    catch
                    {
                        throw;
                    }
                    PopulateDevices(cboDevices.Text, false);
                    SaveSettings();
                    MessageBox.Show("Device Probably Disabled!", "OK?", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    return;
                }

                //If it's not enabled try enabling it
                {

                    try
                    {
                        targetdevice.InvokeMethod("Enable", null);
                    }
                    catch (System.NullReferenceException nullex)
                    {
                        //There's an error that occurs internally
                        //with the system.management classes
                        //that causes an error trying to set the output of the invocation
                    }
                    catch
                    {
                        throw;
                    }
                    PopulateDevices(cboDevices.Text, false);
                    SaveSettings();
                    MessageBox.Show("Device Probably Enabled!", "OK?", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something failed!", "Darn", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Exclamation);
            }
        }

        static bool IsRunningAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// Save the last name into the settings
        /// </summary>
        void SaveSettings()
        {
            var path = System.IO.Path.Combine(AppContext.BaseDirectory, $"{System.IO.Path.GetFileName(Application.ExecutablePath)}.settings");

            var osettings = new System.Text.Json.Nodes.JsonObject();
            osettings["devicename"] = cboDevices.Text;

            System.IO.File.WriteAllText(path, osettings.ToJsonString());
        }

        void LoadSettings()
        {
            var path = System.IO.Path.Combine(AppContext.BaseDirectory, $"{System.IO.Path.GetFileName(Application.ExecutablePath)}.settings");
            if (System.IO.File.Exists(path))
            {
                var osettings = System.Text.Json.Nodes.JsonObject.Parse(System.IO.File.ReadAllText(path));

                var devicename = osettings["devicename"]?.ToString();

                if (!string.IsNullOrEmpty(devicename))
                {
                    cboDevices.Text = devicename;
                    PopulateDevices(devicename, false);
                }
            }
        }

        

        private void cboDevices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { 
                btnFindDevices.PerformClick();
            }

        }
    }
}
