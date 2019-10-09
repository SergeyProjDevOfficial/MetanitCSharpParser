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
        string prefix = Books.CSharp.Basics;


        public MainPage()
        {
            this.InitializeComponent();

            parser = new MetanitParser();

            folders.SelectionChanged += new SelectionChangedEventHandler(FolderSelected);
            files.SelectionChanged += new SelectionChangedEventHandler(LessonSelected);
        }


        // on current lesson selected => load this lesson
        private async void LessonSelected(object sender, SelectionChangedEventArgs e)
        {
            if (files.SelectedIndex < 0)
                return;

            string href = parser.GetHref(prefix, files.SelectedValue.ToString());

            var text = await parser.GetPageContent(href);

            output.Text = href;
            foreach (var a in text)
                output.Text += a+"\n";


            splitView2.IsPaneOpen = !splitView2.IsPaneOpen;
        }



        // on folder selected => show available lessons
        private async void FolderSelected(object sender, SelectionChangedEventArgs e)
        {
            if (folders.SelectedIndex < 0) 
                return;

            splitView.IsPaneOpen = !splitView.IsPaneOpen;
            splitView2.IsPaneOpen = !splitView.IsPaneOpen;

            // Perform the query to get all cells with the content
            List<string> lessons = await parser.GetLessons(prefix, folders.SelectedIndex);

            // files - listview
            files.Items.Clear();
            foreach (var item in lessons)
                files.Items.Add(item);
        }



        // on button ShowFoldersClick => show available folders
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Perform the query to get all cells with the content
            List<string> chapters = await parser.GetChapters(prefix);

            //folders - listview
            folders.Items.Clear();
            foreach (var item in chapters)
                folders.Items.Add(item);

            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

    }
}
