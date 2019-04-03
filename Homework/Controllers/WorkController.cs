using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Homework.Models;
using Microsoft.AspNetCore.Http;

namespace Homework.Controllers
{
    
    public class WorkController : Controller
    {
        public WorkController(IWorkRepository workItems)
        {
            WorkItems = workItems;
        }

        public IWorkRepository WorkItems { get; set; }
        
        [Route("~/api/work/Create",
            Name = "create")]
        [HttpPost]
        public IActionResult Create(WorkItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            WorkItems.Add(item);
            return new NoContentResult();
        }


        [Route("~/api/work/Update",
            Name = "update")]
        public IActionResult Update(string key, WorkItem item)
        {
            Request.Method = "PUT";
            if (item == null || item.Key != key)
            {
                return BadRequest();
            }

            var work = WorkItems.Find(key);
            if (work == null)
            {
                return NotFound();
            }

            WorkItems.Update(item);
            return new NoContentResult();
        }

        [Route("~/api/work/Delete",
            Name = "delete")]
        public IActionResult Delete(string key)
        {
            Request.Method = "DELETE";
            var work = WorkItems.Find(key);
            if (work == null)
            {
                return NotFound();
            }

            WorkItems.Remove(key);
            return new NoContentResult();
        }
        [Route("~/api/work/GetAll",
            Name = "getall")]
        public IEnumerable<WorkItem> GetAll()
        {
            return WorkItems.GetAll();
        }
    }
}