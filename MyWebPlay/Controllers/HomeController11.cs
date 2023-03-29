﻿using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Regex_Replace_Multiple()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Regex_Replace_Multiple(IFormCollection f)
        {

            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string dukien1 = f["DuKien1"].ToString();
            if (string.IsNullOrEmpty(dukien1))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string input = f["Input"].ToString();
            if (string.IsNullOrEmpty(input))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string yes = f["DuKien3"].ToString();
            if (string.IsNullOrEmpty(yes))
            {
                ViewData["Loi6"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string dukien2 = f["DuKien2"].ToString();
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi5"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string output = f["Output"].ToString();
            //if (string.IsNullOrEmpty(output))
            //{
            //    ViewData["Loi3"] = "Trường này không được để trống!";
            //    return this.Regex_Replace_Multiple();
            //}

            var listInput = Regex.Split(input, "\r\n");
            var listOutput = Regex.Split(output, "\r\n");

            if (listInput.Length != listOutput.Length)
            {
                ViewData["Loi3"] = "Số lượng thành phần pattern input và output không tương xứng...";
                return this.Regex_Replace_Multiple();
            }

            if (yes == "0")
            {
                for (int i = 0; i < listInput.Length - 1; i++)
                {
                    for (int j = i + 1; j < listInput.Length; j++)
                    {
                        if (listInput[j].Contains(listInput[i]))
                        {
                            string t = listInput[i];
                            listInput[i] = listInput[j];
                            listInput[j] = t;

                            string tt = listOutput[i];
                            listOutput[i] = listOutput[j];
                            listOutput[j] = tt;
                        }
                    }
                }
            }

            string result = "\r\n";

            for (int i = 0; i < listInput.Length; i++)
            {
                Regex regex = new Regex(listInput[i]);

                if (int.Parse(dukien1) == 1)
                {
                    while (listInput[i].Contains(@"\\") == true)
                    {
                        listInput[i] = listInput[i].Replace(@"\\", @"\");
                    }

                    while (listOutput[i].Contains(@"\\") == true)
                    {
                        listOutput[i] = listOutput[i].Replace(@"\\", @"\");
                    }

                    if (int.Parse(dukien2) == -1)
                    {
                        result = regex.Replace(chuoi, listOutput[i]);
                        chuoi = regex.Replace(chuoi, listOutput[i]);
                    }
                    else
                    {
                        result = regex.Replace(chuoi, listOutput[i], int.Parse(dukien2));
                        chuoi = regex.Replace(chuoi, listOutput[i], int.Parse(dukien2));
                    }
                }
                else
                {
                    result = chuoi.Replace(listInput[i], listOutput[i]);
                    chuoi = chuoi.Replace(listInput[i], listOutput[i]);
                }
            }

            TextCopy.ClipboardService.SetText(result);

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result + " - " + listInput[0] + " - " + listInput[1] ;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }
    }
}