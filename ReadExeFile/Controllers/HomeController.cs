using Microsoft.AspNetCore.Mvc;
using ReadExeFile.Models;
using System.Collections;
using System.Diagnostics;
using DataAccess.Models;
using System.IO;
using ExcelDataReader;
using OfficeOpenXml;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography.X509Certificates;
using static OfficeOpenXml.ExcelErrorValue;
using System;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReadExeFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        ArrayList dmn = new ArrayList();


        public IActionResult Index()
        {
            string data = TempData["CheckClass"] as string;

            string checkclass = TempData["CheckTestCase"] as string;
            string checkFoder = TempData["CheckPathFoder"] as string;
            string addSuccess = TempData["AddSuccess"] as string;


            if (data != null)
            {
                ViewBag.data = data;
                return View();
            }
            if(checkclass != null)
            {
                ViewBag.checkclass = checkclass;
                return View();
            }
            if(checkFoder!= null)
            {
                ViewBag.checkFoder = checkFoder;
                return View();
            }
            if(addSuccess != null) {
                ViewBag.addSuccess = addSuccess;
                return View();
            }
            return View();
        }
        static List<Solution> solution1s = new List<Solution>();
        static Dictionary<string, double> studentTotalMarks = new Dictionary<string, double>();
        static string testCode = "";
        static string stringClass = "";
        public async Task<IActionResult> GetUser(string Foder, string TestCase)
        {
            Foder = "aaa";
            solution1s.Clear();
            studentTotalMarks.Clear();
            testCode = "";
            stringClass = "";
            string[] splitPaths = Foder.Split('\\');
            string[] fileNameParts = splitPaths[^1].Split('_');

            stringClass = fileNameParts[0];
            try
            {
                testCode = fileNameParts[1];

            }
            catch
            {
                TempData["CheckPathFoder"] = "Sai Đường Dẫn FoderStudent Hoặc Cấu Trúc Fille";
                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("String class: " + stringClass);
            Console.WriteLine("Test code: " + testCode);

            List<User> users = await GetStudentFromApi(stringClass);

            string executablePaths = TestCase;
            string executablePath = Foder;
            string arguments = ""; // (nếu có)
            string output;
            string[] listFilePathFoder = Directory.GetDirectories(executablePath);
            //Tên Sv + List linkfilec của sv
            Dictionary<string, string[]> mapLinkFileC = new Dictionary<string, string[]>();

            string[] filess = Directory.GetDirectories(executablePaths);

            Dictionary<string, string[]> mapTestCaseFile = new Dictionary<string, string[]>();
            Dictionary<string, TestCase[]> mapTestCaseSolution = new Dictionary<string, TestCase[]>();

            TestCase[] test = { };
            string[] valueTestCase = { };
            string keys = "";
            string result = "";
            string[] partss;
            try
            {
                for (int i = 0; i < filess.Length; i++)
                {
                    string[] file = Directory.GetFiles(filess[i]);
                    mapTestCaseFile.Add(filess[i], file);
                }
                foreach (var testCase in mapTestCaseFile)
                {
                    valueTestCase = testCase.Value;
                    keys = testCase.Key;
                    string[] parts = keys.Split('\\');
                    string testCaseName = parts[parts.Length - 1];
                    string[] testCaseNameParts = testCaseName.Split('_');
                    result = testCaseNameParts[0]; // kết quả sẽ là "Q1"
                    string[] values = testCase.Value;
                    string checkKey = result.ToLower();

                    foreach (var valuee in valueTestCase)
                    {
                        string contents = System.IO.File.ReadAllText(valuee);
                        partss = contents.Split('|');
                        string input1 = partss[1];
                        string output1 = partss[3];
                        string mark1 = partss[5];
                        Array.Resize(ref test, test.Length + 1);
                        test[test.Length - 1] = new TestCase(result, mark1, input1, output1);
                        Console.WriteLine(valuee.ToString());
                    }
                    //if (test == null)
                    //{
                    //    TempData["CheckTestCase"] = "Sai Đường Dẫn Test Case Hoặc Cấu Trúc Fille";
                    //    return RedirectToAction("Index", "Home");
                    //}
                    mapTestCaseSolution.Add(result, test);
                    Array.Resize(ref test, 0);
                }
            }
            catch(Exception ex)
            {
                TempData["CheckTestCase"] = "Sai Đường Dẫn Test Case Hoặc Cấu Trúc Fille(ex: ..../TestCase)";
                return RedirectToAction("Index", "Home");
            }


            // đường dẫn file exe bài tập. Key He123 value C:\Users\NTD\Desktop\SE1437\HE140001_ANVHE_1
            // key là tên trong solution và value là Solution
            string answer = "";
            Dictionary<string, Solution[]> mapSolutions = new Dictionary<string, Solution[]>();
            List<Solution> solutions = new List<Solution>();
            double mark = 0;
            try
            {
                for (int i = 0; i < listFilePathFoder.Length; i++)  // chạy 2 forder
                {
                    string[] pathExe = Directory.GetFiles(listFilePathFoder[i]);
                    string[] splitPath = listFilePathFoder[i].Split('\\');
                    string nameMssvStudent = splitPath[splitPath.Length - 1];
                    mapLinkFileC.Add(splitPath[splitPath.Length - 1], pathExe);
                }
                foreach (var entry in mapLinkFileC) // lặp qua key, add value vào array
                {
                    string studentName = entry.Key;

                    string[] parts = studentName.Split('_'); // Tách chuỗi thành các phần tử bằng dấu "_"
                    string studentCode = parts[0].Substring(2); // Lấy phần con số từ vị trí thứ 3 đến hết chuỗi

                    string[] values = entry.Value;

                    //solutions.Add(key, null);
                    foreach (string value in values) //chạy array lấy ra output và name cho vào list solution
                    {
                        // Tìm vị trí cuối cùng của dấu gạch chéo ngược (\) trong chuỗi đường dẫn
                        int lastIndex = value.LastIndexOf('\\');

                        // Lấy phần tên tập tin sau dấu gạch chéo ngược
                        answer = value.Substring(lastIndex + 1);

                        // Xóa phần mở rộng file (.exe) nếu có
                        int extensionIndex = answer.LastIndexOf('.');
                        if (extensionIndex >= 0)
                        {
                            answer = answer.Substring(0, extensionIndex);
                        }

                        // In ra tên tập tin Q1
                        Console.WriteLine(answer);
                        // Tạo một đối tượng ProcessStartInfo để cấu hình thực thi file .exe
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = value,
                            Arguments = arguments,
                            RedirectStandardInput = true, // Chuyển hướng đầu vào của tiến trình
                            RedirectStandardOutput = true, // Chuyển hướng đầu ra của tiến trình
                            UseShellExecute = false, // Không sử dụng Shell để thực thi
                            CreateNoWindow = true // Không tạo ra cửa sổ mới cho tiến trình
                        };

                        // Tạo đối tượng Process để chạy file .exe
                        using (Process process = new Process())
                        {
                            process.StartInfo = startInfo;
                            process.Start();

                            // Gửi dữ liệu vào tiến trình nếu có
                            if (mapTestCaseSolution != null)
                            {
                                // truyền input
                                foreach (var map in mapTestCaseSolution)
                                {
                                    string questionNo = map.Key;
                                    if (answer.Equals(map.Key))
                                    {
                                        TestCase[] valuess = map.Value;
                                        foreach (var item in valuess)
                                        {
                                            process.StandardInput.WriteLine(item.InPut);
                                            process.StandardInput.Close();
                                            output = process.StandardOutput.ReadToEnd();
                                            //Console.WriteLine(output);
                                            // Chờ tiến trình kết thúc
                                            process.WaitForExit(); string cleanedInput = output.Replace("\r\n", "");
                                            int startIndex = cleanedInput.IndexOf("OUTPUT:") + 7;
                                            string outputValue = cleanedInput.Substring(startIndex);
                                            // Sử dụng giá trị đầu ra theo ý muốn trong ứng dụng web của bạn
                                            //solution1s.Add(new Solution(studentName, questionNo, outputValue));

                                            //var marks = item.STT;
                                            if (outputValue.Trim().Contains(item.Output.Trim()))
                                            {
                                                //mark += double.Parse(item.Mark);
                                                solution1s.Add(new Solution(studentName, studentCode, questionNo + " " + testCode, item.InPut, item.Output, item.Mark, outputValue, Convert.ToDouble(item.Mark)));
                                            }
                                            else
                                            {
                                                solution1s.Add(new Solution(studentName, studentCode, questionNo + " " + testCode, item.InPut, item.Output, "0", outputValue, double.Parse("0")));
                                            }
                                            process.Start();
                                        }
                                    }
                                }
                                process.StandardInput.Close();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["CheckTestCase"] = "Sai Đường Dẫn Test Case Hoặc Cấu Trúc Fille (ex:..../SE1606_123456)";
                return RedirectToAction("Index", "Home");
            }

            foreach (var so in solution1s)
            {
                Console.WriteLine(so.ToString());
                //Console.WriteLine("đây là output: " + so.OutPut);
                if (studentTotalMarks.ContainsKey(so.StuName))
                {
                    // Nếu học sinh đã tồn tại trong từ điển, cộng điểm vào tổng điểm hiện tại
                    studentTotalMarks[so.StuName] += so.TotalMark;
                }
                else
                {
                    // Nếu học sinh chưa tồn tại trong từ điển, thêm học sinh và gán điểm ban đầu
                    studentTotalMarks.Add(so.StuName, so.TotalMark);
                }

                User studentToRemove = users.FirstOrDefault(s => s.Mssv == so.Mssv);
                if (studentToRemove != null)
                {
                    users.Remove(studentToRemove);
                }
            }
            //foreach (var kvp in studentTotalMarks)
            //{
            //    Console.WriteLine("Học sinh {0}: Tổng điểm = {1}", kvp.Key, kvp.Value);
            //}

            //foreach (var solution in solutions) // add output vào trong mapSolution 
            //{
            //    Solution[] sos = new Solution[] { new Solution(solution.StuName, solution.OutPut) };
            //    if (mapSolutions.ContainsKey(solution.StuName))
            //    {
            //        sos = mapSolutions.GetValueOrDefault(solution.StuName);
            //        Solution so = new Solution(solution.StuName, solution.OutPut);
            //        //sos.Append(so);
            //        Array.Resize(ref sos, sos.Length + 1);
            //        sos[sos.Length - 1] = so;
            //        mapSolutions[solution.StuName] = sos;
            //    }
            //    else
            //    {
            //        mapSolutions.Add(solution.StuName, sos);
            //    }
            //}
            ViewBag.list = solution1s;
            ViewBag.listUsers = users;
            ViewBag.StudentTotalMarks = studentTotalMarks;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToDataBase()
        {
            QuestionDetail questionDetail = new QuestionDetail()
            {
                QuestionId = testCode,
                Q = 0,
                Status = 1,
                Category = 1, 
                Note = "",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            int checkQuestionDetail = await AddQuestionCodeFromApi(questionDetail);
                using (ProjectPrn231Context context = new ProjectPrn231Context())
            {
                var checkClass = context.Questions.Include(x => x.Class).FirstOrDefault(u=>u.Class.Name == stringClass);
                if(checkClass != null)
                {
                    TempData["CheckClass"] = "Lớp Học này đã hoàn thành  mã đề "+checkClass.QuestionId+"";
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in studentTotalMarks)
                {
                    User usersFinish = await GetOneStudentFromApi(item.Key);
                    Question questionDTO = new()
                    {
                        StudentId = usersFinish.Id,
                        QuestionId = testCode,
                        TotalMark = item.Value,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        ClassId = usersFinish.ClassId
                    };
                    context.Questions.Add(questionDTO);
                    context.SaveChanges();
                }

                List<User> users = await GetStudentFromApi(stringClass);
                foreach (var so in solution1s)
                {
                    User studentToRemove = users.FirstOrDefault(s => s.Mssv == so.Mssv);
                    if (studentToRemove != null)
                    {
                        users.Remove(studentToRemove);
                    }
                }
                foreach (var item in solution1s)
                { User user = context.Users.FirstOrDefault(u=>u.Mssv == item.Mssv);
                    QuestionNo question = new QuestionNo()
                    {
                        QuestionId = testCode,
                        StudentId = user.Id,
                        QuestionStt = item.Question,
                        Mark = item.Mark,
                        Status = "0",
                        Note = "true",
                        InputTestCase = item.InputTestCase,
                        OutputTestCase = item.OutputTestCase,
                        Output = item.OutPut,
                        ClassId = user.ClassId
                        
                    };
                    context.QuestionNos.Add(question);
                    context.SaveChanges();
                }
                foreach (var item in users)
                {
                    User user = context.Users.FirstOrDefault(u => u.Mssv == item.Mssv);
                    QuestionNo questionNo = new QuestionNo()
                    {
                        QuestionId = testCode,
                        StudentId = item.Id,
                        QuestionStt = "",
                        Mark = "0",
                        Status = "1",
                        Note = "NotFound",
                        InputTestCase = "",
                        OutputTestCase = "",
                        Output = "",
                        ClassId = user.ClassId,
                    };
                    Question question = new Question()
                    {
                        StudentId = item.Id,
                        QuestionId = testCode,
                        TotalMark = 0,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        ClassId = user.ClassId,
                    };
                    context.Questions.Add(question);
                    context.QuestionNos.Add(questionNo);
                    context.SaveChanges();
                }
            }
            TempData["AddSuccess"] = "Thêm Điểm Thành Công Vào database";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult TakeMark()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GetMark()
        {
            return RedirectToAction("ViewTeacher", "ViewUser");
        }
        public IActionResult logOut()
        {
            return RedirectToAction("Index", "User");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<List<User>> GetStudentFromApi(string className)
        {
            List<User> users = new List<User>();

            string link = "https://localhost:7153/api/Users/" + className;

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
                
        public async Task<User>GetOneStudentFromApi(string name)
        {
            User user = new User();

            string link = "https://localhost:7153/api/Users?name=" + name;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(data);
                    }
                }
            }
            return user;
        }

        public async Task<int> AddQuestionCodeFromApi(QuestionDetail questionDetail)
        {
            string link = "https://localhost:7153/api/QuestionDetails";

            using (HttpClient client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(questionDetail), Encoding.UTF8, "application/json");
                using (HttpResponseMessage res = await client.PostAsync(link, httpContent))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        // Add successful, return 1
                        return 1;
                    }
                    else
                    {
                        // Add unsuccessful, return 0
                        return 0;
                    }
                }
            }
        }
        public async Task AddQuestionFromApi(QuestionDTO questionDTO)
        {
            string link = "https://localhost:7153/api/Question";

            using (HttpClient client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(questionDTO), Encoding.UTF8, "application/json");
                using (HttpResponseMessage res = await client.PostAsync(link, httpContent))
                {
                }
            }
        }
    }
}