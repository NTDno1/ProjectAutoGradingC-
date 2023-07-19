using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReadExeFile.Controllers
{
    public class ViewUserController : Controller
    {
        public async Task<IActionResult> Index(int id, int ids)
        {
            List<Question> questions = await GetMarkByStudentIdi(id);
            if (ids != 0)
            {
                List<QuestionNo> questioNo = await GetMarkDetail(ids);
                ViewBag.questioNo = questioNo;
                return View(questions);
            }
            else
            {
                return View(questions);

            }
        }
        public async Task<IActionResult> ViewTeacher(string className ,int ids)
        {
            List<Class> classs = await GetListClass();
            List<Question> questions = await GetMarkByStudentIdi();
            if (ids != 0)
            {
                List<QuestionNo> questioNo = await GetMarkDetail(ids);
                ViewBag.questioNo = questioNo;
                return View(questions);
            }
            else
            {
                ViewBag.classs = classs;
                return View(questions);
            }
            //return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            List<Question> questions = await GetMarkByStudentIdi(id);
            List<QuestionNo> questioNo = await GetMarkDetail(id);
            ViewBag.Questions = questions; // Truyền danh sách câu hỏi vào ViewBag
            ViewBag.questioNo = questioNo;
            return RedirectToAction("Index"); // Chuyển hướng đến action Index
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

        public async Task<List<Question>> GetMarkByStudentIdi()
        {
            List<Question> question = new List<Question>();

            string link = "https://localhost:7153/api/Question/";

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
        public async Task<List<QuestionNo>> GetMarkDetail(int id)
        {
            List<QuestionNo> question = new List<QuestionNo>();

            string link = "https://localhost:7153/api/QuestionNoes/"+id+"";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        question = JsonConvert.DeserializeObject<List<QuestionNo>>(data);
                    }
                }
            }
            return question;
        }
        public async Task<List<Class>> GetListClass()
        {
            List<Class> question = new List<Class>();

            string link = "https://localhost:7153/api/Classes";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        question = JsonConvert.DeserializeObject<List<Class>>(data);
                    }
                }
            }
            return question;
        }
        public async Task<List<User>> ListUser()
        {
            List<User> users = new List<User>();

            string link = "https://localhost:7153/api/User/";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<User>>(data);
                    }
                }
            }
            return users;
        }
    }
   
}
