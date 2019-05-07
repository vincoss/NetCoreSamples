using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApps_Mvc_DependencyInjection.Models;
using WebApps_Mvc_DependencyInjection.Services;
using WebApps_Mvc_DependencyInjection.ViewModels;

namespace WebApps_Mvc_DependencyInjection.Controllers
{
    public class BrainstormController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;

        public BrainstormController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessionList = await _sessionRepository.ListAsync();

            var model = sessionList.Select(session => new StormSessionViewModel()
            {
                Id = session.Id,
                DateCreated = session.DateCreated,
                Name = session.Name,
                IdeaCount = session.Ideas.Count
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewSessionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _sessionRepository.AddAsync(new BrainstormSession()
                {
                    DateCreated = DateTimeOffset.Now,
                    Name = model.SessionName
                });
            }

            return RedirectToAction(actionName: nameof(Index));
        }


        public class NewSessionModel
        {
            [Required]
            public string SessionName { get; set; }
        }
    }
}