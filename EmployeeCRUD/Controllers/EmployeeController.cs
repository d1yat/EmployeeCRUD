using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Mvc;
// using System.IO;
// using iTextSharp.text;
// using iTextSharp.text.pdf;
// using iTextSharp.tool.xml;
// using iTextSharp.text.html.simpleparser;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> objCatlist = _context.Employees;
            return View(objCatlist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                var cdate=DateTime.Now;
                empobj.RecordCreatedOn = cdate;

                _context.Employees.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                var cdate=DateTime.Now;
                empobj.RecordCreatedOn = cdate;

                _context.Employees.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            var deleterecord = _context.Employees.Find(id);
            if (deleterecord == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data Deleted Successfully !";
            return RedirectToAction("Index");
        }

        // [HttpPost]
        // // [ValidateInput(false)]
        // public FileResult Export(string GridHtml)
        // {
        //     using (MemoryStream stream = new System.IO.MemoryStream())
        //     {
        //         StringReader sr = new StringReader(GridHtml);
        //         Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //         pdfDoc.SetPageSize(PageSize.A4.Rotate());
        //         PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //         pdfDoc.Open();
        //         XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //         pdfDoc.Close();
        //         return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        //     }
        // }
    }
}
