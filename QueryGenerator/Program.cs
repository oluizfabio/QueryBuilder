using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QueryGeneratorCore;

namespace QueryGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = "@../../../../../DataJSON";
            List<string> files = new List<string>(Directory.GetFiles(rootPath));

            Console.WriteLine("What Query do you want to generate");
            Console.WriteLine(string.Join("\n", files.Select(file => file.Substring(file.IndexOf("DataJSON") + 9))));

            string filename = Console.ReadLine();
            var filePath = files.FirstOrDefault(file => file.ToLower().Contains(filename.ToLower()));
            if (string.IsNullOrEmpty(filePath))
            {
                QuerySchema querySchema = JSONReaderFactory.Create(File.ReadAllText(filePath));
                QueryGeneratorBuilder builder = new QueryGeneratorBuilder(querySchema);
                Console.WriteLine(builder.Build());
            }
            else
            {
                Console.WriteLine("File not found!");
            }
            Console.ReadKey();
        }
    }
}