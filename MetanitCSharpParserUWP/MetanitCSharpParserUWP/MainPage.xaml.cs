using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MetanitCSharpParserUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();

            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = "https://metanit.com/sharp/mvc5/1.1.php";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(address);


            // Perform the query to get all cells with the content
            //var menuItems = document.QuerySelectorAll("span").Where(m => m.ClassName.Contains("folder"));
            IEnumerable<IElement> menuItems = from item in document.QuerySelectorAll("span")
                            where item.ClassName.Contains("folder")
                            select item;

            // We are only interested in the text - select it with LINQ
            var titles = menuItems.Select(m => m.TextContent).ToList();

            foreach (var item in titles)
                folders.Items.Add(item);
        }


        // open SplitView panel
        private void Button_Click(object sender, RoutedEventArgs e) => 
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
    }
}
