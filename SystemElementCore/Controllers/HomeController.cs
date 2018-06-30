using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SystemElementCore.Models;

namespace SystemElementCore.Controllers
{
    public class HomeController : Controller
    {
        private IElementRepository elementRepository;
        private String path;
        public HomeController(IElementRepository repository)
        {
            elementRepository = repository;
        }
        [HttpGet]
        public IActionResult Index(string permalink = null)
        {
            IEnumerable<string> listDirectories = readRootFolder();
            if (listDirectories.Count() > 0)
            {
                ViewBag.listDirectories = listDirectories;
                return View("~/Views/ShoDir/showDirs.cshtml");
            }
            Element parentElement = null;

            if (permalink != null)
            {
                parentElement = elementRepository.findParentElementByPermalink(permalink);
            }
            else
            {
                parentElement = elementRepository.findNullParentId();
            }

            if (parentElement == null)
            {
                if (permalink.IndexOf('/') != -1)
                {
                    string[] arrayParams = permalink.Split('/');
                    string parEln = arrayParams[arrayParams.Length - 1];
                    parentElement = elementRepository.findParentElementByPermalink(parEln);
                }
            }
            ViewBag.parentElement = parentElement;
            ViewBag.elements = elementRepository.findParentId(parentElement.Id);
            if (parentElement == null)
            {
                return View("Eror");
            }
            return View();
        }


        private IEnumerable<string> readRootFolder()
        {
            path = ".\\root";
            IEnumerable<string> listDirectories = Directory.EnumerateDirectories(path);

            return listDirectories;
            //var temp = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //IFileProvider provider = new PhysicalFileProvider(path);
            //IDirectoryContents contents = provider.GetDirectoryContents(""); // the applicationRoot contents
            //IFileInfo fileInfo = provider.GetFileInfo("wwwroot/js/site.js"); // a file under applicationRoot

            //foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            //{
            //    int a = 10;
            //}

        }
        private void ProcessDirectory(string targetDirectory)
        {

        }
    }
}