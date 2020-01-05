using jsreport.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace jsreport_asp_net_core_example.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/Home
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var rs = new ReportingService("http://localhost:5488"); //put your URL here
            string json = System.IO.File.ReadAllText(@"C:\temp\data.json"); //Putting your json data here. You can pass an Object too
            
            var report = await rs.RenderByNameAsync("report-name", json); //here your must put the report name

            var memoryStream = new MemoryStream();
            await report.Content.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/pdf") { FileDownloadName = "report.pdf" };
        }
    }
}
