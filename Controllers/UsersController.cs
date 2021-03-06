﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiVendorAPI.Data;
using MultiVendorAPI.Dtos;
using MultiVendorAPI.Models;

namespace MultiVendorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Getusers()
        {
            List<UserToSend> user;
            List<Users> buser;

            buser = await _context.users.Include("Agent").Where(x => x.Status == "Approve").ToListAsync();

            user = buser.ConvertAll(x => new UserToSend
            {

                Address = x.Address,
                Agent = x.Agent,
                Email = x.Email,
                Facebook = x.Facebook,
                Id = x.Id,
                Level = x.Level,
                Name = x.Name,
                Status = x.Status,
                StokisId = x.StokisId,
                Phone = x.Phone,
                StripeKey = x.StripeKey

            });


            return Ok(user);
        }



        // GET: api/Users
        [HttpGet("stokis")]
        public async Task<IActionResult> Getstokis()
        {
            List<UserToSend> user;
            List<Users> buser;

            buser = await _context.users.Where(x => x.Level == "Stokis" && x.Status == "Approve").ToListAsync();

            user = buser.ConvertAll(x => new UserToSend
            {

                Address = x.Address,
                Agent = x.Agent,
                Email = x.Email,
                Facebook = x.Facebook,
                Id = x.Id,
                Level = x.Level,
                Name = x.Name,
                StokisId = x.StokisId,
                StripeKey = x.StripeKey

            });


            return Ok(user);
        }





        // GET: api/Users
        [HttpGet("stokis/pending")]
        public async Task<IActionResult> GetstokisPending()
        {
            List<UserToSend> user;
            List<Users> buser;

            buser = await _context.users.Where(x => x.Level == "Stokis" && x.Status == "Pending").ToListAsync();

            user = buser.ConvertAll(x => new UserToSend
            {

                Address = x.Address,
                Agent = x.Agent,
                Email = x.Email,
                Facebook = x.Facebook,
                Id = x.Id,
                Level = x.Level,
                Name = x.Name,
                StokisId = x.StokisId,
                StripeKey = x.StripeKey

            });


            return Ok(user);
        }



        // GET: api/Users
        [HttpGet("agent/pending/{id}")]
        public async Task<IActionResult> agent(int id)
        {
            List<UserToSend> user;
            List<Users> buser;

            buser = await _context.users.Where(x => x.Level == "Agent" && x.StokisId == id && x.Status == "Pending").ToListAsync();

            user = buser.ConvertAll(x => new UserToSend
            {

                Address = x.Address,
                Agent = x.Agent,
                Email = x.Email,
                Facebook = x.Facebook,
                Id = x.Id,
                Level = x.Level,
                Name = x.Name,
                StokisId = x.StokisId,
                StripeKey = x.StripeKey

            });


            return Ok(user);
        }








        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }






        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpGet("approve/{id}")]
        public async Task<IActionResult> PutUsers(int id)
        {

            var users = await _context.users.FindAsync(id);
            users.Status = "Approve";
            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.users.Any(e => e.Id == id);
        }
    }
}
