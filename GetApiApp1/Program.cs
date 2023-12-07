using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;

namespace GetApiApp1
{
    public class Program
    {
        public static string logFilePath = "C:\\Ecolab-Training\\cs-projects\\GetApiApp1\\GetApiApp1\\LoggerFile.txt";
        public static Logger logger = new Logger(logFilePath);
        public static void Main()
        {
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7014");
                GetAllStudents(client).Wait();
                AddNewStudent(client).Wait();
                UpdateStudent(client).Wait();
                DeleteStudent(client).Wait();
            }
        }
        private static async Task GetAllStudents(HttpClient client)
        {
            string getAPIUrl = "api/Student";
            HttpResponseMessage responseMessage = await client.GetAsync(getAPIUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                var apiJsonData = await responseMessage.Content.ReadAsStringAsync();
                List<StudentModel> students = JsonConvert.DeserializeObject<List<StudentModel>>(apiJsonData);
                foreach (StudentModel student in students)
                {
                    Console.WriteLine($"{student.Id}, {student.Name}, {student.Gender}, {student.Fees}");
                }
                logger.Log("Get All Students is Success");
            }
            else
            {
                logger.Log(responseMessage.Content.ToString());
            }
        }
        private static async Task AddNewStudent(HttpClient client)
        {
            StudentModel student = new StudentModel()
            {
                Id = 11,
                Name = "OldName",
                Gender = 'M',
                Fees = 0.1231
            };
            string postAPIUrl = "api/Student";
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync<StudentModel>(postAPIUrl, student);
            if (!responseMessage.IsSuccessStatusCode)
            {
                logger.Log(responseMessage.ReasonPhrase);
                Console.WriteLine(responseMessage.ReasonPhrase);
                return;
            }
            logger.Log("New Student Added Successfully");
            Console.WriteLine("New Student Added Successfully");
        }

        private static async Task UpdateStudent(HttpClient client)
        {
            StudentModel updatedStudent = new StudentModel()
            {
                Id = 11,
                Name = "Updated by Calling an API on Console APP",
                Gender = 'F',
                Fees = 9.874561
            };
            var json = JsonConvert.SerializeObject(updatedStudent);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string putAPIUrl = "api/Student";
            HttpResponseMessage responseMessage = await client.PutAsync(putAPIUrl, content);
            if(!responseMessage.IsSuccessStatusCode)
            {
                logger.Log(responseMessage.ReasonPhrase);
                Console.Write(responseMessage.ReasonPhrase);
                return;
            }
            logger.Log("Student Updated Successfully");
            Console.WriteLine("Student Updated Successfully");
        }

        private static async Task DeleteStudent(HttpClient client)
        {
            string deleteAPIUrl = "api/Student/11";
            HttpResponseMessage responseMessage = await client.DeleteAsync(deleteAPIUrl);
            if(!responseMessage.IsSuccessStatusCode)
            {
                logger.Log(responseMessage.ReasonPhrase);
                Console.WriteLine(responseMessage.ReasonPhrase); return;
            }
            logger.Log("Student Deleted Successfully");
            Console.WriteLine("Student Deleted Successfully");
        }

        /*private static async Task GetAllPosts(HttpClient client)
        {
            HttpResponseMessage responseMessgae = await client.GetAsync("posts");
            if (responseMessgae.IsSuccessStatusCode)
            {
                var apiJsonData = await responseMessgae.Content.ReadAsStringAsync();
                List<PostModel> posts = JsonConvert.DeserializeObject<List<PostModel>>(apiJsonData);
                foreach (var post in posts)
                {
                    Console.WriteLine(post.id);
                }
            }
        }

        public static void Main()
        {
            DoAllWork();
        }
        private static async void DoAllWork()
        {
            Console.WriteLine("All Work started" + DateTime.Now);
            Task task1 = ClothesWork();
            Task task2 = Cleaning();
            Task task3 = Cooking();
            Task.WaitAll(task1, task2, task3);
            Console.WriteLine("All Work Done" + DateTime.Now);
        }
        private static async Task ClothesWork()
        {
            string response = await WashClothes();
            await DryClothes(response);
        }
        private static async Task<string> WashClothes()
        {
            Console.WriteLine("washing started" + DateTime.Now);
            await Task.Delay(1000);
            Console.WriteLine("Washing is Done" + DateTime.Now);
            return "wet clothes";
        }
        private static async Task DryClothes(string response)
        {
            Console.WriteLine("Drying started" + DateTime.Now);
            await Task.Delay(2000);
            Console.WriteLine("Drying is Done" + DateTime.Now);
        }
        private static async Task Cleaning()
        {
            Console.WriteLine("Cleaning Home started" + DateTime.Now);
            await Task.Delay(2000);
            Console.WriteLine("Cleaning Home is Done" + DateTime.Now);
        }
        private async static Task Cooking()
        {
            Console.WriteLine("Cooking Food started" + DateTime.Now);
            await Task.Delay(2000);
            Console.WriteLine("Cooking Food is Done" + DateTime.Now);
        }*/
    }
}