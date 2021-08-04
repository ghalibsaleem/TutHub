using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutHub.Models;

namespace TutHub.DataHandlers
{
    public class CourseHandler
    {
        private IConfiguration _config;
        public CourseHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Course> GetCourse(int id)
        {

            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {

                await conn.OpenAsync();

                string sqlStr = String.Format("select * from Courses where course_id={0}", id);
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {

                    using (var sqlDataReader = await cmd.ExecuteReaderAsync())
                    {

                        while (await sqlDataReader.ReadAsync())
                        {
                            List<object> list = new List<object>();

                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                list.Add(sqlDataReader[i]);
                            }
                            Course course = new Course();
                            course.FromArray(list);
                            return course;                            
                        }
                    }
                }
            }
            return null;
        }


        public async Task<Course> InsertCourse(Course course)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("insert into Courses (course_id, name, description, tags, owner_id, date_created) " +
                    "values({0},'{1}','{2}','{3}','{4}','{5}'); ", course.ToArray());

                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return course;
                    }
                }
            }
            return null;
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("update Courses " +
                    "set name = '{1}', description='{2}', tag='{3}', owner_id= {4}, date_created = '{5}'  " +
                    "where course_id={0}", course.ToArray());

                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return course;
                    }
                }
            }
            return null;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("delete from Courses where course_id={0}", id);

                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
