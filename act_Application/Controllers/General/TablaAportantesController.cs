﻿using act_Application.Data.Repository;
using act_Application.Models.BD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace act_Application.Controllers.General
{
    public class TablaAportantesController : Controller
    {
        [Authorize(Policy = "AdminSocioOnly")]
        public IActionResult Index()
        {
            var arobj = (List<ActAportacione>) new AportacionRepository().OperacionesAportaciones( 2, 0, 0, "");
            return View(arobj);
        }
    }
}
