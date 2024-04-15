using DPFP;
using DPFP.Processing;
using DPFP.Verification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DP_Wrapper.Wrapper.DPWrapper;

namespace DP_Wrapper.Class_s
{
    public class ValidadorDP: ProcessWrapperDP
    {
        private readonly Func<ProcessWrapperDP, CaptureWrapper> _captureWrapperFactory;
        private CaptureWrapper _captureWrapper;
        private Enrollment Enrollment;
        private DPFP.Verification.Verification Verificator;
        public ValidadorDP(Func<ProcessWrapperDP, CaptureWrapper> captureWrapperFactory)
        {
            CaptureWrapper = captureWrapperFactory(this);
            this.Initialize(CaptureWrapper);
        }
        public override void Process(Sample sample)
        {
 
            DPFP.FeatureSet features = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Verification);
 
            if (features != null && (SourceTemplate.PlantillaHuella != null))
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                Verificator.Verify(features, SourceTemplate.PlantillaHuella, ref result);
           
                if (result.Verified)
                    MessageBox.Show("La huella digital a sido verificada.");
                else
                    MessageBox.Show("La huella digital no a sido verificada.");
            }
        }
        
        public override void InitializeCapture()
        {
            base.InitializeCapture();
            Enrollment = new Enrollment();
            Verificator = new DPFP.Verification.Verification(); 
          
        }
        public void StopCapture()
        {
           base.StopCapture();
           Enrollment.Clear();
        }
    }
}
