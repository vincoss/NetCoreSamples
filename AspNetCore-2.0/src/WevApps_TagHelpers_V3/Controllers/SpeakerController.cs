﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WevApps_TagHelpers_V3.Controllers
{
    public class SpeakerController : Controller
    {
        private List<Speaker> Speakers =
            new List<Speaker>
            {
            new Speaker {SpeakerId = 10},
            new Speaker {SpeakerId = 11},
            new Speaker {SpeakerId = 12}
            };

        [Route("Speaker/{id:int}")]
        public IActionResult Detail(int id) =>
            View(Speakers.FirstOrDefault(a => a.SpeakerId == id));

        [Route("/Speaker/Evaluations", Name = "speakerevals")]
        public IActionResult Evaluations() => View();

        [Route("/Speaker/EvaluationsCurrent", Name = "speakerevalscurrent")]
        public IActionResult Evaluations(int speakerId, bool currentYear) => View();

        public IActionResult Index() => View(Speakers);
    }

    public class Speaker
    {
        public int SpeakerId { get; set; }
    }
}
