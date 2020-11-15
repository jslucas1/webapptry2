using System.Data.SQLite;
using System.IO;
using API.Models.Interfaces;

namespace API.Models
{
    public class DeleteBook : IDeleteBook
    {
        public void RemoveBook(int id)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string cs = "URI=file:"+currentDir + @"\Models\book.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"DELETE FROM books WHERE id=@id";
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}