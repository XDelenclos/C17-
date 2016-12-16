using ConsoleApplication2;
using EventsAndDelegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var video = new Video() { Title = "Video 1" };
            var videoEncoder = new videoencoder(); // publisher
            var mailService = new MailService(); //subscriber
            var messageService = new MessageService();//subscriber

            videoEncoder.VideoEncoded += mailService.onVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

            videoEncoder.Encode(video);
            Console.ReadLine();
        }
    }
    public class MailService
    {
        public void onVideoEncoded(Object source, VideoEventArgs e)
        {
            Console.WriteLine("MailService: Sending an email..."+e.Video.Title);
        }
    }

    public class MessageService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("MessageService: Sending a text Message...."+e.Video.Title);
        }
    }

}
