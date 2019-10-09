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
        // link == MainPage + prefix
        private async Task<IDocument> GetDocument(string link)
        {
            // Setup the configuration to support document loading
            IConfiguration config = Configuration.Default.WithDefaultLoader();

            // Asynchronously get the document in a new context using the configuration
            return await BrowsingContext.New(config).OpenAsync(link);
        }


        // глава
        public async Task<List<string>> GetChapters(string prefix) 
        {
            var document = await GetDocument(MainPage + prefix);

            var items =

                //query
                from item in document.QuerySelectorAll("span")
                where item.ClassName.Contains("folder")
                select item;

            return items.Select(m => m.TextContent).ToList();
        }


        // урок
        public async Task<List<string>> GetLessons(string prefix, int index) 
        {
            var document = await GetDocument(MainPage + prefix);

            var items =

                from item in document.QuerySelectorAll("ol")
                where item.ClassName.Contains("subsubcontent")
                select item;

            string text = items.Select(m => m.TextContent).ToList()[index];
            var strings = text.Split("\n").ToList<string>();

            List<string> results = new List<string>();

            foreach (string str in strings)
            {
                if (str.Any(x => !char.IsLetter(x)))
                    results.Add(str);
            }

            return results;
        }


        // ссылка ( <a href="?">текст</a>)
        public string GetHref(string prefix, string text) 
        {
            IDocument document = Task.Run(async () => await GetDocument(MainPage + prefix)).Result;

            var items =

                from item in document.QuerySelectorAll("a")
                where item.TextContent.Contains(text.Trim())
                select item.GetAttribute("href");

            string href = "";
            foreach (var item in items)
                href = "http:" + item;

            return href;
        }


        // полный текст урока
        public async Task<List<string>> GetPageContent(string link) 
        {
            var document = await GetDocument(link);

            var item =

                //query for text
                from i1 in document.QuerySelectorAll("p, div.container")
                select i1;


            return item.Select(m => m.TextContent).ToList();
        }

    }
}
