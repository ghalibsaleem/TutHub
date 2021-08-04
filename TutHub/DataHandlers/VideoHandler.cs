using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutHub.Models;

namespace TutHub.DataHandlers
{
    public class VideoHandler
    {
        private IConfiguration _config;
        public VideoHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Video> GetVideo(int id)
        {

            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {

                await conn.OpenAsync();

                string sqlStr = String.Format("select * from Videos where video_id={0}", id);
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
                            Video video = new Video();
                            video.FromArray(list);
                            return video;
                        }
                    }
                }
            }
            return null;
        }


        public async Task<List<Video>> GetVideoList(int course_id)
        {

            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            List<Video> lstVideos = new List<Video>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {

                await conn.OpenAsync();

                string sqlStr = String.Format("select * from Videos where course_id={0}", course_id);
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
                            Video video = new Video();
                            video.FromArray(list);
                            lstVideos.Add(video);
                        }
                    }
                }
            }
            return lstVideos;
        }

        public async Task<Video> InsertVideo(Video video)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("insert into Videos (video_id, name, description, tag, course_id, date_created, video_url) " +
                    "values({0},'{1}','{2}','{3}',{4},'{5}','{6}'); ", video.ToArray());

                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return video;
                    }
                }
            }
            return null;
        }


        //Need Attention
        public async Task<List<bool>> InsertListVideo(List<Video> lstVideo)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");
            List<bool> lstResult = new List<bool>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();

                foreach (var video in lstVideo)
                {
                    string sqlStr = String.Format("insert into Videos (video_id, name, description, tag, course_id, date_created, video_url) " +
                    "values({0},'{1}','{2}','{3}',{4},'{5}','{6}'); ", video.ToArray());

                    using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                    {
                        if (await cmd.ExecuteNonQueryAsync() == 1)
                        {
                            lstResult.Add(true);
                        }
                        else
                        {
                            lstResult.Add(false);
                        }
                    }
                }
                
            }
            return lstResult;
        }


        //TODO
        public async Task<Video> UpdateVideo(Video course)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("update Courses set usr_id='{0}', f_name, l_name, gender, u_password, is_student, is_teacher, photo_url, language_pref) " +
                    "values('{0}','{1}','{2}',{3},'{4}',{5},{6},'{7}',{8});", course.ToArray());

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

        public async Task<bool> DeleteVideo(int id)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("delete from Video where course_id={0}", id);

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
