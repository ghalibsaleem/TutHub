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
        
        private string usr_id;

        [Required]
        public string Usr_id
        {
            get { return usr_id; }
            set { usr_id = value; }
        }


        private string f_name;
        [Required]
        public string F_Name
        {
            get { return f_name; }
            set { f_name = value; }
        }

        private string l_name;
        [Required]
        public string L_Name
        {
            get { return l_name; }
            set { l_name = value; }
        }


        private Gender u_Gender;

        public Gender U_Gender
        {
            get { return u_Gender; }
            set { u_Gender = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private int is_student;

        public int IsStudent
        {
            get { return is_student; }
            set { is_student = value; }
        }

        private int is_teacher;

        public int IsTeacher
        {
            get { return is_teacher; }
            set { is_teacher = value; }
        }


        private string photo_url;

        public string PhotoUrl
        {
            get { return photo_url; }
            set { photo_url = value; }
        }


        private int language_pref;

        public int LanguagePref
        {
            get { return language_pref; }
            set { language_pref = value; }
        }



        public object[] ToArray()
        {   
            return new object[]{ usr_id, f_name, l_name, (int)u_Gender, password, is_student, is_teacher, photo_url, language_pref };
        }

    }
}
