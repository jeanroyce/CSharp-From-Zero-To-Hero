using System.Collections.Generic;
using System.IO;
using BootCamp.Chapter.Exceptions;
using BootCamp.Chapter.Models;
using Newtonsoft.Json;

namespace BootCamp.Chapter
{
    public static class JsonReader
    {
        public static IEnumerable<Transaction> Read(string filepath)
        {
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath)) throw new NoTransactionsFoundException();
            var fileContent = File.ReadAllText(filepath);
            
            return JsonConvert.DeserializeObject<IEnumerable<Transaction>>(fileContent);
        }
    }
}
