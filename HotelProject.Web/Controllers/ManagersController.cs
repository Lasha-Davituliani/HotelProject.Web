using HotelProject.Models;
using HotelProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers
{
    public class ManagersController : Controller
    {
        private readonly ManagerRepository _managerRepository;

        public ManagersController(ManagerRepository managerRepository) 
        {
            _managerRepository = managerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _managerRepository.GetManagers();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Manager model)
        {
            await _managerRepository.AddManager(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _managerRepository.GetSingleManager(id);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _managerRepository.DeleteManager(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _managerRepository.GetSingleManager(id);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePOST(Manager model)
        {
            await _managerRepository.UpdateManager(model);
            return RedirectToAction("Index");
        }
    }
}
