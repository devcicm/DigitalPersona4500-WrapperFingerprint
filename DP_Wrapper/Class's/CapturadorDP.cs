using DP_Wrapper.Class_s;
using DPFP;
using DPFP.Processing;
using DPFP.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DP_Wrapper.Wrapper.DPWrapper;

namespace DP_Wrapper
{
    public class CapturadorDP : ProcessWrapperDP
    {
        private readonly Func<ProcessWrapperDP, CaptureWrapper> _captureWrapperFactory;
        private CaptureWrapper _captureWrapper;
        private Enrollment Enrollment;

        public CapturadorDP(Func<ProcessWrapperDP, CaptureWrapper> captureWrapperFactory)
        {
            CaptureWrapper = captureWrapperFactory(this);
            this.Initialize(CaptureWrapper);
        }
        public override void Process(Sample sample)
        {
            try
            { 
                if (!(Enrollment.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready))
                {
                    base.Process(sample);
     
                    FeatureSet features = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Enrollment);
                    if (features != null)
                    {
 
                        Enrollment.AddFeatures(features);

                    }
     
                    if (Enrollment.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready)
                    {
                        SourceTemplate.PlantillaHuella = Enrollment.Template;                  
                    }
                }

            }
            finally
            {
 
                switch (Enrollment.TemplateStatus)
                {
                    case DPFP.Processing.Enrollment.Status.Ready:

                        StopCapture();
                        Enrollment.Clear(); 
                        break;

                    case DPFP.Processing.Enrollment.Status.Failed:  

                        MessageBox.Show("Ha fallado la captura y el proceso, reiniciando sistema de captura");
                        _captureWrapper.StopCapture();
                        _captureWrapper.StartCapture();
                        break;
                }
            }
        }
        public override void InitializeCapture()
        {
            Enrollment = new Enrollment();
            base.InitializeCapture();
        }
        public override void StopCapture()
        {
            base.StopCapture();
            Enrollment.Clear();
        }
    }
}
