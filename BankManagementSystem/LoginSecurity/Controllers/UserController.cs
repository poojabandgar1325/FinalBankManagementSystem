using AutoMapper;
using LoginSecurity.Models.Domains;
using LoginSecurity.Models.DTO;
using LoginSecurity.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        // POST api/<SignupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetail value)
        {
            bool response = await userRepository.SaveUserDeatilAsync(value);

            if (response)
                return Ok("Added Succesfully");

            return BadRequest("User Already Exists");

        }
        
    //  [HttpPut("{userName}")]
      [HttpPut]
        //[Route("api/[controller]/user/{userName}")]
         [Route("UpdateUser/{userName}")]
        public async Task<IActionResult> Put([FromRoute] string userName, [FromBody] Models.DTO.UserUpdateDTO userUpdateDTO)
        {
            //convert DTO to Domain model
            var user = new Models.Domains.UserDetail()
            {
                Name = userUpdateDTO.Name,
                Password = userUpdateDTO.Password,
                Address = userUpdateDTO.Address,
                State = userUpdateDTO.State,
                Country = userUpdateDTO.Country,
                Email = userUpdateDTO.Email,
                PAN = userUpdateDTO.PAN,
                Contact = userUpdateDTO.Contact,
                DOB = userUpdateDTO.DOB,
                AccountType = userUpdateDTO.AccountType
            };

            //Update User using repository
          bool  response = await userRepository.UpdateUserAsync(userName, user);


            //if null then notfound
            
            if (response)
                return Ok("Updated Successfully ");

            return BadRequest("Something went wrong..!");

            ////convert domain back to DTO
            //var userDTO = new Models.DTO.UserDetailDTO
            //{
            //    Name = user.Name,
            //    Password = user.Password,
            //    Address = user.Address,
            //    State = user.State,
            //    Country = user.Country,
            //    Email = user.Email,
            //    PAN = user.PAN,
            //    Contact = user.Contact,
            //    DOB = user.DOB,
            //    AccountType = user.AccountType
            //};

            ////Return Ok response

            //return Ok(userDTO);
        }
    }
}
