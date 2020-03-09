using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Client
{
    class Program
    {
        public class Options
        {
            [Option("input-type", Required = true, HelpText = "Type of input. Must be one of 'text' or 'file'.")]
            public string InputType { get; set; }

            [Option('t', "text", Required = true, HelpText = "Text or text contained file to be processed")]
            public string TextFile { get; set; }

            [Option('o', "output", Required = true, HelpText = "Output wave filename")]
            public string Output { get; set; }

            [Option('i', "id", Required = true, HelpText = "Your text-to-speech API ID")]
            public string UserId { get; set; }

            [Option('k', "token", Required = true, HelpText = "Your text-to-speech API Token")]
            public string ApiToken { get; set; }

            [Option('l', "lang", Required = true, HelpText = "Code of language to use (e.g., az-AZ, tr-TR)")]
            public string Language { get; set; }

            [Option("tts-id", Required = false, HelpText = "Identification of voice for given language. To see identification of voices call getVoices() method of Synthesizer class.")]
            public string TTSId { get; set; }
        }
        static void Main(string[] args)
        {
            string text = null;
            string output = null;
            string userId = null;
            string token = null;
            string lang = null;
            string inputType = null;
            string ttsId = null;
            try
            {
                // Parsing commandline arguments
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(opts => {
                        text = opts.TextFile;
                        output = opts.Output;
                        userId = opts.UserId;
                        token = opts.ApiToken;
                        lang = opts.Language;
                        inputType = opts.InputType;
                        ttsId = opts.TTSId;
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown while parsing arguments: " + ex.Message);
                return;
            }
            Synthesizer synthesizer = new Synthesizer(userId, token, lang);
            Task<string> voicesTask = synthesizer.GetVoices();
            if(voicesTask != null)
            {
                if(!(voicesTask.IsCanceled || voicesTask.IsFaulted))
                {
                    if(!voicesTask.IsCompleted)
                    {
                        voicesTask.Wait();
                    }
                    Console.WriteLine(voicesTask.Result);
                }
            }
            Task<byte[]> resultTask = null;
            if (inputType == "text")
            {
                resultTask = synthesizer.SynthesizeText(text, ttsId);
            }
            else
            {
                resultTask = synthesizer.Synthesize(text, ttsId);
            }
            if (resultTask == null)
            {
                Console.WriteLine("Text-to-speech process failed.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }
            if (resultTask.IsCanceled || resultTask.IsFaulted)
            {
                Console.WriteLine("Text-to-speech process failed.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }
            if (!resultTask.IsCompleted)
            {
                resultTask.Wait();
            }
            byte[] result = resultTask.Result;
            if (result == null)
            {
                Console.WriteLine("Text-to-speech process failed.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }
            FileStream writer = new FileStream(output, FileMode.OpenOrCreate);
            writer.Write(result, 0, result.Length);
            writer.Close();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
