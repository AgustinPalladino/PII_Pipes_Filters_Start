using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            IFilter greyscaleFilter = new FilterGreyscale(); 
            IFilter edgeDetectionFilter = new FilterEdgeDetection(); 
            
            IPipe pipeNull = new PipeNull(); 
            IPipe pipeSerial2 = new PipeSerial(edgeDetectionFilter, pipeNull); 
            IPipe pipeSerial1 = new PipeSerial(greyscaleFilter, pipeSerial2);  
            picture = pipeSerial1.Send(picture); 
            provider.SavePicture(picture, @"luke1.jpg");
        }
    }
}
