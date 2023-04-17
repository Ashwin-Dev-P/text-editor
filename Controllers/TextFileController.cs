using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// Models.
using TextEditor.Models;

// SQL
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace TextEditor.Controllers
{
    public class TextFileController : Controller
    {


        //public List<TextFileModel> files = new List<TextFileModel>();

        // To connect db
        IConfiguration configuration;
        public SqlConnection connection;
        public TextFileController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connection = new SqlConnection(configuration.GetConnectionString("TextEditorContextConnection"));

        }

        // Method to get TextFiles.
        private List<TextFileModel> GetTextFiles()
        {
            
            

            SqlCommand command = new SqlCommand("fetchSchedules", connection);

            // Get user id of currently loggedIn user
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string query = $"Select Id, Name, CreatedAt, LastUpdatedAt from TextFiles where ( UserId = '{userId}' )";
            Console.WriteLine(query);

            command.CommandText = query;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            

            List<TextFileModel> files = new List<TextFileModel>();
            while (reader.Read())
            {

                TextFileModel textFile = new TextFileModel();
                textFile.Id = (int)reader["Id"];
                textFile.Name = (string)reader["Name"];
               
                textFile.CreatedAt = (DateTime)reader["CreatedAt"];
                textFile.LastUpdatedAt = (DateTime)reader["LastUpdatedAt"];
                

                files.Add(textFile);
            }

            reader.Close();
            connection.Close();

            return files;
        }

        // GET: TextFileController
        public ActionResult Index()
        {
            return View(GetTextFiles());
        }

        // GET: TextFileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TextFileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // Method to insert into Schedule table
        private void CreateTextFile(string name, string content)
        {
            
            SqlCommand command = new SqlCommand("createTextFile", connection);

            // Get user id of currently loggedIn user
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            string query = $"INSERT INTO TextFiles( Name , Content, UserId ) VALUES( '{name}', '{content}','{userId}' )";
            Console.WriteLine(query);
            command.CommandText = query;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }

        // POST: TextFileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                CreateTextFile(collection["name"], collection["content"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // Method to get the schedule object
        private TextFileModel getTextFile(int id)
        {
            

            SqlCommand command = new SqlCommand("getTextFile", connection);

            // Get user id of currently loggedIn user
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string query = $"Select Name, Content from TextFiles where ( Id = '{id}' )";
            Console.WriteLine(query);

            command.CommandText = query;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            reader.Read();
            
            TextFileModel textFile = new TextFileModel();
            
            textFile.Name = (string)reader["Name"];
            textFile.Content = (string)reader["Content"];

            reader.Close();
            connection.Close();
            return textFile;
        }

        // GET: TextFileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(getTextFile(id));
        }


        public void updateTextFile(int id, string name , string content)
        {
            
            
            SqlCommand command = new SqlCommand("updateTextFile", connection);


            string lastUpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $"UPDATE TextFiles " +
                $"SET LastUpdatedAt='{lastUpdatedAt}' , Name='{name}' , Content='{content}'  " +
                $"WHERE ( Id = {id} )";

            Console.WriteLine(query);

            command.CommandText = query;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        // POST: TextFileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                updateTextFile(id, collection["Name"], collection["Content"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TextFileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TextFileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
