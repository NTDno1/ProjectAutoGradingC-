using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReadExeFile.Controllers
{
    public class ViewUserController : Controller
    {
        public async Task<IActionResult> Index(int id, string questionid)
        {
            List<QuestionDTO> questions = await GetMarkByStudentIdi(id);
            if (questionid != null)
            {
                List<QuestionNo> questioNo = await GetMarkDetail(id,questionid);
                ViewBag.questioNo = questioNo;
                return View(questions);
            }
            else
            {
                return View(questions);

            }
        }
        public async Task<IActionResult> ViewTeacher(int classId, int studentId, string markDetail)
        {
            List<Class> classs = await GetListClass();
            //List<QuestionDTO> questions = await GetMarkByStudentIdi();
            if (classId != 0)
            {
                List<QuestionDTO> questionListss = await GetMarkByStudentIdi();
                List<UserDTO> users = await ListUserByClassId(classId);
                ViewBag.questionList = questionListss;
                ViewBag.users = users;
                ViewBag.classs = classs;
                if (studentId != 0)
                {
                    List<QuestionDTO> questionList = await GetMarkByStudentIdi(studentId);
                    List<QuestionDTO> questionLists = await GetMarkByStudentIdi();
                    ViewBag.users = users;
                    ViewBag.classs = classs;
                    ViewBag.questionList = questionList;
                    ViewBag.questionLists = questionLists;

                    if (markDetail != null)
                    {
                        List<QuestionNo> listMarkDetail = await GetMarkDetail(studentId, markDetail);
                        ViewBag.users = users;
                        ViewBag.classs = classs;
                        ViewBag.questionList = questionList;
                        ViewBag.listMarkDetail = listMarkDetail;
                    }
                }
                return View();
            }
            else
            {
                ViewBag.classs = classs;
                return View();
            }
        }
        public async Task<IActionResult> Detail(int id)
        {
            List<QuestionDTO> questions = await GetMarkByStudentIdi(id);
            //List<QuestionNo> questioNo = await GetMarkDetail(id);
            ViewBag.Questions = questions; // Truyền danh sách câu hỏi vào ViewBag
            //ViewBag.questioNo = questioNo;
            return RedirectToAction("Index"); // Chuyển hướng đến action Index
        }
        public IActionResult TakeMark()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GetMark()
        {
            return RedirectToAction("ViewTeacher", "ViewUser");
        }

        public async Task<List<QuestionDTO>> GetMarkByStudentIdi(int id)
        {
            List<QuestionDTO> question = new List<QuestionDTO>();

            string link = "https://localhost:7153/id?id=" + id + "";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        question = JsonConvert.DeserializeObject<List<QuestionDTO>>(data);
                    }
                }
            }
            return question;
        }

        public async Task<List<QuestionDTO>> GetMarkByStudentIdi()
        {
            List<QuestionDTO> question = new List<QuestionDTO>();

            string link = "https://localhost:7153/api/Question";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        question = JsonConvert.DeserializeObject<List<QuestionDTO>>(data);
                    }
                }
            }
            return question;
        }

        //public async Task<List<Question>> GetMarkByStudentIdi()
        //{
        //    List<Question> question = new List<Question>();

        //    string link = "https://localhost:7153/api/Question/";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        using (HttpResponseMessage res = await client.GetAsync(link))
        //        {
        //            using (HttpContent content = res.Content)
        //            {
        //                string data = await content.ReadAsStringAsync();
        //                question = JsonConvert.DeserializeObject<List<Question>>(data);
        //            }
        //        }
        //    }
        //    return question;
        //}
        public async Task<List<QuestionNo>> GetMarkDetail(int id , string questionId)
        {
            List<QuestionNo> question = new List<QuestionNo>();

            string link = "https://localhost:7153/api/QuestionNoes/"+id+"/"+ questionId + "";

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

        public async Task<List<QuestionNo>> GetMarkDetail(int id)
        {
            List<QuestionNo> question = new List<QuestionNo>();

            string link = "https://localhost:7153/api/QuestionNoes/" + id +"";

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
        public async Task<List<UserDTO>> ListUserByClassId(int classId)
        {
            List<UserDTO> users = new List<UserDTO>();

            string link = "https://localhost:7153/GetByClassId?classId=" + classId + "";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<UserDTO>>(data);
                    }
                }
            }
            return users;
        }
        public async Task<User> UserByUserId(int classId)
        {
            User users = new User();

            string link = "https://localhost:7153/GetByClassId?classId=" + classId + "";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<User>(data);
                    }
                }
            }
            return users;
        }

    }

}
