using Microsoft.CognitiveServices.Speech;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SpeechWPMCalculator
{
    class Program
    {
        public static async Task RecognizeSpeechAsync() //To do: continuous recognition
        {
            //// Subscribe to event
            //recognizer.Recognized += (s, e) =>
            //{
            //    if (e.Result.Reason == ResultReason.RecognizedSpeech)
            //    {
            //        // Do something with the recognized text
            //        // e.Result.Text
            //    }
            //};

            //// Start continuous speech recognition
            //await recognizer.StartContinuousRecognitionAsync();

            //// Stop continuous speech recognition
            //await recognizer.StopContinuousRecognitionAsync();


            var config = SpeechConfig.FromSubscription("", "westus");
            using (var recognizer = new SpeechRecognizer(config))
            {
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                {

                    //To Do, test and implement the continuous speech to text.
                    Console.WriteLine($"We recognized: {result.Text}");
                    var wordCounter = result.Text;
                    //Using Regex, removing all punctuations from the result string, spliting it into wordArray
                    wordCounter = Regex.Replace(wordCounter, @"[^\w\d\s]", "");
                    string[] wordArray = wordCounter.Split();
                    Console.WriteLine("The count of words is " + wordArray.Length);

                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"Canceled: Reason= {cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"Canceled: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"Canceled: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"Canceled: Did you update the subscription info?");
                    }
                }

            }
        }

        static void Main()
        {
            Console.WriteLine("Please start speaking, I am listening...");
            RecognizeSpeechAsync().Wait();
            Console.WriteLine("Please press <Return> to continue");
            Console.ReadLine();
        }
    }
}
