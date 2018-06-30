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
        private List<Element> elements = null;

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


        private void readRootFolder()
        {
            path = ".\\root";
            ProcessDirectory(path);

            //var temp = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //IFileProvider provider = new PhysicalFileProvider(path);
            //IDirectoryContents contents = provider.GetDirectoryContents(""); // the applicationRoot contents
            //IFileInfo fileInfo = provider.GetFileInfo("wwwroot/js/site.js"); // a file under applicationRoot

            //foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            //{
            //    int a = 10;
            //}

        }
        private void ProcessDirectory(string rootDirectory)
        {
            IEnumerable<string> listDirectories = Directory.EnumerateDirectories(rootDirectory);
            foreach (String element in listDirectories)
            {
                ProcessDirectory(element);
            }
            Element rootElement = new Element();

        }
        private Element makeElement(string rootDirectory, string currentDirectory)
        {
            Element element = new Element();

            return element;
        }
        /*
         
         необходимо сгенерированные элементам выдать id

        var myBookings = myExistingListOfTen.Select((b, index) => new Booking
                 {
                     Id = index + 1, 
                     From=b.From, 
                     To=b.To
                 });

        это даст возможность генерить id
        чтобы сохранить исп 
        Create procedure Insert_ListItem 

        @Name data_type 
        AS 
        Set nocount on 
        Begin 
        If Not Exists(Select Count(1) From YouTable where Name =@Name)
        BEGIN
        INSERT INTO TABLES vALUES (@Name) 
        END
        End Set Nocount off
         
         
         
         */
    }
}