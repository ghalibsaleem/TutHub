using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TutHub.Models.enums;

namespace TutHub.Models
{
    public class User
    {
        [Required]
        public string usr_id { get; set; }



        [Required]
        public string f_name { get; set; }

        [Required]
        public string l_name { get; set; }

        [Required]
        public string email { get; set; }


        public Gender u_Gender { get; set; }

       

        public bool is_student { get; set; }


        public bool is_teacher { get; set; }


        public string photo_url { get; set; }

        

        public int language_pref { get; set; }

        [Required]
        public DateTime dateJoined { get; set; }



        public object[] ToArray()
        {   
            return new object[]{ usr_id, f_name, l_name, email, u_Gender.ToString(), is_student, is_teacher, photo_url, language_pref, dateJoined.ToString("s") };
        }

        public void FromArray(List<object> lst)
        {
            usr_id = (string)lst[0];
            f_name = (string)lst[1];
            l_name = (string)lst[2];
            email = (string)lst[3];
            u_Gender = Enum.Parse<Gender>((string)lst[4]);
                
            is_student = (bool)lst[5];
            is_teacher = (bool)lst[6];
            photo_url = (string)lst[7];
            language_pref = (int)lst[8];
            dateJoined = (DateTime)lst[9];
        }

    }
}
