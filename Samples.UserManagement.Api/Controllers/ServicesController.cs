using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Service>> Get()
            => Ok(_serviceRepository.Query(null));
    }
}
