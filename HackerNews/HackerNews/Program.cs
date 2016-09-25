using Newtonsoft.Json;
using System;

namespace HackerNews
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Web scraper demo");
            Console.WriteLine();

            int requestedPosts = -1;

            // validating command line arguments
            if ((args.Length > 1) && !String.IsNullOrEmpty(args[0]) && !String.IsNullOrEmpty(args[1]))
            {
                if (args[0].Equals("--posts", StringComparison.InvariantCultureIgnoreCase) || args[0].Equals("-p", StringComparison.InvariantCultureIgnoreCase))
                {
                    int argValue = -1;

                    if (Int32.TryParse(args[1], out argValue) && (argValue > 0) && (argValue <= 100))
                    {
                        requestedPosts = argValue;
                    }
                    else
                    {
                        requestedPosts = 0;
                    }
                }
            }

            // exit with a message if the arguments are incorrect
            if (requestedPosts < 0)
            {
                Console.WriteLine("Invalid arguments!");
                return;
            }
            else if (requestedPosts == 0)
            {
                Console.WriteLine("Invalid number of posts!");
                return;
            }

            // launch the scraper
            Scraper scr = new Scraper(requestedPosts);
            scr.Run();

            // print results (if any) as JSON
            if (scr.Results.Count > 0)
            {
                Console.WriteLine(JsonConvert.SerializeObject(scr.Results, Formatting.Indented));
            }
        }
    }
}
