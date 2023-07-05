﻿using Microsoft.AspNetCore.Mvc;
using ReadExeFile.Models;
using System.Collections;
using System.Diagnostics;
using System.IO;
using ExcelDataReader;
using OfficeOpenXml;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography.X509Certificates;
using static OfficeOpenXml.ExcelErrorValue;
using System;

namespace ReadExeFile.Controllers
{
    class Solution
    {
        public string StuName { get; set; }
        public string OutPut { get; set; }
        public string Questions { get; set; }
        public Solution(string stuName, string outPut)
        {
            StuName = stuName;
            OutPut = outPut;
        }
        public Solution(string stuName, string outPut, string question)
        {
            StuName = stuName;
            OutPut = outPut;
            Questions = question;
        }
    }
    class Solution2
    {
        public string StuName { get; set; }
        public string Question { get; set; }
        public string OutPut1 { get; set; }
        public string OutPut2 { get; set; }

        public Solution2(string stuName, string question, string outPut1, string outPut2)
        {
            StuName = stuName;
            Question = question;
            OutPut1 = outPut1;
            OutPut2 = outPut2;
        }
    }
    class TestCases
    {
        public string Questions { get; set; }
        public string STT { get; set; }
        public string InPut { get; set; }
        public string Output { get; set; }

        public TestCases(string question, string stt, string input, string output)
        {
            Questions = question;
            STT = stt;
            InPut = input;
            Output = output;
        }
    }
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
            string executablePaths = "C:\\Users\\NTD\\Desktop\\TestCase\\paper_no1";
            string executablePath = "C:\\Users\\NTD\\Desktop\\SE1437";
            //string executablePaths = "C:\\Users\\NTD\\Desktop\\SE1437\\HE140001_ANVHE_1";
            string arguments = ""; // (nếu có)
            string input1 = "10"; // (nếu có)
            int input2 = 30; // (nếu có)
            int input3 = 5; // (nếu có)
            string output;
            //string filePath = "C:\\Users\\NTD\\Desktop\\TestCase.xlsx";
            string[] listFilePathFoder = Directory.GetDirectories(executablePath);
            //Tên Sv + List linkfilec của sv
            Dictionary<string, string[]> mapLinkFileC = new Dictionary<string, string[]>();

            string[] filess = Directory.GetDirectories(executablePaths);

            Dictionary<string, string[]> mapTestCaseFile = new Dictionary<string, string[]>();
            Dictionary<string, TestCases[]> mapTestCaseSolution = new Dictionary<string, TestCases[]>();

            TestCases[] test = { };
            string[] valueTestCase = { };
            string keys = "";
            string result = "";
            
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
                //string path = "C:\\Users\\NTD\\Desktop\\TestCase\\paper_no1\\Q1_TestCases\\tc1.txt";
                //string contents = System.IO.File.ReadAllText(path);
                //string input_str = String.Join("  ", new[] { contents.Split('|')[1], contents.Split('|')[2], contents.Split('|')[3] });
                //string output_str = contents.Split('|')[^3];
                //string mark_str = contents.Split('|')[^1];
                //int count = 0;
                foreach (var valuee in valueTestCase)
                {
                    //if (test.Length > 2)
                    //{
                    //    Array.Resize(ref test, test.Length - 2);
                    //}
                    string contents = System.IO.File.ReadAllText(valuee);
                    string input_str = String.Join("  ", new[] { contents.Split('|')[1], contents.Split('|')[2], contents.Split('|')[3] });
                    string output_str = contents.Split('|')[^3];
                    string mark_str = contents.Split('|')[^1];
                    Array.Resize(ref test, test.Length + 1);
                    test[test.Length - 1] = new TestCases(result, mark_str, input_str, output_str);
                    Console.WriteLine(valuee.ToString());
                }
                mapTestCaseSolution.Add(result, test);
                Array.Resize(ref test, test.Length - 2);
                //int ii = 0;
                //if (mapp.ContainsKey(key))
                //{
                //    Console.WriteLine(result);
                //    Console.WriteLine(values[ii]);
                //    TestCases[] testCases = new TestCases[] { new TestCases(result, "aa", "bb", "cc") };
                //    mapp.Add(result, testCases);
                //}
                //else
                //{
                //    Console.WriteLine("đây là else" + result);
                //    Console.WriteLine("đây là else" + values[ii]);
                //}
                //Console.WriteLine("abc" + values[ii] + "num" + ii);
                //ii++;
                // Do something with the key and values
            }

            // đường dẫn file exe bài tập. Key He123 value C:\Users\NTD\Desktop\SE1437\HE140001_ANVHE_1
            // key là tên trong solution và value là Solution
            string answer = "";
            Dictionary<string, Solution[]> mapSolutions = new Dictionary<string, Solution[]>();
            List<Solution> solutions = new List<Solution>();
            List<Solution2> solution1s = new List<Solution2>();
            double mark = 0;
            for (int i = 0; i < listFilePathFoder.Length; i++)  // chạy 2 forder
            {
                string[] pathExe = Directory.GetFiles(listFilePathFoder[i]);
                string[] splitPath = listFilePathFoder[i].Split('\\');
                mapLinkFileC.Add(splitPath[splitPath.Length - 1], pathExe);
            }
            foreach (var entry in mapLinkFileC) // lặp qua key, add value vào array
            {
                string key = entry.Key;

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
                            //process.StandardInput.WriteLine(input1);
                            //process.StandardInput.WriteLine(input2);\
                            //process.StandardInput.WriteLine(input3
                            foreach (var map in mapTestCaseSolution)
                            {
                                string keyss = map.Key;
                                if(answer.Contains(map.Key))
                                {
                                    TestCases[] valuess = map.Value;
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
                                        solution1s.Add(new Solution2(entry.Key, keyss, outputValue, outputValue));
                                        //var marks = item.STT;
                                        if (outputValue.Trim().Contains(item.Output.Trim()))
                                        {
                                            mark+= double.Parse(item.STT);
                                        }
                                        process.Start();
                                    }
                                }
                                
                            }
                            process.StandardInput.Close();
                        }

                        //// Đọc đầu ra từ tiến trình
                        //output = process.StandardOutput.ReadToEnd();

                        //// Chờ tiến trình kết thúc
                        //process.WaitForExit();

                        //// Sử dụng giá trị đầu ra theo ý muốn trong ứng dụng web của bạn
                        ////Console.WriteLine(output);
                        //string[] outputPatchs = output.Split(':');
                        ////Console.WriteLine(outputPatchs[outputPatchs.Length - 1]);
                        ////testCases.Add(key, key, key, key);
                        //solutions.Add(new Solution(key, outputPatchs[outputPatchs.Length - 1]));
                        //solution1s.Add(null);
                    }
                }
            }
            foreach (var solution in solutions) // add output vào trong mapSolution 
            {
                Solution[] sos = new Solution[] { new Solution(solution.StuName, solution.OutPut) };
                if (mapSolutions.ContainsKey(solution.StuName))
                {
                    sos = mapSolutions.GetValueOrDefault(solution.StuName);
                    Solution so = new Solution(solution.StuName, solution.OutPut);
                    //sos.Append(so);
                    Array.Resize(ref sos, sos.Length + 1);
                    sos[sos.Length - 1] = so;
                    mapSolutions[solution.StuName] = sos;
                }
                else
                {
                    mapSolutions.Add(solution.StuName, sos);
                }
            }
            return View();
        }
        public void getTestCase()
        {

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
    }
}