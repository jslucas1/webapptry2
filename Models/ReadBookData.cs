using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using API.Models.Interfaces;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace API.Models
{
    public class ReadBookData : IGetAllBooks, IGetBook
    {
        public List<Book> GetAllBooks()
                {
                    string connStr = "server=sql9.freemysqlhosting.net;user=sql9376719;database=sql9376719;port=3306;password=4k9xKvklX7";
                    MySqlConnection conn = new MySqlConnection(connStr);
                    try
                    {
                        Console.WriteLine("Connecting to MySQL...");
                        conn.Open();
                        // Perform database operations
                        string stm = "SELECT * FROM books";
                        MySqlCommand cmd = new MySqlCommand(stm, conn);

                        List<Book> allBooks = new List<Book>();
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while(rdr.Read())
                            {

                            allBooks.Add(new Book(){Id = rdr.GetInt32(0), Title = rdr.GetString(1), Author=rdr.GetString(2)});
                            }

                        }                       
                        conn.Close();
                        return allBooks;
                    }
                    catch (Exception ex)
                    {
                        List<Book> allBooks = new List<Book>();
                        Console.WriteLine(ex.ToString());
                        conn.Close();
                        return allBooks;
                    }
                    
                    // string currentDir = Directory.GetCurrentDirectory();
                    // //string cs = "URI=file:"+currentDir + @"\Models\book.db";
                    // string cs = "URI=file:"+currentDir + @"/book.db";
                    // Console.WriteLine("looking for the database here without model : " + cs);
                    // using var con = new SQLiteConnection(cs);
                    // con.Open();

                    // string stm = "SELECT * FROM books";
                    // using var cmd = new SQLiteCommand(stm, con);


                    // using SQLiteDataReader rdr = cmd.ExecuteReader();

                    // List<Book> allBooks = new List<Book>();
                    // // allBooks.Add(new Book(){Id=1,Title="Mistborn", Author="Brandon Sanderson"});
                    // // allBooks.Add(new Book(){Id=2,Title="Oathbringer", Author="Brandon Sanderson"});
                    // while(rdr.Read())
                    // {

                    //     allBooks.Add(new Book(){Id = rdr.GetInt32(0), Title = rdr.GetString(1), Author=rdr.GetString(2)});
                    // }

                    // return allBooks;
                }

                public Book GetBook(int id)
                {
                    string currentDir = Directory.GetCurrentDirectory();
                    string cs = "URI=file:"+currentDir + @"\Models\book.db";
                    using var con = new SQLiteConnection(cs);
                    con.Open();

                    string stm = "SELECT * FROM books WHERE id = @id";
                    using var cmd = new SQLiteCommand(stm, con);
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.Prepare();
                    using SQLiteDataReader rdr = cmd.ExecuteReader();

                    rdr.Read();
                    return new Book(){Id = rdr.GetInt32(0), Title = rdr.GetString(1), Author=rdr.GetString(2)};
                }
    }
}