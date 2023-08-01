using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Models;

namespace SocialMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private IPolicyService policyService;
        public PolicyController(IPolicyService policyService)
        {
            this.policyService = policyService;
        }

        [HttpPost("addPolicy")]
        public async Task<ActionResult<Policy>> AddPolicy(CreatePolicyDto policyDto)
        {
            try
            {
                return Ok(await policyService.AddPolicy(policyDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
