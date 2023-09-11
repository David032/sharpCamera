using sharpCamera;

namespace sharpCameraExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Say cheese!");
            PiCamera cam = new PiCamera();
            cam.TakePicture();
        }
    }
}