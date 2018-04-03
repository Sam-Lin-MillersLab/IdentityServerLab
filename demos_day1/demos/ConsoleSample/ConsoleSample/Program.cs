using System;
using System.Linq;
using System.Security.Claims;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var claims = new[]{
                new Claim("sub", "89378937893"),
                new Claim("name", "Brock Allen"),
                //new Claim(ClaimTypes.Email, "BrockAllen@gmail.com"),
                new Claim("email", "BrockAllen@gmail.com"),
                new Claim("language", "en-US"),
                new Claim("language", "fr-FR"),
                new Claim("role", "Geek"),
                new Claim("role", "Nerd"),
            };
            var ci = new ClaimsIdentity(claims, "pwd", "name", "role");
            var user = new ClaimsPrincipal(ci);

            var email = user.Claims.Where(x => x.Type == "email").FirstOrDefault().Value;
            email = user.FindFirst("email").Value;
            Console.WriteLine(email);


            var name = user.Identity.Name;
            var isGeek = user.IsInRole("Geek");
            Console.WriteLine(name + " is a geek: " + isGeek);



        }
    }
}
