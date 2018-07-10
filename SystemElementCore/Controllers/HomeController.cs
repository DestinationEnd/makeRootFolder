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
            repository.TruncateElements();
        }
        [HttpGet]
        public IActionResult Index(string permalink = null)
        {
            if (permalink == null)
            {
                IEnumerable<string> listDirectories = new List<string>();
                readRootFolder();

                if (listDirectories.Count() > 0)
                {
                    ViewBag.listDirectories = listDirectories;
                    return View("~/Views/ShoDir/showDirs.cshtml");
                }
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
            Element parentElement = makeElement(null, path);
            ProcessDirectory(parentElement.Id, path);
        }
        private void ProcessDirectory(int parentId, string rootDirectory)
        {
            IEnumerable<string> listDirectories = Directory.EnumerateDirectories(rootDirectory);
            foreach (String element in listDirectories)
            {
                Element temp = makeElement(parentId, element);
                ProcessDirectory(temp.Id, element);
            }
        }
        private Element makeElement(int? parentId, string currentDirectory)
        {
            Element element = new Element();
            element.Url = currentDirectory;
            element.ParentId = parentId;
            element.Id = elementRepository.StoreElement(element);
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