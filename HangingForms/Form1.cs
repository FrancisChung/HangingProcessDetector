using HangingProcessDetector;

namespace HangingForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void HangProcessButton_Click(object sender, EventArgs e)
        {
            HangingProcess.StartThreadSleep();
        }

        private void DetectorButton_Click(object sender, EventArgs e)
        {
            string process = "HangingForms";
            var detector = new HangDetector();
            detector.IsProcessRunningUsingHungAppAPI(process);
        }
    }
}
