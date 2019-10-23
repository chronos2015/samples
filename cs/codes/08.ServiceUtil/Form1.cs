using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceUtil
{
    public partial class Form1 : Form
    {
        private const string TEAM_CITY = "TeamCity";
        private const string TC_BUILD_AGENT = "TCBuildAgent";
        private const string SONAQ_QUBE = "SonarQube";
        private const string UPSOURCE = "upsource";
        private const string TOMCAT = "tomcat";
        private const string JENKINS = "jenkins";

        public Form1()
        {
            InitializeComponent();
            Point location = this.Location;
            location.X = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width - 30;
            location.Y = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height - 100;
            this.Location = location;
        }

        private static void SetColor(Button target, IEnumerable<ServiceController> query)
        {
            target.Enabled = query.Any();
            if (target.Enabled)
            {
                switch (query.First().Status)
                {
                    case ServiceControllerStatus.Running:
                        target.ForeColor = Color.DarkBlue;
                        break;
                    case ServiceControllerStatus.Stopped:
                        target.ForeColor = Color.Red;
                        break;
                    default:
                        target.ForeColor = Color.Green;
                        break;
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private static IEnumerable<ServiceController> GetService(ServiceController[] services, string name)
        {
            return from i in services where i.ServiceName == name select i;
        }

        private static void ControllService(string name)
        {
            ServiceController[] services = ServiceController.GetServices();
            var service = GetService(services, name).First();
            if (service.Status == ServiceControllerStatus.Stopped)
            {
                service.Start();
            }
            else if (service.Status == ServiceControllerStatus.Running)
            {
                service.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ServiceController[] services = ServiceController.GetServices();

            int size = Process.GetProcessesByName("java").Length + Process.GetProcessesByName("javaw").Length;
            BtnJava.Enabled = size != 0;
            BtnJava.Text = string.Format("java({0})", size);

            SetColor(button6, from i in services where i.ServiceName == "jetty" select i);
            SetColor(BtnTomcat, GetService(services, TOMCAT));
            SetColor(button7, from i in services where i.ServiceName == "glassfish" select i);
            SetColor(button9, from i in services where i.ServiceName == "wildfly" select i);
            SetColor(button10, from i in services where i.ServiceName == "ngix" select i);

            SetColor(BtnJenkins, GetService(services, JENKINS));
            SetColor(BtnTeamCity, GetService(services, TEAM_CITY));
            SetColor(BtnTCBuildAgent, GetService(services, TC_BUILD_AGENT));
            SetColor(BtnSonarQube, GetService(services, SONAQ_QUBE));
            SetColor(Btnupsource, GetService(services, UPSOURCE));
        }

        private void BtnTeamCity_Click(object sender, EventArgs e)
        {
            ControllService(TEAM_CITY);
        }

        private void BtnTCBuildAgent_Click(object sender, EventArgs e)
        {
            ControllService(TC_BUILD_AGENT);
        }

        private void BtnSonarQube_Click(object sender, EventArgs e)
        {
            ControllService(SONAQ_QUBE);
        }

        private void Btnupsource_Click(object sender, EventArgs e)
        {
            ControllService(UPSOURCE);
        }

        private void BtnTomcat_Click(object sender, EventArgs e)
        {
            ControllService(TOMCAT);
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void BtnJava_Click(object sender, EventArgs e)
        {
            foreach(Process p in Process.GetProcessesByName("java"))
            {
                p.Kill();
            }
            foreach (Process p in Process.GetProcessesByName("javaw"))
            {
                p.Kill();
            }
        }

        private void BtnJenkins_Click(object sender, EventArgs e)
        {
            ControllService(JENKINS);
        }
    }
}
