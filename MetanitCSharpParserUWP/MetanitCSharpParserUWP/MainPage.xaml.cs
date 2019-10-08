using AngleSharp;
using AngleSharp.Dom;
using MetanitCSharpParserUWP.Core;
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
        MetanitParser parser;


        public MainPage()
        {
            this.InitializeComponent();

            parser = new MetanitParser(Books.CSharpBasics);

            folders.SelectionChanged += new SelectionChangedEventHandler(FolderSelected);
            files.SelectionChanged += new SelectionChangedEventHandler(LessonSelected);
        }

        private void LessonSelected(object sender, SelectionChangedEventArgs e)
        {
            var lessons = parser.GetHref(files.SelectedValue.ToString());
            string href = "";
            foreach(var lesson in lessons)
                href = "http://"+lesson;

            var content = parser.GetPageContent(href);
            foreach (var a in content)
                output.Items.Add(a);
        }



        // open Lessons panel
        private void FolderSelected(object sender, SelectionChangedEventArgs e)
        {
            if (folders.SelectedIndex < 0) 
                return;

            splitView.IsPaneOpen = !splitView.IsPaneOpen;
            splitView2.IsPaneOpen = !splitView.IsPaneOpen;

            // Perform the query to get all cells with the content
            List<string> lessons = parser.GetLessons(folders.SelectedIndex);

            //files - listview
            files.Items.Clear();
            foreach (var item in lessons)
                files.Items.Add(item);
        }

        // open Folders panel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Perform the query to get all cells with the content
            List<string> chapters = parser.GetChapters();

            //folders - listview
            folders.Items.Clear();
            foreach (var item in chapters)
                folders.Items.Add(item);


            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

    }
}
