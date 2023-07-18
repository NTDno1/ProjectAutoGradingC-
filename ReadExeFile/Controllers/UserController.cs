using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ReadExeFile.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string userName, string passWord)
        {
            User user = await GetOneStudentFromApi(userName, passWord);
            if (user.Role == 2)
            {
                return Redirect($"/Home");
            }
            if (user.Role == 1)
            {
                ViewBag.user = user;
                //return View("Index", user);
                return Redirect($"/ViewUser?id="+user.Id+"");
            }
            else
            {
                ViewData["Error"] = "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.";
                return View("Index");
            }

        }
        public IActionResult SignUp()
        {
            return View();
        }
        public async Task<User> GetOneStudentFromApi(string user, string pass)
        {
            User users = new User();

            string link = "https://localhost:7153/api/Users/" + user + "/" + pass + "";

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
