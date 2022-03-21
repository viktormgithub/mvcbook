using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookMVC.Models;
using Microsoft.Data.SqlClient;

namespace BookMVC.Controllers;

public class BookController : Controller
{
    private const string ConnectionString =
        "Server=localhost,1433;Database=Bookbase;User Id=sa;Password=MyPass@word;Encrypt=no;";


    [HttpGet, Route("/")]
    public async Task<IActionResult> Index()
    {
        var result = new List<Book>();

        const string procedureName = "SelectBooks";

        await using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync(); // открываем подключение

            var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = procedureName;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        var id = (int)reader.GetValue(0);
                        var title = (string)reader.GetValue(1);
                        var author = (string)reader.GetValue(2);
                        var year = (short)reader.GetValue(3);
                        var publisher = (string)reader.GetValue(4);
                        var annotation = (string)reader.GetValue(5);
                        var isbn10 = (string)reader.GetValue(6);
                        var isbn13 = (string)reader.GetValue(7);
                        var content = (string)reader.GetValue(8);

                        result.Add(new Book
                        {
                            Id = id, Title = title, Author = author, Year = year, Publisher = publisher,
                            Annotation = annotation, ISBN10 = isbn10, ISBN13 = isbn13, Content = content
                        });
                    }
                }
            }
        }

        return View(result);
    }

    [HttpGet, Route("/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        Book? result = null;

        const string procedureName = "SelectBookById";

        await using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync(); // открываем подключение

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;

            command.CommandType = System.Data.CommandType.StoredProcedure;

            var idParam = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };

            command.Parameters.Add(idParam);

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        var title = (string)reader.GetValue(1);
                        var author = (string)reader.GetValue(2);
                        var year = (short)reader.GetValue(3);
                        var publisher = (string)reader.GetValue(4);
                        var annotation = (string)reader.GetValue(5);
                        var isbn10 = (string)reader.GetValue(6);
                        var isbn13 = (string)reader.GetValue(7);
                        var content = (string)reader.GetValue(8);

                        result = new Book
                        {
                            Id = id, Title = title, Author = author, Year = year, Publisher = publisher,
                            Annotation = annotation, ISBN10 = isbn10, ISBN13 = isbn13, Content = content
                        };
                    }
                }
            }
        }

        return View(result);
    }


    [HttpGet, Route("/create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, Route("/create")]
    public async Task<IActionResult> Create(Book book)
    {
        const string procedureName = "AddBook";

        await using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync(); // открываем подключение

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;

            // указываем, что команда представляет хранимую процедуру
            command.CommandType = System.Data.CommandType.StoredProcedure;

            var titleParam = new SqlParameter
            {
                ParameterName = "@title",
                Value = book.Title
            };

            command.Parameters.Add(titleParam);

            var authorParam = new SqlParameter
            {
                ParameterName = "@author",
                Value = book.Author
            };

            command.Parameters.Add(authorParam);

            var yearParam = new SqlParameter
            {
                ParameterName = "@year",
                Value = book.Year
            };

            command.Parameters.Add(yearParam);

            var publisherParam = new SqlParameter
            {
                ParameterName = "@publisher",
                Value = book.Publisher
            };

            command.Parameters.Add(publisherParam);

            var annotationParam = new SqlParameter
            {
                ParameterName = "@annotation",
                Value = book.Annotation
            };

            command.Parameters.Add(annotationParam);

            var isbn10Param = new SqlParameter
            {
                ParameterName = "@isbn10",
                Value = book.ISBN10
            };

            command.Parameters.Add(isbn10Param);

            var isbn13Param = new SqlParameter
            {
                ParameterName = "@isbn13",
                Value = book.ISBN13
            };

            command.Parameters.Add(isbn13Param);

            var contentParam = new SqlParameter
            {
                ParameterName = "@content",
                Value = book.Content
            };

            command.Parameters.Add(contentParam);

            await command.ExecuteNonQueryAsync();
        }

        return RedirectToAction("Index", "Book");
    }

    [HttpGet, Route("/update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        Book? result = null;

        const string procedureName = "SelectBookById";

        await using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync(); // открываем подключение

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;

            command.CommandType = System.Data.CommandType.StoredProcedure;

            var idParam = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };

            command.Parameters.Add(idParam);

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var title = (string)reader.GetValue(1);
                        var author = (string)reader.GetValue(2);
                        var year = (short)reader.GetValue(3);
                        var publisher = (string)reader.GetValue(4);
                        var annotation = (string)reader.GetValue(5);
                        var isbn10 = (string)reader.GetValue(6);
                        var isbn13 = (string)reader.GetValue(7);
                        var content = (string)reader.GetValue(8);

                        result = new Book
                        {
                            Id = id, Title = title, Author = author, Year = year, Publisher = publisher,
                            Annotation = annotation, ISBN10 = isbn10, ISBN13 = isbn13, Content = content
                        };
                    }
                }
            }
        }

        return View(result);
    }

    [HttpPost, Route("/update")]
    public async Task<IActionResult> Update(Book book)
    {
        const string procedureName = "UpdateBookById";

        await using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;

            command.CommandType = System.Data.CommandType.StoredProcedure;

            var idParam = new SqlParameter
            {
                ParameterName = "@id",
                Value = book.Id
            };

            command.Parameters.Add(idParam);

            var titleParam = new SqlParameter
            {
                ParameterName = "@title",
                Value = book.Title
            };

            command.Parameters.Add(titleParam);

            var authorParam = new SqlParameter
            {
                ParameterName = "@author",
                Value = book.Author
            };

            command.Parameters.Add(authorParam);

            var yearParam = new SqlParameter
            {
                ParameterName = "@year",
                Value = book.Year
            };

            command.Parameters.Add(yearParam);

            var publisherParam = new SqlParameter
            {
                ParameterName = "@publisher",
                Value = book.Publisher
            };

            command.Parameters.Add(publisherParam);

            var annotationParam = new SqlParameter
            {
                ParameterName = "@annotation",
                Value = book.Annotation
            };

            command.Parameters.Add(annotationParam);

            var isbn10Param = new SqlParameter
            {
                ParameterName = "@isbn10",
                Value = book.ISBN10
            };

            command.Parameters.Add(isbn10Param);

            var isbn13Param = new SqlParameter
            {
                ParameterName = "@isbn13",
                Value = book.ISBN13
            };

            command.Parameters.Add(isbn13Param);

            var contentParam = new SqlParameter
            {
                ParameterName = "@content",
                Value = book.Content
            };

            command.Parameters.Add(contentParam);

            await command.ExecuteNonQueryAsync();
        }

        return RedirectToAction("Details", "Book", new { id = book.Id });
    }

    [HttpPost, Route("/delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        const string procedureName = "DeleteBookById";

        await using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            var idParam = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };

            command.Parameters.Add(idParam);

            await command.ExecuteNonQueryAsync();
        }

        return RedirectToAction("Index", "Book");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}