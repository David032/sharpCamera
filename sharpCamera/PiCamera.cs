using System.Diagnostics;
namespace sharpCamera
{
    public class PiCamera
    {
        private TaskCompletionSource<bool>? imageTaken;
        private Process? cameraCommand;
        private string outputPath = "/output";

        public PiCamera()
        {
            if (!Directory.Exists(outputPath))
            {
                // Can't set permissions for unix in .net 6 easily :(
                // DirectoryInfo di = Directory.CreateDirectory(outputPath);
            }
        }

        public void TakePicture()
        {
            imageTaken = new TaskCompletionSource<bool>();
            try
            {
                using (cameraCommand = new Process())
                {
                    string fileNameToOutput = "-o " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    cameraCommand.StartInfo.UseShellExecute = false;
                    cameraCommand.StartInfo.FileName = "raspistill";
                    cameraCommand.StartInfo.Arguments = fileNameToOutput;

#if DEBUG
                    Console.Out.WriteLine(cameraCommand.StartInfo.Arguments);
#endif
                    cameraCommand.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RecordVideo()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}