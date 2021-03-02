using System.Collections.Generic;
using SQLite;
using App4.Models;
using System.Threading.Tasks;

namespace App4.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync()
        {
            //get all notes
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            //get a specific note
            return database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteSync(Note note)
        {
            if (note.ID != 0)
                return database.UpdateAsync(note);
            else
                return database.InsertAsync(note);
        }

        public Task<int> DeleteNoteSync(Note note)
        {
            return database.DeleteAsync(note);
        }
    }
}