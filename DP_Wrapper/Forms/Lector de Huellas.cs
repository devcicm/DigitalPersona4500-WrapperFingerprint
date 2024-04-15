using DP_Wrapper.Class_s;
using DPFP.Processing;
using static DP_Wrapper.Wrapper.DPWrapper;

namespace DP_Wrapper
{
    public partial class Form1 : Form
    {
        private readonly CapturadorDP _CapturadorDP;
        private readonly ValidadorDP _ValidadorDP;
        public Form1(CapturadorDP CapturadorDP, ValidadorDP validadorDP)
        {
            InitializeComponent();
            _CapturadorDP = CapturadorDP;
            _ValidadorDP = validadorDP;
        }

        private void bttn_CapturarHuella_Click(object sender, EventArgs e)
        {
            _CapturadorDP.InitializeCapture();
        }

        private void bttn_VerificarHuella_Click(object sender, EventArgs e)
        {
            _ValidadorDP.InitializeCapture();
        }
    }
}
