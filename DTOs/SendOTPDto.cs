using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class sendOTPDto
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
    }
}

// driver = mysql
// connect = host=localhost dbname=cyberpanel user=cyberpanel password=fggv3pppSVPArT port=3306
// password_query = SELECT email as user, password FROM e_users WHERE email='%u';
// user_query = SELECT '5000' as uid, '5000' as gid, mail FROM e_users WHERE email='%u';
