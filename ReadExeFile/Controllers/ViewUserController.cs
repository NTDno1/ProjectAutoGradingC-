using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReadExeFile.Controllers
{
    public class ViewUserController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            List<Question> questions = await GetMarkByStudentIdi(id); 
            return View(questions);
        }
        public async Task<List<Question>> GetMarkByStudentIdi(int id)
        {
           List<Question> question = new List<Question>();

            string link = "https://localhost:7153/api/Question/"+id+"";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        question = JsonConvert.DeserializeObject<List<Question>>(data);
                    }
                }
            }
            return question;
        }
    }
   
}
