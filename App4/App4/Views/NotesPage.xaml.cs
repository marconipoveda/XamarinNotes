using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using App4.Models;
using App4.Views;
using System.Linq;

namespace App4.Views
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var notes = new List<Note>();

            //Create a Note object from each note file.
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            //Set the list of notes as data source for the CollectionView
            collectionView.ItemsSource = notes.OrderBy(d => d.Date).ToList();

        }

        async void OnAddClicked(object o, EventArgs e)
        {
            //Navigate to NoteEntryPage, w/o passing any data.
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }

        async void OnSelectionChanged(object o, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.Filename}");
            }
            
        }
    }
}