using DPFP.Capture;
using DPFP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using DPFP.Processing;
using static DP_Wrapper.Wrapper.DPWrapper;

namespace DP_Wrapper.Wrapper
{
    public class DPWrapper
    {
        public class CaptureWrapper
        {
            private readonly Capture _capturer;

            public CaptureWrapper(Capture capturer, ProcessWrapperDP proceso)
            {
                _capturer = capturer ?? throw new ArgumentNullException(nameof(capturer));
                _capturer.EventHandler = new EventHandlerWrapper(this, proceso);
            }

            public void StartCapture()
            {
                _capturer.StartCapture();
            }

            public void StopCapture()
            {
                _capturer.StopCapture();
            }
        }

        public class EventHandlerWrapper : DPFP.Capture.EventHandler
        {
            private readonly CaptureWrapper _wrapper;
            private readonly ProcessWrapperDP _procesos;

            public EventHandlerWrapper(CaptureWrapper wrapper, ProcessWrapperDP procesos)
            {
                _wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
                _procesos = procesos ?? throw new ArgumentNullException(nameof(procesos));
            }

            public void OnComplete(object capture, string readerSerialNumber, Sample sample)
            {
                _procesos.Process(sample);
            }

            public void OnFingerGone(object capture, string readerSerialNumber)
            {
                // Implementar lógica si es necesario
            }

            public void OnFingerTouch(object capture, string readerSerialNumber)
            {
                // Implementar lógica si es necesario
            }

            public void OnReaderConnect(object capture, string readerSerialNumber)
            {
                // Implementar lógica si es necesario
            }

            public void OnReaderDisconnect(object capture, string readerSerialNumber)
            {
                // Implementar lógica si es necesario
            }

            public void OnSampleQuality(object capture, string readerSerialNumber, CaptureFeedback captureFeedback)
            {
                // Implementar lógica si es necesario
            }
        }

        public class ProcessWrapperDP
        {
            public CaptureWrapper? CaptureWrapper { get; set; }
            public void Initialize(CaptureWrapper captureWrapper)
            {
                CaptureWrapper = captureWrapper ?? throw new ArgumentNullException(nameof(captureWrapper));
            }
            public virtual void InitializeCapture()
            {
                CaptureWrapper?.StartCapture();
            }
            public virtual void StopCapture()
            {
                CaptureWrapper?.StopCapture();

            }
            public virtual void Process(Sample sample)
            {
                DrawPicture(ConvertSampleToBitmap(sample));
            }
            protected void DrawPicture(Bitmap originalBitmap)
            {
                // El tamaño deseado
                Size desiredSize = new Size(434, 588);

                // Crear un nuevo bitmap con el tamaño deseado
                Bitmap resizedBitmap = new Bitmap(desiredSize.Width, desiredSize.Height);

                // Dibujar la imagen original en el nuevo bitmap con el tamaño deseado
                using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                {
                    graphics.DrawImage(originalBitmap, 0, 0, desiredSize.Width, desiredSize.Height);
                }

                // Guardar el bitmap redimensionado en la carpeta deseada
                SaveBitmapToFile(resizedBitmap);

            }
            protected void SaveBitmapToFile(Bitmap bitmap)
            {
                // Asegurarte de que el directorio MuestrasDactilares exista
                string folderPath = @"B:\MuestrasDactilares";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Formatear la fecha y hora actual para crear un nombre de archivo único
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string filePath = Path.Combine(folderPath, $"Huella_{timestamp}.png");

                // Guardar la imagen redimensionada
                bitmap.Save(filePath, ImageFormat.Png);
            }
            protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
            {
                DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
                Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
                Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
                return bitmap;
            }
            protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
            {
                DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
                DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
                DPFP.FeatureSet features = new DPFP.FeatureSet();
                Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
                if (feedback == DPFP.Capture.CaptureFeedback.Good)
                    return features;
                else
                    return null;
            }
        }
        public static CaptureWrapper CreateCaptureWrapper(ProcessWrapperDP proceso)
        {
            var capture = new Capture(); // Asume que tienes un constructor adecuado
            return new CaptureWrapper(capture, proceso);
        }
    }
}
