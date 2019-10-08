using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitCSharpParserUWP.Core
{
    class MetanitParser : Books
    {
        //book section
        private string prefix { get; set; } 
        
        private IDocument document { get; set; }


        public MetanitParser(string prefix)
        {
            Create();

            async void Create()
            {
                // Setup the configuration to support document loading
                IConfiguration config = Configuration.Default.WithDefaultLoader();

                // Asynchronously get the document in a new context using the configuration
                document = await BrowsingContext.New(config).OpenAsync(MainPage + prefix);
            }
        }
        


        public List<string> GetChapters() //глава
        {
            var items =
                
                from item in document.QuerySelectorAll("span")
                where item.ClassName.Contains("folder")
                select item;

            return items.Select(m => m.TextContent).ToList();
        }



        public List<string> GetLessons(int index) //урок
        {
            var items =

                from item in document.QuerySelectorAll("ol")
                where item.ClassName.Contains("subsubcontent")
                select item;

            string text = items.Select(m => m.TextContent).ToList()[index];
            var strings = text.Split("\n").ToList<string>();

            List<string> results = new List<string>();

            foreach (var str in strings)
                try { 
                    results.Add(str.Remove(0, 3));
                }
                catch { }

            return results;
        }


        public List<string> GetHref(string text)
        {
            var items =

                from item in document.QuerySelectorAll("a")
                where item.TextContent.Contains(text)
                select item.GetAttribute("href");

            return items.ToList();
        }


        public List<string> GetPageContent(string link) //Статья
        {
            var items =

                from item in document.QuerySelectorAll("p")
                select item;

            return items.Select(m => m.TextContent).ToList();
        }

    }
}
